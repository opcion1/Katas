using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class Frame
    {
        public Frame()
        {
            IsLastOne = false;
            IsDone = false;
        }
        public int? FirstAttemp { get; set; }
        public int? SecondAttemp { get; set; }
        public bool IsDone { get; set; }
        public bool IsLastOne { get; set; }
    }
}
