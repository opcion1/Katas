using System;
using Xunit;

namespace BowlingGame.Tests
{
    public class GameValidatorTests
    {
        private GameValidator _gameValidator;
        private Frame[] _emptyFrames = new Frame[10]; 

        public GameValidatorTests()
        {
            for (int i = 0; i < 9; i++)
            {
                _emptyFrames[i] = new Frame();
            }
            _emptyFrames[9] = new Frame { IsLastOne = true };
        }

        [Fact]
        public void ValidateGame_WhenPinsNegative_ThrowInvalidArgument()
        {
            //Arrange
            _gameValidator = new GameValidator(_emptyFrames);
            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.ValidateGame(-1));
        }

        [Fact]
        public void ValidateGame_WhenPinsGreaterThan10_ThrowInvalidArgument()
        {
            //Arrange
            _gameValidator = new GameValidator(_emptyFrames);
            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.ValidateGame(11));
        }

        [Fact]
        public void ValidateGame_WhenFramePinsGreaterThan10_ThrowInvalidArgument()
        {
            //Arrange
            _gameValidator = new GameValidator(_emptyFrames);
            //Act
            bool isFirstAttempValid = _gameValidator.ValidateGame(6).isValid;

            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.ValidateGame(5));
            Assert.True(isFirstAttempValid);
        }

        [Fact]
        public void ValidateGame_WhenMoreThanFrame_ThrowInvalidArgument()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 10; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            _gameValidator = new GameValidator(frames);


            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.ValidateGame(5));
        }

        private Frame GetValidDoneFrame()
        {
            return new Frame { FirstAttemp = 6, SecondAttemp = 2, IsDone = true, IsLastOne = false };
        }

        [Fact]
        public void ValidateGame_WhenLastIsSpare_CanThrowOneMoreBeforeArgumentException()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            frames[9] = GetSpareFrame(isDone: false, isLastFrame: true);
            _gameValidator = new GameValidator(frames);

            //Act
            bool isOneMoreValid = _gameValidator.ValidateGame(3).isValid;

            //Assert
            Assert.True(isOneMoreValid);
            Assert.Throws<ArgumentException>(() => _gameValidator.ValidateGame(1));
        }

        private Frame GetSpareFrame(bool isDone, bool isLastFrame)
        {
            return new Frame { FirstAttemp = 6, SecondAttemp = 4, IsDone = isDone, IsLastOne = isLastFrame };
        }

        [Fact]
        public void ValidateGame_WhenLastIsStrike_CanThrowTwoMoreBeforeArgumentException()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            frames[9] = GetStrikeFrame(isDone: false, isLastFrame: true);
            _gameValidator = new GameValidator(frames);

            //Act
            bool isLastOneAfterStrikeValid = _gameValidator.ValidateGame(4).isValid;
            bool isLastTwoAfterStrikeValid = _gameValidator.ValidateGame(4).isValid;

            //Assert
            Assert.True(isLastOneAfterStrikeValid);
            Assert.True(isLastTwoAfterStrikeValid);
            Assert.Throws<ArgumentException>(() => _gameValidator.ValidateGame(1));
        }

        private Frame GetStrikeFrame(bool isDone, bool isLastFrame)
        {
            return new Frame { FirstAttemp = 10, IsDone = isDone, IsLastOne = isLastFrame };
        }

        [Fact]
        public void GameValidator_DoesNotReturnStrike_WhenBonusThrows()
        {
            //Arrange
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = GetValidDoneFrame();
            }
            frames[9] = GetStrikeFrame(isDone: false, isLastFrame: true);
            _gameValidator = new GameValidator(frames);

            //Act
            bool isStrike = _gameValidator.ValidateGame(10).isStrike;

            Assert.False(isStrike);
        }

        [Fact]
        public void GameValidator_ReturnIsStrike_WhenFrameStrike()
        {
            //Arrange
            _gameValidator = new GameValidator(_emptyFrames);

            //Act
            (bool isValid, bool isStrike, bool isSpare) validateStrike = _gameValidator.ValidateGame(10);

            //Assert
            Assert.True(validateStrike.isValid);
            Assert.True(validateStrike.isStrike);
            Assert.False(validateStrike.isSpare);
        }
        [Fact]
        public void GameValidator_ReturnIsSpare_WhenFrameSpare()
        {
            //Arrange
            _gameValidator = new GameValidator(_emptyFrames);

            //Act
            bool isFirstValid = _gameValidator.ValidateGame(6).isValid;
            (bool isValid, bool isStrike, bool isSpare) validateSpare = _gameValidator.ValidateGame(4);

            //Assert
            Assert.True(isFirstValid);
            Assert.True(validateSpare.isValid);
            Assert.True(validateSpare.isSpare);
            Assert.False(validateSpare.isStrike);
        }

    }
}
