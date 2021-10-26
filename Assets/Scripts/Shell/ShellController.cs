using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ShellController
    {
        private List<Shell> _shells;
        private readonly LayerMask _shellMask = 6;

        private const string PREFAB_PATH = "Prefabs/Shell";

        public List<Shell> Shells { get => _shells; }

        public ShellController(IUnit player, IUnit enemy)
        {
            player.ShellHit += InflictDamage;
            enemy.ShellHit += InflictDamage;
        }

        public GameObject InitShell(int damage, Transform startPosition)
        {
            var shellObject = CreateShell(damage, startPosition);
            shellObject.layer = _shellMask;
            return shellObject;
        }

        private GameObject CreateShell(int damage, Transform startPosition)
        {
            var shellPrefab = Resources.Load(PREFAB_PATH) as GameObject;
            var shellObject = Object.Instantiate(shellPrefab, startPosition.position, startPosition.rotation);

            var shell = new Shell(damage, shellObject);
            _shells.Add(shell);

            return shellObject;
        }

        private void InflictDamage(GameObject shell, IDamagebleUnit unit)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (shell.GetInstanceID() == _shells[i].ShellObject.GetInstanceID())
                {
                    unit.TakingDamage(_shells[i].Damage);
                }
            }
        }

        public void Destroy(GameObject shell)
        {
            for (int i = 0; i < _shells.Count; i++)
            {
                if (shell.GetInstanceID() == _shells[i].ShellObject.GetInstanceID())
                {
                    _shells.Remove(_shells[i]);
                    GameObject.Destroy(shell);
                }
            }
        }
    }
}
