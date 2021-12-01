using System.Collections.Generic;
using Interfaces;
using Markers;
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

            if (!Physics.Raycast(ray, out var hitInfo) || !hitInfo.transform.GetComponent<Enemy.Enemy>()) return;
            var activePlayer = _playerController.Find(x => x.State == NameManager.State.Attack);
            if (activePlayer == null) return;
            UnitRotation.RotateUnit( activePlayer,hitInfo.transform);
            TargetSelected(hitInfo.transform);
        }

        private void TargetSelected(Component transform)
        {
            var hpBar = transform.GetComponentInChildren<UnitHealthBar>().foregroundImage.fillAmount;
            foreach (var enemy in _enemyList)
            {
                enemy.GetComponentInChildren<TargetSelectedPoint>().GetComponent<MeshRenderer>().enabled = false;
                
            }
            transform.GetComponentInChildren<TargetSelectionPoint>().GetComponent<MeshRenderer>().enabled = false;
            if(hpBar != 0)
                transform.GetComponentInChildren<TargetSelectedPoint>().GetComponent<MeshRenderer>().enabled = true;
        }
        
        public void Execute(float deltaTime)
        {
            SelectingTarget();
        }

        public IModel Model { get; set; }
    }  
}

