using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IReInit
    {
        public event Action StartAgain;
        public event Action GameOver;
        public event Action<int> NewRoundStart;
        public bool Lost { get; }
        public void StartNewTurn();
        public void NewRound();
        public void Renew();
    }
}