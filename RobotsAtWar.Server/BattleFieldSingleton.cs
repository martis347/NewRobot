﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsAtWar.Server
{
    public class BattleFieldSingleton
    {

        private static BattleField _battleField;

        public static BattleField BattleField
        {
            get
            {
                if (_battleField == null)
                {
                    _battleField = new BattleField();
                }
                return _battleField;
                
            }
        }
    }
}
