using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CannyEdgeDetector_20170920
{
    class RunCannyEdgeDetector
    {
        private string SrcFolderPath = string.Empty;
        private string DstFolderPath = string.Empty;
                
        public RunCannyEdgeDetector(string srcFolderPath, string dstFolderPath)
        {
            SrcFolderPath = srcFolderPath;
            DstFolderPath = dstFolderPath;
        }

        public void Run()
        {
            if (!Directory.Exists(SrcFolderPath))
            {
                Console.WriteLine("Source folder doesn't exist.");
                return;
            }

            if (!Directory.Exists(DstFolderPath))
            {
                Directory.CreateDirectory(DstFolderPath);
            }

            // Process all the BMP files in the source folder.
            DirectoryInfo srcDir = new DirectoryInfo(SrcFolderPath);
            foreach(FileInfo srcFile in srcDir.EnumerateFiles("*.bmp"))
            {
                string originalPath = Path.Combine(DstFolderPath, "01_Original_" + srcFile.Name);
                srcFile.CopyTo(originalPath);
                string gaussianedPath = Path.Combine(DstFolderPath, "02_Gaussian_" + srcFile.Name);
                string flattenedPath = Path.Combine(DstFolderPath, "03_Flattened_" + srcFile.Name);
                string edgePath = Path.Combine(DstFolderPath, "04_Edge_" + srcFile.Name);
                ProcessSingleBmpFile(originalPath, gaussianedPath, flattenedPath, edgePath);
            }
        }

        private void ProcessSingleBmpFile(string originalPath, string gaussianedPath, string flattenedPath, string edgePath)
        {
            GaussianFilter gf = new GaussianFilter(originalPath, gaussianedPath);
            gf.RunImageProcess();

            Flatten f = new Flatten(gaussianedPath, flattenedPath);
            f.RunImageProcess();

            EdgeDetection ed = new EdgeDetection(flattenedPath, edgePath);
            ed.RunImageProcess();
        }
    }
}
