using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using static System.Net.WebRequestMethods;

namespace DemoClient
{
    public class ClientService
    {   
        /// <summary>
        /// Här skriver vi en sammanfattning
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="translator"></param>
        /// <returns>RestResponse</returns>
        public RestResponse GetTranslatedText(string text, string translator)
        {
            RestClient client = CreateClient($"https://api.funtranslations.com/translate/{translator}.json");
            return MakeRestCall(client, text);
        }
        
        private RestResponse MakeRestCall(RestClient restClient, string inputText)
        {
            using (RestClient client = restClient)
            {
                var request = new RestRequest();
                request.AddParameter("text", inputText);
                var response = client.Post(request);
                return response;
            }
        }
        
        private RestClient CreateClient(string url)
        {
            var options = new RestClientOptions(url);
            var client = new RestClient(options);
            return client;
        }          
    }
}
