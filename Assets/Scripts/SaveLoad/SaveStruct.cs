using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Controllers;
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
        private Saver _saver;
        private void AddSave(List<UnitModel> player, List<UnitModel> enemies,List<SkillCd> skills, int turnNumber)
        {
            _saver = new Saver(player, enemies, skills, turnNumber);
           _savelist.AddLast(_saver);
           var jsonstruct = JsonUtility.ToJson(_saver);
            File.WriteAllText(Application.persistentDataPath + "/Gamedata.json", jsonstruct);
            SaveFile(jsonstruct);
        }

        private void AddSave()
        {
            var players = FindObjectsOfType<Player.Player>().ToList();
            var playerList = players.Select(player => player.Controller.Model as UnitModel).ToList();
            var enemies = FindObjectsOfType<Enemy.Enemy>().ToList();
            var enemyUnit = enemies.Select(enemy => enemy.Controller.Model as UnitModel).ToList();
            _saver = new Saver(playerList, enemyUnit, _arbitr.GetCoolDowns(), _step.TurnNumber);
            var jsonstruct = JsonUtility.ToJson(_saver);
       //     File.WriteAllText(Application.persistentDataPath + "/tmp_save.json", jsonstruct);
            _savelist.AddLast(JsonUtility.FromJson<Saver>(jsonstruct));
        }
        public SaveStruct(InputController inputController,SkillArbitr turn, StepController step)
        {
            inputController.ControlButtonPressed += CheckButton;
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
                    var players = FindObjectsOfType<Player.Player>().ToList();
                    var playerList = players.Select(player => player.Controller.Model as UnitModel).ToList();
                    var enemies = FindObjectsOfType<Enemy.Enemy>().ToList();
                    var enemyUnit = enemies.Select(enemy => enemy.Controller.Model as UnitModel).ToList();
                    AddSave(playerList,enemyUnit,_arbitr.GetCoolDowns(),_step.TurnNumber);
                    break;
                }
                case KeyCode.L:
                    Loader.Load();
                    break;
                case KeyCode.Z:
                    if (_savelist.Count==0) break;
                    Loader.Load(GetPrevious());
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
        public List<UnitModel> PlayerModel;
        public List<UnitModel> AbstractUnits;
        public List<SkillCd> SkillCDs;
        public int turnNumber;

        internal Saver(List<UnitModel> playerModel, List<UnitModel> enemy, List<SkillCd> CDController, int turnNum) :this()
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
        internal SkillCd(int skill, bool aval):this()
        {
            skillCool = skill;
            skillAvail = aval;
        }
    }
}