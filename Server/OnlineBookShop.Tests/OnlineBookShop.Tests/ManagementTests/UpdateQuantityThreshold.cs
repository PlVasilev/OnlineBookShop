using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OnlineBookShop.Tests.Models;
using RestSharp;
using RestSharp.Serialization.Json;

namespace OnlineBookShop.Tests.ManagementTests
{
    class UpdateQuantityThreshold
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;
        private string _token;

        public UpdateQuantityThreshold()
        {
            _client = new RestClient(BaseURL);
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            var request = new RestRequest(BaseURL + "/Identity/Login", Method.POST);
            var newUser = new
            {
                username = "admin",
                password = "123"
            };
            request.AddJsonBody(newUser);
            var response = _client.Execute(request);
            var token = new JsonDeserializer().Deserialize<LoginResponseModel>(response);
            _token = token.Token;
        }

        [TestCase(-310)]
        public void Test_Quantity_Limit_Whit_Invalid_Quantity(int limit)
        {
            var request = new RestRequest(BaseURL + $"/Management/QuantityThreshold", Method.PUT);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var data = new
            {
                limit
            };
            request.AddJsonBody(data);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [TestCase(10)]
        public void Test_Quantity_Limit_Whit_Valid_Quantity(int limit)
        {
            var request = new RestRequest(BaseURL + $"/Management/QuantityThreshold", Method.PUT);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var data = new
            {
                limit
            };
            request.AddJsonBody(data);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


    }
}
