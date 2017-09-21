using System;

namespace CannyEdgeDetector_20170920
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintUsage();
                return;
            }

            string srcFolderPath = args[0];
            string dstFolderPath = args[1];

            RunCannyEdgeDetector r = new RunCannyEdgeDetector(srcFolderPath, dstFolderPath);
            r.Run();
        }       

        static void PrintUsage()
        {
            Console.WriteLine(">CannyEdgeDetector.exe <InputFolderPath> <OutputFolderPath>");
            Console.WriteLine("\tInputFolderPath: the folder contains the BMP file before process.");
            Console.WriteLine("\tOutputFolderPath: the folder contains the BMP file after process.");
        }
    }
}
