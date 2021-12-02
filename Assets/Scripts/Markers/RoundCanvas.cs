using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Markers
{
    public class RoundCanvas : MonoBehaviour
    {
        private Animator _animator;
        private TextMeshProUGUI _textMeshPro;
        private GameOverCanvas _gameOverCanvas;
        private Button _restartButton;
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
            _stepController.GameOver += LostBaner;
            _gameOverCanvas = FindObjectOfType<GameOverCanvas>();
            _restartButton = _gameOverCanvas.GetComponentInChildren<Button>();
            _restartButton.onClick.AddListener(delegate
            {
                Debug.Log("Click");
                _stepController.ReInitController.NewTry();
                _gameOverCanvas.GetComponent<Canvas>().enabled = false;
            });

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
            _stepController.AddTimer();
        }

        private void LostBaner(int round)
        {
            //Time.timeScale = GAME_PAUSE;
            //_textMeshPro.text = $"You reached round {round}. Try Better";
            //_animator.Play(NewRound,-1,0f);
            _gameOverCanvas.GetComponent<Canvas>().enabled = true;
        }
        
    }
}

