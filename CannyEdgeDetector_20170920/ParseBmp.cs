using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CannyEdgeDetector_20170920
{
    class ParseBmp
    {
        // The BMP file header(first 54 bytes).
        public byte[] Header { get; private set; }
        // Content is the BMP file bytes except the header.
        public byte[] Content { get; private set; }
        // The X-size of the BMP file.
        public int X { get; private set; }
        // The Y-size of the BMP file.
        public int Y { get; private set; }
        // The depth of the BMP file(bit of the image/8)
        public int Depth { get; private set; }
        // The "Wrapped" content.
        public byte[,] Matrix { get; private set; }
                
        public void LoadBmp(string path)
        {
            var bmpBytes = Common.ReadBmp(path);
            int offset = BitConverter.ToInt32(bmpBytes, 10);
            Header = new byte[offset];
            Array.Copy(bmpBytes, Header, offset);
            Content = new byte[bmpBytes.Length - offset];
            Array.Copy(bmpBytes, offset, Content, 0, Content.Length);
            Y = BitConverter.ToInt32(Header, 18);
            X = BitConverter.ToInt32(Header, 22);
            Depth = BitConverter.ToInt16(Header, 28)/8;
            Wrap();
        }

        /// <summary>
        /// "Wrap" the 1-D byte array into a 2-D array, or, matrix.
        /// </summary>
        public void Wrap()
        {
            Matrix = new byte[X, Y*Depth];
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y*Depth; y++)
                {
                    Matrix[x, y] = Content[x * Y * Depth + y];
                }
            }
        }

        /// <summary>
        /// "Merge" the 2-D matrix back into 1-D list.
        /// </summary>
        /// <param name="matrix">The 2-D matrix requires merge.</param>
        /// <returns>The 1-D list.</returns>
        public IEnumerable<byte> Merge(byte[,] matrix)
        {
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y*Depth; y++)
                {
                    yield return matrix[x, y];
                }
            }
        }               
    }
}
