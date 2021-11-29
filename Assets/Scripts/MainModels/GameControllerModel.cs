using System.Collections.Generic;
using Interfaces;

namespace MainModels
{
    public class GameControllerModel
    {
        public GameControllerModel()
        {
            ExecuteControllers = new List<IExecute>();
            LateExecuteControllers = new List<ILateExecute>();
            FixedControllers = new List<IFixedExecute>();
        }

        public List<IExecute> ExecuteControllers { get; set; }
        public List<ILateExecute> LateExecuteControllers { get; set; }
        internal List<IFixedExecute> FixedControllers { get; set; }
    }
}