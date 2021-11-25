namespace Markers
{
    public class NameManager
    {
        public const string BULLET_POOL = "[Bullet_Pool]";
        internal const string PARTICLE_PATH = "Partcle/CompleteTankExplosion";
        internal static readonly float SHOT_FORCE = 250.0f;
        public const int TriesCount = 3;
        public const float RoundModifier = 1.1f;
        internal const int FireSkillCd = 3;
        internal const int EarthSkillCd = 2;
        public enum ElementList 
        {
            Fire=0,
            Water =1,
            Earth =2
        }
    }
}