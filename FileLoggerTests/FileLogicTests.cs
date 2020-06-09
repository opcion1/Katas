using FileLogger.Tests.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileLogger.Tests
{
    public class FileLogicTests
    {
        private readonly MockFileWrapper _mockWrapper;

        public FileLogicTests()
        {
            _mockWrapper = new MockFileWrapper();
        }

        [Theory]
        [InlineData("2020/06/06", "weekend.txt")]
        [InlineData("2020/06/07", "weekend.txt")]
        [InlineData("2020/06/08", "log20200608.txt")]
        [InlineData("2020/06/09", "log20200609.txt")]
        public void GetLogPathName_ReturnWeekendExpectedFileName(string strDateTest, string expected)
        {
            //Arrange
            DateTime.TryParse(strDateTest, out DateTime dateTest);

            //Act
            string fileName = new FileLogic(new FileWrapper()).GetLogPathName(dateTest);

            //Assert
            Assert.Equal(expected, fileName);
        }

        [Fact]
        public void GetLogPathName_WhenWeekendDay_CheckAndSaveFormerWeekendFile()
        {
            //Arrange
            DateTime saturdayTest = new DateTime(2020, 6, 6);
            _mockWrapper
                .Setup_FileExists(true)
                .Setup_GetLastWriteTime(saturdayTest.AddDays(-6))
                .Setup_MoveFile();

            //Act
            string fileName = new FileLogic(_mockWrapper.Object).GetLogPathName(saturdayTest);

            //Assert
            Assert.Equal("weekend.txt", fileName);
            _mockWrapper.Verify_GetLastWriteTime(Times.Once);
            _mockWrapper.Verify_MoveFile(Times.Once);
            _mockWrapper.Verify_MoveFile_withNewFileName("weekend-20200530.txt", Times.Once);
        }
        [Fact]
        public void GetLogPathName_WhenWeekendDayAndFileDoesNotExists_DoNothing()
        {
            //Arrange
            DateTime saturdayTest = new DateTime(2020, 6, 6);
            _mockWrapper
                .Setup_FileExists(false)
                .Setup_GetLastWriteTime(saturdayTest.AddDays(-6))
                .Setup_MoveFile();

            //Act
            string fileName = new FileLogic(_mockWrapper.Object).GetLogPathName(saturdayTest);

            //Assert
            Assert.Equal("weekend.txt", fileName);
            _mockWrapper.Verify_GetLastWriteTime(Times.Never);
            _mockWrapper.Verify_MoveFile(Times.Never);
        }

    }
}
