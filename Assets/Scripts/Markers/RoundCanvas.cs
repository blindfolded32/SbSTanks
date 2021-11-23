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

        
        private void ShowText(int roundNumber)
        {
            _textMeshPro.text = $"Round {roundNumber}";
            _animator.enabled = true;
        }

        private void Update()
        {
            ShowText(1);
        }
        
        public IModel Model { get; set; }
        public void Execute(float deltaTime)
        {
            
        }
    }
}

