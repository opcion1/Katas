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
        DateTime GetLastWriteTime(string path);
        void MoveFile(string oldPath, string newPath);
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

        public DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        public void MoveFile(string oldPath, string newPath)
        {
            File.Move(oldPath, newPath);
        }
    }
}
