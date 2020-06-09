using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLogger.Tests.Mock
{
    public class MockFileLogic : Mock<IFileLogic>
    {
        public MockFileLogic Setup_GetLogPathName(string returnPath)
        {
            Setup(fl => fl.GetLogPathName(It.IsAny<DateTime>()))
                .Returns(returnPath)
                .Verifiable();
            return this;
        }
        public MockFileLogic Verify_GetLogPathName(Func<Times> times)
        {
            Verify(fl => fl.GetLogPathName(It.IsAny<DateTime>()), times);

            return this;
        }
    }
}
