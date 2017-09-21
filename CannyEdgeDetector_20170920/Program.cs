using System;
using System.IO;

namespace CannyEdgeDetector_20170920
{
    class Program
    {
        static void Main(string[] args)
        {
            string configPath = "config.xml";
            if (args.Length > 0)
                configPath = args[0];

            if (!File.Exists(configPath))
            {
                PrintUsage();
                return;
            }

            CannyEdgeDetector(configPath);
        }

        static void CannyEdgeDetector(string configPath)
        {
            Config cfg = new Config();
            if (!cfg.LoadConfig(configPath))
                return;
            RunCannyEdgeDetector r = new RunCannyEdgeDetector(cfg);
            r.Run();
        }

        static void PrintUsage()
        {
            Console.WriteLine(">CannyEdgeDetector.exe [ConfigPath]");
            Console.WriteLine("\tConfigPath is the path of config.");
            Console.WriteLine("\tThe config path can be left empty if config.xml is at the same directory.");
        }
    }
}
