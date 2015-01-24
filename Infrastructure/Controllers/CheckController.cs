using System.Web.Http;
using Business;

namespace Infrastructure.Controllers
{
    public class CheckController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return BattleField.Warrior1.Check();
        }
    }
}