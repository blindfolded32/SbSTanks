using System;
using System.Collections.Generic;
using Unit;
using Random = UnityEngine.Random;
using static UnityEngine.Object;


namespace Controllers
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
        public static void ReInit(IEnumerable<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.unitInitializationData.Element = (Random.Range(0,2));
            }
        }
        public void NewRound(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.isDead = false;
               enemy.unitInitializationData.Damage *= 1.1f;
                enemy.unitInitializationData.Hp.InjectNewHp(enemy.unitInitializationData.Hp.Max*1.1f);
                enemy.GetComponentInChildren<EnemyHealthBar>()._foregroundImage.fillAmount =1;
            }
            _playerController.GetView.unitInitializationData.Element = (Random.Range(0, 2));
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
                currentEnemy[i].unitInitializationData.Damage = _defParams[i].unitInitializationData.Damage;
                currentEnemy[i].unitInitializationData.Hp.InjectNewHp(_defParams[i].unitInitializationData.Hp.Max);
                currentEnemy[i].GetComponentInChildren<EnemyHealthBar>()._foregroundImage.fillAmount =1;
            }
            
        }
    }
}