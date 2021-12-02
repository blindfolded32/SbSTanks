using Controllers;
using TMPro;
using UnityEngine;


namespace Markers
{
    public class RoundCanvas : MonoBehaviour
    {
        private Animator _animator;
        private TextMeshProUGUI _textMeshPro;
        private GameOverCanvas _gameOverCanvas;
        private static StepController _stepController;
        private static readonly int NewRound = Animator.StringToHash("NewRound");

        private const int GAME_PAUSE = 0;

        public static void Init(StepController stepController)
        {
            _stepController = stepController;
        }
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            _stepController.NewRound += ShowText;
            _stepController.NewTurn += ShowTurn;
            _stepController.ReInitController.GameOver += LostBaner;
            _gameOverCanvas = FindObjectOfType<GameOverCanvas>();

        }
        private void ShowText(int roundNumber)
        {
            _textMeshPro.text = $"Round {roundNumber}";
            _animator.Play(NewRound,-1,0f);
        }

        private void ShowTurn(int turnNumber)
        {
            _textMeshPro.text = $"turn {turnNumber}";
            _animator.Play(NewRound,-1,0f);
        }

        private void LostBaner(int round)
        {
            Time.timeScale = GAME_PAUSE;
            _textMeshPro.text = $"You reached round {round}. Try Better";
            _animator.Play(NewRound,-1,0f);
            _gameOverCanvas.GetComponent<Canvas>().enabled = true;
        }
        
    }
}

