using System.Collections.Generic;
using System.Linq;
using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    public class AdaptiveMergedStickStrategy : MergedStickStrategy
    {
        public override string Name { get; } = "Merged sticks (Auto)";

        public override VoxelGroup Compress( VoxelGroup voxelGroup )
        {
            var max = 0;
            for ( var i = 0; i < 3; i++ )
                if ( GetSize( voxelGroup, i ) > GetSize( voxelGroup, max ) )
                    max = i;

            return Compress( voxelGroup, max );
        }
    }
}