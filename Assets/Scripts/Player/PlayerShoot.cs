using Bullet;
using Controllers;
using UnityEngine;

namespace Player
{
    public class PlayerShoot
    {
        private readonly BulletPool _bulletPool;
       
        private static readonly float SHOT_FORCE = 250.0f;
        public static void Shot(PlayerController playerController, int shotElement)
        {
            if (playerController.GetView.IsDead) return;;
            
            var shell = playerController.GetView.BulletPool.GetItem("Bullet");
            shell.Damage = playerController.GetView.Controller.Model.Damage;
            shell.Element = shotElement;
            shell.Transform.position = playerController.GetView.ShotPoint.position;
            shell.transform.rotation = playerController.GetView.ShotPoint.rotation;
            shell.gameObject.SetActive(true);
            var shellRb = shell.GetComponent<Rigidbody>();
            shellRb.AddForce(shell.transform.forward * SHOT_FORCE, ForceMode.Impulse);
            
            playerController.IsPlayerTurn = false;
        }
    }
}