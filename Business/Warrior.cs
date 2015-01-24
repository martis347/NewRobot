using System;
using System.Collections.Generic;
using System.Threading;
using log4net;

namespace Business
{
    public enum States { Attacking, Defending, Resting, Checking, Interrupted, DoingNothing }

    public enum Actions {Attack, Defend, Rest, Check, DoNothing}

    public class Warrior
    {
        private readonly Opponent _opponent;
        private readonly List<Commands> _strategy;
        private int _currentActionNumber = 0;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Warrior));

        private States State { get; set; }
        private int Life { get; set; }
        private string Name { get; set; }

        public int GetLife() { return Life; }

        public Warrior (Opponent opponent, List<Commands> strategy)
        {
            Life = 10;
            _opponent = opponent;
            _strategy = strategy;
        }


        public int Attack(int time)
        {
            if (time < 1 || time > 3)
            {
                Logger.Info("Invalid time!");
                State = States.DoingNothing;
                return 0;
            }

            State = States.Attacking;
            var damage = (time * 2) - 1; // TODO: think about ballance.
            Logger.Info("You are trying to deal " + damage + " damage!");

            ChargeAction(time);

            if (State == States.Interrupted)
            {
                Logger.Info("You were trying to attack, but you were interrupted");
                return 0;
            }

            _opponent.FeelMyWrath(damage);

            State = States.DoingNothing;

            return damage;
        }

        public string Check()
        {
            var opponentState = _opponent.GetState();

            Logger.Info("You are checking the State of your enemy");

            Logger.InfoFormat("The State of your enemy is: {0}", opponentState);

            return opponentState;
        }


        // TODO: change int to enum?
        public int GetAttacked(int damage)
        {
            if (damage < 0 || damage > 5)
            {
                Logger.Info("Invalid damage!");
                return 0;
            }

            if (State == States.Defending || damage == 0)
            {
                Logger.Info("You didn`t lose any health because you were defending!");
                return 0;
            }

            if (State == States.Attacking || State == States.Resting)
                State = States.Interrupted;

            Logger.InfoFormat("I was damaged for {0} hitpoints", damage);

            Life -= damage;

            return damage;
        }


        public void Defend(int time)
        {
            if (time < 1)
            {
                Logger.Info("Invalid time!");
                return;
            }
            State = States.Defending;

            Logger.Info("You are defending for " + time + "s.");

            ChargeAction(time);

            State = States.DoingNothing;
        }

        public int Rest(int time)
        {
            if (time < 1 || time > 10)
            {
                Logger.Info("Invalid time!");
                return 0;
            }
            State = States.Resting;

            Logger.Info("You are resting for " + time + "s");

            ChargeAction(time);

            // TODO: think about rules ... this might be a bad ballance.
            if (State == States.Interrupted)
            {
                Logger.Info("You were trying to rest, but you were interrupted");
                return 0;
            }

            State = States.DoingNothing;

            var healPoints = (int) Math.Pow(2.0, time);

            Life += healPoints;

            return healPoints;
        }

        public void DoNothing()
        {
            State = States.DoingNothing;
            Logger.Info("Idling");
        }

        private void ChargeAction(int time)
        {
            for (int i = 0; i < time; i++)
            {
                Thread.Sleep(1000);
                if (State == States.Interrupted)
                    break;
            }
        }

        public bool IsAlive()
        {
            return Life > 0;
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
                    Attack(command.Time);
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

    public class Commands
    {
        public Actions Action;
        public int Time;

        public Commands(Actions action, int time)
        {
            Action = action;
            Time = time;
        }
    }
}