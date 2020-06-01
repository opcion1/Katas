using System;
using Xunit;

namespace BowlingGame.Tests
{
    public class GameValidatorTests
    {
        private GameValidator _gameValidator;

        [Fact]
        public void IsGameValid_WhenPinsNegative_ThrowInvalidArgument()
        {
            //Arrange
            _gameValidator = new GameValidator();
            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.IsThrowValid(-1, null));
        }

        [Fact]
        public void ValidateGame_WhenPinsGreaterThan10_ThrowInvalidArgument()
        {
            //Arrange
            _gameValidator = new GameValidator();
            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.IsThrowValid(11, null));
        }

        [Fact]
        public void ValidateGame_WhenFramePinsGreaterThan10_ThrowInvalidArgument()
        {
            //Arrange
            _gameValidator = new GameValidator();

            //Assert
            Assert.Throws<ArgumentException>(() => _gameValidator.IsThrowValid(6, 5));
        }

    }
}
