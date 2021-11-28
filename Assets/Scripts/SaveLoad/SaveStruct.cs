using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        private List<SkillCd> _skill;
        private SkillArbitr _arbitr;
        private StepController _step;

        private void AddSave(PlayerModel player, List<UnitModel> enemies,List<SkillCd> skills, int turnNumber)
        {
            var save = new Saver(player, enemies, skills, turnNumber);
           _savelist.AddLast(save);
           var jsonstruct = JsonUtility.ToJson(save);
            File.WriteAllText(Application.persistentDataPath + "/Gamedata.json", jsonstruct);
            SaveFile(jsonstruct);
        }

        private void AddSave()
        {
            var player = FindObjectOfType<Player.Player>().Controller.Model as PlayerModel;
            var enemies = FindObjectsOfType<Enemy.Enemy>().ToList();
            var enemyUnit = enemies.Select(enemy => enemy.Controller.Model as UnitModel).ToList() ;
            var cds = _skill;
            var save = new Saver(player, enemyUnit, _arbitr.GetCoolDowns(), _step.TurnNumber);
            _savelist.AddLast(save);
            var jsonstruct = JsonUtility.ToJson(save);
            File.WriteAllText(Application.persistentDataPath + "/Gamedata.json", jsonstruct);
            SaveFile(jsonstruct);
        }
        public SaveStruct(InputController inputController,SkillArbitr turn, StepController step)
        {
            inputController.SkillUsed += CheckButton;
            _arbitr = turn;
            _step = step;
            step.NewTurn += (x) => AddSave();
        }
        private void CheckButton(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.R:
                {
                    var player = FindObjectOfType<Player.Player>().Controller.Model as PlayerModel;
                    var enemies = FindObjectsOfType<Enemy.Enemy>().ToList();
                    var enemyUnit = enemies.Select(enemy => enemy.Controller.Model as UnitModel).ToList();
                    var cds = _skill;
                    AddSave(player,enemyUnit,_arbitr.GetCoolDowns(),_step.TurnNumber);
                    break;
                }
                case KeyCode.L:
                    var load = new Loader();
                    load.Load();
                    break;
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
        public void SaveFile(string data)
        {
            string destination = Application.persistentDataPath + "/save.dat";
            Debug.Log(destination);
            FileStream file;
 
            if(File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
    }
    [Serializable]
    public struct Saver
    {
        public PlayerModel PlayerModel;
        public List<UnitModel> AbstractUnits;
        public List<SkillCd> SkillCDs;
        public int turnNumber;

        internal Saver(PlayerModel playerModel, List<UnitModel> enemy, List<SkillCd> CDController, int turnNum)
        {
            PlayerModel = playerModel;
            AbstractUnits = enemy;
            SkillCDs = CDController;
            turnNumber = turnNum;
        }
    }
[Serializable]
    public struct SkillCd
    {
        public int skillCool;
        public bool skillAvail;
        internal SkillCd(int skill, bool aval)
        {
            skillCool = skill;
            skillAvail = aval;
        }
    }
}