using Markers;
using UnityEngine;

namespace Controllers.Input
{
    public class TargetSelection : MonoBehaviour
    {
        private TargetSelectionPoint _enemySelectionCircle;
        private TargetSelectedPoint _enemySelectedCircle;
        private MeshRenderer _meshRenderer;
        
        void Start()
        {
            _enemySelectionCircle = GetComponentInChildren<TargetSelectionPoint>();
            _enemySelectedCircle = GetComponentInChildren<TargetSelectedPoint>();
            //_meshRenderer = _enemySelectionCircle.GetComponent<MeshRenderer>();
        }

        private void OnMouseOver()
        {
            if (_enemySelectedCircle.GetComponent<MeshRenderer>().enabled) return;
            //_meshRenderer.enabled = true;
            _enemySelectionCircle.GetComponent<MeshRenderer>().enabled = true;
        }

        private void OnMouseExit()
        {
            _enemySelectionCircle.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

