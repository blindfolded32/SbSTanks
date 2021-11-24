using Interfaces;
using UnityEngine;

namespace Player
{
    public interface IPlayerController : IUnitController
    {
        public PlayerModel PlayerModel { get; }
        public Player GetView { get; }
    }
}