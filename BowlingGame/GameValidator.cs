using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace BowlingGame
{
    public class GameValidator
    {
        public const string PINS_LESSER_THAN_0 = "Pins must be greater than 0";
        public const string PINS_GREATER_THAN_10 = "Pins must be equal or less than 10";
        public const string FRAME_GREATER_THAN_10 = "A frame can't be greater than 10";
        public const string GAME_OVER = "The game is over";
        private Frame[] _frames;

        public GameValidator(Frame[] frames)
        {
            _frames = frames;
        }
        public (bool isValid, bool isStrike, bool isSpare) ValidateGame(int pins)
        {
            if (IsValidePin(pins))
            {
                return ValidateAndAnalyseFrame(pins);
            }
            else
                return (isValid: false, isStrike: false, isSpare: false);
        }

        private bool IsValidePin(int pins)
        {
            if (pins < 0)
                throw new ArgumentException(PINS_LESSER_THAN_0);
            if (pins > 10)
                throw new ArgumentException(PINS_GREATER_THAN_10);

            return true;
        }

        private (bool isValid, bool isStrike, bool isSpare) ValidateAndAnalyseFrame(int pins)
        {
            Frame currentFrame = _frames.FirstOrDefault(frame => !frame.IsDone);
            if (currentFrame is null)
                throw new ArgumentException(GAME_OVER);
            if (currentFrame.IsLastOne)
                return (isValid:IsLastOneValid(currentFrame, pins), false,false);

            bool isStrike = false;
            bool isSpare = false;
            if (currentFrame.FirstAttemp.HasValue)
            {
                if (currentFrame.FirstAttemp + pins > 10 )
                    throw new ArgumentException(FRAME_GREATER_THAN_10);
                currentFrame.SecondAttemp = pins;
                currentFrame.IsDone = true;
                isSpare = currentFrame.FirstAttemp + currentFrame.SecondAttemp == 10;
            }
            else 
            {
                currentFrame.FirstAttemp = pins;
                currentFrame.IsDone = pins == 10;
                isStrike = currentFrame.FirstAttemp == 10;
            }
            return (isValid:true, isStrike: isStrike, isSpare: isSpare);
        }

        private bool IsLastOneValid(Frame currentFrame, int pins)
        {
            if (!currentFrame.FirstAttemp.HasValue)
            {
                currentFrame.FirstAttemp = pins;
            }
            else if (!currentFrame.SecondAttemp.HasValue)
            {
                if (currentFrame.FirstAttemp < 10 && currentFrame.FirstAttemp + pins > 10)
                    throw new ArgumentException(FRAME_GREATER_THAN_10);
                currentFrame.SecondAttemp = pins;
                currentFrame.IsDone = currentFrame.FirstAttemp + currentFrame.SecondAttemp < 10;
            }
            else
            {
                currentFrame.IsDone = true;
            }
            return true;
        }
    }
}
