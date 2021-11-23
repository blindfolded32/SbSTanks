using Controllers;
using Controllers.Model;
using Interfaces;
using Unit;

namespace Player
{
    public class PlayerModel : UnitModel
    {
        public int GetAndSetIndexOfTimer { get; set; }
        public TimerController GetAndSetTimerController { get; set; }
        public TimerData GetAndSetTimeData { get; set; }
        public PlayerModel(TimerController timerController, UnitInitializationData parameters )
        {
            GetAndSetTimerController = timerController;
            HP = parameters.Hp;
            Damage = parameters.Damage;
            Element = parameters.Element;
        }

    }
}