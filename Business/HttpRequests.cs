using System;
using System.IO;
using System.Net;
using System.Text;
using Business.Tools;

namespace Business
{
    //TODO: rename
    public class HttpRequests
    {
        private const string OpponentUrl = "OpponentUrl";

        public static void WaitForOpponent()
        {
            while (true)
            {
                try
                {
                    // TODO: move http logic to a wrapper class
                    var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OpponentUrl) + "attack");
                    request.GetResponse();

                    Console.WriteLine("Found!!!");

                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Waiting...");
                }
            }
        }

        public string Get()
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OpponentUrl) + "check");

            var response = request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public bool IsAlive()
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OpponentUrl) + "dead");

            var response = request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString.Equals("true");
        }

        // TODO: ether business or infrastructure
        public int Post(string action, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(ConfigSettings.ReadSetting(OpponentUrl) + action);
            request.Timeout = 100000;

            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return Int32.Parse(responseString);
        } 
    }
}