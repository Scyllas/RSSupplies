using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSupplies
{
    class Common
    {

        internal static void DeleteExistingFile(string filePath)
        {
            File.Delete(filePath);
        }


        internal static void Log(string message)
        {
            if (!File.Exists("Log.txt"))
            {
                File.Create("Log.txt");
            }

            File.AppendText(message);
        }
    }
}
