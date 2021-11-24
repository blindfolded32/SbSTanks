using Interfaces;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class RoundCanvas : MonoBehaviour,IExecute
    {
        private Animator _animator;
        private TextMeshProUGUI _textMeshPro;
        private StepController _stepController;

        public RoundCanvas(StepController stepController)
        {
            _stepController = stepController;
        }
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            //_stepController.NewTurn += ShowText;
        }

        private void Update()
        {
            ShowText(1);
        }

        private void ShowText(int roundNumber)
        {
            _textMeshPro.text = $"Round {roundNumber}";
            _animator.enabled = true;
        }
        public void Execute(float deltaTime)
        {
            
        }
    }
}

