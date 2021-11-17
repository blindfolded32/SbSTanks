using System.Linq;
using UnityEngine;

namespace SbSTanks
{
    public class SkillControler : IController
    {
        private readonly PlayerController _player;
        private readonly StepController _stepController;
        private IPCInputSpace _buttonControler;
        public SkillControler(PlayerController player, StepController stepController,IPCInputSpace inputState)
        {
            _player = player;
            _stepController = stepController;
            inputState.ButtonDown += SkillSelector;
            
        }
        private void EarthSkill()
        {
            _player.isPlayerTurn = true;
            var transformPosition = _player.SwitchEnemyButtonsMatching.ElementAt(Random.Range(0, _player.SwitchEnemyButtonsMatching.Count))
                .Value.transform.position;
            _player.RotatePlayer(Quaternion.LookRotation(transformPosition - _player.GetTransform().position));
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
            _player.isPlayerTurn = true;
            foreach (var enemy in _player.SwitchEnemyButtonsMatching.Values)
            {
                enemy.TakingDamage(10,_player.GetPlayerElement());
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