using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.Utils
{
    internal class RestCalls
    {
        string baseUrl = "https://sandbox-partners-api.airalo.com";
        public RestResponse RequestAccessToken()
        {
            string client_id = Environment.GetEnvironmentVariable("CLIENT_ID");
            string client_secret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            if (string.IsNullOrEmpty(client_id) || string.IsNullOrEmpty(client_secret))
            {
                throw new InvalidOperationException("Client ID or Client Secret is not set in the environment variables.");
            }

            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest("/v2/token", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);
            request.AddParameter("grant_type", "client_credentials");
            RestResponse response = client.Execute(request);

            return response;
        }
    }
}
