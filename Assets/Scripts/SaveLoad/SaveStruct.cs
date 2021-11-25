using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Controllers;
using Player;
using Unit;
using UnityEngine;
using static UnityEngine.Object;

namespace SaveLoad
{
    [Serializable]
    public class SaveStruct
    { 
        [SerializeField]private LinkedList<Saver> _savelist = new LinkedList<Saver>();
        private List<SkillCd> _skillCds = new List<SkillCd>();
        private SkillArbitr _arbiter;
        private List<SkillCd> _skill;

        private void AddSave(UnitModel player, List<UnitModel> enemies,SkillArbitr skills)
        {
            _skillCds.Add(new SkillCd(skills._earthUsed, skills._isEarthAvailable));
            _skillCds.Add(new SkillCd(skills._fireUsed,skills._isEarthAvailable));
            var save = new Saver(player, enemies, _skillCds);
           _savelist.AddLast(save);
           var jsonstruct = JsonUtility.ToJson(save);
            File.WriteAllText(Application.persistentDataPath + "/Gamedata.json", jsonstruct);
            SaveFile(jsonstruct);
        }

        public SaveStruct(InputController inputController,SkillArbitr turn)
        {
            inputController.SkillUsed += CheckButton;
            _arbiter = turn;
        }

        private void CheckButton(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.R:
                {
                    var player = FindObjectOfType<Player.Player>().Controller.Model as PlayerModel;
                    var enemies = FindObjectsOfType<Enemy.Enemy>().ToList();
                    List<UnitModel> enemyUnit = new List<UnitModel>();
                    foreach (var enemy in enemies)
                    {
                        enemyUnit.Add(enemy.Controller.Model as UnitModel);
                    }
                    var cds = _skill;
                    AddSave(player,enemyUnit,_arbiter);
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
        public UnitModel PlayerModel;
        public List<UnitModel> AbstractUnits;
        public List<SkillCd> SkillCDs;

        internal Saver(UnitModel playerModel, List<UnitModel> enemy, List<SkillCd> CDController)
        {
            PlayerModel = playerModel;
            AbstractUnits = enemy;
            SkillCDs = CDController;
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