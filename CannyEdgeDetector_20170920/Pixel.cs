using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class Pixel
    {
        private int Gx, Gy;
        public double G { get; private set; }
        public int T { get; private set; }
        public Pixel(int gx, int gy)
        {
            Gx = gx;
            Gy = gy;
            Set();
        }
        private void Set()
        {
            G = Math.Sqrt(Gx * Gx + Gy * Gy);
            double t = (Gx == 0 && Gy == 0) ? 4 : Math.Atan2(Gy, Gx);
            if (t < 0)
                t += Math.PI;
            T = (int)Math.Round(t * 4 / (Math.PI));
            T = T % 4;
        }
    }
}
