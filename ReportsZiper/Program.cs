using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ReportsZiper
{



    class Program
    {
        
        //Disablin close button in console
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
    


        private static LoadXmlResult xmlResult;



        static void Main(string[] args)
        {
            Console.Title = "Pakowanie raportów. Nie zamykać okna. Testy można kontynuować.";

            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_CLOSE, MF_BYCOMMAND);   //disables close button

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            xmlResult = LoadFromXml.mLoadFromXML(); //do xmlResult zapisane są ściezki i częstotliwość z pliku konfiguracyjnego .xml
            foreach (string str in xmlResult.PathList)
            {
                try
                {
                    ZipFiles.ZipFilesInFolder(str);
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(Environment.NewLine + "ERROR. PATH NOT FOUND:  " + str);
                    Console.WriteLine("OPERATION STOPPED");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(Environment.NewLine + "ERROR:  " + e.ToString());
                    Console.WriteLine("OPERATION STOPPED");
                    break;
                   
                }

            }
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms" + Environment.NewLine + "Operation done. Press enter to exit");
            Console.ReadLine();
        }
    }
}
