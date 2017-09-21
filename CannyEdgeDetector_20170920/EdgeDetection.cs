using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class EdgeDetection:ImageProcess
    {
        Dictionary<int, Tuple<int, int>> DirectionDict = new Dictionary<int, Tuple<int, int>>();
        private Pixel[,] GTMatrix;
        
        public EdgeDetection(string inputPath, string outputPath, Config cfg):base(inputPath,outputPath, cfg)
        {
        }

        protected override void Init()
        {
            // The rounded edge direction angle.
            // 0: 0 degree against y-axis.
            // 1: 45 degrees against y-axis.
            // 2: 90 degrees against y-axis.
            // 3: 135 degrees agains y-axis.
            DirectionDict.Add(0, new Tuple<int, int>(1, 0));
            DirectionDict.Add(1, new Tuple<int, int>(1, 1));
            DirectionDict.Add(2, new Tuple<int, int>(0, 1));
            DirectionDict.Add(3, new Tuple<int, int>(1, -1));
            GTMatrix = new Pixel[X, Y];     
        }

        /// <summary>
        /// Run edge detector.
        /// 1. Find the intensity gradient for all the pixels.
        /// 2. Run non-maximum suppression.
        /// </summary>
        protected override void Run()
        {
            SetIGMatrix();
            NonMaxSuppression();
        }
        
        /// <summary>
        /// Set the intensity gradient for all pixels.
        /// </summary>
        private void SetIGMatrix()
        {
            for(int i = 0; i < X; i++)
            {
                for(int j = 0; j < Y ; j++)
                {
                    GTMatrix[i, j] = SetIntensityGradient(i, j);
                }
            }
        }

        /// <summary>
        /// Set the intensity gradient of a certain pixel.
        /// </summary>
        /// <param name="i">i-Coordinate</param>
        /// <param name="j">j-Coordinate</param>
        /// <returns></returns>
        private Pixel SetIntensityGradient(int i, int j)
        {   
            // Current value
            int current = Matrix[i, j * Depth];
            // Neigbor value of x-direction and y-direction
            int nextX = i + 1 < X ? Matrix[i + 1, j * Depth] : current;
            int nextY = (j + 1) * Depth < Y ? Matrix[i, (j + 1) * Depth] : current;
            // Gradient of x and y direction
            int gx = nextX - current;
            int gy = nextY - current;
            Pixel p = new Pixel(gx, gy);
            return p;
        }

        /// <summary>
        /// Run non max suppression on all pixels.
        /// </summary>
        private void NonMaxSuppression()
        {
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    // If the current value is the local max, then return 0xff(white), otherwise 0x00(black)
                    byte value = (byte)(LocalMax(i, j) ? 0xff : 0x00);
                    for (int d = 0; d < Depth; d++)
                    {
                        ProcessedMatrix[i, j * Depth + d] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Check if a certain pixel is local max.
        /// </summary>
        /// <param name="i">i-Coordinate</param>
        /// <param name="j">j-Coordinate</param>
        /// <returns>If pixel [i, j] is local max</returns>
        private bool LocalMax(int i, int j)
        {
            var current = GTMatrix[i, j];
            int xVector = DirectionDict[current.T].Item1;
            int yVector = DirectionDict[current.T].Item2;

            // If the neigbor pixel doesn't exists, then return false.
            if (!(Common.Valid(i + xVector, X) && Common.Valid(j + yVector, Y)))
                return false;
            if (!(Common.Valid(i - xVector, X) && Common.Valid(j - yVector, Y)))
                return false;

            // Get the current value and the value on left/right side of the current vector.
            double leftValue = GTMatrix[i + xVector, j + yVector].G;
            double rightValue = GTMatrix[i - xVector, j - yVector].G;
            double currentValue = GTMatrix[i, j].G;

            return (currentValue - leftValue > Cfg.Threshold) && (currentValue - rightValue > Cfg.Threshold);
        }        
    }
}
