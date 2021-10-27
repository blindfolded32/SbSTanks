using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ShellController: IDisposable
    {
        private List<Shell> _shells = new List<Shell>();
        private readonly LayerMask _shellMask = 6;
        private IUnit _player;
        private IUnit _enemy;

        private const string PREFAB_PATH = "Prefabs/Shell";
        private const int SHELLS_COUNT = 5;
        private const float OFFSET_MODIFER = 0.5f;

        public List<Shell> Shells { get => _shells; }

        public ShellController(IUnit player, IUnit enemy)
        {
            _player = player;
            _enemy = enemy;

            _player.ShellHit += InflictDamage;
            _enemy.ShellHit += InflictDamage;

            for (int i = 0; i < SHELLS_COUNT; i++)
            {
                CreateShell(i);
            }
        }

        public GameObject GetShell(int damage, Transform startPosition)
        {
            GameObject shellObject = null;

            for (int i = 0; i < _shells.Count; i++)
            {
                if (!_shells[i].isOnScene)
                {
                    shellObject = _shells[i].ShellObject;
                    _shells[i].damage = damage;
                    _shells[i].isOnScene = true;
                }
            }

            if (shellObject is null)
            {
                CreateShell(OFFSET_MODIFER);

                shellObject = _shells[_shells.Count - 1].ShellObject;
                _shells[_shells.Count - 1].damage = damage;
                _shells[_shells.Count - 1].isOnScene = true;
            }

            shellObject.layer = _shellMask;
            shellObject.transform.Translate(startPosition.position);
            shellObject.transform.rotation = startPosition.rotation;

            return shellObject;
        }

        private void CreateShell(float offset)
        {
            var shellPrefab = Resources.Load(PREFAB_PATH) as GameObject;
            var shellObject = UnityEngine.Object.Instantiate(shellPrefab, new Vector3(0 + offset,-20,0), new Quaternion());
            
            var shell = new Shell(shellObject);
            _shells.Add(shell);
        }

        private void InflictDamage(GameObject shell, IDamagebleUnit unit)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (shell.GetInstanceID() == _shells[i].ShellObject.GetInstanceID())
                {
                    unit.TakingDamage(_shells[i].damage);
                }
            }
        }

        public void ReturnShell(GameObject shell)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (shell.GetInstanceID() == _shells[i].ShellObject.GetInstanceID())
                {
                    shell.transform.Translate(_shells[i].ShellPositionInPool.position);
                    _shells[i].isOnScene = false;
                    _shells[i].damage = 0;
                }
            }
        }

        public void Dispose()
        {
            _player.ShellHit -= InflictDamage;
            _enemy.ShellHit -= InflictDamage;
        }
    }
}
