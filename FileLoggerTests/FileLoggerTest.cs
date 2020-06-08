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
        private readonly MockFileWrapper _mockWrapper;
        private readonly MockFileLogic _mockLogic;

        public FileLoggerTest()
        {
            _mockWrapper = new MockFileWrapper();
            _mockLogic = new MockFileLogic();
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
            _mockWrapper
                .Setup_FileExists(false)
                .Setup_CreateFile();
            _mockLogic
                .Setup_GetLogPathName("log.txt");
            FileLogger.FileLogger logger = new FileLogger.FileLogger(_mockWrapper.Object, _mockLogic.Object);

            //Act
            logger.Log("Test");

            //Assert
            _mockWrapper.Verify_FileExits(Times.Once);
            _mockWrapper.Verify_CreateFile(Times.Once);
        }

        [Fact]
        public void Log_WhenExists_DoNotCreateFile()
        {
            //Arrange
            _mockWrapper
                .Setup_FileExists(true)
                .Setup_CreateFile();
            _mockLogic
                .Setup_GetLogPathName("log.txt");
            FileLogger.FileLogger logger = new FileLogger.FileLogger(_mockWrapper.Object, _mockLogic.Object);

            //Act
            logger.Log("Test");

            //Assert
            _mockWrapper.Verify_FileExits(Times.Once);
            _mockWrapper.Verify_CreateFile(Times.Never);
        }

    }
}
