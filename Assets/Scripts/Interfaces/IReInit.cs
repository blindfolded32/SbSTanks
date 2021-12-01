using System;

namespace Interfaces
{
    public interface IReInit
    {
        public event Action StartAgain;
        public event Action<int> GameOver;
        public int RoundNumber { get; }
        public bool Lost { get; }
        public void StartNewTurn();
        public void NewRound();
        public void Renew();
        public void NewTry();
    }
}