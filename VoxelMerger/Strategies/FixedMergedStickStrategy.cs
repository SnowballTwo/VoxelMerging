using System;
using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    public class FixedMergedStickStrategy : MergedStickStrategy
    {
        private readonly int _Dimension;

        public FixedMergedStickStrategy( int dimension )
        {
            _Dimension = dimension;
        }

        public override string Name {
            get {
                switch( _Dimension )
                {
                    case 0: return "Merged sticks (X)";
                    case 1: return "Merged sticks (Y)";
                    case 2: return "Merged sticks (Z)";
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override VoxelGroup Compress( VoxelGroup voxelGroup )
        {
            return Compress( voxelGroup, _Dimension );
        }
    }
}