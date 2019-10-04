using System;
using System.IO;
using VoxelMerger.Strategies;

namespace VoxelMerger
{
    internal class Program
    {
        public static void Main( string[] args )
        {
            var defects = GetDefects();

            CompressWithStrategy( defects, new FixedStickStrategy(0) );
            CompressWithStrategy( defects, new FixedStickStrategy(1) );
            CompressWithStrategy( defects, new FixedStickStrategy(2) );
            CompressWithStrategy( defects, new AdaptiveStickStrategy() );
            CompressWithStrategy( defects, new FixedMergedStickStrategy(0) );
            CompressWithStrategy( defects, new FixedMergedStickStrategy(1) );
            CompressWithStrategy( defects, new FixedMergedStickStrategy(2) );
            CompressWithStrategy( defects, new AdaptiveMergedStickStrategy() );
            
            Console.ReadKey();
        }

        private static void CompressWithStrategy( Defect[] defects, Strategy strategy )
        {
            var before = 0;
            var after = 0;


            Console.WriteLine( "Strategy: " + strategy.Name );

            foreach( var defect in defects )
            {
                var compressed = strategy.Compress( defect );
                if( !Equals( defect, compressed ) )
                {
                    Console.WriteLine( "Faulty compression" );
                    Console.ReadKey();
                }

                before += defect.Voxels.Length;
                after += compressed.Voxels.Length;
            }

            Console.WriteLine( "Before: " + before );
            Console.WriteLine( "After: " + after );
            Console.WriteLine( "Compression: " + (double) after / before );
            Console.WriteLine();
        }

        private static Defect[] GetDefectsFromFile()
        { 
            using( var stream = File.OpenRead( "TestData/defects.raw" ) )
            using( var reader = new BinaryReader( stream ) )
            {
                var defectCount = reader.ReadInt32();
                var defects = new Defect[defectCount];

                for( var i = 0; i < defectCount; i++ )
                    defects[ i ] = Defect.Deserialize( reader );

                return defects;
            }
        }

        private static Defect[] GetDefects()
        {
            /*return new[]
            {
                new Defect( 0, 0, 0, 2, 2, 3, new[]
                {
                    new Voxel( 0, 0, 0 ),
                    new Voxel( 0, 0, 1 ),
                    new Voxel( 0, 1, 0 ),
                    new Voxel( 0, 1, 1 ),
                    new Voxel( 1, 0, 0 ),
                    new Voxel( 1, 0, 1 ),
                    new Voxel( 1, 1, 0 ),
                    new Voxel( 1, 1, 1 ),
                    new Voxel( 1, 1, 2 ),
                } )
            };*/

            return GetDefectsFromFile();
        }
    }
}