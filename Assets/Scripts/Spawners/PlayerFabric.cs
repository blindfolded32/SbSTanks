using Controllers.Model;
using Unit;
using Unit.Model;
using UnityEngine;

namespace Spawners
{
    public class PlayerFabric
    {
        public Player Create(Transform transform, UnitInitializationData parameters)
        {
            var player = Object.Instantiate(Resources.Load<Player>("Prefabs/Player"), transform);
            player.unitInitializationData = parameters;
            return player;
        }
    }
}