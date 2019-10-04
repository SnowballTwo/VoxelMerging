using System.Linq;

namespace VoxelMerger.Strategies
{
    public class AdaptiveStickStrategy : StickStrategy
    {
        public override string Name { get; } = "Adaptive sticks";

        public override Defect Compress( Defect defect )
        {
            var x = CompressX( defect );
            var y = CompressY( defect );
            var z = CompressZ( defect );

            var min = x;
            if( y.Voxels.Length < min.Voxels.Length )
                min = y;
            if( z.Voxels.Length < min.Voxels.Length )
                min = z;

            return min;
        }

        private Defect CompressX( Defect defect )
        {
            var voxels = CreateSticks( defect, 0 ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray();
            return new Defect( defect, voxels );
        }

        private Defect CompressY( Defect defect )
        {
            var voxels = CreateSticks( defect, 1 ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray();
            return new Defect( defect, voxels );
        }

        private Defect CompressZ( Defect defect )
        {
            var voxels = CreateSticks( defect, 2 ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray();
            return new Defect( defect, voxels );
        }
    }
}