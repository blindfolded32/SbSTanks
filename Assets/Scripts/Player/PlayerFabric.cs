using Bullet;
using Controllers;
using Markers;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerFabric
    {
        public Player Player { get; private set; }

        public Player Create(Transform transform, UnitInitializationData parameters, TimerController timerController)
        {
            Player = Object.Instantiate(Resources.Load<Player>("Prefabs/Player"), transform);
            Player.Controller = new PlayerController(new PlayerModel(timerController,parameters), 
                                                        Object.FindObjectOfType<Player>());
            Player.Element = parameters.Element;
            Player.ShotPoint = Player.GetComponentInChildren<ShotPoint>().transform;
            Player.BulletPool = ServiceLocator.Resolve<BulletPool>();
            return Player;
        }
    }
}