using log4net;

namespace Business
{
    public class Opponent
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Opponent));

        private readonly HttpRequests _httpRequests;

        public Opponent()
        {
            _httpRequests = new HttpRequests();
        }

        public bool IsAlive()
        {
            return _httpRequests.IsAlive();
        }

        public string GetState()
        {
            return _httpRequests.Get();
        }

        public void FeelMyWrath(int damage)
        {
            int hitPoints = _httpRequests.Post("attack", "=" + damage);

            if (hitPoints > 0)
                Logger.InfoFormat("Opponent was damaged for {0} hitpoints", hitPoints);
            else
                Logger.Info("Opponent didn`t lose any health because he was defending!");
        }
    }
}
