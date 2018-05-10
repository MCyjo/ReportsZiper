using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportsZiper
{
    public static class ZipFiles
    {
        public static void ZipFilesInFolder(string path)
        {
            string folderName = ReturnFolderName(path);

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                Console.Write("Compressing:  " + path);
                zip.AddDirectory(path, "");
                string path1 = path + "\\" + folderName + ".zip";
                zip.Save(path1);
                Console.Write("   DONE" + Environment.NewLine);
            }
             DeleteFiles(path);
        }

        private static string ReturnFolderName(string path)
        {
            string folderName = path.Split('\\')[(path.Split('\\').Count())-1];
            return folderName;
        }

        private static void DeleteFiles(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                if(file.Extension != ".zip")
                {
                    file.Delete();
                }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
