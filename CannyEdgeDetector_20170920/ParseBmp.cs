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
        private byte[] Content;
        private byte[] Header;
        private int X;
        private int Y;
        private int Depth;
        private byte[,] Matrix;
        
        
        
        public void LoadBmp(string path)
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
            Depth = BitConverter.ToInt16(Header, 28)/8;
            Wrap();
        }

        private void Wrap()
        {
            Matrix = new byte[X, Y*Depth];
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y*Depth; y++)
                {
                    Matrix[x, y] = Content[x * Y*Depth + y];
                }
            }
        }

        private IEnumerable<byte> Merge(byte[,] matrix)
        {
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y*Depth; y++)
                {
                    yield return matrix[x, y];
                }
            }
        }

        public void RunGaussisanFilter(string outputPath)
        {
            GaussianFilter gf = new GaussianFilter(Matrix, Depth);
            gf.Convolution();
            var newContent = Merge(gf.MaskedMatrix);
            var newBytes = Header.Concat(newContent);
            Common.WriteBmp(newBytes, outputPath);
        }

        public void RunFlattern(string outputPath)
        {
            Flattern f = new Flattern(Matrix, Depth);
            f.RunFlattern();
            var newContent = Merge(f.FlatternMatrix);
            var newBytes = Header.Concat(newContent);
            Common.WriteBmp(newBytes, outputPath);
        }
    }
}
