﻿using Controllers.Model;

namespace Interfaces
{
    public interface IParameters
    {
        public Health Hp { get; set; }
        public float Damage { get; set; }
        public int ElementId { get; set; }
    }
}