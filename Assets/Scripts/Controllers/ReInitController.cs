using System.Collections.Generic;
using UnityEngine;

namespace SbSTanks
{
    public class ReInitController
    {
        public void ReInit(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.SetUnitElement(Random.Range(0,2));
            }
        }
        public void NewRound(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.isDead = false;
                enemy.Parameters.Damage *= 1.1f;
                enemy.Parameters.Hp.InjectNewHp(enemy.Parameters.Hp.Max*1.1f);
            }
        }
    }
}