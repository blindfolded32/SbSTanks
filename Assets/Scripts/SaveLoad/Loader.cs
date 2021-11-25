using System.IO;
using UnityEngine;

namespace SaveLoad
{
    public class Loader
    {
        public void Load()
        {
            var jsonstring = File.ReadAllText(Application.persistentDataPath + "/Gamedata.json");
            var jsonstruct = JsonUtility.FromJson<Saver>(jsonstring);
            var reinit = ServiceLocator.Resolve<MainInitializator>();
            reinit.GameLoad(jsonstruct);
        }
    }
}