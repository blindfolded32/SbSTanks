using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SbSTanks
{
    public class ShellController: IDisposable, IFixedExecute, IController
    {
        private List<Shell> _shells = new List<Shell>(8); //TODO - take out the model
        private readonly LayerMask _shellMask = 6;
        private IUnit _player;
        private List<Enemy> _enemies;
        private LayerMask _groundMask;
        private const string PREFAB_PATH = "Prefabs/Shell";
        private const float NEW_SHELL_OFFSET = 0.5f;
        private const float X_ROTATE_IN_FLY = 0.7f;
    //    public List<Shell> Shells { get => _shells; }
        public ShellController(IUnit player, List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
            _groundMask = 1<<8;
            _player.ShellHit += InflictDamage;
            CreateShell(0);
            foreach (var enemy in _enemies)
            {
                enemy.ShellHit += InflictDamage;
                CreateShell(enemy.ElementId);  //Забрать из параметров!!!
            }
        }

        public void FixedExecute(float deltaTime, float fixedDeltaTime)
        {
            foreach (var shell in _shells.Where(shell => shell.isOnScene))
            {
                shell.ShellObject.transform.Rotate(X_ROTATE_IN_FLY, 0, 0);
                var isGrounded = Physics.CheckSphere(shell.ShellObject.transform.position, shell.ShellHeight, _groundMask);
                if (isGrounded)
                {
                    ReturnShell(shell.ShellObject);
                }
            }
        }
        public GameObject GetShell(int damage, Transform startPosition, int elementId)
        {
            GameObject shellObject = null;
            foreach (var shell in _shells.Where(shell => !shell.isOnScene))
            {
                shellObject = shell.ShellObject;
                shell.damage = damage;
                shell.isOnScene = true;
                shell.ElementId = elementId;
                break;
            }
            if (shellObject is null)
            {
                CreateShell(NEW_SHELL_OFFSET);
                shellObject = _shells[_shells.Count - 1].ShellObject;
                _shells[_shells.Count - 1].damage = damage;
                _shells[_shells.Count - 1].isOnScene = true;
            }
            shellObject.layer = _shellMask;
            shellObject.transform.position = startPosition.position;
            shellObject.transform.rotation = startPosition.rotation;
            return shellObject;
        }
        private void CreateShell(float offset)
        {
            var shellPrefab = Resources.Load(PREFAB_PATH) as GameObject;
            var shellObject = UnityEngine.Object.Instantiate(shellPrefab, new Vector3(0 + offset,-20.5f,0), new Quaternion());
            var shell = new Shell(shellObject);
            _shells.Add(shell);
        }
        private void InflictDamage(GameObject shell, IDamagebleUnit unit)
        {
           // Debug.Log($"Shell owner has type {unit.GetType()} and element is {unit.GetUnitElement}");
         if (unit.GetType() != typeof(Player) && unit.GetUnitElement == 1)
         {
             foreach (var enemy in _enemies)
             {
                 Debug.Log($"Damage from element {_shells[0].ElementId}");
                 enemy.TakingDamage(_shells[0].damage, _shells[0].ElementId);
             }
         }
         else
         {
             foreach (var shellitem in _shells)
             {
                 if (shell.GetInstanceID() == shellitem.ShellObject.GetInstanceID())
                 {
                     Debug.Log($"Shell with element {shellitem.ElementId} to unit element {unit.GetUnitElement}");
                     unit.TakingDamage(shellitem.damage, shellitem.ElementId);
                     break;
                 }
             }
         }
        }
        public void ReturnShell(GameObject shell)
        {
            foreach (var shellitem in _shells)
            {
                if (shell.GetInstanceID() == shellitem.ShellObject.GetInstanceID())
                {
                    shell.transform.position = shellitem.ShellPositionInPool;
                    shell.GetComponent<Rigidbody>().Sleep();
                    shellitem.isOnScene = false;
                    shellitem.damage = 0;
                    break;
                }
            }
        }
        public void Dispose()
        {
            _player.ShellHit -= InflictDamage;
            foreach (var shell in _enemies) shell.ShellHit -= InflictDamage;
        }
    }
}
