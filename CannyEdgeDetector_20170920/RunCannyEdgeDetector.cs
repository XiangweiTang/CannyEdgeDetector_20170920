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
        private Config Cfg = new Config();
                
        public RunCannyEdgeDetector(Config cfg)
        {
            Cfg = cfg;
        }

        public void Run()
        {
            if (!Directory.Exists(Cfg.SrcFolderPath))
            {
                Console.WriteLine("Source folder doesn't exist.");
                return;
            }

            if (!Directory.Exists(Cfg.DstFolderPath))
            {
                Directory.CreateDirectory(Cfg.DstFolderPath);
            }

            // Process all the BMP files in the source folder.
            DirectoryInfo srcDir = new DirectoryInfo(Cfg.SrcFolderPath);
            foreach(FileInfo srcFile in srcDir.EnumerateFiles("*.bmp"))
            {
                Console.WriteLine("Processing " + srcFile.Name);
                string nameCore = srcFile.Name.Replace(srcFile.Extension, string.Empty);
                string originalPath = Path.Combine(Cfg.DstFolderPath, nameCore + "_01_Original.bmp");
                srcFile.CopyTo(originalPath,true);
                string gaussianedPath = Path.Combine(Cfg.DstFolderPath, nameCore + "_02_Gaussian.bmp");
                string flattenedPath = Path.Combine(Cfg.DstFolderPath, nameCore + "_03_Flattened.bmp");
                string edgePath = Path.Combine(Cfg.DstFolderPath, nameCore + "_04_Edge.bmp");
                ProcessSingleBmpFile(originalPath, gaussianedPath, flattenedPath, edgePath);
            }
        }

        private void ProcessSingleBmpFile(string originalPath, string gaussianedPath, string flattenedPath, string edgePath)
        {
            GaussianFilter gf = new GaussianFilter(originalPath, gaussianedPath, Cfg);
            gf.RunImageProcess();

            //Flatten f = new Flatten(gaussianedPath, flattenedPath, Cfg);
            //f.RunImageProcess();

            EdgeDetection ed = new EdgeDetection(gaussianedPath, edgePath, Cfg);
            ed.RunImageProcess();
        }
    }
}
