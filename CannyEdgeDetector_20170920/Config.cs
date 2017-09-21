using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CannyEdgeDetector_20170920
{
    class Config
    {
        // The lower threshold is, the more accurate.
        public double Threshold { get; private set; } = 1.5;
        
        public string SrcFolderPath { get; private set; } = string.Empty;
        public string DstFolderPath { get; private set; } = string.Empty;

        public bool LoadConfig(string configPath)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(configPath);

                SrcFolderPath = xDoc["Root"]["SrcFolderPath"].InnerText;
                DstFolderPath = xDoc["Root"]["DstFolderPath"].InnerText;
                Threshold = double.Parse(xDoc["Root"]["Threshold"].InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Config error.");
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
}
