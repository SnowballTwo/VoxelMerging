using System.Collections.Generic;
using System.Linq;

namespace VoxelMerger.Strategies
{
    public class AdaptiveMergedStickStrategy : StickStrategy
    {
        public override string Name { get; } = "Adaptive merged sticks";

        public override Defect Compress( Defect defect )
        {
            var x = Compress( defect, 0 );
            var y = Compress( defect, 1 );
            var z = Compress( defect, 2 );

            var min = x;
            if( y.Voxels.Length < min.Voxels.Length )
                min = y;
            if( z.Voxels.Length < min.Voxels.Length )
                min = z;

            return min;
        }

        private Defect Compress( Defect defect, int dimension )
        {
            var sticks = CreateSticks( defect, dimension );

            var finished = false;
            while( !finished )
            {

                finished = !Merge(sticks);
            }
            
            var voxels = sticks.SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray();
            return new Defect( defect, voxels );

        }

        private bool Merge(List<Voxel>[][] sticks )
        {
            var modified = false;
            
            for( var a = 0; a < sticks.Length; a++ )
            {
                for( var b = 0; b < sticks[ a ].Length; b++ )
                {
                    var list = sticks[ a ][ b ];
                    if( list == null )
                        continue;

                    foreach( var stick in list )
                    {
                        var last = stick;
                        
                        for( var da = a + 1; da < sticks.Length; da++ )
                        {
                            var otherList = sticks[ da ][ b ];
                            
                            var otherStick = otherList?.FirstOrDefault( s => s.CanMerge( last ) );
                            if( otherStick == null )
                                break;

                            stick.Merge( otherStick );
                            otherList.Remove( otherStick );

                            modified = true;
                        }
                    }
                }
            }

            return modified;
        }
    }
}