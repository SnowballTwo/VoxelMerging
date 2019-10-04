using System.Linq;

namespace VoxelMerger.Strategies
{
    public class AdaptiveStickStrategy : StickStrategy
    {
        public override string Name { get; } = "Sticks (Auto)";

        public override Defect Compress( Defect defect )
        {
            var max = 0;
            for (var i = 0; i < 3; i++)
                if( defect.GetSize( i ) > defect.GetSize( max ) )
                    max = i;
            
            return new Defect( defect, CreateSticks( defect, max ).SelectMany( a => a.Where( v => v != null ).SelectMany( b => b.Where( v => v != null ) ) ).ToArray());
        }
    }
}