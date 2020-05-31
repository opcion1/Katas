﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class GameScoreCalculator
    {
        private int _score { get; set; }
        private int _currentScoreWeight { get; set; }
        private int _nextScoreWeight { get; set; }

        public int Score { get { return _score; } }
        public GameScoreCalculator()
        {
            _score = 0;
            _currentScoreWeight = 1;
            _nextScoreWeight = 1;
        }
        public GameScoreCalculator UpdateScore(int score, bool isSpare, bool isStrike)
        {
            _score += score * _currentScoreWeight;
            if (isStrike)
            {
                _currentScoreWeight = _nextScoreWeight+1;
                _nextScoreWeight = 2;
            }
            else if (isSpare)
            {
                _currentScoreWeight = _nextScoreWeight+1;
                _nextScoreWeight = 1;
            }
            else
            {
                _currentScoreWeight = _nextScoreWeight;
                _nextScoreWeight = 1;
            }
            return this;
        }
    }
}
