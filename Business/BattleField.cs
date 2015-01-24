using System;
using System.Threading;
using log4net;

namespace Business
{
    public class BattleField
    {
        private static ILog _logger;
        public static Warrior Warrior1;
        private Opponent _opponent;

        public void StartBattle()
        {
            _logger = LogManager.GetLogger(typeof(BattleField));

            StartBattleCore();
        }

        private void StartBattleCore()
        {
            _opponent = new Opponent();

            Warrior1 = new Warrior(_opponent, Strategy.YourStrategy());

            Counter();

            FightLoop();
        }

        private void FightLoop()
        {
            while (true)
            {
                if (!Warrior1.IsAlive())
                {
                    _logger.Info("..........NOT WINNER..........");
                    Thread.Sleep(5000);
                    Environment.Exit(0);
                }
                if (!_opponent.IsAlive())
                {
                    _logger.Info(".............WINNER............");
                    break;
                }
                Warrior1.ExecuteNextCommand();
            }
        }

        private void Counter()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Battle starts in 3");
            Thread.Sleep(1000);
            Console.WriteLine("Battle starts in 2");
            Thread.Sleep(1000);
            Console.WriteLine("Battle starts in 1");
            Thread.Sleep(1000);
        }
    }
}