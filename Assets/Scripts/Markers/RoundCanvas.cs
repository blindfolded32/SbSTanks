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
            _stepController.ReInitController.NewRoundStart += ShowText;
            _stepController.NewTurn += ShowTurn;
        }
        private void ShowText(int roundNumber)
        {
            _textMeshPro.text = $"Round {roundNumber}";
            _animator.Play("NewRound",-1,0f);
        }

        private void ShowTurn(int turnNumber)
        {
            _textMeshPro.text = $"turn {turnNumber}";
            _animator.Play("NewRound",-1,0f);
        }
    }
}

