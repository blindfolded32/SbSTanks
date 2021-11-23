using Controllers;
using Controllers.Model;
using Unit;

namespace Player
{
    public class PlayerModel : UnitModel
    {
        public int GetAndSetIndexOfTimer { get; set; }
        public TimerController GetAndSetTimerController { get; set; }
        public TimerData GetAndSetTimeData { get; set; }
        public PlayerModel(TimerController timerController, UnitInitializationData parameters ) : base(parameters.Hp,parameters.Damage, parameters.Element)
        {
            GetAndSetTimerController = timerController;
            HP = parameters.Hp;
            Damage = parameters.Damage;
            Element = parameters.Element;
        }

    }
}