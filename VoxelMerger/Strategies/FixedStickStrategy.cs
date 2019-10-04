using System;
using System.Collections.Generic;
using System.Linq;
using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    public class FixedStickStrategy : StickStrategy
    {
        private readonly int _Dimension;

        public FixedStickStrategy( int dimension )
        {
            _Dimension = dimension;
        }

        public override string Name {
            get {
                switch( _Dimension )
                {
                    case 0: return "Sticks (X)";
                    case 1: return "Sticks (Y)";
                    case 2: return "Sticks (Z)";
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override VoxelGroup Compress( VoxelGroup voxelGroup )
        {
            return new VoxelGroup( voxelGroup, CreateSticks( voxelGroup, _Dimension ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray());
        }
    }
}