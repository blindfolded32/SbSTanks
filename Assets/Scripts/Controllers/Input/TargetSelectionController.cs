using System.Collections.Generic;
using Interfaces;
using Player;
using Pointers;
using UnityEngine;

namespace Controllers
{
    public class TargetSelectionController : IExecute
    {
        private readonly Camera _camera;
        private readonly List<IUnitController> _playerController;
        private readonly List<Enemy.Enemy> _enemyList;
        
        public TargetSelectionController(Camera camera, List<IUnitController> playerController, List<Enemy.Enemy> enemyList)
        {
            _camera = camera;
            _playerController = playerController;
            _enemyList = enemyList;
        }

        private void SelectingTarget()
        {
            if (!UnityEngine.Input.GetMouseButtonDown(0)) return;
            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo) && hitInfo.transform.GetComponent<Enemy.Enemy>())
            {
               PlayerRotation.RotatePlayer( _playerController.Find(x=>x.GetState==NameManager.State.Attack),hitInfo.transform);
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

        public IModel Model { get; set; }
    }  
}

