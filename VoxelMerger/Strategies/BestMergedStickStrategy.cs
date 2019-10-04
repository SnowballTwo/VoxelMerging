using System.Collections.Generic;
using System.Linq;
using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    public class BestMergedStickStrategy : MergedStickStrategy
    {
        public override string Name { get; } = "Merged sticks (Best)";

        public override VoxelGroup Compress( VoxelGroup voxelGroup )
        {
            VoxelGroup min = null;
            for ( var i = 0; i < 3; i++ )
            {
                var result = Compress( voxelGroup, i );
                if ( min == null || result.Voxels.Length < min.Voxels.Length )
                    min = result;
            }
               
            return min;

        }

        
    }
}