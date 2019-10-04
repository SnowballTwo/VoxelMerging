using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    /// <summary>
    /// Base class for voxel merging strategies
    /// </summary>
    public abstract class Strategy
    {
        /// <summary>
        /// Display name
        /// </summary>
        public abstract string Name { get; }
        
        /// <summary>
        /// Compresses the specified <paramref name="voxelGroup"/> and returns it as a new one.
        /// </summary>
        /// <param name="voxelGroup"></param>
        /// <returns></returns>
        public abstract VoxelGroup Compress( VoxelGroup voxelGroup );
    }
}