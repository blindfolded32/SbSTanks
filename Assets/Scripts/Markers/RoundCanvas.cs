using Controllers;
using TMPro;
using UnityEngine;

namespace Markers
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
            _stepController.ReInitController.GameOver += LostBaner;
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

        private void LostBaner(int round)
        {
            _textMeshPro.text = $"You reached round {round}. Try Better";
            _animator.Play("NewRound",-1,0f);
        }
        
    }
}

