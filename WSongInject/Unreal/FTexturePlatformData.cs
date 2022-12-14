using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSongInject.Exceptions;

namespace WSongInject.Unreal
{
    internal class FTexturePlatformData
    {
        int SizeX { get; set; } = 256;
        int SizeY { get; set; } = 256;
        int NumSlices { get; set; } = 1;
        string PixelFormat { get; set; } = "PF_B8G8R8A8";
        int FirstMip { get; set; } = 0;
        uint BulkByteFlags { get; set; } = 72;
        ulong ContainerOffset { get; set; } = 1178; // ?
        byte[] Pixels { get; set; }
        ulong Unknown { get; set; } = 12; // ?

        public FTexturePlatformData(BinaryReader reader)
        {
            SizeX = reader.ReadInt32();
            SizeY = reader.ReadInt32();
            NumSlices = reader.ReadInt32();
            PixelFormat = FStringHelper.Read(reader);
            FirstMip = reader.ReadInt32();
            if (FirstMip != 0)
                throw new UnhandledUnrealReadException("FirstMip was not 0?");

            // not implementing FArray, so only able to read 1 element in lol
            var arraySize = reader.ReadInt32();
            if (arraySize != 1)
                throw new UnhandledUnrealReadException("No real FArray implementation");

            // for each array element, read the mipmap
            for (var i = 0; i < arraySize; i++)
            {
                // note: this is wrong? 01 00 00 00?
                var mipIsCooked = reader.ReadUInt32() == 1;
                if (!mipIsCooked)
                    throw new UnhandledUnrealReadException("mipIsCooked expected to be 1?");

                BulkByteFlags = reader.ReadUInt32();
                if (BulkByteFlags != 0x48)
                    throw new UnhandledUnrealReadException("FByteBulkData flags not handled, expected 72");

                var elementCount = reader.ReadUInt32();
                var elementCountDisk = reader.ReadUInt32();
                if (elementCount != elementCountDisk)
                    throw new UnhandledUnrealReadException("Expected elementCount == elementCountDisk");

                var bulkDataContainerOffset = reader.ReadUInt64(); // unknown
                if (bulkDataContainerOffset != 9999)
                    throw new UnhandledUnrealReadException("Expected offset to be ?");

                Pixels = reader.ReadBytes((int)elementCount);

                var mipSizeX = reader.ReadUInt32();
                var mipSizeY = reader.ReadUInt32();
                if (mipSizeX != SizeX || mipSizeY != SizeY)
                    throw new UnhandledUnrealReadException("Expected mipmap size to be same as platform texture size");

                Unknown = reader.ReadUInt64();
                if (Unknown != 0x0C)
                    throw new UnhandledUnrealReadException("Expected unknown platform data field to be 12?");
            }
        }

        public FTexturePlatformData(int sizeX, int sizeY, byte[] pixels)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Pixels = pixels;
        }

        internal int CalculatePlatformDataSize()
        {
            // header = 60 bytes
            // mipmap info at end = 16 bytes
            return 60 + Pixels.Length + 16;
        }

        internal void Write(BinaryWriter writer)
        {
            if (!BitConverter.IsLittleEndian)
                // must double check the result of Write() (must be written as LE)
                throw new Exception("Big-endian not handled yet");

            writer.Write(SizeX);
            writer.Write(SizeY);
            writer.Write(NumSlices);
            FStringHelper.Write(writer, PixelFormat);
            writer.Write(FirstMip);
            writer.Write(1); // ArraySize?

            writer.Write(1); // WRONG: mipIsCooked?
            writer.Write(BulkByteFlags);

            writer.Write(Pixels.Length); // ElementCount
            writer.Write(Pixels.Length); // ElementCountDisk

            writer.Write(ContainerOffset);
            writer.Write(Pixels);

            // mipmap data
            writer.Write(SizeX);
            writer.Write(SizeY);
            writer.Write(Unknown); // ?
        }
    }
}
