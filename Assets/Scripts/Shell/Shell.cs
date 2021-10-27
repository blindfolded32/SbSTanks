using UnityEngine;

namespace SbSTanks
{
    public class Shell
    {
        private GameObject _shellObject;
        private Transform _shellPositionInPool;

        public int damage;
        public bool isOnScene;

        public GameObject ShellObject { get => _shellObject; }
        public Transform ShellPositionInPool { get => _shellPositionInPool; }

        public Shell(GameObject shellObject)
        {
            _shellObject = shellObject;
            _shellPositionInPool = shellObject.transform;
        }
    }
}