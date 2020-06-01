using System;
using System.Linq;

namespace BowlingGame
{
    public class GameAnalyzor
    {
        private Frame[] _frames;
        public GameAnalyzor(Frame[] frames)
        {
            _frames = frames;
        }

        public (bool isGameOver, bool doesTwoNextCountDouble, bool doesNextCountDouble) UpdateGame(int pins)
        {
            Frame currentFrame = _frames.FirstOrDefault(frame => !frame.IsDone);

            if (currentFrame.IsLastOne)
                return (isGameOver: IsGameOver(currentFrame, pins), false, false);

            bool doesTwoNextCountDouble = false;
            bool doesNextCountDouble = false;
            if (currentFrame.FirstAttemp.HasValue)
            {
                currentFrame.SecondAttemp = pins;
                currentFrame.IsDone = true;
                doesNextCountDouble = currentFrame.FirstAttemp + currentFrame.SecondAttemp == 10;
            }
            else
            {
                currentFrame.FirstAttemp = pins;
                currentFrame.IsDone = pins == 10;
                doesTwoNextCountDouble = currentFrame.FirstAttemp == 10;
            }
            return (isGameOver: false, doesTwoNextCountDouble: doesTwoNextCountDouble, doesNextCountDouble: doesNextCountDouble);
        }

        private bool IsGameOver(Frame currentFrame, int pins)
        {
            if (!currentFrame.FirstAttemp.HasValue)
            {
                currentFrame.FirstAttemp = pins;
                return false;
            }
            else if (!currentFrame.SecondAttemp.HasValue)
            {
                currentFrame.SecondAttemp = pins;
                currentFrame.IsDone = currentFrame.FirstAttemp + currentFrame.SecondAttemp < 10;
            }
            else
            {
                currentFrame.IsDone = true;
            }
            return currentFrame.IsDone; ;
        }

        internal int? GetFirstAttempScore()
        {
            Frame currentFrame = _frames.FirstOrDefault(frame => !frame.IsDone);
            if ((currentFrame.FirstAttemp ?? 11) < 10)
                return currentFrame.FirstAttemp;
            return null;
        }
    }
}
