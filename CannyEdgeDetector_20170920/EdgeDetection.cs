using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class EdgeDetection
    {
        byte[,] Matrix;
        int X, Y;
        int Depth;
        Dictionary<int, Tuple<int, int>> DirectionDict = new Dictionary<int, Tuple<int, int>>();
        public Pixel[,] GTMatrix { get; private set; }

        public byte[,] EdgeMatrix;


        public EdgeDetection(byte[,] matrix, int depth)
        {
            Matrix = matrix;
            Depth = depth;
            Init();
        }
        private void Init()
        {
            X = Matrix.GetLength(0);
            Y = Matrix.GetLength(1);
            DirectionDict.Add(0, new Tuple<int, int>(1, 0));
            DirectionDict.Add(1, new Tuple<int, int>(1, 1));
            DirectionDict.Add(2, new Tuple<int, int>(0, 1));
            DirectionDict.Add(3, new Tuple<int, int>(1, -1));
            GTMatrix = new Pixel[X, Y / Depth];
            EdgeMatrix = new byte[X, Y];
        }

        public void RunEdgeDetection()
        {
            SetGTMatrix();
            NonMaxSuppression();
        }

        private void SetGTMatrix()
        {
            for(int i = 0; i < X; i++)
            {
                for(int j = 0; j < Y / Depth; j++)
                {
                    GTMatrix[i, j] = SetGradient(i, j);
                }
            }
        }

        private Pixel SetGradient(int i, int j)
        {
            int current = Matrix[i, j * Depth];
            int nextX = i + 1 < X ? Matrix[i + 1, j * Depth] : current;
            int nextY = (j + 1) * Depth < Y ? Matrix[i, (j + 1) * Depth] : current;
            int gx = nextX - current;
            int gy = nextY - current;
            Pixel p = new Pixel(gx, gy);
            return p;
        }

        private void NonMaxSuppression()
        {
            for(int i=0;i<X;i++)
            {
                for (int j = 0; j * Depth < Y; j++)
                {
                    byte value = (byte)(LocalMax(i, j) ? 0xff : 0x00);
                    for (int d = 0; d < Depth; d++)
                    {
                        EdgeMatrix[i, j * Depth + d] = value;
                    }
                }
            }
        }
        
        private bool LocalMax(int i, int j)
        {
            var current = GTMatrix[i, j];
            int xVector = DirectionDict[current.T].Item1;
            int yVector = DirectionDict[current.T].Item2;
            if (!(Common.Valid(i + xVector, X / Depth) && Common.Valid(j + yVector, Y / Depth)))
                return false;
            if (!(Common.Valid(i - xVector, X / Depth) && Common.Valid(j - yVector, Y / Depth)))
                return false;

            double leftValue = GTMatrix[i + xVector, j + yVector].G;
            double rightValue = GTMatrix[i - xVector, j - yVector].G;
            double currentValue = GTMatrix[i, j].G;

            return (currentValue >= leftValue) && (currentValue >= rightValue);
        }
    }
}
