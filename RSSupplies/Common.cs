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

            string path = @"log.txt";
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path)) { }

            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("This");
                sw.WriteLine("is Extra");
                sw.WriteLine("Text");
            }

        }
    }
}
