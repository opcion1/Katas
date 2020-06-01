using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace BowlingGame
{
    public class GameValidator
    {
        private const string PINS_LESSER_THAN_0 = "Pins must be greater than 0";
        private const string PINS_GREATER_THAN_10 = "Pins must be equal or less than 10";
        private const string FRAME_GREATER_THAN_10 = "A frame can't be greater than 10";

        public GameValidator()
        {
        }
        public bool IsThrowValid(int pins, int? firstAttempScore)
        {
            if (pins < 0)
                throw new ArgumentException(PINS_LESSER_THAN_0);
            if (pins > 10)
                throw new ArgumentException(PINS_GREATER_THAN_10);
            if (firstAttempScore.HasValue && pins + firstAttempScore.Value > 10)
                throw new ArgumentException(FRAME_GREATER_THAN_10);

            return true;
        }
    }
}
