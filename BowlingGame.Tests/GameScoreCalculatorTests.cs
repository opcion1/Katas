using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingGame.Tests
{
    public class GameScoreCalculatorTests
    {
        private readonly GameScoreCalculator _gameScoreCalculator;

        public GameScoreCalculatorTests()
        {
            _gameScoreCalculator = new GameScoreCalculator();
        }

        [Fact]
        public void UpdateScore_AfterSpare_CountDouble()
        {
            //Act
            _gameScoreCalculator
                .UpdateScore(score: 3, isSpare: false, isStrike: false)
                .UpdateScore(7, isSpare: true, isStrike: false)
                .UpdateScore(5, isSpare: false, isStrike: false);
            //Assert
            Assert.Equal(20, _gameScoreCalculator.Score);
        }
        [Fact]
        public void UpdateScore_AfterStrike_CountTwiceDouble()
        {
            //Act
            int scoreOne = _gameScoreCalculator
                            .UpdateScore(10, isSpare: false, isStrike: true)
                            .UpdateScore(7, isSpare: false, isStrike: false)
                            .Score;
            int scoreTwo = _gameScoreCalculator
                            .UpdateScore(2, isSpare: false, isStrike: false)
                            .Score;

            Assert.Equal(24, scoreOne);
            Assert.Equal(28, scoreTwo);
        }
        [Fact]
        public void UpdateScore_WhenStandardFrame_SumOK()
        {
            //Act
            int scoreOne = _gameScoreCalculator
                            .UpdateScore(2, isSpare: false, isStrike: false)
                            .UpdateScore(7, isSpare: false, isStrike: false)
                            .Score;

            //Assert
            Assert.Equal(9, scoreOne);
        }

    }
}
