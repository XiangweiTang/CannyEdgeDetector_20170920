using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class GaussianFilter
    {
        byte[,] Matrix;
        int X, Y;
        int Depth;
        public byte[,] MaskedMatrix { get; private set; }
        public GaussianFilter(byte[,] matrix, int depth)
        {
            Matrix = matrix;
            X = Matrix.GetLength(0);
            Y = Matrix.GetLength(1);
            MaskedMatrix = new byte[X, Y];
            Depth = depth;
        }

        public void Convolution()
        {
            for(int i = 0; i < X; i++)
            {
                for(int j = 0; j < Y; j++)
                {
                    MaskedMatrix[i, j] = SetValue(i, j);
                }
            }
        }

        private byte SetValue(int i, int j)
        {
            int value = 0;
            int divisor = 0;
            for(int x = -2; x <= 2; x++)
            {
                for(int y = -2; y <= 2; y++)
                {
                    if (Common.Valid(i + x, X) &&Common.Valid(j + y*Depth, Y))
                    {
                        int neigborValue = Matrix[i, j + y * Depth];
                        int maskValue= Common.MaskMatrix[2 - x, 2 - y];
                        divisor += maskValue;
                        value += neigborValue * maskValue;
                    }
                }
            }
            return (divisor == 0) ? (byte)0 : Convert.ToByte(value / divisor);
        }
    }
}
