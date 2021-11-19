namespace SbSTanks
{
    public interface IParameters
    {
        public Health Hp { get; }
        public float Damage { get; set; }
        public int ElementId { get; }
    }
}