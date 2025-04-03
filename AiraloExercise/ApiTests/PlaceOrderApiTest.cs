using AiraloExercise.ApiTests.OrderResponseModels;
using AiraloExercise.ApiTests.SimListResponseModels;
using AiraloExercise.ApiTests.Utils;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace AiraloExercise.ApiTests
{
    internal class PlaceOrderApiTest
    {

        RestCalls restCalls = new RestCalls();
        ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
        string token;
        int quantity = 6;
        string package_id = "merhaba-7days-1gb";
        string description = $"Boris 6 merhaba-7days-1gb {DateTime.Now.ToString("yyyyMMddHHmmSS")}";

        [OneTimeSetUp]
        public void Setup()
        {
            RestResponse authResponse = restCalls.RequestAccessToken();
            token = extractResponseData.ExtractToken(authResponse.Content);
            Assert.That(authResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                $"Expected status code 200, but got {authResponse.StatusCode}");
        }

        [Test]
        [Order(1)]
        public void PlaceOrderAndGetListOfeSim()
        {
            RestResponse placeOrderResponse = restCalls.SubmitOrder(token, quantity, package_id, description);
            Assert.That(placeOrderResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                $"Expected status code 200, but got {placeOrderResponse.StatusCode}");

            OrderResponseModel orderResponse = JsonConvert.DeserializeObject<OrderResponseModel>(placeOrderResponse.Content);

            string responsePackageId = orderResponse.data.package_id;
            int responseQuantity = orderResponse.data.quantity;
            string responseDescription = orderResponse.data.description;
            int validity = orderResponse.data.validity;
            string data = orderResponse.data.data;
            double price = orderResponse.data.price;

            Assert.Multiple(() =>
            {
                Assert.That(orderResponse.data, Is.Not.Null, "No data returned.");
                Assert.That(responsePackageId, Is.EqualTo("merhaba-7days-1gb"), $"Package id {responsePackageId} is not expected.");
                Assert.That(responseQuantity, Is.EqualTo(6), $"Expected quantity is 6, but was {responseQuantity}.");
                Assert.That(responseDescription, Is.EqualTo(description), $"Expected description is \"{description}\", but was \"{responseDescription}.\"");
                Assert.That(validity, Is.EqualTo(7), $"Expected validity is 7 days, but was {validity} days.");
                Assert.That(data, Is.EqualTo("1 GB"), $"Expected data is 1 GB, but was {data}.");
                Assert.That(price, Is.EqualTo(4.5), $"Expected price is 4.5, but was {price}.");
                Assert.That(orderResponse.data.sims.Count, Is.EqualTo(6), $"Expected sim count to be 6, but was {orderResponse.data.sims.Count}");
            });


        }
        [Test]
        [Order(2)]
        public void GetListOfSimCards()
        {
            string dateForFilter = DateTime.Now.ToString("yyyy-MM-dd");
            string createdAtFilter = $"{dateForFilter} - {dateForFilter}";
            RestResponse getSimListResponse = restCalls.GetListOfeSims(token, "order", createdAtFilter);

            Assert.That(getSimListResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                $"Expected status code 200, but got {getSimListResponse.StatusCode}");

            SimListResponseModel simListResponse = JsonConvert.DeserializeObject<SimListResponseModel>(getSimListResponse.Content);
            List<SimData> simsToVerify = new List<SimData>();

            foreach (var sim in simListResponse.data)
            {
                if (sim.simable.description == description)
                {
                    simsToVerify.Add(sim);
                }
            }

            Assert.That(simsToVerify.Count, Is.EqualTo(6));

            foreach (var sim in simsToVerify)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(sim.simable.package_id, Is.EqualTo(package_id),
                        $"Package id {sim.simable.package_id} is not expected.");
                    Assert.That(sim.simable.quantity, Is.EqualTo(quantity),
                        $"Expected quantity is 6, but was {sim.simable.quantity}.");
                    Assert.That(sim.simable.description, Is.EqualTo(description),
                        $"Expected description is \"{description}\", but was \"{sim.simable.description}.\"");
                    Assert.That(sim.simable.validity, Is.EqualTo("7"),
                        $"Expected validity is 7 days, but was \"{sim.simable.validity}\" days.");
                    Assert.That(sim.simable.data, Is.EqualTo("1 GB"),
                        $"Expected data is 1 GB, but was {sim.simable.data}.");
                    Assert.That(sim.simable.price, Is.EqualTo("4.5"),
                        $"Expected price is 4.5, but was {sim.simable.price}.");
                });
            }
        }

    }
}
