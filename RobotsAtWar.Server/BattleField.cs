using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsAtWar.Server
{
    public class BattleField
    {
        private var _warriorByName = new Dictionary<string, Warrior>(); 
        private DateTime _battleTime;
 
        public void RegisterWarrior(string warriorName)
        {
            _warriorByName.Add(warriorName, new Warrior(warriorName));

            if (_warriorByName.Count == 1)
            {
                _battleTime = DateTime.UtcNow.AddSeconds(5);
            }
        }

        public DateTime GetBattleTime()
        {
            return _battleTime;
        }

        public Warrior GetWarriorByName(string warriorName)
        {
            return _warriorByName[warriorName];
        }


    }

    public class Warrior
    {
        private readonly string _warriorName;

        public Warrior(string warriorName)
        {
            _warriorName = warriorName;
        }
    }
}
