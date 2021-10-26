using UnityEngine;

namespace SbSTanks
{
    public class MainInitializator
    {
        public MainInitializator(GameInitializationData data, GameController mainController)
        {
            data.Enemy.Init(data.EnemyInitializationData);
            data.Player.Init(data.PlayerInitializationData);
        }
    }
}