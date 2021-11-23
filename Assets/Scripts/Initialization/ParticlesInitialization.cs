using System.Collections.Generic;
using IdentificationElements;
using Player;
using Unit;
using UnityEngine;

namespace Initialization
{
    public class ParticlesInitialization
    {
        private GameObject _particleSystemGameObject;

        private const string PARTICLE_PATH = "Partcle/CompleteTankExplosion";
        public ParticlesInitialization(IPlayerController player, List<Enemy.Enemy> enemies)
        {
            _particleSystemGameObject = Resources.Load<GameObject>(PARTICLE_PATH);
            List<AbstractUnit> tanks = new List<AbstractUnit>();
            tanks.Add(player.GetView);
            
            for (int i = 0; i < enemies.Count; i++)
            {
                tanks.Add(enemies[i]);
            }
            for(int i = 0; i < tanks.Count; i++)
            {
                var particleSystem = Object.Instantiate(_particleSystemGameObject);
                particleSystem.transform.position = tanks[i].ShotPoint.position;

                particleSystem.transform.SetParent(tanks[i].ShotPoint.transform);
                particleSystem.gameObject.AddComponent<ParticleSystemShotIdentificator>();
            }
        }
    }
}

