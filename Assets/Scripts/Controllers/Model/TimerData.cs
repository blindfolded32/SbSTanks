namespace SbSTanks
{
    public class TimerData
    {
        private float _startTime;
        private float _deltaTime;
        private bool _isTimerEnd;

        public float GetStartTime { get => _startTime; }
        public float GetDeltaTime { get => _deltaTime; }
        public bool IsTimerEnd { get => _isTimerEnd; set => _isTimerEnd = value; }

        public TimerData(float deltatime, float currentTime)
        {
            _startTime = currentTime;
            _deltaTime = deltatime;
            _isTimerEnd = false; //поменять на событие
        }

    }
}

