using System.IO;
using UnityEngine;

namespace SaveLoad
{
    public class Loader
    {
        public static void Load()
        {
            var jsonstring = File.ReadAllText(Application.persistentDataPath + "/Gamedata.json");
            var jsonstruct = JsonUtility.FromJson<Saver>(jsonstring);
            var reinit = ServiceLocator.Resolve<MainInitializator>();
            reinit.GameLoad(jsonstruct);
        }

        public static void Load(Saver save)
        {
            var reinit = ServiceLocator.Resolve<MainInitializator>();
            reinit.GameLoad(save);
        }
    }
}