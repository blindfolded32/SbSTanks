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
using static UnityEngine.Purchasing.MiniJSON.Json;

namespace SaveLoad
{
    [Serializable]
    public class SaveStruct
    { 
        private LinkedList<Saver> _savelist = new LinkedList<Saver>();
        private SkillArbitr _skill;

        private void AddSave(Saver save)
        {
            _savelist.AddLast(save);
            var jsonstruct = JsonUtility.ToJson(save);
            File.WriteAllText(Application.persistentDataPath + "/Gamedata.json", jsonstruct);
            SaveFile(save.PlayerModel.Element.ToString());
        }

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
                    var enemies = FindObjectsOfType<Enemy.Enemy>().ToList();
                    List<UnitModel> enemyUnit = new List<UnitModel>();
                    foreach (var enemy in enemies)
                    {
                        enemyUnit.Add(enemy.Controller.Model as UnitModel);
                    }
                    var cds = _skill;
                    AddSave(new Saver(player,enemyUnit,_skill));
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
    internal struct Saver
    {
        public UnitModel PlayerModel;
        public List<UnitModel> AbstractUnits;
        public SkillArbitr SkillCDs;


        internal Saver(PlayerModel playerModel, List<UnitModel> enemy, SkillArbitr CDController)
        {
            PlayerModel = playerModel;
            AbstractUnits = enemy;
            SkillCDs = CDController;
        }
    }
}