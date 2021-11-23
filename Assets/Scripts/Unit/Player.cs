using System;
using Controllers;
using IdentificationElements;
using Interfaces;
using Spawners;
using UnityEngine;

namespace Unit
{
   // [RequireComponent(typeof(Animator))]
    public class Player : AbstractUnit
    {
        public event Action PlayerDead;
        public bool GetHitStatus { get; set; } = false;

        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
            if (unitInitializationData.Hp.GetCurrentHp <= 0) PlayerDead?.Invoke();
        }
      
    }
}
