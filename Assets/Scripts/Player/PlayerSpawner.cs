using System;
using System.Collections.Generic;
using Controllers.Model;
using Interfaces;
using Markers;
using SaveLoad;
using Unit;
using Random=UnityEngine.Random;

namespace Player
{
    public class PlayerSpawner
    {
        internal readonly List<Player> Players = new List<Player>();
        internal readonly List<IUnitController> PlayerControllers = new List<IUnitController>();
        public PlayerSpawner(IEnumerable<PlayerSpawnPoint> points)
        {
            //var fabric = new EnemyFabric();
            foreach (var point in points)
            {
                var player = PlayerFabric.Create(point.transform,
                    new UnitModel(new Health(10, 10), 1,(NameManager.ElementList) Random.Range(0,2)));
                Players.Add(player);
                PlayerControllers.Add(player.Controller);
            }
        }

        public void LoadPlayers(Saver save)
        {
            for (var i = 0; i <Players.Count; i++)
            {
                Players[i].Controller.SetParams(save.PlayerModel[i]);
            }
        }
    }
}