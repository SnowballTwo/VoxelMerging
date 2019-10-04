using System;
using System.IO;
using JetBrains.Annotations;

namespace VoxelMerger.Model
{
    /// <summary>
    /// Collection of voxels with bounding box
    /// </summary>
    public class VoxelGroup : IEquatable<VoxelGroup>
    {
        public VoxelGroup( ushort positionX, ushort positionY, ushort positionZ, ushort sizeX, ushort sizeY,
            ushort sizeZ, [NotNull] Voxel[] voxels )
        {
            Voxels = voxels;
            PositionX = positionX;
            PositionY = positionY;
            PositionZ = positionZ;
            SizeX = sizeX;
            SizeY = sizeY;
            SizeZ = sizeZ;
        }

        public VoxelGroup( [NotNull] VoxelGroup other, [NotNull] Voxel[] voxels )
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

        #region IEquatable<VoxelGroup> Members

        public bool Equals( VoxelGroup other )
        {
            if ( ReferenceEquals( null, other ) ) return false;
            if ( ReferenceEquals( this, other ) ) return true;
            if ( !( PositionX == other.PositionX && PositionY == other.PositionY && PositionZ == other.PositionZ &&
                    SizeX == other.SizeX && SizeY == other.SizeY && SizeZ == other.SizeZ ) )
                return false;

            var map = CreateMap(v => v != null );
            var otherMap = other.CreateMap(v => v != null);

            for ( var x = 0; x < SizeX; x++ )
            for ( var y = 0; y < SizeY; y++ )
            for ( var z = 0; z < SizeZ; z++ )
                if ( map[ x ][ y ][ z ] != otherMap[ x ][ y ][ z ] )
                    return false;

            return true;
        }

        #endregion

        public static VoxelGroup Deserialize( BinaryReader reader )
        {
            var positionX = reader.ReadUInt16();
            var positionY = reader.ReadUInt16();
            var positionZ = reader.ReadUInt16();
            var sizeX = reader.ReadUInt16();
            var sizeY = reader.ReadUInt16();
            var sizeZ = reader.ReadUInt16();

            var count = reader.ReadInt32();
            var voxels = new Voxel[count];

            for ( var i = 0; i < count; i++ )
                voxels[ i ] = Voxel.Deserialize( reader );

            return new VoxelGroup( positionX, positionY, positionZ, sizeX, sizeY, sizeZ, voxels );
        }

        public T[][][] CreateMap<T>( [NotNull]Func<Voxel, T> selector )
        {
            var result = new T[SizeX][][];
            for ( var x = 0; x < SizeX; x++ )
            {
                result[ x ] = new T[SizeY][];
                for ( var y = 0; y < SizeY; y++ )
                    result[ x ][ y ] = new T[SizeZ];
            }

            foreach ( var voxel in Voxels )
                for ( var x = 0; x < voxel.SizeX; x++ )
                for ( var y = 0; y < voxel.SizeY; y++ )
                for ( var z = 0; z < voxel.SizeZ; z++ )
                    result[ x + voxel.PositionX - PositionX ][ y + voxel.PositionY - PositionY ][
                        z + voxel.PositionZ - PositionZ ] = selector(voxel);

            return result;
        }
        
        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType() != GetType() ) return false;
            return Equals( (VoxelGroup) obj );
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

        public static bool operator ==( VoxelGroup left, VoxelGroup right )
        {
            return Equals( left, right );
        }

        public static bool operator !=( VoxelGroup left, VoxelGroup right )
        {
            return !Equals( left, right );
        }
    }
}