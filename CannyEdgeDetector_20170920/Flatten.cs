using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    // This class is to change 24 bit bit map into 25-bit "grey scale".
    class Flatten:ImageProcess
    {        
        public Flatten(string inputPath, string outputPath):base(inputPath,outputPath)
        {
        }

        protected override void Init()
        {
        }

        /// <summary>
        /// GREY=(R+G+B)/3
        /// </summary>
        protected override void Run()
        {
            for(int x = 0; x < X; x++)
            {
                for(int y = 0; y < Y ; y ++)
                {
                    int sum = 0;
                    for(int d = 0; d < Depth; d++)
                    {
                        sum += Matrix[x, y*Depth + d];
                    }
                    byte avg = (byte)((sum / Depth));
                    for(int d = 0; d < Depth; d++)
                    {
                        ProcessedMatrix[x, y*Depth + d] = avg;
                    }
                }
            }
        }
    }
}
