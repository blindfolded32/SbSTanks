using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class Player : Unit
    {
        protected override void OnCollisionEnter(Collision collision)
        {
            ShellHit?.Invoke(collision.gameObject, this);
            _shellController.ReturnShell(collision.gameObject);
            //
        }
    }
}
