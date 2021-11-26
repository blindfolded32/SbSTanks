﻿using Bullet;
using Interfaces;
using UnityEngine;
using static Markers.NameManager;

namespace Unit
{
    public static class UnitShoot
    {
       
        public static void Shot(IUnitController controller, Transform shotTransform, float damage, ElementList shotElement)
        {
            if (controller.State == State.Dead)
            {
                controller.State = State.Fired;
                return;
            }
            var shell = ServiceLocator.Resolve<BulletPool>().GetItem("Bullet");
            shell.AddDamage(damage);
            shell.AddElement(shotElement);
            shell.Transform.position = shotTransform.position;
            shell.transform.rotation = shotTransform.rotation;
            shell.gameObject.SetActive(true);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            controller.State = State.Fired;
        }
    }
}