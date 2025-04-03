using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.ApiTests.Utils
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

        public RestResponse SubmitOrder(string token, int quantity, string package_id, string? description)
        {
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest("/v2/orders", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AlwaysMultipartFormData = true;
            request.AddParameter($"quantity", quantity);
            request.AddParameter("package_id", package_id);
            request.AddParameter("description", description);
            RestResponse response = client.Execute(request);
            return response;
        }

        public RestResponse GetListOfeSims(string token, string include, string createdAt)
        {
            string filterParameters = $"include={include}&filter[created_at]={createdAt}&limit=100&page=1";
            RestClientOptions options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(120),
            };
            RestClient client = new RestClient(options);
            RestRequest request = new RestRequest($"/v2/sims?{filterParameters}", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
