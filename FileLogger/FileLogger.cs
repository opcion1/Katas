using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger
{
    public static class FileLogger
    {
        public static void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(message);
                sw.Close();
            }
        }
    }
}
