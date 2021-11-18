namespace SbSTanks
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
