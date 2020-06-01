using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class Game
    {
        Frame[] _emptyGame;
        private readonly GameValidator _gameValidator;
        private readonly GameAnalyzor _gameAnalyzor;
        private readonly GameScoreCalculator _gameScoreCalculator;
        private bool _isGameOver;
        private const string GAME_OVER = "The game is over";

        public Game()
        {
            _isGameOver = false;
            _emptyGame = InitializeEmptyGame();
            _gameValidator = new GameValidator();
            _gameAnalyzor = new GameAnalyzor(_emptyGame);
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
            if (_isGameOver)
                throw new ArgumentException(GAME_OVER);

            int? firstAttempScore = _gameAnalyzor.GetFirstAttempScore();
            if (_gameValidator.IsThrowValid(pins, firstAttempScore))
            {
                (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) = _gameAnalyzor.UpdateGame(pins);
                _gameScoreCalculator.UpdateScore(pins, doesNextCountDouble, doesTwoNextCountDouble);
                _isGameOver = isGameOver;
            }
        }
        public int Score()
        {
            return _gameScoreCalculator.Score;
        }
    }
}
