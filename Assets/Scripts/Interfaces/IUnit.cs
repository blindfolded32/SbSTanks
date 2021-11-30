using UnityEngine;

namespace Interfaces
{
    public interface IUnit
    {
        public IParameters Parameters { get; set; }
        public Transform GetShotPoint { get; }
    }
}