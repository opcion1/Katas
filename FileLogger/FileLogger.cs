using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger
{
    public class FileLogger
    {
        private IFileWrapper _fileWrapper { get; set; }
        private IFileLogic _fileLogic { get; set; }
        public FileLogger(IFileWrapper fileWrapper,
                            IFileLogic fileLogic)
        {
            _fileWrapper = fileWrapper;
            _fileLogic = fileLogic;
        }

        public void Log(string message)
        {
            string logPath = _fileLogic.GetLogPathName(DateTime.Today);
            if (!_fileWrapper.FileExists(logPath))
            {
                _fileWrapper.CreateFile(logPath);
            }
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine(message);
            }
        }
    }
}
