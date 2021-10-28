using UnityEngine;

namespace SbSTanks
{
    public class Shell
    {
        private GameObject _shellObject;
        private Vector3 _shellPositionInPool;

        public int damage;
        public bool isOnScene;

        public GameObject ShellObject { get => _shellObject; }
        public Vector3 ShellPositionInPool { get => _shellPositionInPool; }

        public Shell(GameObject shellObject)
        {
            _shellObject = shellObject;
            _shellPositionInPool = shellObject.transform.position; 
        }
    }
}