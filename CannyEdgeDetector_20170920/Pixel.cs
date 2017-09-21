using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    class Pixel
    {
        // Gradient of x and y directions.
        private int Gx, Gy;
        // Size of the gradient.
        public double G { get; private set; }
        // The direction of the gradient(Theta).
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

            // Change the direction to [0, Pi].
            if (t < 0)
                t += Math.PI;
            T = (int)Math.Round(t * 4 / (Math.PI));

            // Round the direction to {0, 1, 2, 3}.
            T = T % 4;
        }
    }
}
