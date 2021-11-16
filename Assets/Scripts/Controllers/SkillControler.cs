using System.Linq;
using UnityEngine;

namespace SbSTanks
{
    public class SkillControler 
    {
        private readonly PlayerController _player;
       
        private IPCInputSpace _buttonControler;
        public SkillControler(PlayerController player, IPCInputSpace inputState)
        {
            _player = player;
            inputState.ButtonDown += SkillSelector;
        }

        private void EarthSkill()
        {
            var transformPosition = _player.SwitchEnemyButtonsMatching.ElementAt(Random.Range(0, _player.SwitchEnemyButtonsMatching.Count))
                .Value.transform.position;
            _player.RotatePlayer(Quaternion.LookRotation(transformPosition - _player.GetPosition()));
            _player.isPlayerTurn = true;
            Debug.Log("make it random");
        }

        private void WaterSkill()
        {
            _player.isPlayerTurn = true;
            Debug.Log("Target on it");
        }

        private void FireSkill()
        {
            foreach (var enemy in _player.SwitchEnemyButtonsMatching.Values)
            {
                enemy.TakingDamage(10,_player.GetPlayerElement());
            }
            _player.isPlayerTurn = true;
            Debug.Log("AOE");
        }

        public void SkillSelector(KeyCode id)
        {
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