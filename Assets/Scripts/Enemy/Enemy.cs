using Unit;
using UnityEngine;

namespace Enemy
{
    public class Enemy : AbstractUnit
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void ConfirmDeath()
        {
            if(!_particleSystem.isPlaying) _particleSystem.Play();
        }
        
        public void FlyCanI()
        {
            var position = transform.position;
            position = new Vector3(position.x,Mathf.PingPong(Time.time, 5f),position.z);
            transform.position = position;
        }

        public void StopSmoke()
        {
            if(_particleSystem.isPlaying) _particleSystem.Stop();
        }
        public void ReturnToGround()
        {
            var position = transform.position;
            position = new Vector3(position.x,0,position.z);
            transform.position = position;
        }
    }
    
}
