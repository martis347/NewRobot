using System;
using System.IO;
using Business.Tools;
using log4net.Config;
using Topshelf;

namespace Infrastructure
{
    class Program
    {
        static void Main()
        {
            XmlConfigurator.Configure(new FileInfo("..\\..\\App.config"));

            HostFactory.Run(x =>
            {
                x.Service<HttpApiService>(s =>
                {
                    s.ConstructUsing(name => new HttpApiService(new Uri(ConfigSettings.ReadSetting("MyUrl"))));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartManually();
                x.SetDescription("RobotsAtWar");
                x.SetDisplayName("RobotsAtWar");
                x.SetServiceName("RobotsAtWar");
            });
        }
    }
}
