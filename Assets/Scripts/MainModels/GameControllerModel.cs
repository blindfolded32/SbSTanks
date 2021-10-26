using System.Collections.Generic;

namespace SbSTanks
{
    public class GameControllerModel
    {
        private readonly List<IFixedExecute> _fixedControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<ILateExecute> _lateExecuteControllers;
        public GameControllerModel()
        {
            _executeControllers = new List<IExecute>(8);
            _lateExecuteControllers = new List<ILateExecute>(8);
            _fixedControllers = new List<IFixedExecute>(8);
        }

        public List<IExecute> ExecuteControllers => _executeControllers;

        public List<ILateExecute> LateExecuteControllers => _lateExecuteControllers;

        internal List<IFixedExecute> FixedControllers => _fixedControllers;
    }
}