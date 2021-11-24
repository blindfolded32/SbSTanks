using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Interfaces;
using Player;
using Unit;
using UnityEngine;
using static UnityEngine.Object;

namespace SaveLoad
{
    [Serializable]
    public class SaveStruct
    { 
        private LinkedList<Saver> _savelist = new LinkedList<Saver>();
        private SkillArbitr _skill;

        private void AddSave(Saver save)=> _savelist.AddLast(save);
        public SaveStruct(InputController inputController,SkillArbitr turn)
        {
            inputController.SkillUsed += CheckButton;
            _skill = turn;
        }

        private void CheckButton(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.R:
                {
                    var player = FindObjectOfType<Player.Player>().Controller.Model as PlayerModel;
                    var enemies = FindObjectsOfType<Enemy.Enemy>();
                    var cds = _skill;
                    AddSave(new Saver(player,enemies,_skill));
                    break;
                }
                default: break;
            }
        }
        private Saver GetPrevious()
        {
            if (_savelist.Count == 0) return default;
            var save = _savelist.Last.Value;
            _savelist.Remove(_savelist.Last.Value);
            return save;
        }
    }
    [Serializable]
    internal struct Saver
    {
        private PlayerModel _playerModel;
        private AbstractUnit[] _abstractUnits;
        private SkillArbitr _skillCDs;


        internal Saver(PlayerModel playerModel, AbstractUnit[] enemy, SkillArbitr CDController)
        {
            _playerModel = playerModel;
            _abstractUnits = enemy;
            _skillCDs = CDController;
        }
    }
}