using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    // This class is to run Gaussian Filter
    class GaussianFilter:ImageProcess
    {
        public GaussianFilter(string inputPath, string outputPath):base(inputPath,outputPath)
        {
        }

        protected override void Init()
        {
        }

        /// <summary>
        /// Run convolutoin on all pixels.
        /// </summary>
        protected override void Run()
        {
            for(int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y * Depth; j++)
                {
                    ProcessedMatrix[i, j] = Convolution(i, j);
                }
            }
        }

        /// <summary>
        /// Calculate convolution of pixel at [i, j].
        /// </summary>
        /// <param name="i">i-Coordinate</param>
        /// <param name="j">j-Coordinate</param>
        /// <returns>The result after Convolution at [i,j]</returns>
        private byte Convolution(int i, int j)
        {
            int value = 0;
            int divisor = 0;
            for(int x = -2; x <= 2; x++)
            {
                for(int y = -2; y <= 2; y++)
                {
                    if (Common.Valid(i + x, X) && Common.Valid(j + y * Depth, Y * Depth))
                    {
                        int neigborValue = Matrix[i + x, j + y * Depth];
                        int maskValue = Common.MaskMatrix[2 - x, 2 - y];
                        divisor += maskValue;
                        value += neigborValue * maskValue;
                    }
                }
            }
            int gaussed=((divisor == 0) ? 0 : value / divisor);
            return (byte)gaussed;
        }
    }
}
