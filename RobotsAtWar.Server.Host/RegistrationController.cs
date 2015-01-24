using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RobotsAtWar.Server.Host
{
    public class RegistrationController : ApiController
    {

        // POST api/<controller>
        public void Post(string warriorName)
        {
            BattleFieldSingleton.BattleField.RegisterWarrior(warriorName);
        }
        
    }
}