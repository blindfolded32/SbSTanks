using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ShellController
    {
        private List<Shell> _shells;

        private const string PREFAB_PATH = "Prefabs/Shell";

        public List<Shell> Shells { get => _shells; }

        public GameObject InitShell(int damage, Transform startPosition)
        {
            var shellObject = CreateShell(damage, startPosition);
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
