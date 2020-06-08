using FileLogger;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileLoggerTests
{
    public class FileLoggerTest
    {
        [Fact]
        public void Log_AddMessageOK()
        {
            FileLogger.FileLogger.Log("Test");
        }
    }
}
