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

            string bufor = "MinimalizacjaStanow.xlsx";
            FileStream streamIn = null;
            FileStream streamOut = null;

                streamIn = new FileStream(AppDomain.CurrentDomain.BaseDirectory + bufor, FileMode.Open, FileAccess.Read);

                XSSFWorkbook document = new XSSFWorkbook(streamIn);

                streamIn.Close();

                MinimalizationCreator triangle = new MinimalizationCreator(document);

                streamOut = new FileStream(AppDomain.CurrentDomain.BaseDirectory + bufor,
                FileMode.Append, FileAccess.Write);

                document.Write(streamOut);

                streamIn.Close();
                streamOut.Close();

        }
    }
}
