using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Interfaces;
using Unit;
using UnityEngine;

namespace Shell
{
    public class ShellController: IDisposable, IFixedExecute, IController
    {
        private List<Shell> _shells = new List<Shell>(8); //TODO - take out the model
        private readonly LayerMask _shellMask = 6;
        private PlayerController _player;
        private List<Enemy> _enemies;
        private LayerMask _groundMask;
        private const string PREFAB_PATH = "Prefabs/Shell";
        private const float NEW_SHELL_OFFSET = 0.5f;
        private const float X_ROTATE_IN_FLY = 0.7f;
        
      /*  public ObjectPool<GameObject> shellobjectpool = new ObjectPool<GameObject>(CreateFunc);

        private static GameObject CreateFunc()
        {
            var shellPrefab = Resources.Load(PREFAB_PATH) as GameObject;
            var shellObject = UnityEngine.Object.Instantiate(shellPrefab);
            shellObject.SetActive(false);
            
            return shellObject;
        }*/
        public ShellController(PlayerController player, List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
            _groundMask = 1<<8;
           // _player.ShellHit += InflictDamage;
            CreateShell(0);
            /*foreach (var enemy in _enemies)
            {
                enemy.ShellHit += InflictDamage;
               // CreateShell(enemy.Parameters.ElementId);  
            }*/
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
        public GameObject GetShell(float damage, Transform startPosition, int elementId)
        {
            GameObject shellObject = null;
            foreach (var shell in _shells.Where(shell => !shell.IsActive))
            {
                shellObject = shell.ShellObject;
                shell.damage = damage;
                shell.Activate(true);
                shell.ElementId = elementId;
                break;
            }
            if (shellObject is null)
            {
                CreateShell(NEW_SHELL_OFFSET);
                shellObject = _shells[_shells.Count - 1].ShellObject;
                _shells[_shells.Count - 1].damage = damage;
            }
            shellObject.layer = _shellMask;
            shellObject.transform.position = startPosition.position;
            shellObject.transform.rotation = startPosition.rotation;
            return shellObject;
        }
        private void CreateShell(float offset)
        {
            var shellPrefab = Resources.Load(PREFAB_PATH) as GameObject;
            var shellObject = UnityEngine.Object.Instantiate(shellPrefab);
            shellObject.SetActive(false);
            var shell = new Shell(shellObject);
            _shells.Add(shell);
        }
        private void InflictDamage(GameObject shell, IDamagebleUnit unit)
        {
            foreach (var shellitem in _shells)
            {
                if (shell.GetInstanceID() == shellitem.ShellObject.GetInstanceID())
                {
                    // Debug.Log($"Shell with element {shellitem.ElementId} to unit element {unit.GetUnitElement}");
                    unit.TakingDamage(shellitem.damage, shellitem.ElementId);
                    break;
                }
            }
          
        }
        public void ReturnShell(GameObject shell)
        {
            foreach (var shellitem in _shells)
            {
                if (shell.GetInstanceID() == shellitem.ShellObject.GetInstanceID())
                {
                    shell.GetComponent<Rigidbody>().Sleep();
                    shell.SetActive(false);
                    break;
                }
            }
        }
        public void Dispose()
        {
            //_player.ShellHit -= InflictDamage;
           // foreach (var shell in _enemies) shell.ShellHit -= InflictDamage;
        }
    }
}
