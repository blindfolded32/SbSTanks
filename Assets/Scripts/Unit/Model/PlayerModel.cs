namespace SbSTanks
{
    public class PlayerModel
    {
        public int GetAndSetIndexOfTimer { get; set; }
        public TimerController GetAndSetTimerController { get; set; }
        public TimerData GetAndSetTimeData { get; set; }
        public PlayerModel(TimerController timerController)
        {
            GetAndSetTimerController = timerController;
        }

    }
}

