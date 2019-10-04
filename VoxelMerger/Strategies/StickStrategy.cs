using System;
using System.Collections.Generic;
using VoxelMerger.Model;

namespace VoxelMerger.Strategies
{
    public abstract class StickStrategy : Strategy
    {
        protected static int GetSize( VoxelGroup group, int dimension )
        {
            switch ( dimension )
            {
                case 0: return group.SizeX;
                case 1: return group.SizeY;
                case 2: return group.SizeZ;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected static List<Voxel>[][] CreateSticks( VoxelGroup voxelGroup, int dimension )
        {
            var map = voxelGroup.CreateMap( v => v );

            switch ( dimension )
            {
                case 0: return CreateXSticks( voxelGroup, map );
                case 1: return CreateYSticks( voxelGroup, map );
                case 2: return CreateZSticks( voxelGroup, map );
                default:
                    throw new ArgumentException();
            }
        }

        private static List<Voxel>[][] CreateXSticks( VoxelGroup voxelGroup, Voxel[][][] map )
        {
            var voxels = new List<Voxel>[voxelGroup.SizeY][];
            for ( var y = 0; y < voxelGroup.SizeY; y++ )
                voxels[ y ] = new List<Voxel>[voxelGroup.SizeZ];

            for ( var y = 0; y < voxelGroup.SizeY; y++ )
            for ( var z = 0; z < voxelGroup.SizeZ; z++ )
            {
                Voxel currentNew = null;

                for ( var x = 0; x < voxelGroup.SizeX; x++ )
                {
                    var mapVoxel = map[ x ][ y ][ z ];
                    if ( mapVoxel == null )
                    {
                        if ( currentNew != null )
                        {
                            if ( voxels[ y ][ z ] == null )
                                voxels[ y ][ z ] = new List<Voxel>();
                            voxels[ y ][ z ].Add( currentNew );
                        }


                        currentNew = null;
                    }
                    else
                    {
                        var nextNew = new Voxel( (ushort) ( x + voxelGroup.PositionX ),
                            (ushort) ( y + voxelGroup.PositionY ), (ushort) ( z + voxelGroup.PositionZ ) );
                        if ( currentNew == null )
                            currentNew = nextNew;
                        else
                            currentNew.Merge( nextNew );
                    }
                }

                if ( currentNew == null ) 
                    continue;
                
                if ( voxels[ y ][ z ] == null )
                    voxels[ y ][ z ] = new List<Voxel>();
                
                voxels[ y ][ z ].Add( currentNew );
            }

            return voxels;
        }

        private static List<Voxel>[][] CreateYSticks( VoxelGroup voxelGroup, Voxel[][][] map )
        {
            var voxels = new List<Voxel>[voxelGroup.SizeX][];
            
            for ( var x = 0; x < voxelGroup.SizeX; x++ )
                voxels[ x ] = new List<Voxel>[voxelGroup.SizeZ];

            for ( var x = 0; x < voxelGroup.SizeX; x++ )
            for ( var z = 0; z < voxelGroup.SizeZ; z++ )
            {
                Voxel currentNew = null;

                for ( var y = 0; y < voxelGroup.SizeY; y++ )
                {
                    var mapVoxel = map[ x ][ y ][ z ];
                    if ( mapVoxel == null )
                    {
                        if ( currentNew != null )
                        {
                            if ( voxels[ x ][ z ] == null )
                                voxels[ x ][ z ] = new List<Voxel>();
                            voxels[ x ][ z ].Add( currentNew );
                        }

                        currentNew = null;
                    }
                    else
                    {
                        var nextNew = new Voxel( (ushort) ( x + voxelGroup.PositionX ),
                            (ushort) ( y + voxelGroup.PositionY ), (ushort) ( z + voxelGroup.PositionZ ) );
                        if ( currentNew == null )
                            currentNew = nextNew;
                        else
                            currentNew.Merge( nextNew );
                    }
                }

                if ( currentNew == null ) 
                    continue;
                
                if ( voxels[ x ][ z ] == null )
                    voxels[ x ][ z ] = new List<Voxel>();
                
                voxels[ x ][ z ].Add( currentNew );
            }

            return voxels;
        }

        private static List<Voxel>[][] CreateZSticks( VoxelGroup voxelGroup, Voxel[][][] map )
        {
            var voxels = new List<Voxel>[voxelGroup.SizeX][];
            for ( var x = 0; x < voxelGroup.SizeX; x++ )
                voxels[ x ] = new List<Voxel>[voxelGroup.SizeY];

            for ( var x = 0; x < voxelGroup.SizeX; x++ )
            for ( var y = 0; y < voxelGroup.SizeY; y++ )
            {
                Voxel currentNew = null;

                for ( var z = 0; z < voxelGroup.SizeZ; z++ )
                {
                    var mapVoxel = map[ x ][ y ][ z ];
                    if ( mapVoxel == null )
                    {
                        if ( currentNew != null )
                        {
                            if ( voxels[ x ][ y ] == null )
                                voxels[ x ][ y ] = new List<Voxel>();
                            voxels[ x ][ y ].Add( currentNew );
                        }

                        currentNew = null;
                    }
                    else
                    {
                        var nextNew = new Voxel( (ushort) ( x + voxelGroup.PositionX ),
                            (ushort) ( y + voxelGroup.PositionY ), (ushort) ( z + voxelGroup.PositionZ ) );
                        if ( currentNew == null )
                            currentNew = nextNew;
                        else
                            currentNew.Merge( nextNew );
                    }
                }

                if ( currentNew == null )
                    continue;
                
                if ( voxels[ x ][ y ] == null )
                    voxels[ x ][ y ] = new List<Voxel>();
                
                voxels[ x ][ y ].Add( currentNew );
            }

            return voxels;
        }
    }
}