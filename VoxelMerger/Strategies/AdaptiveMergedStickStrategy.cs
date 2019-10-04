using System.Collections.Generic;
using System.Linq;

namespace VoxelMerger.Strategies
{
    public class AdaptiveMergedStickStrategy : MergedStickStrategy
    {
        public override string Name { get; } = "Merged sticks (Auto)";

        public override Defect Compress( Defect defect )
        {
            var max = 0;
            for (var i = 0; i < 3; i++)
                if( defect.GetSize( i ) > defect.GetSize( max ) )
                    max = i;
            return Compress( defect, max );

        }

        
    }
}