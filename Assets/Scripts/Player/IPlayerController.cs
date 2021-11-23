using Interfaces;

namespace Player
{
    public interface IPlayerController : IController
    {
        public PlayerModel PlayerModel { get; }
        public Player GetView { get; }
    }
}