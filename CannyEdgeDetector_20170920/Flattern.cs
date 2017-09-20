using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class Flattern
    {
        byte[,] Matrix;
        int X, Y;
        int Depth;
        public byte[,] FlatternMatrix;

        public Flattern(byte[,] matrix, int depth)
        {
            Matrix = matrix;
            Depth = depth;
            Init();
        }

        private void Init()
        {
            X = Matrix.GetLength(0);
            Y = Matrix.GetLength(1);
            FlatternMatrix = new byte[X, Y];
        }

        public void RunFlattern()
        {
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y * Depth < Y ; y ++)
                {
                    int sum = 0;
                    for(int d = 0; d < Depth; d++)
                    {
                        sum += Matrix[x, y*Depth + d];
                    }
                    byte avg = (byte)((sum / Depth));
                    for(int d = 0; d < Depth; d++)
                    {
                        FlatternMatrix[x, y*Depth + d] = avg;
                    }
                }
            }
        }
    }
}
