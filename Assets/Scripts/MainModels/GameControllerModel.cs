using System.Collections.Generic;
using Interfaces;

namespace MainModels
{
    public class GameControllerModel
    {
        public GameControllerModel()
        {
            ExecuteControllers = new List<IExecute>(8);
            LateExecuteControllers = new List<ILateExecute>(8);
            FixedControllers = new List<IFixedExecute>(8);
        }

        public List<IExecute> ExecuteControllers { get; set; }
        public List<ILateExecute> LateExecuteControllers { get; set; }
        internal List<IFixedExecute> FixedControllers { get; set; }
    }
}