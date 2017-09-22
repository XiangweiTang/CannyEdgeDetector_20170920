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
        /// <summary>
        /// Time is limited, use a fixed Gaussian filter.
        /// </summary>
        public static int[,] MaskMatrix = new int[,]
        {
            { 2,4,5,4,2 },
            { 4,9,12,9,4 },
            { 5,12,15,12,5 },
            { 4,9,12,9,4 },
            { 2,4,5,4,2 }
        };

        /// <summary>
        /// Read a bmp file, get the bytes.
        /// </summary>
        /// <param name="bmpPath">The bmp file path</param>
        /// <returns>The byte array of the bmp file.</returns>
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

        /// <summary>
        /// Write a bmp file to certain path.
        /// </summary>
        /// <param name="bmpContent">The bytes need to write into bmp file.</param>
        /// <param name="bmpPath">The output bmp path.</param>
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

        /// <summary>
        /// Check if an index is valid.
        /// </summary>
        /// <param name="i">The index we need to check.</param>
        /// <param name="max">The max value(length) of the array.</param>
        /// <returns></returns>
        public static bool IndexValidation(int i, int max)
        {
            return 0 <= i && i < max;
        }
    }
}
