using System;
using System.IO;

namespace VoxelMerger
{
    /// <summary>
    /// Not actually a voxel, but potentially a cuboid with arbitrary dimension.
    /// </summary>
    public class Voxel
    {
        public Voxel( ushort positionX, ushort positionY, ushort positionZ, ushort sizeX = 1, ushort sizeY = 1, ushort sizeZ = 1 )
        {
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }
        
        public Voxel( int positionX, int positionY, int positionZ )
        {
            PositionX = (ushort)positionX;
            PositionY = (ushort)positionY;
            PositionZ = (ushort)positionZ;
            SizeX = 1;
            SizeY = 1;
            SizeZ = 1;
        }

        public ushort PositionX { get; private set; }

        public ushort PositionY { get; private set; }

        public ushort PositionZ { get; private set; }

        public ushort SizeX { get; private set; }

        public ushort SizeY { get; private set; }

        public ushort SizeZ { get; private set; }

        public static Voxel Deserialize( BinaryReader reader )
        {
            return new Voxel(
                reader.ReadUInt16(),
                reader.ReadUInt16(),
                reader.ReadUInt16(),
                reader.ReadUInt16(),
                reader.ReadUInt16(),
                reader.ReadUInt16() );
        }
       
        public bool CanMerge( Voxel other )
        {
            if( PositionX + SizeX == other.PositionX || other.PositionX + other.SizeX == PositionX )
                return PositionY == other.PositionY && PositionZ == other.PositionZ && SizeY == other.SizeY && SizeZ == other.SizeZ;
            if( PositionY + SizeY == other.PositionY || other.PositionY + other.SizeY == PositionY )
                return PositionX == other.PositionX && PositionZ == other.PositionZ && SizeX == other.SizeX && SizeZ == other.SizeZ;
            if( PositionZ + SizeZ == other.PositionZ || other.PositionZ + other.SizeZ == PositionZ )
                return PositionX == other.PositionX && PositionY == other.PositionY && SizeX == other.SizeX && SizeY == other.SizeY;
            return false;
        }

        public void Merge( Voxel other )
        {
            if( !CanMerge( other ) )
                throw new ArgumentException( "Attempt to merge unmergable voxels" );

            if( PositionX + SizeX == other.PositionX )
            {
                SizeX += other.SizeX;
            }
            else if( PositionY + SizeY == other.PositionY )
            {
                SizeY += other.SizeY;
            }
            else if( PositionZ + SizeZ == other.PositionZ )
            {
                SizeZ += other.SizeZ;
            }
            else if( other.PositionX + other.SizeX == PositionX )
            {
                PositionX = other.PositionX;
                SizeX += other.SizeX;
            }
            else if( PositionY + SizeY == other.PositionY )
            {
                PositionY = other.PositionY;
                SizeY += other.SizeY;
            }
            else if( PositionZ + SizeZ == other.PositionZ )
            {
                PositionZ = other.PositionZ;
                SizeZ += other.SizeZ;
            }
        }
    }
}