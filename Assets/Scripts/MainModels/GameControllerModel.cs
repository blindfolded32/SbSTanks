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

        public List<IExecute> ExecuteControllers { get; }
        public List<ILateExecute> LateExecuteControllers { get; }
        internal List<IFixedExecute> FixedControllers { get; }
    }
}