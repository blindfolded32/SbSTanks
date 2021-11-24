using System;
using System.Collections.Generic;
using Controllers;
using Player;
using Unit;

namespace SaveLoad
{
    [Serializable]
    public class SaveStruct
    {
        private LinkedList<Saver> _savelist = new LinkedList<Saver>();

        private void AddSave(Saver save)=> _savelist.AddLast(save);

        private Saver GetPrevious()
        {
            if (_savelist.Count == 0) return default;
            var save = _savelist.Last.Value;
            _savelist.Remove(_savelist.Last.Value);
            return save;
        }
    }
    internal abstract class Saver
    {
        private PlayerModel _playerModel;
        private AbstractUnit[] _abstractUnits;
        private ReInitController _turnController;
        private SkillArbitr _skillCDs;


        protected Saver(PlayerModel playerModel, AbstractUnit[] enemy, ReInitController turnController,
            SkillArbitr CDController)
        {
            _playerModel = playerModel;
            _abstractUnits = enemy;
            _turnController = turnController;
            _skillCDs = CDController;
        }
    }
}