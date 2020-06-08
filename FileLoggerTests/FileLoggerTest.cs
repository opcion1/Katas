using FileLogger;
using FileLogger.Tests.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace FileLoggerTests
{
    public class FileLoggerTest
    {
        private readonly MockFile _mockFile;

        public FileLoggerTest()
        {
            _mockFile = new MockFile();
            CreateTestFile();
        }

        private static void CreateTestFile()
        {
            if (!File.Exists("log.txt"))
            {
                File.Create("log.txt");
            }
        }

        [Fact]
        public void Log_WhenNotExists_CreateFile()
        {
            //Arrange
            _mockFile
                .Setup_FileExists(false)
                .Setup_CreateFile();
            FileLogger.FileLogger logger = new FileLogger.FileLogger(_mockFile.Object);

            //Act
            logger.Log("Test");

            //Assert
            _mockFile.Verify_FileExits(Times.Once);
            _mockFile.Verify_CreateFile(Times.Once);
        }

        [Fact]
        public void Log_WhenExists_DoNotCreateFile()
        {
            //Arrange
            _mockFile
                .Setup_FileExists(true)
                .Setup_CreateFile();
            FileLogger.FileLogger logger = new FileLogger.FileLogger(_mockFile.Object);

            //Act
            logger.Log("Test");

            //Assert
            _mockFile.Verify_FileExits(Times.Once);
            _mockFile.Verify_CreateFile(Times.Never);
        }
    }
}
