using Bullet;
using Controllers;
using Controllers.Model;
using Markers;
using Shell;
using Unit;
using Unit.Model;
using UnityEngine;

namespace Spawners
{
    public class PlayerFabric
    {
        public Player Player { get; private set; }

        public Player Create(Transform transform, UnitInitializationData parameters, TimerController timerController)
        {
            Player = Object.Instantiate(Resources.Load<Player>("Prefabs/Player"), transform);
            Player.PlayerController = new PlayerController(new PlayerModel(timerController,parameters), 
                                                        Object.FindObjectOfType<Player>());
            Player.Element = parameters.Element;
            Player.ShotPoint = Player.GetComponentInChildren<ShotPoint>().transform;
            Player.BulletPool = ServiceLocator.Resolve<BulletPool>();
            return Player;
        }
    }
}