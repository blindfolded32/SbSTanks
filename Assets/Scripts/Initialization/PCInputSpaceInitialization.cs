using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leo_Part
{
    public class PCInputSpaceInitialization
    {
        private IPCInputSpace _pcInputSpace;

        public PCInputSpaceInitialization()
        {
            _pcInputSpace = new PCInputSpace();
        }

        public IPCInputSpace GetInputSpace()
        {
            return _pcInputSpace;
        }



    }
}
