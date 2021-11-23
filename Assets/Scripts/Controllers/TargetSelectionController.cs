using System.Collections.Generic;
using Interfaces;
using Pointers;
using Unit;
using UnityEngine;
using Component = UnityEngine.Component;

namespace Controllers
{
    public class TargetSelectionController : IExecute
    {
        private readonly Camera _camera;
        private readonly PlayerController _playerController;
        private readonly List<Enemy> _enemyList;
        
        public TargetSelectionController(Camera camera, PlayerController playerController, List<Enemy> enemyList)
        {
            _camera = camera;
            _playerController = playerController;
            _enemyList = enemyList;
        }

        private void SelectingTarget()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo) && hitInfo.transform.GetComponent<Enemy>())
            {
                _playerController.RotatePlayer(hitInfo.transform);
                TargetSelected(hitInfo.transform);
            }
        }

        private void TargetSelected(Component transform)
        {
            foreach (var enemy in _enemyList)
            {
                enemy.GetComponentInChildren<TargetSelectedPoint>().GetComponent<MeshRenderer>().enabled = false;
            }
            transform.GetComponentInChildren<TargetSelectionPoint>().GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponentInChildren<TargetSelectedPoint>().GetComponent<MeshRenderer>().enabled = true;
        }
        
        public void Execute(float deltaTime)
        {
            SelectingTarget();
        }
    }  
}

