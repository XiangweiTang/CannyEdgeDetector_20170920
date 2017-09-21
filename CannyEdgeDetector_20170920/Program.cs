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
            string OriginalPath = @"01_Original.bmp";
            string GaussianedPath = @"02_Gaussianed.bmp";
            string FlatternPath = @"03_Flattern.bmp";
            string EdgePath = @"04_Edge.bmp";
            ParseBmp pb = new ParseBmp();
            pb.LoadBmp(OriginalPath);
            //pb.RunGaussisanFilter(GaussianedPath);

            //pb.LoadBmp(GaussianedPath);
            //pb.RunFlattern(FlatternPath);

            //pb.LoadBmp(FlatternPath);
            pb.RunIntencityGradient(EdgePath);
        }
    }
}
