using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger
{
    public class FileLogger
    {
        private IFileWrapper _fileWrapper { get; set; }
        public FileLogger(IFileWrapper fileWrapper)
        {
            this._fileWrapper = fileWrapper;
        }

        public void Log(string message)
        {
            if (!_fileWrapper.FileExists("log.txt"))
            {
                _fileWrapper.CreateFile("log.txt");
            }
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                sw.WriteLine(message);
            }
        }
    }
}
