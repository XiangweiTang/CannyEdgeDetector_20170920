﻿using System;
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
        int Width = 3;
        public byte[,] MaskedMatrix { get; private set; }
        public GaussianFilter(byte[,] matrix, int width)
        {
            Matrix = matrix;
            X = Matrix.GetLength(0);
            Y = Matrix.GetLength(1);
            MaskedMatrix = new byte[X, Y];
            Width = width;
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
                    if (Valid(i + x, X) && Valid(j + y*Width, Y))
                    {
                        int neigborValue = Matrix[i, j + y * Width];
                        int maskValue= Common.MaskMatrix[2 - x, 2 - y];
                        divisor += maskValue;
                        value += neigborValue * maskValue;
                    }
                }
            }
            return (divisor == 0) ? (byte)0 : Convert.ToByte(value / divisor);
        }

        private bool Valid(int i, int max)
        {
            return 0 <= i && i < max;
        }
    }
}
