using Interfaces;

namespace Controllers
{
    public class TargetAvailability
    {
        public bool CheckTarget(IUnitController unit)
        {
            return unit.State != NameManager.State.Dead || unit.State != NameManager.State.Levitate;
        }
    }
}