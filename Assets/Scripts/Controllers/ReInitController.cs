using System;
using System.Collections.Generic;
using Interfaces;
using Player;
using Unit;
using static UnityEngine.Object;
using Random = UnityEngine.Random;
using static Markers.NameManager;


namespace Controllers
{
    public class ReInitController : IReInit, IController
    {
        public event Action StartAgain;
        public event Action GameOver;
        public event Action<int> NewRoundStart;
        public bool Lost { get; private set; } = false;
        private int RoundNumber { get; set; }
        
        private readonly PlayerController _playerController;
        private readonly List<Enemy.Enemy> _enemies;
        private List<Enemy.Enemy> _defParams;
        private int _triesCount;
        public ReInitController(PlayerController playerController,List<Enemy.Enemy> enemies)
        {
            _playerController = playerController;
            _enemies = enemies;
            _defParams = _enemies;
            _playerController.GetView.PlayerDead += NewTry;
            _triesCount = TriesCount;
            RoundNumber = 1;
        }
        public void ReInit(IEnumerable<Enemy.Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.Controller.IsFired = false;
                enemy.Element = (ElementList) (Random.Range(0, 2));
            }
        }
        public void NewRound(IEnumerable<Enemy.Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.IsDead = false;
                enemy.Controller.Model.Damage *= RoundModifier;
                enemy.Controller.Model.HP.InjectNewHp( enemy.Controller.Model.HP.Max * RoundModifier);
                enemy.GetComponentInChildren<UnitHealthBar>().ResetBar(1.0f);
                RoundNumber++;
            }
            _playerController.Model.Element = (ElementList) (Random.Range(0, 2));
            NewRoundStart?.Invoke(RoundNumber);
        }

        public void Renew()
        {
            _playerController.GetView.GetComponentInChildren<UnitHealthBar>().RenewBar(_playerController.GetView);
            foreach (var enemy in _enemies)
            {
                enemy.GetComponentInChildren<UnitHealthBar>().RenewBar(enemy);
            }
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
            RoundNumber = 1;
            NewRoundStart?.Invoke(RoundNumber);
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
                currentEnemy[i].GetComponentInChildren<UnitHealthBar>()._foregroundImage.fillAmount =1;
            }
            
        }
    }
}