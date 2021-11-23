using Bullet;
using Controllers;
using Shell;
using UnityEngine;

namespace Unit
{
    public class PlayerShoot
    {
        private readonly BulletPool _bulletPool;
       
        private readonly float SHOT_FORCE = 250.0f;
        public void Shot(PlayerController playerController, int shotElement)
        {
            var shell = playerController.GetView.BulletPool.GetItem("Bullet");
            shell.Damage = playerController.GetView.unitInitializationData.Damage;
            shell.Element = shotElement;
            shell.Transform.position = playerController.GetView.shotPoint.position;
            shell.transform.rotation = playerController.GetView.shotPoint.rotation;
            shell.gameObject.SetActive(true);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            //GetHitStatus = true;
            playerController.IsPlayerTurn = false;
        }
    }
}