using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger
{
    public interface IFileWrapper
    {
        bool FileExists(string path);
        void CreateFile(string path);
    }

    public class FileWrapper : IFileWrapper
    {
        public void CreateFile(string path)
        {
            File.Create(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
