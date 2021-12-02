public class NameManager
{
    public const string BULLET_POOL = "[Bullet_Pool]";
    internal const string PARTICLE_PATH = "Partcle/CompleteTankExplosion";
    internal const float SHOT_FORCE = 240.0f;
    internal const float ROTATION_TIME = 0.5f;
    public const int TriesCount = 3;
    public const float RoundModifier = 1.1f;
    internal const int FireSkillCd = 3;
    internal const int EarthSkillCd = 2;
    public const float TurnCoolDown = 2.0f;
    public const float ImageUpdateSpeed = 1f;
    public enum ElementList 
    {
        Fire=0,
        Water =1,
        Earth =2
    }
    public enum State
    {
        Idle=0,
        Attack =1,
        Fired=2,
        Levitate=3,
        Dead=4
    }
}