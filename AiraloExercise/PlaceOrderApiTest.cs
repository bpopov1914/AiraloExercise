using AiraloExercise.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AiraloExercise
{
    internal class PlaceOrderApiTest
    {

        RestCalls restCalls = new RestCalls();
        ResponseDataExtractors extractResponseData = new ResponseDataExtractors();

        [Test]
        public void PlaceOrder()
        {
            RestResponse authResponse = restCalls.RequestAccessToken();
            string token = extractResponseData.ExtractToken(authResponse.Content);
            Assert.That(authResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                $"Expected status code 200, but got {authResponse.StatusCode}");
        }
    }
}
