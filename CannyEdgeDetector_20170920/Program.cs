using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\v-xianta\Downloads\Valve_original_(1).bmp";
            ParseBmp pb = new ParseBmp();
            pb.Test(path);
        }
    }
}
