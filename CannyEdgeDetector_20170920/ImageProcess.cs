using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CannyEdgeDetector_20170920
{
    abstract class ImageProcess
    {        
        protected byte[,] Matrix { get; private set; }
        protected int X { get; private set; }
        protected int Y { get; private set; }
        protected int Depth { get; private set; }
        protected byte[,] ProcessedMatrix { get; set; }
        protected Config Cfg { get; private set; }
        private string InputPath;
        private string OutputPath;
        private ParseBmp Pb = new ParseBmp();        

        /// <summary>
        /// Read bmp file from the input path, and write the processed file into output path.
        /// </summary>
        /// <param name="inputPath">The path of the BMP file before process.</param>
        /// <param name="outputPath">The path of the BMP file after process.</param>
        public ImageProcess(string inputPath, string outputPath, Config cfg)
        {
            InputPath = inputPath;
            OutputPath = outputPath;
            Cfg = cfg;
            BasicInit();
            Init();
        }

        /// <summary>
        /// Common initializations.
        /// </summary>
        private void BasicInit()
        {
            // Load BMP file.
            Pb.LoadBmp(InputPath);

            // Get value from the ParseBmp class.
            Matrix = Pb.Matrix;
            X = Pb.X;
            Y = Pb.Y;
            Depth = Pb.Depth;

            // The processed matrix should be the same size as the original one.
            ProcessedMatrix = new byte[X, Y * Depth];
        }

        /// <summary>
        /// Run different image process procedures.
        /// </summary>
        public void RunImageProcess()
        {            
            Run();

            // Merge the binary matrix back into binary array. 
            var newContent = Pb.Merge(ProcessedMatrix);
            byte[] array = new byte[Pb.Content.Length];
            int length = Math.Min(array.Count(), newContent.Count());
            Array.Copy(newContent.ToArray(), array, length);
            var newBytes = Pb.Header.Concat(array);

            // Output BMP file.
            Common.WriteBmp(newBytes, OutputPath);
        }

        /// <summary>
        /// Run will generate ProcessedMatrix
        /// </summary>
        protected abstract void Run();

        /// <summary>
        /// Make neccessary initializations.
        /// </summary>
        protected abstract void Init();
    }
}
