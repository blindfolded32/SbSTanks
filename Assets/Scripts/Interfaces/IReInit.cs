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
        public void ReInit(IEnumerable<Enemy.Enemy> enemies);
        public void NewRound(IEnumerable<Enemy.Enemy> enemies);

        public void Renew();
    }
}