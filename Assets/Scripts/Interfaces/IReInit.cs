using System;

namespace Interfaces
{
    public interface IReInit
    {
        public event Action StartAgain;
        public int RoundNumber { get; }
        public bool Lost { get; }
        public void StartNewTurn();
        public void NewRound(float RoundModifier = default);
        public void Renew();
        public void NewTry();
    }
}