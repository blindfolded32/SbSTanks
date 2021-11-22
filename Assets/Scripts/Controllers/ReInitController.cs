using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using static UnityEngine.Object;


namespace SbSTanks
{
    public class ReInitController
    {
        public event Action StartAgain;
        public event Action GameOver;
        private int _triesCount = 3;
        public bool Lost = false;
        
        private PlayerController _playerController;
        private readonly List<Enemy> _enemies;
        private List<Enemy> _defParams;
        public ReInitController(PlayerController playerController,List<Enemy> enemies)
        {
            _playerController = playerController;
            _enemies = enemies;
            _defParams = _enemies;
            _playerController.GetView.PlayerDead += NewTry;
        }
        public void ReInit(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.SetUnitElement(Random.Range(0,2));
            }
        }
        public void NewRound(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.isDead = false;
                enemy.Parameters.Damage *= 1.1f;
                enemy.Parameters.Hp.InjectNewHp(enemy.Parameters.Hp.Max*1.1f);
                enemy.GetComponentInChildren<EnemyHealthBar>()._foregroundImage.fillAmount =1;
            }
            _playerController.GetView.Parameters.ElementId = (Random.Range(0, 2));
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
            var currentEnemy = FindObjectsOfType<Enemy>();
            for (int i = 0; i < currentEnemy.Length; i++)
            {
                currentEnemy[i].isDead = false;
                currentEnemy[i].Parameters.Damage = _defParams[i].Parameters.Damage;
                currentEnemy[i].Parameters.Hp.InjectNewHp(_defParams[i].Parameters.Hp.Max);
                currentEnemy[i].GetComponentInChildren<EnemyHealthBar>()._foregroundImage.fillAmount =1;
            }
            
        }
    }
}