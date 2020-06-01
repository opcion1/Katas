using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingGame.Tests
{
    public class GameAnalyzorTests
    {
        private GameAnalyzor _gameAnalyzor;
        private Frame[] _emptyFrames = new Frame[10];

        public GameAnalyzorTests()
        {
            for (int i = 0; i < 9; i++)
            {
                _emptyFrames[i] = new Frame();
            }
            _emptyFrames[9] = new Frame { IsLastOne = true };
        }

        [Fact]
        public void ValidateGame_WhenLastFrameIsStrike_GameIsNotOver()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            frames[9] = new Frame { IsLastOne = true };
            _gameAnalyzor = new GameAnalyzor(frames);

            //Act
            (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) = _gameAnalyzor.UpdateGame(10);

            //Assert
            Assert.False(isGameOver);
            Assert.False(doesTwoNextCountDouble);
            Assert.False(doesNextCountDouble);
        }

        private Frame GetValidDoneFrame()
        {
            return new Frame { FirstAttemp = 6, SecondAttemp = 2, IsDone = true, IsLastOne = false };
        }

        [Fact]
        public void ValidateGame_WhenLastFrmaeIsSpare_GameIsNotOver()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            frames[9] = new Frame { IsLastOne = true, FirstAttemp = 3 };
            _gameAnalyzor = new GameAnalyzor(frames);

            //Act
            (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) = _gameAnalyzor.UpdateGame(7);

            //Assert
            Assert.False(isGameOver);
            Assert.False(doesTwoNextCountDouble);
            Assert.False(doesNextCountDouble);
        }

        private Frame GetSpareFrame(bool isDone, bool isLastFrame)
        {
            return new Frame { FirstAttemp = 6, SecondAttemp = 4, IsDone = isDone, IsLastOne = isLastFrame };
        }

        [Fact]
        public void ValidateGame_GameIsOver_WhenLastIsClassic()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            frames[9] = new Frame { IsLastOne = true, FirstAttemp = 3 };
            _gameAnalyzor = new GameAnalyzor(frames);

            //Act
            (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) = _gameAnalyzor.UpdateGame(5);

            //Assert
            Assert.True(isGameOver);
            Assert.False(doesTwoNextCountDouble);
            Assert.False(doesNextCountDouble);
        }

        private Frame GetStrikeFrame(bool isDone, bool isLastFrame)
        {
            return new Frame { FirstAttemp = 10, IsDone = isDone, IsLastOne = isLastFrame };
        }

        [Fact]
        public void GameAnalyzor_NextTwoCountDouble_WhenFrmaeIsStrike()
        {
            //Arrange
            _gameAnalyzor = new GameAnalyzor(_emptyFrames);

            //Act
            (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) = _gameAnalyzor.UpdateGame(10);

            //Assert
            Assert.False(isGameOver);
            Assert.True(doesTwoNextCountDouble);
            Assert.False(doesNextCountDouble);
        }

        [Fact]
        public void GameAnalyzor_ReturnNextCountDouble_WhenFrameIsSpare()
        {
            //Arrange
            _gameAnalyzor = new GameAnalyzor(_emptyFrames);

            //Act
            _gameAnalyzor.UpdateGame(2);
            (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) = _gameAnalyzor.UpdateGame(8);

            //Assert
            Assert.False(isGameOver);
            Assert.False(doesTwoNextCountDouble);
            Assert.True(doesNextCountDouble);
        }
    }
}
