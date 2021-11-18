﻿using System.Linq;
using UnityEngine;

namespace SbSTanks
{
    public class SkillControler : IController
    {
        private readonly PlayerController _player;
        private readonly StepController _stepController;
        private readonly ButtonActivationController _buttonControler;
        private readonly ButtonActivationController _buttonActivationController;
        public SkillControler(PlayerController player, StepController stepController, 
                            SkillButtons skillButtons,IPCInputSpace inputState, ButtonActivationController buttonActivationController)
        {
            _player = player;
            _stepController = stepController;
            _buttonControler = buttonActivationController;
            inputState.ButtonDown += SkillSelector;
            skillButtons.UiSkill += SkillSelector;
        }
        private void EarthSkill()
        {
            if (_stepController.GetTurnNumber%3 != 0) return;
            _player.IsPlayerTurn = true;
            var transformPosition = _buttonControler.SwitchEnemyButtonsMatching.
                ElementAt(Random.Range(0, _buttonControler.SwitchEnemyButtonsMatching.Count))
                .Value.transform;
            _player.RotatePlayer(transformPosition);
            _player.PlayerModel.GetPlayer.Shot(_player,0);
        }
        private void WaterSkill()
        {
            _player.IsPlayerTurn = true;
            _player.PlayerModel.GetPlayer.Shot(_player,2);
        }
        private void FireSkill()
        {
            if (_stepController.GetTurnNumber%2 !=0) return;
            _player.IsPlayerTurn = true;
            foreach (var enemy in _buttonControler.SwitchEnemyButtonsMatching.Values.Where(enemy => !enemy.isDead))
            {
                enemy.TakingDamage(10,1);
            }
            _player.IsPlayerTurn = false;
        }
        private void SkillSelector(KeyCode id)
        {
            if(!_player.IsPlayerTurn) return;
            switch (id)
            {
                case KeyCode.Q:
                {
                    EarthSkill();
                    break;
                }
                case KeyCode.W:
                {
                    WaterSkill();
                    break;
                }
                case KeyCode.E:
                {
                    FireSkill();
                    break;
                }
                default:
                {
                    Debug.Log("Something Wrong");
                    break;
                }
            }
        }
    }
}