using System;
using Controllers.Model;
using Unit;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerModel : UnitModel
    {
        public PlayerModel(UnitModel parameters ) : base(parameters.HP,parameters.Damage, parameters.Element, parameters.UnitPosition)
        {
            HP = parameters.HP;
            Damage = parameters.Damage;
            Element = parameters.Element;
            UnitPosition = parameters.UnitPosition;
        }

    }
}