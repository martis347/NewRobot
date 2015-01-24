using System.Collections.Generic;

namespace Business
{
    class Strategy
    {
        public static List<Commands> YourStrategy()
        {
            /*List<Commands> strategy = new List<Commands>
            {
                new Commands(Actions.Attack, 2),
                new Commands(Actions.Attack, 3),
                new Commands(Actions.Rest, 2),
                new Commands(Actions.Defend, 2),
                new Commands(Actions.Attack, 1),
                new Commands(Actions.Attack, 3)
            };*/

            List<Commands> strategy = new List<Commands>
            {
                new Commands(Actions.Defend, 3),
                new Commands(Actions.Attack, 1),
                new Commands(Actions.Defend, 2),
                new Commands(Actions.Rest, 2),
                new Commands(Actions.Defend, 1),
                new Commands(Actions.Attack, 2)
            };
            return strategy;
        }
    }
}
