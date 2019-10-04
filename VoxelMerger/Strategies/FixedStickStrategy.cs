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
                    case 0: return "X-sticks";
                    case 1: return "Y-sticks";
                    case 2: return "Z-sticks";
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