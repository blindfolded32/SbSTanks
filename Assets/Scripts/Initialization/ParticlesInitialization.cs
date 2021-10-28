using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ParticlesInitialization
    {
        private GameObject _particleSystemGameObject;

        private const string PARTICLE_PATH = "Partcle/CompleteTankExplosion";
        public ParticlesInitialization()
        {
            _particleSystemGameObject = Resources.Load<GameObject>(PARTICLE_PATH);
            List<GameObject> tanks = new List<GameObject>();
            tanks.Add(GameObject.FindObjectOfType<Player>().gameObject);
            tanks.Add(GameObject.FindObjectOfType<Enemy>().gameObject);

            for(int i = 0; i < tanks.Count; i++)
            {
                var particleSystem = GameObject.Instantiate(_particleSystemGameObject);
                if(tanks[i].TryGetComponent<Player>(out var player))
                {
                    particleSystem.transform.position = player.GetShotPoint.transform.position;
                }
                if (tanks[i].TryGetComponent<Enemy>(out var enemy))
                {
                    particleSystem.transform.position = enemy.GetShotPoint.transform.position;
                }
                particleSystem.transform.SetParent(tanks[i].transform);
                particleSystem.gameObject.AddComponent<ParticleSystemShotIdentificator>();
            }
        }
    }
}

