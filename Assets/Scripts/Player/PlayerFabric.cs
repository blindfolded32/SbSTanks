using Markers;
using Unit;
using UnityEngine;

namespace Player
{
    public class PlayerFabric
    {
        //public Player Player { get; private set; }

        public Player Create(Transform transform, UnitModel parameters)
        {
            var Player = Object.Instantiate(Resources.Load<Player>("Prefabs/Player"), transform);
            Player.Controller = new PlayerController(new PlayerModel(parameters), Player);
            Player.Element = parameters.Element;
            Player.ShotPoint = Player.GetComponentInChildren<ShotPoint>().transform;
            return Player;
        }
    }
}