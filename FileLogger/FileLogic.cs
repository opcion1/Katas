using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace FileLogger
{
    public interface IFileLogic
    {
        string GetLogPathName(DateTime date);
    }
    public class FileLogic : IFileLogic
    {
        private IFileWrapper _fileWrapper { get; set; }

        private const string _weekendLogFile = "weekend.txt";
        private const string _formerWeekendLogFile = "weekend-{0}.txt";

        public FileLogic(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public string GetLogPathName(DateTime date)
        {
            List<DayOfWeek> dayOfWeekend = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };
            if (dayOfWeekend.Contains(date.DayOfWeek))
            {
                if (_fileWrapper.FileExists(_weekendLogFile))
                {
                    SaveWeekendFileIfNeeded(date);
                }
                return _weekendLogFile;
            }
            else
                return $"log{date.ToString("yyyyMMdd")}.txt";
        }

        private void SaveWeekendFileIfNeeded(DateTime date)
        {
            DateTime lastWriteTime = _fileWrapper.GetLastWriteTime(_weekendLogFile);
            if ((date - lastWriteTime).TotalDays > 2)
            {
                string newFileName =
                    (lastWriteTime.DayOfWeek == DayOfWeek.Saturday)
                    ? String.Format(_formerWeekendLogFile, lastWriteTime.ToString("yyyyMMdd"))
                    : String.Format(_formerWeekendLogFile, lastWriteTime.AddDays(-1).ToString("yyyyMMdd"));
                _fileWrapper.MoveFile(_weekendLogFile, newFileName);
            }
        }

        private string SaveWeekendFile(DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        private DateTime GetPreviousSaturday(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return date.AddDays(-7);
            else
                return date.AddDays(-8);
        }
    }
}
