using Bullet;
using Interfaces;
using UnityEngine;

namespace Unit
{
    public static class UnitShoot
    {
        private static readonly float SHOT_FORCE = 250.0f;
        public static void Shot(IUnitController controller, Transform shotTransform, float damage, int shotElement)
        {
            if (controller.IsDead) return;
            var shell = ServiceLocator.Resolve<BulletPool>().GetItem("Bullet");
            shell.AddDamage(damage);
            shell.AddElement(shotElement);
            shell.Transform.position = shotTransform.position;
            shell.transform.rotation = shotTransform.rotation;
            shell.gameObject.SetActive(true);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            controller.IsFired = true;
        }
    }
}