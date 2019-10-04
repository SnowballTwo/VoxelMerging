using System;
using System.IO;
using JetBrains.Annotations;

namespace VoxelMerger
{
    public class Defect : IEquatable<Defect>
    {
        public Defect( ushort positionX, ushort positionY, ushort positionZ, ushort sizeX, ushort sizeY, ushort sizeZ, [NotNull] Voxel[] voxels )
        {
            Voxels = voxels;
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        public Defect( [NotNull] Defect other, [NotNull] Voxel[] voxels )
        {
            Voxels = voxels;
            PositionX = other.PositionX;
            PositionY = other.PositionY;
            PositionZ = other.PositionZ;
            SizeX = other.SizeX;
            SizeY = other.SizeY;
            SizeZ = other.SizeZ;
        }

        [NotNull] public Voxel[] Voxels { get; }

        public ushort PositionX { get; }

        public ushort PositionY { get; }

        public ushort PositionZ { get; }

        public ushort SizeX { get; }

        public ushort SizeY { get; }

        public ushort SizeZ { get; }

        public bool Equals( Defect other )
        {
            if( ReferenceEquals( null, other ) ) return false;
            if( ReferenceEquals( this, other ) ) return true;
            if( !( PositionX == other.PositionX && PositionY == other.PositionY && PositionZ == other.PositionZ && SizeX == other.SizeX && SizeY == other.SizeY && SizeZ == other.SizeZ ) )
                return false;

            var map = CreateBooleanMap();
            var otherMap = other.CreateBooleanMap();

            for( var x = 0; x < SizeX; x++ )
            for( var y = 0; y < SizeY; y++ )
            for( var z = 0; z < SizeZ; z++ )
                if( map[ x ][ y ][ z ] != otherMap[ x ][ y ][ z ] )
                    return false;

            return true;
        }

        public ushort GetSize( int dimension )
        {
            switch( dimension )
            {
                case 0: return SizeX;
                case 1: return SizeY;
                case 2: return SizeZ;
                default:
                    throw new ArgumentOutOfRangeException( nameof(dimension) );
            }
        }

        public ushort GetPosition( int dimension )
        {
            switch( dimension )
            {
                case 0: return PositionX;
                case 1: return PositionY;
                case 2: return PositionZ;
                default:
                    throw new ArgumentOutOfRangeException( nameof(dimension) );
            }
        }

        public static Defect Deserialize( BinaryReader reader )
        {
            var positionX = reader.ReadUInt16();
            var positionY = reader.ReadUInt16();
            var positionZ = reader.ReadUInt16();
            var sizeX = reader.ReadUInt16();
            var sizeY = reader.ReadUInt16();
            var sizeZ = reader.ReadUInt16();

            var count = reader.ReadInt32();
            var voxels = new Voxel[count];

            for( var i = 0; i < count; i++ )
                voxels[ i ] = Voxel.Deserialize( reader );

            return new Defect( positionX, positionY, positionZ, sizeX, sizeY, sizeZ, voxels );
        }

        public bool[][][] CreateBooleanMap()
        {
            var result = new bool[SizeX][][];
            for( var x = 0; x < SizeX; x++ )
            {
                result[ x ] = new bool[SizeY][];
                for( var y = 0; y < SizeY; y++ )
                    result[ x ][ y ] = new bool[SizeZ];
            }

            foreach( var voxel in Voxels )
                for( var x = 0; x < voxel.SizeX; x++ )
                for( var y = 0; y < voxel.SizeY; y++ )
                for( var z = 0; z < voxel.SizeZ; z++ )
                    result[ x + voxel.PositionX - PositionX ][ y + voxel.PositionY - PositionY ][ z + voxel.PositionZ - PositionZ ] = true;

            return result;
        }

        public Voxel[][][] CreateVoxelMap()
        {
            var result = new Voxel[SizeX][][];
            for( var x = 0; x < SizeX; x++ )
            {
                result[ x ] = new Voxel[SizeY][];
                for( var y = 0; y < SizeY; y++ )
                    result[ x ][ y ] = new Voxel[SizeZ];
            }

            foreach( var voxel in Voxels )
                for( var x = 0; x < voxel.SizeX; x++ )
                for( var y = 0; y < voxel.SizeY; y++ )
                for( var z = 0; z < voxel.SizeZ; z++ )
                    result[ x + voxel.PositionX - PositionX ][ y + voxel.PositionY - PositionY ][ z + voxel.PositionZ - PositionZ ] = voxel;

            return result;
        }

        public override bool Equals( object obj )
        {
            if( ReferenceEquals( null, obj ) ) return false;
            if( ReferenceEquals( this, obj ) ) return true;
            if( obj.GetType() != GetType() ) return false;
            return Equals( (Defect) obj );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PositionX.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ PositionY.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ PositionZ.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ SizeX.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ SizeY.GetHashCode();
                hashCode = ( hashCode * 397 ) ^ SizeZ.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==( Defect left, Defect right )
        {
            return Equals( left, right );
        }

        public static bool operator !=( Defect left, Defect right )
        {
            return !Equals( left, right );
        }
    }
}