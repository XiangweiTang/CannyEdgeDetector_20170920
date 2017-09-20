using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CannyEdgeDetector_20170920
{
    static class Common
    {
        public static int[,] MaskMatrix = new int[,]
        {
            { 2,4,5,4,2 },
            { 4,9,12,9,4 },
            { 5,12,15,12,5 },
            { 4,9,12,9,4 },
            { 2,4,5,4,2 }
        };

        public static byte[] ReadBmp(string bmpPath)
        {
            using(FileStream fs=new FileStream(bmpPath,FileMode.Open,FileAccess.Read))
            {
                int count = Convert.ToInt32(fs.Length);
                using(BinaryReader br=new BinaryReader(fs))
                {
                    return br.ReadBytes(count);
                }
            }
        }

        public static void WriteBmp(IEnumerable<byte> bmpContent, string bmpPath)
        {
            using(FileStream fs=new FileStream(bmpPath, FileMode.Create, FileAccess.ReadWrite))
            {
                using(BinaryWriter bw=new BinaryWriter(fs))
                {
                    bw.Write(bmpContent.ToArray());
                }
            }
        }
    }
}
