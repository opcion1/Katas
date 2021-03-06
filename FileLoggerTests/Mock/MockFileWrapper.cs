﻿using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLogger.Tests.Mock
{
    public class MockFileWrapper : Mock<IFileWrapper>
    {
        public MockFileWrapper Setup_FileExists(bool doesExists)
        {
            Setup(f => f.FileExists(It.IsAny<string>()))
                .Returns(doesExists)
                .Verifiable();
                
            return this;
        }
        public MockFileWrapper Setup_CreateFile()
        {
            Setup(f => f.CreateFile(It.IsAny<string>()))
                .Verifiable();

            return this;
        }
        public MockFileWrapper Setup_GetLastWriteTime(DateTime testDate)
        {
            Setup(f => f.GetLastWriteTime(It.IsAny<string>()))
                .Returns(testDate)
                .Verifiable();

            return this;
        }
        public MockFileWrapper Setup_MoveFile()
        {
            Setup(f => f.MoveFile(It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            return this;
        }

        public MockFileWrapper Verify_FileExits(Func<Times> times)
        {
            Verify(x => x.FileExists(It.IsAny<string>()), times);

            return this;
        }

        public MockFileWrapper Verify_CreateFile(Func<Times> times)
        {
            Verify(x => x.CreateFile(It.IsAny<string>()), times);

            return this;
        }

        public MockFileWrapper Verify_GetLastWriteTime(Func<Times> times)
        {
            Verify(x => x.GetLastWriteTime(It.IsAny<string>()), times);

            return this;
        }

        public MockFileWrapper Verify_MoveFile_withNewFileName(string newFileNameExpected, Func<Times> times)
        {
            Verify(x => x.MoveFile(It.IsAny<string>(), newFileNameExpected), times);

            return this;
        }

        public MockFileWrapper Verify_MoveFile(Func<Times> times)
        {
            Verify(x => x.MoveFile(It.IsAny<string>(),It.IsAny<string>()), times);

            return this;
        }
    }
}
