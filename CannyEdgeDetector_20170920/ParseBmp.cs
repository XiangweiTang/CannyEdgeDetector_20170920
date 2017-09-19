using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CannyEdgeDetector_20170919
{
    class ParseBmp
    {
        public byte[] Content { get; private set; }
        public byte[] Header { get; private set; }
        public int Size { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public byte[,] Matrix { get; private set; }
        
        
        public void Test(string path)
        {
            var bmpBytes = Common.ReadBmp(path);
            Init(bmpBytes);
        }

        private void Init(byte[] bmpBytes)
        {
            int offset = BitConverter.ToInt32(bmpBytes, 10);
            Header = new byte[offset];
            Array.Copy(bmpBytes, Header, offset);
            Content = new byte[bmpBytes.Length - offset];
            Array.Copy(bmpBytes, offset, Content, 0, Content.Length);
            X = BitConverter.ToInt32(Header, 18);
            Y = BitConverter.ToInt32(Header, 22);
            Width = BitConverter.ToInt16(Header, 28)/8;
            Wrap();
            GaussianFilter gf = new GaussianFilter(Matrix);
            gf.Convolution();
            var newMatrix = gf.MaskedMatrix;
            var newContent = Merge(newMatrix).ToList();
            var newBmpBytes = Header.Concat(newContent).ToArray();
            Common.WriteBmp(newBmpBytes, "1.bmp");
        }

        private void Wrap()
        {
            Matrix = new byte[X, Y*Width];
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y*Width; y++)
                {
                    Matrix[x, y] = Content[x * Y*Width + y];
                }
            }
        }

        private IEnumerable<byte> Merge(byte[,] matrix)
        {
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y*Width; y++)
                {
                    yield return matrix[x, y];
                }
            }
        }
    }
}
