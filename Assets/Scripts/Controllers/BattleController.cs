using System.Collections.Generic;

namespace SbSTanks
{
    public class BattleController
    {
        private PlayerController _playerController;
        private List<Enemy> _enemies;
        public BattleController(PlayerController playerController, List<Enemy> enemies)
        {
            _playerController = playerController;
            _enemies = enemies;
        }

        public void NewRound(int round)
        {
        }
    }
}