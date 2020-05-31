using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class Game
    {
        Frame[] _emptyGame;
        private readonly GameValidator _gameValidator;
        private readonly GameScoreCalculator _gameScoreCalculator;

        public Game()
        {
            _emptyGame = InitializeEmptyGame();
            _gameValidator = new GameValidator(_emptyGame);
            _gameScoreCalculator = new GameScoreCalculator();
        }

        private Frame[] InitializeEmptyGame()
        {
            Frame[] frames = new Frame[10];
            for (int i = 0; i < 9; i++)
            {
                frames[i] = new Frame();
            }
            frames[9] = new Frame { IsLastOne = true };
            return frames;
        }

        public void Roll(int pins)
        {
            (bool isValid, bool isStrike, bool isSpare) validAndAnalyse = _gameValidator.ValidateGame(pins);
            if (validAndAnalyse.isValid)
            {
                _gameScoreCalculator.UpdateScore(pins, validAndAnalyse.isSpare, validAndAnalyse.isStrike);
            }
        }
        public int Score()
        {
            return _gameScoreCalculator.Score;
        }
    }
}
