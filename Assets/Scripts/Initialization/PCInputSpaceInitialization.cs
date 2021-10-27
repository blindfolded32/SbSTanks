using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leo_Part
{
    public class PCInputSpaceInitialization
    {
        private IPCInputSpace _pcInputSpace;
        private bool _spaceValue;

        public PCInputSpaceInitialization()
        {
            _pcInputSpace = new PCInputSpace();
            _pcInputSpace.OnSpaceChange += GetSpaceKey;
        }

        public IPCInputSpace GetInputSpace()
        {
            return _pcInputSpace;
        }

        public bool GetSpaceValue()
        {
            return _spaceValue;
        }

        public void GetSpaceKey(bool f)
        {
            _spaceValue = f;
        }
    }
}
