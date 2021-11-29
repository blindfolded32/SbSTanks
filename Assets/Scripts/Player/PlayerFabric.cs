using Markers;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerFabric
    {
        //public Player Player { get; private set; }

        public static Player Create(Transform transform, UnitModel parameters)
        {
            var player = Object.Instantiate(Resources.Load<Player>("Prefabs/Player"), transform);
            player.Controller = new PlayerController(new UnitModel(parameters.HP,parameters.Damage,parameters.Element), player);
            player.Element = parameters.Element;
            player.ShotPoint = player.GetComponentInChildren<ShotPoint>().transform;
            return player;
        }
    }
}