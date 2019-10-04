using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VoxelMerger.Model;
using VoxelMerger.Strategies;

namespace VoxelMerger
{
    internal class Program
    {
        public static void Main( string[] args )
        {
            var voxelGroups = GetVoxelGroups();

            CompressWithStrategy( voxelGroups, new FixedStickStrategy(0) );
            CompressWithStrategy( voxelGroups, new FixedStickStrategy(1) );
            CompressWithStrategy( voxelGroups, new FixedStickStrategy(2) );
            CompressWithStrategy( voxelGroups, new AdaptiveStickStrategy() );
            CompressWithStrategy( voxelGroups, new FixedMergedStickStrategy(0) );
            CompressWithStrategy( voxelGroups, new FixedMergedStickStrategy(1) );
            CompressWithStrategy( voxelGroups, new FixedMergedStickStrategy(2) );
            CompressWithStrategy( voxelGroups, new AdaptiveMergedStickStrategy() );
            CompressWithStrategy( voxelGroups, new BestMergedStickStrategy());
            
            Console.ReadKey();
        }

        private static void CompressWithStrategy( VoxelGroup[] voxelGroups, Strategy strategy )
        {
            var before = 0;
            var after = 0;

            Console.WriteLine( "Strategy: " + strategy.Name );

            foreach( var voxelGroup in voxelGroups )
            {
                var compressed = strategy.Compress( voxelGroup );
                if( !Equals( voxelGroup, compressed ) )
                {
                    Console.WriteLine( "Faulty compression" );
                    Console.ReadKey();
                }

                before += voxelGroup.Voxels.Length;
                after += compressed.Voxels.Length;
            }

            Console.WriteLine( "Before: " + before );
            Console.WriteLine( "After: " + after );
            Console.WriteLine( "Compression: " + (double) after / before );
            Console.WriteLine();
        }

        private static VoxelGroup[] GetVoxelGroupsFromFile()
        { 
            using( var stream = File.OpenRead( "TestData/defects.raw" ) )
            using( var reader = new BinaryReader( stream ) )
            {
                var voxelGroupCount = reader.ReadInt32();
                
                var voxelGroups = new VoxelGroup[voxelGroupCount];
                
                for( var i = 0; i < voxelGroupCount; i++ )
                    voxelGroups[ i ] = VoxelGroup.Deserialize( reader );

                return voxelGroups;
            }
        }

        private static VoxelGroup[] GetVoxelGroups()
        {
            return GetVoxelGroupsFromFile();
        }
    }
}