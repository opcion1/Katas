using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger.Tests.Mock
{
    public class MockFile : Mock<IFileWrapper>
    {
        public MockFile Setup_FileExists(bool doesExists)
        {
            Setup(f => f.FileExists(It.IsAny<string>()))
                .Returns(doesExists)
                .Verifiable();
                
            return this;
        }
        public MockFile Setup_CreateFile()
        {
            Setup(f => f.CreateFile(It.IsAny<string>()))
                .Verifiable();
            return this;
        }

        public MockFile Verify_FileExits(Func<Times> times)
        {
            Verify(x => x.FileExists(It.IsAny<string>()), times);

            return this;
        }

        public MockFile Verify_CreateFile(Func<Times> times)
        {
            Verify(x => x.CreateFile(It.IsAny<string>()), times);

            return this;
        }
    }
}
