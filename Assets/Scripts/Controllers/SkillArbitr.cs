using System;
using System.Collections.Generic;
using SaveLoad;
using UnityEngine;
using static Markers.NameManager;

namespace Controllers
{
    [Serializable] 
    public class SkillArbitr
    {
        
        [SerializeField]public int _fireUsed=0;
        [SerializeField] public bool _isFireAvailable = true;
        
        [SerializeField]public int _earthUsed =0;
        [SerializeField]public bool _isEarthAvailable = true;

        private StepController _stepController;
        private InputController _controller;
        private SkillController _skillController;

        public SkillArbitr(StepController stepController, InputController inputController, SkillController skillController)
        {
            _stepController = stepController;
            _controller = inputController;
            _skillController = skillController;
            _controller.SkillUsed += SkillSelector;
            stepController.NewTurn += CheckAvailability;
            stepController.ReInitController.NewRoundStart += (x) => ResetCd();
        }

        public void SetSkills(List<SkillCd> skill = default)
        {
            if (skill != null && !skill.Equals(default))
            {
                _earthUsed = skill[0].skillCool;
                _isEarthAvailable = skill[0].skillAvail;
                _fireUsed = skill[1].skillCool;
                _isFireAvailable = skill[1].skillAvail;
            }
        }

        public List<SkillCd> GetCoolDowns()
        {
            var coolDownList = new List<SkillCd>();
            coolDownList.Add(new SkillCd(_earthUsed,_isEarthAvailable));
            coolDownList.Add(new SkillCd(_fireUsed,_isFireAvailable));
            return coolDownList;
        }
        private void ResetCd()
        {
         _fireUsed=0;
       _isFireAvailable = true;
       _earthUsed =0;
         _isEarthAvailable = true;
         CheckAvailability(_stepController.TurnNumber);
        }
        private void CheckAvailability(int turnNumber)
        {
            Debug.Log($"Turn is {turnNumber} and earth next use is  {(_earthUsed + EarthSkillCd)}");
            Debug.Log($"Turn is {turnNumber} and fire next use is  {(_fireUsed + FireSkillCd)}");
            if (turnNumber - (_earthUsed + EarthSkillCd) >= 0) _isEarthAvailable = true;
            if (turnNumber - (_fireUsed + FireSkillCd) >= 0) _isFireAvailable = true;
            _controller.UIInput.ButtonState(KeyCode.Q,_isFireAvailable);
            _controller.UIInput.ButtonState(KeyCode.E,_isEarthAvailable);
        }
        private void SkillSelector(KeyCode id)
        {
            if(_stepController.PlayerTurn != State.Attack) return;
            switch (id)
            {
                case KeyCode.E:
                {
                    if (_isEarthAvailable)
                    {
                        _skillController.EarthSkill();
                        _isEarthAvailable = false;
                        _earthUsed = _stepController.TurnNumber;
                    }
                    _controller.UIInput.ButtonState(id,_isEarthAvailable);
                    break;
                }
                case KeyCode.W:
                {
                    _skillController.WaterSkill();
                    break;
                }
                case KeyCode.Q:
                {
                    if (_isFireAvailable)
                    {
                        _skillController.FireSkill();
                        _isFireAvailable = false;
                        _fireUsed = _stepController.TurnNumber;
                    }
                    _controller.UIInput.ButtonState(id,_isFireAvailable);
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