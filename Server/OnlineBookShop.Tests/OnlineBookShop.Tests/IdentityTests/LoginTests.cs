using OnlineBookShop.Tests.Models;
using RestSharp.Serialization.Json;

namespace OnlineBookShop.Tests.IdentityTests
{
    using System.Net;
    using NUnit.Framework;
    using RestSharp;
    class LoginTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;

        public LoginTests()
        {
            _client = new RestClient(BaseURL);
        }

        [TestCase("",  "123", TestName = "Invalid_UserName")]
        [TestCase("testUser",  "", TestName = "Invalid_Password")]
     

        public void Test_LogIn_With_Invalid_Data(string username,  string password)
        {
            var request = new RestRequest(BaseURL + "/Identity/Login", Method.POST);
            var newUser = new
            {
                username,
                password
            };
            request.AddJsonBody(newUser);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestCase("user1", "123", TestName = "Not_Existing_Username")]
        [TestCase("user", "1234", TestName = "Wrong_Password")]
        public void Test_LogIn_With_Invalid_Credentials(string username, string password)
        {
            var request = new RestRequest(BaseURL + "/Identity/Login", Method.POST);
            var newUser = new
            {
                username,
                password
            };
            request.AddJsonBody(newUser);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [TestCase("user", "123", TestName = "Valid_Credentials")]
        public void Test_LogIn_With_Valid_Credentials(string username, string password)
        {
            var request = new RestRequest(BaseURL + "/Identity/Login", Method.POST);
            var newUser = new
            {
                username,
                password
            };
            request.AddJsonBody(newUser);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.ContentType.StartsWith("application/json"));

            var token = new JsonDeserializer().Deserialize<LoginResponseModel>(response);
            Assert.IsTrue(token.Token != null);
            
        }
    }
}
