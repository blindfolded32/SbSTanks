using System.Linq;
using UnityEngine;

namespace SbSTanks
{
    public class SkillControler : IController
    {
        private readonly PlayerController _player;
        private readonly StepController _stepController;
        private ButtonActivationController _buttonControler;
        private readonly ButtonActivationController _buttonActivationController;
        public SkillControler(PlayerController player, StepController stepController, SkillButtons skillButtons,IPCInputSpace inputState, ButtonActivationController buttonActivationController)
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
            
            _player.isPlayerTurn = true;
            var transformPosition = _buttonControler.SwitchEnemyButtonsMatching.ElementAt(Random.Range(0, _buttonControler.SwitchEnemyButtonsMatching.Count))
                .Value.transform;
            _player.RotatePlayer(transformPosition);
            _player.GetPlayerModel.GetPlayer.Shot(_player.GetPlayerModel,0);
            Debug.Log("make it random");
        }
        private void WaterSkill()
        {
            _player.isPlayerTurn = true;
            _player.GetPlayerModel.GetPlayer.Shot(_player.GetPlayerModel,2);
            Debug.Log("Target on it");
        }
        private void FireSkill()
        {
            if (_stepController.GetTurnNumber%2 !=0) return;
           
            _player.isPlayerTurn = true;
            foreach (var enemy in _buttonControler.SwitchEnemyButtonsMatching.Values)
            {
                if (!enemy.isDead) enemy.TakingDamage(10,1);
            }
            Debug.Log("AOE");
        }
        private void SkillSelector(KeyCode id)
        {
            if(!_stepController.isPlayerTurn) return;
            switch (id)
            {
                case KeyCode.Q:
                {
                    Debug.Log("Earth");
                    EarthSkill();
                    break;
                }
                case KeyCode.W:
                {
                    Debug.Log("Water");
                    WaterSkill();
                    break;
                }
                case KeyCode.E:
                {
                    Debug.Log("Fire");
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