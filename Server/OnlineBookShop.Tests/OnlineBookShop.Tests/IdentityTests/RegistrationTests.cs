namespace OnlineBookShop.Tests.IdentityTests
{
    using System.Net;
    using NUnit.Framework;
    using RestSharp;
    public class RegistrationTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;

        public RegistrationTests()
        {
            _client = new RestClient(BaseURL);
        }

        [TestCase("testUser", "testtest.test", "123", TestName = "Invalid_Email")]
        [TestCase("", "test@test.test", "123", TestName = "Invalid_UserName")]
        [TestCase("testUser", "test@test.test", "", TestName = "Invalid_Password")]
        [TestCase("user", "user@user.user", "", TestName = "Existing_Username")]
        [TestCase("user2", "user@user.user", "", TestName = "Existing_Email")]
      
        public void Test_Registration_With_Invalid_Data(string username, string email, string password)
        {
            var request = new RestRequest( BaseURL + "/Identity/Register", Method.POST);
            var newUser = new
            {
                username,
                email,
                password
            };
            request.AddJsonBody(newUser);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestCase("testUser2", "test@test.test2", "123", TestName = "Valid_parameters")]
        public void Test_Registration_With_Valid_Data(string username, string email, string password)
        {
            var request = new RestRequest(BaseURL + "/Identity/Register", Method.POST);
            var newUser = new
            {
                username,
                email,
                password
            };
            request.AddJsonBody(newUser);
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}