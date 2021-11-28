using TMPro;
using UnityEngine;

namespace Controllers
{
    public class RoundCanvas : MonoBehaviour
    {
        private Animator _animator;
        private TextMeshProUGUI _textMeshPro;
        private static StepController _stepController;

        public static void Init(StepController stepController)
        {
            _stepController = stepController;
        }
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            _stepController.NewTurn += ShowText;
            
        }

 
        private void ShowText(int roundNumber)
        {
            _textMeshPro.text = $"Round {roundNumber}";
            _animator.Play("NewRound",-1,0f);
        }
    }
}

