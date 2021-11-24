using System;
using UnityEngine;
using static Markers.NameManager;

namespace Controllers
{
    [Serializable] 
    public class SkillArbitr
    {
        
        [SerializeField]private int _fireUsed=0;
        [SerializeField] private bool _isFireAvailable = true;
        
        [SerializeField]private int _earthUsed =0;
        [SerializeField]private bool _isEarthAvailable = true;

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
        private void ResetCd()
        {
         _fireUsed=0;
       _isFireAvailable = true;
       _earthUsed =0;
         _isEarthAvailable = true;
         CheckAvailability(_stepController.GetTurnNumber);
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
            if (!_stepController.PlayerTurn) return;
            switch (id)
            {
                case KeyCode.E:
                {
                    if (_isEarthAvailable)
                    {
                        _skillController.EarthSkill();
                        _isEarthAvailable = false;
                        _earthUsed = _stepController.GetTurnNumber;
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
                        _fireUsed = _stepController.GetTurnNumber;
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