using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileLogger.Tests
{
    public class FileLogicTests
    {
        [Theory]
        [InlineData("2020/06/06", "weekend.txt")]
        [InlineData("2020/06/07", "weekend.txt")]
        [InlineData("2020/06/08", "log20200608.txt")]
        [InlineData("2020/06/09", "log20200609.txt")]
        public void GetLogPathName_WhenWeekend_ReturnWeekend(string strDateTest, string expected)
        {
            //Arrange
            DateTime.TryParse(strDateTest, out DateTime dateTest);

            //Act
            string fileName = new FileLogic().GetLogPathName(dateTest);

            //Assert
            Assert.Equal(expected, fileName);
        }

    }
}
