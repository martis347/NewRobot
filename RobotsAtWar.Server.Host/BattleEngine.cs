﻿using System;
using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Business;

namespace RobotsAtWar.Server.Host
{
    class BattleEngine
    {
        private readonly HttpSelfHostServer _server;
        private const string EventSource = "HttpApiService";

        public BattleEngine(Uri address)
        {
            if (!EventLog.SourceExists(EventSource))
            {
                EventLog.CreateEventSource(EventSource, "Application");
            }
            EventLog.WriteEntry(EventSource,
                String.Format("Creating server at {0}",
                address));
            var config = new HttpSelfHostConfiguration(address);
            config.Routes.MapHttpRoute("DefaultApi",
                "{controller}"
                

            );
            _server = new HttpSelfHostServer(config);
        }

        public void Start()
        {
            EventLog.WriteEntry(EventSource, "Opening HttpApiService server.");
            _server.OpenAsync();

           
        }

        public void Stop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
        }


    }
}
