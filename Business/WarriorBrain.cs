using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    class WarriorBrain
    {
        private WarriorClient _warriorClient;

        public WarriorBrain(WarriorClient warriorClient)
        {
            _warriorClient = warriorClient;
        }

        public void Start()
        {
            _warriorClient.Register();
            DateTime battleTime = DateTime.MinValue;
            while (battleTime == DateTime.MinValue)
            {
                battleTime = _warriorClient.GetBattleTime();
            }
            Thread.Sleep(battleTime - DateTime.UtcNow);
            Fight();
        }

        private void Fight()
        {
            while (true)
            {
                if (WarriorClient.IsBattleOver())
                {
                    Environment.Exit(0);
                }
                ExecuteNextCommand();
            }
        }

        public void ExecuteNextCommand()
        {
            ExecuteCommand(_strategy[_currentActionNumber % _strategy.Count]);
            _currentActionNumber++;
        }

        private void ExecuteCommand(Commands command)
        {
            switch (command.Action)
            {
                case Actions.Attack:
                    WarriorClient.Attack(command.Time);
                    break;
                case Actions.Defend:
                    Defend(command.Time);
                    break;
                case Actions.Rest:
                    Rest(command.Time);
                    break;
                case Actions.Check:
                    Check();
                    break;
                default:
                    DoNothing();
                    break;
            }
        }
    }

    public class WarriorClient
    {
        public void Register()
        {
            

        }

        public DateTime GetBattleTime()
        {
            

        }
    }
}
