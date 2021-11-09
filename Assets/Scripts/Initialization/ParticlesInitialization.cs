using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ParticlesInitialization
    {
        private GameObject _particleSystemGameObject;

        private const string PARTICLE_PATH = "Partcle/CompleteTankExplosion";
        public ParticlesInitialization(Player player, List<Enemy> enemies)
        {
            _particleSystemGameObject = Resources.Load<GameObject>(PARTICLE_PATH);
            List<IUnit> tanks = new List<IUnit>();
            tanks.Add(player);
            
            for (int i = 0; i < enemies.Count; i++)
            {
                tanks.Add(enemies[i]);
            }
            

            for(int i = 0; i < tanks.Count; i++)
            {
                var particleSystem = GameObject.Instantiate(_particleSystemGameObject);
                particleSystem.transform.position = tanks[i].GetShotPoint.transform.position;

                particleSystem.transform.SetParent(tanks[i].Transform);
                particleSystem.gameObject.AddComponent<ParticleSystemShotIdentificator>();
            }
        }
    }
}

