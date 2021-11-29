using System.Collections.Generic;
using System.Linq;
using IdentificationElements;
using Unit;
using UnityEngine;
using static NameManager;

namespace Initialization
{
    public class ParticlesInitialization
    {
        private GameObject _particleSystemGameObject;

       
        public ParticlesInitialization(List<Player.Player> player, List<Enemy.Enemy> enemies)
        {
            _particleSystemGameObject = Resources.Load<GameObject>(PARTICLE_PATH);
            List<AbstractUnit> tanks = new List<AbstractUnit>();
            tanks.AddRange(player.Cast<AbstractUnit>());
            tanks.AddRange(enemies.Cast<AbstractUnit>());

            /* for (int i = 0; i < enemies.Count; i++)
            {
                tanks.Add(enemies[i]);
            }*/
            foreach (var tank in tanks)
            {
                var particleSystem = Object.Instantiate(_particleSystemGameObject);
                particleSystem.transform.position = tank.ShotPoint.position;

                particleSystem.transform.SetParent(tank.ShotPoint.transform);
                particleSystem.gameObject.AddComponent<ParticleSystemShotIdentificator>();
            }
        }
    }
}

