using System;
using System.Collections.Generic;
using System.Text;

namespace FileLogger
{
    public interface IFileLogic
    {
        string GetLogPathName(DateTime date);
    }
    public class FileLogic : IFileLogic
    {
        public string GetLogPathName(DateTime date)
        {
            List<DayOfWeek> dayOfWeekend = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };
            if (dayOfWeekend.Contains(date.DayOfWeek))
            {
                return "weekend.txt";
            }
            else
                return $"log{date.ToString("yyyyMMdd")}.txt";
        }
    }
}
