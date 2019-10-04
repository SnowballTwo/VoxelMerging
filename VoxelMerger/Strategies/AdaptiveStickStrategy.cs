using System.Linq;
using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    public class AdaptiveStickStrategy : StickStrategy
    {
        public override string Name { get; } = "Sticks (Auto)";

        public override VoxelGroup Compress( VoxelGroup voxelGroup )
        {
            var max = 0;
            for (var i = 0; i < 3; i++)
                if ( GetSize( voxelGroup, i ) > GetSize( voxelGroup, max ) )
                    max = i;
            
            return new VoxelGroup( voxelGroup, CreateSticks( voxelGroup, max ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray());
        }
    }
}