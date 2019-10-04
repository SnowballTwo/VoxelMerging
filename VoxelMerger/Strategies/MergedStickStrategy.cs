using System.Collections.Generic;
using System.Linq;

namespace VoxelMerger.Strategies
{
    public abstract class MergedStickStrategy : StickStrategy
    {
        protected Defect Compress( Defect defect, int dimension )
        {
            var sticks = CreateSticks( defect, dimension );

            var finished = false;

            while( !finished ) finished = !Merge( sticks );

            var voxels = sticks.SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray();
            return new Defect( defect, voxels );
        }
        
        private static bool Merge( List<Voxel>[][] sticks )
        {
            var modified = false;

            for( var a = 0; a < sticks.Length; a++ )
            for( var b = 0; b < sticks[ a ].Length; b++ )
            {
                var list = sticks[ a ][ b ];
                if( list == null )
                    continue;

                foreach( var stick in list )
                {
                    modified |= MergeA( stick, a, b, sticks );
                    modified |= MergeB( stick, a, b, sticks );
                }
            }

            return modified;
        }

        private static bool MergeA( Voxel stick, int a, int b, List<Voxel>[][] sticks )
        {
            var modified = false;

            for( var da = a + 1; da < sticks.Length; da++ )
            {
                var otherList = sticks[ da ][ b ];

                var otherStick = otherList?.FirstOrDefault( stick.CanMerge );
                if( otherStick == null )
                    break;

                stick.Merge( otherStick );
                otherList.Remove( otherStick );

                modified = true;
            }

            return modified;
        }

        private static bool MergeB( Voxel stick, int a, int b, List<Voxel>[][] sticks )
        {
            var modified = false;

            for( var db = b + 1; db < sticks[ a ].Length; db++ )
            {
                var otherList = sticks[ a ][ db ];

                var otherStick = otherList?.FirstOrDefault( stick.CanMerge );
                if( otherStick == null )
                    break;

                stick.Merge( otherStick );
                otherList.Remove( otherStick );

                modified = true;
            }

            return modified;
        }
    }
}