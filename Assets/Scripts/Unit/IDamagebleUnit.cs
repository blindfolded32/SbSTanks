namespace SbSTanks
{
    public interface IDamagebleUnit
    {
        public void TakingDamage(int damage, int element);
        public int GetUnitElement { get; }
    }
}