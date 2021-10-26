using UnityEngine;

namespace SbSTanks
{
    public class Enemy : Unit
    {
        protected override void OnCollisionEnter(Collision collision)
        {
            shellHit?.Invoke(collision.gameObject);
        }
    }
}
