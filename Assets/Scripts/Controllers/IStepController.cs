using System;

namespace Controllers
{
    public interface IStepController
    {
        int TurnNumber { get; set; }
        bool IsPlayerTurn { get; }
        event Action<int> NewTurn;
        event Action<int> NewRound;
        
        void AddTimer();
    }
}