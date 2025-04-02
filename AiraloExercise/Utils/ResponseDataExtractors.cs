using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise.Utils
{
    public class ResponseDataExtractors
    {
        public string ExtractToken(string jsonResponse)
        {
            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new ArgumentException("The JSON response is empty or null", nameof(jsonResponse));
            }
            try
            {
                JObject jsonObject = JObject.Parse(jsonResponse);
                string token = jsonObject["data"]?["access_token"]?.ToString();

                if (string.IsNullOrEmpty(token))
                {

                    throw new KeyNotFoundException($"The key \"access_token\" was not found in the 'data' object of the JSON response.");
                }

                return token;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error parsing the JSON response.", ex);
            }
        }
    }
}
