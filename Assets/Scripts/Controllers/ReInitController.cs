using System;
using System.Collections.Generic;
using Player;
using Unit;
using static UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Controllers
{
    public class ReInitController
    {
        public event Action StartAgain;
        public event Action GameOver;
        private int _triesCount = 3;
        public bool Lost = false;
        
        private PlayerController _playerController;
        private readonly List<Enemy.Enemy> _enemies;
        private List<Enemy.Enemy> _defParams;
        public ReInitController(PlayerController playerController,List<Enemy.Enemy> enemies)
        {
            _playerController = playerController;
            _enemies = enemies;
            _defParams = _enemies;
            _playerController.GetView.PlayerDead += NewTry;
        }
        public static void ReInit(IEnumerable<Enemy.Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.Controller.IsFired = false;
                enemy.Element = (Random.Range(0, 2));
            }
        }
        public void NewRound(List<Enemy.Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.IsDead = false;
                enemy.Controller.Model.Damage *= 1.1f;
                enemy.Controller.Model.HP.InjectNewHp( enemy.Controller.Model.HP.Max * 1.1f);
                enemy.GetComponentInChildren<EnemyHealthBar>()._foregroundImage.fillAmount =1;
            }
            _playerController.Model.Element = (Random.Range(0, 2));
        }
        public void NewTry()
        {
            if (_triesCount == 0)
            {
                GameOver?.Invoke();
                return;
            }
            StartAgain?.Invoke();
            _triesCount--;
            Lost = true;
        }
        private void RestartGame()
        {
            var currentEnemy = FindObjectsOfType<Enemy.Enemy>();
            for (int i = 0; i < currentEnemy.Length; i++)
            {
                currentEnemy[i].IsDead = false;
               // currentEnemy[i].UnitInitializationData.Damage = _defParams[i].UnitInitializationData.Damage;
               // currentEnemy[i].UnitInitializationData.Hp.InjectNewHp(_defParams[i].UnitInitializationData.Hp.Max);
                currentEnemy[i].GetComponentInChildren<EnemyHealthBar>()._foregroundImage.fillAmount =1;
            }
            
        }
    }
}