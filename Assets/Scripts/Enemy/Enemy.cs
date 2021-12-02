using Markers;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class Enemy : AbstractUnit
    {
        private ParticleSystem _particleSystem;
        private MeshRenderer _targetSelection;
        public MeshRenderer targetSelected;

        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            targetSelected = GetComponentInChildren<TargetSelectedPoint>().GetComponent<MeshRenderer>();
            _targetSelection = GetComponentInChildren<TargetSelectionPoint>().GetComponent<MeshRenderer>();
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
        public void ChangeTargetState(bool state)
        {
            targetSelected.gameObject.SetActive(state);
            _targetSelection.gameObject.SetActive(state);
        }
        
        private void OnMouseOver()
        {
            if (targetSelected.enabled) return;
            //_meshRenderer.enabled = true;
            _targetSelection.enabled = true;
        }

        private void OnMouseExit()
        {
            _targetSelection.enabled = false;
        }
        
    }
    
}
