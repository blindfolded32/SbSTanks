using System.Collections.Generic;
using Interfaces;
using Unit;
using UnityEngine;

namespace Controllers.Input
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
            if (!Physics.Raycast(ray, out var hitInfo) || !hitInfo.transform.GetComponent<Enemy.Enemy>()) return; //TODO REmove GET
            var activePlayer = _playerController.Find(x => x.State == NameManager.State.Attack);
            if (activePlayer == null) return;
            UnitRotation.RotateUnit( activePlayer,hitInfo.transform);
            TargetSelected(hitInfo.transform.GetComponent<Enemy.Enemy>());
        }
        private void TargetSelected(Enemy.Enemy transform)
        {
            foreach (var enemy in _enemyList)
            {
                enemy.targetSelected.enabled = false;
            }
            transform.targetSelected.enabled = true;
        }
        public void Execute(float deltaTime)
        {
            SelectingTarget();
        }
    }  
}

