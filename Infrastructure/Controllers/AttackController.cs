using System;
using System.Web.Http;
using Business;

namespace Infrastructure.Controllers
{
    public class AttackController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "Service is running :)";
        }

        // POST api/<controller>
        public int Post([FromBody] string strength)
        {
            return BattleField.Warrior1.GetAttacked(Int32.Parse(strength));
        }
    }
}