using UnityEngine;

namespace SbSTanks
{
    public class Shell
    {
        private GameObject _shellObject;
        private Vector3 _shellPositionInPool;
        private float _shellHeight;

        private int _elementId;

        public int damage;
        
        public bool isOnScene;

        public GameObject ShellObject { get => _shellObject; }
        public Vector3 ShellPositionInPool { get => _shellPositionInPool; }
        public float ShellHeight { get => _shellHeight; }

        public int ElementId { get  => _elementId; }

        public Shell(GameObject shellObject, int elementId)
        {
            _shellObject = shellObject;
            _shellPositionInPool = shellObject.transform.position;
            _shellHeight = shellObject.GetComponent<CapsuleCollider>().height;
            _elementId = elementId;
        }
    }
}