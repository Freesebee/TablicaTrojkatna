using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace TablicaTrojkatna
{
    class Program
    {
        static void Main(string[] args)
        {

            string nameIn = "TablicaStanowDoMinimalizacji.xlsx",
                nameOut = "ZminimalizowanaTablicaStanow.xlsx";
            FileStream streamIn = null;
            FileStream streamOut = null;
            try
            {
                streamIn = new FileStream(AppDomain.CurrentDomain.BaseDirectory + nameIn, FileMode.Open, FileAccess.Read);

                XSSFWorkbook document = new XSSFWorkbook(streamIn);

                streamIn.Close();

                MinimalizationCreator triangle = new MinimalizationCreator(document);

                streamOut = new FileStream(AppDomain.CurrentDomain.BaseDirectory + nameOut,
                FileMode.Append, FileAccess.Write);

                document.Write(streamOut);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                streamIn.Close();
                streamOut.Close();
                Console.WriteLine("Nacisnij dowolny przycisk aby kontynuowac");
                Console.ReadKey();
            }
        }
    }
}
