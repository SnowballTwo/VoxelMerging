namespace VoxelMerger.Strategies
{
    public abstract class Strategy
    {
        public abstract string Name { get; }
        public abstract Defect Compress( Defect defect );
    }
}