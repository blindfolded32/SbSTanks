using System;
using System.Collections;
using System.Collections.Generic;
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

        private int _shellsCount;

        private const string PREFAB_PATH = "Prefabs/Shell";
       // private const int SHELLS_COUNT = 5;
        private const float NEW_SHELL_OFFSET = 0.5f;
        private const float X_ROTATE_IN_FLY = 0.7f;

        public List<Shell> Shells { get => _shells; }

        public ShellController(IUnit player, List<Enemy> enemies)
        {
            _player = player;
            _enemies = enemies;
            _groundMask = 1<<8;
            _shellsCount = _enemies.Count ;
/*
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].ShellHit += InflictDamage;
            }
*/
            _player.ShellHit += InflictDamage;
            CreateShell(0,_player.ElementId);

            foreach (var enemy in _enemies)
            {
                enemy.ShellHit += InflictDamage;
                CreateShell(enemy.ElementId, enemy.ElementId);
            }
  /*          for (int i = 1; i < _enemies.Count; i++)
            {
                CreateShell(i,_enemies[i].ElementId);
            }
    */
        }

        public void FixedExecute(float deltaTime, float fixedDeltaTime)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (_shells[i].isOnScene)
                {
                    _shells[i].ShellObject.transform.Rotate(X_ROTATE_IN_FLY, 0, 0);

                    var isGrounded = Physics.CheckSphere(_shells[i].ShellObject.transform.position, _shells[i].ShellHeight, _groundMask);

                    if (isGrounded)
                    {
                        ReturnShell(_shells[i].ShellObject);
                    }
                }
            }
        }

        public GameObject GetShell(int damage, Transform startPosition,int elementId)
        {
            //Debug.Log($"Shell id is {elementId}");
            GameObject shellObject = null;

            for (int i = 0; i < _shells.Count; i++)
            {
                if (!_shells[i].isOnScene)
                {
                    shellObject = _shells[i].ShellObject;
                    _shells[i].damage = damage;
                    _shells[i].isOnScene = true;
                    break;
                }
            }

            if (shellObject is null)
            {
                CreateShell(NEW_SHELL_OFFSET,elementId);

                shellObject = _shells[_shells.Count - 1].ShellObject;
                _shells[_shells.Count - 1].damage = damage;
                _shells[_shells.Count - 1].isOnScene = true;
            }

            shellObject.layer = _shellMask;
            shellObject.transform.position = startPosition.position;
            shellObject.transform.rotation = startPosition.rotation;

            return shellObject;
        }

        private void CreateShell(float offset, int elementId)
        {
            var shellPrefab = Resources.Load(PREFAB_PATH) as GameObject;
            var shellObject = UnityEngine.Object.Instantiate(shellPrefab, new Vector3(0 + offset,-20.5f,0), new Quaternion());
            var shell = new Shell(shellObject,elementId);
            _shells.Add(shell);
        }

        private void InflictDamage(GameObject shell, IDamagebleUnit unit, int elementId)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (shell.GetInstanceID() == _shells[i].ShellObject.GetInstanceID())
                {
                  //  Debug.Log($"Player element is {_player.ElementId} and ememies {_enemies[i].ElementId}");
                   unit.TakingDamage(_shells[i].damage, elementId);
                    break;
                }
            }
        }

        public void ReturnShell(GameObject shell)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (shell.GetInstanceID() == _shells[i].ShellObject.GetInstanceID())
                {
                    shell.transform.position = _shells[i].ShellPositionInPool;
                    shell.GetComponent<Rigidbody>().Sleep();
                    _shells[i].isOnScene = false;
                    _shells[i].damage = 0;
                    break;
                }
            }
        }

        public void Dispose()
        {
            _player.ShellHit -= InflictDamage;

            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].ShellHit -= InflictDamage;
            }         
        }
    }
}
