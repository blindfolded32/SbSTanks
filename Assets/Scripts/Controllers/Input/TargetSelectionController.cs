using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Markers;
using Player;
using Pointers;
using UnityEngine;
using Component = UnityEngine.Component;

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
            if (!Input.GetMouseButtonDown(0)) return;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo) && hitInfo.transform.GetComponent<Enemy.Enemy>())
            {
               PlayerRotation.RotatePlayer( _playerController.Find(x=>x.GetState!=NameManager.State.Fired),hitInfo.transform);
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

