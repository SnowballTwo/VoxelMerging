using System;
using System.Collections.Generic;

namespace VoxelMerger.Strategies
{
    public abstract class StickStrategy : Strategy
    {
        protected List<Voxel>[][] CreateSticks( Defect defect, int dimension )
        { 
            switch( dimension )
            {
                case 0: return CreateXSticks( defect );
                case 1: return CreateYSticks( defect );
                case 2: return CreateZSticks( defect );
                default:
                    throw new ArgumentException();
            }
        }

        private List<Voxel>[][] CreateXSticks( Defect defect )
        {
            var voxels = new List<Voxel>[defect.SizeY][];
            for (var y = 0; y < defect.SizeY; y++)
                voxels[y] = new List<Voxel>[defect.SizeZ];
            
            var map = defect.CreateVoxelMap();

            for( var y = 0; y < defect.SizeY; y++ )
            for( var z = 0; z < defect.SizeZ; z++ )
            {
                Voxel currentNew = null;

                for( var x = 0; x < defect.SizeX; x++ )
                {
                    var mapVoxel = map[ x ][ y ][ z ];
                    if( mapVoxel == null )
                    {
                        if( currentNew != null )
                        { 
                            if (voxels[y][z] == null)
                                voxels[y][z] = new List<Voxel>();
                            voxels[y][z].Add( currentNew );
                        }

                        
                        currentNew = null;
                    }
                    else
                    {
                        var nextNew = new Voxel( (ushort) ( x + defect.PositionX ), (ushort) ( y + defect.PositionY ), (ushort) ( z + defect.PositionZ ) );
                        if( currentNew == null )
                            currentNew = nextNew;
                        else
                            currentNew.Merge( nextNew );
                    }
                }

                if( currentNew != null )
                {
                    if (voxels[y][z] == null)
                        voxels[y][z] = new List<Voxel>();
                    voxels[y][z].Add( currentNew );
                }
                
            }

            return voxels;
        }

        private List<Voxel>[][] CreateYSticks( Defect defect )
        {
            var voxels = new List<Voxel>[defect.SizeX][];
            for (var x = 0; x < defect.SizeX; x++)
                voxels[x] = new List<Voxel>[defect.SizeZ];
            
            var map = defect.CreateVoxelMap();

            for( var x = 0; x < defect.SizeX; x++ )
            for( var z = 0; z < defect.SizeZ; z++ )
            {
                Voxel currentNew = null;

                for( var y = 0; y < defect.SizeY; y++ )
                {
                    var mapVoxel = map[ x ][ y ][ z ];
                    if( mapVoxel == null )
                    {
                        if( currentNew != null )
                        {
                            if (voxels[x][z] == null)
                                voxels[x][z] = new List<Voxel>();
                            voxels[x][z].Add( currentNew );
                        }
                        currentNew = null;
                    }
                    else
                    {
                        var nextNew = new Voxel( (ushort) ( x + defect.PositionX ), (ushort) ( y + defect.PositionY ), (ushort) ( z + defect.PositionZ ) );
                        if( currentNew == null )
                            currentNew = nextNew;
                        else
                            currentNew.Merge( nextNew );
                    }
                }

                if( currentNew != null )
                {
                    if (voxels[x][z] == null)
                        voxels[x][z] = new List<Voxel>();
                    voxels[x][z].Add( currentNew );
                }
            }

            return voxels;
        }

        private List<Voxel>[][] CreateZSticks( Defect defect )
        {
            var voxels = new List<Voxel>[defect.SizeX][];
            for (var x = 0; x < defect.SizeX; x++)
                voxels[x] = new List<Voxel>[defect.SizeY];
            
            var map = defect.CreateVoxelMap();

            for( var x = 0; x < defect.SizeX; x++ )
            for( var y = 0; y < defect.SizeY; y++ )
            {
                Voxel currentNew = null;

                for( var z = 0; z < defect.SizeZ; z++ )
                {
                    var mapVoxel = map[ x ][ y ][ z ];
                    if( mapVoxel == null )
                    {
                        if( currentNew != null )
                        {
                            if (voxels[x][y] == null)
                                voxels[x][y] = new List<Voxel>();
                            voxels[x][y].Add( currentNew );
                        }
                        currentNew = null;
                    }
                    else
                    {
                        var nextNew = new Voxel( (ushort) ( x + defect.PositionX ), (ushort) ( y + defect.PositionY ), (ushort) ( z + defect.PositionZ ) );
                        if( currentNew == null )
                            currentNew = nextNew;
                        else
                            currentNew.Merge( nextNew );
                    }
                }

                if( currentNew != null )
                {
                    if (voxels[x][y] == null)
                        voxels[x][y] = new List<Voxel>();
                    voxels[x][y].Add( currentNew );
                }
            }

            return voxels;
        }
    }
}