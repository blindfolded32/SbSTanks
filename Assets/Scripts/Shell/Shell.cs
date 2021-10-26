using UnityEngine;

namespace SbSTanks
{
    public class Shell
    {
        private int _damage;
        private GameObject _shellObject;

        public int Damage { get => _damage; }
        public GameObject ShellObject { get => _shellObject; }

        public Shell(int damage, GameObject shellObject)
        {
            _damage = damage;
            _shellObject = shellObject;
        }
    }
}