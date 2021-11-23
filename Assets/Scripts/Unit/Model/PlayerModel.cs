using Controllers;
using Controllers.Model;
using UnityEngine;

namespace Unit.Model
{
    public class PlayerModel
    {
        public Health HP { get; set; }
        public float Damage { get; set; }

        public int Element { get; set; }
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

