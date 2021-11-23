using System;
using Unit;
using UnityEngine;

namespace Player
{
   // [RequireComponent(typeof(Animator))]
    public class Player : AbstractUnit
    {
        public event Action PlayerDead;
        public bool GetHitStatus { get; set; } = false;

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
        }
      
    }
}
