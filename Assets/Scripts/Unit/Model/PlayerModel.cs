namespace SbSTanks
{
    public class PlayerModel
    {
        private Player _player;//
        private int _indexOfTimer;
        private TimerController _timerController;//
        private TimerData _timeData;
        public Player GetPlayer { get => _player; }
       
        public int GetAndSetIndexOfTimer { get => _indexOfTimer; set => _indexOfTimer = value; }
        public TimerController GetAndSetTimerController { get => _timerController; set => _timerController = value; }
        public TimerData GetAndSetTimeData { get => _timeData; set => _timeData = value; }
        public PlayerModel(TimerController timerController, Player player)
        {
            _timerController = timerController;
            _player = player;
        }

    }
}

