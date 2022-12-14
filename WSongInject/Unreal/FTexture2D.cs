using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSongInject.Exceptions;

namespace WSongInject.Unreal
{
    internal class FTexture2D
    {
        // Seems to be constant?
        static readonly byte[] Header = {
	        0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00,
	        0x0E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        uint ContainerSize { get; set; } // Size of Texture2D + ?
        FTexturePlatformData TexturePlatformData { get; set; }

        public FTexture2D(BinaryReader binaryReader)
        {
            var header = binaryReader.ReadBytes(Header.Length);
            if (!header.SequenceEqual(Header)) // SLOW SLOW SLOW SLOW SLOW SLOW SLOW SLOW SLOW SLOW
                throw new UnhandledUnrealReadException("Header was not equal");
            ContainerSize = binaryReader.ReadUInt32();
            TexturePlatformData = new FTexturePlatformData(binaryReader);
        }

        public FTexture2D(int width, int height, byte[] pixels)
        {
            TexturePlatformData = new FTexturePlatformData(width, height, pixels);
            ContainerSize = (uint)TexturePlatformData.CalculatePlatformDataSize() + ?;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            writer.Write(ContainerSize);
            TexturePlatformData.Write(writer);
        }
    }
}
