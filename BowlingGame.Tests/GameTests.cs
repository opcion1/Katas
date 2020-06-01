using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BowlingGame.Tests
{
    public class GameTests
    {
        [Fact]
        public void PerfectGame_Is300()
        {
            Game game = new Game();
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            int score = game.Score();

            Assert.Equal(300, score);
        }
        [Fact]
        public void Score_WhenSpare_CountNextDouble()
        {
            Game game = new Game();
            game.Roll(6);
            game.Roll(4);
            int score1 = game.Score();
            game.Roll(8);
            int score2 = game.Score();
            game.Roll(1);
            int score3 = game.Score();

            Assert.Equal(10, score1);
            Assert.Equal(26, score2);
            Assert.Equal(27, score3);
        }
        [Fact]
        public void Score_WhenStrike_TwoNeoutDouble()
        {
            Game game = new Game();
            game.Roll(6);
            game.Roll(3);
            int score1 = game.Score();
            game.Roll(10);
            int score2 = game.Score();
            game.Roll(4);
            int score3 = game.Score();
            game.Roll(4);
            int score4 = game.Score();

            Assert.Equal(9, score1);
            Assert.Equal(19, score2);
            Assert.Equal(27, score3);
            Assert.Equal(35, score4);
        }
        [Fact]
        public void Roll_ThrowArgumentException_WhenGameIsOver()
        {
            Game game = new Game();
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);
            game.Roll(4);
            game.Roll(3);

            Assert.Throws<ArgumentException>(() => game.Roll(4));
        }
    }
}
