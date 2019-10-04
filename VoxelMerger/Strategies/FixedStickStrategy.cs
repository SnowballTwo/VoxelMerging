using System;
using System.Collections.Generic;
using System.Linq;

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

        public override Defect Compress( Defect defect )
        {
            return new Defect( defect, CreateSticks( defect, _Dimension ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray());
        }
    }
}