namespace OnlineBookShop.Tests.ManagementTests
{
    using System.Net;
    using NUnit.Framework;
    using Models;
    using RestSharp;
    using RestSharp.Serialization.Json;
    using System.Collections.Generic;
    class InventoryTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;
        private string _token;


        public InventoryTests()
        {
            _client = new RestClient(BaseURL);
        }


        [Test]
        public void Retrieve_Inventory_Books_Types_With_Valid_User_Test()
        {
            // Log in With Admin User
            var request = new RestRequest(BaseURL + "/Identity/Login", Method.POST);
            var newUser = new
            {
                username = "admin",
                password = "123"
            };
            request.AddJsonBody(newUser);
            var logInResponse = _client.Execute(request);
            var token = new JsonDeserializer().Deserialize<LoginResponseModel>(logInResponse);
            _token = token.Token;
            Assert.AreEqual(HttpStatusCode.OK, logInResponse.StatusCode);

            // See inventory
            request = new RestRequest(BaseURL + "/Management/Inventory", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var bookInventory = new JsonDeserializer().Deserialize<List<BookForInventoryViewModel>>(response);

            foreach (var book in bookInventory)
            {
                string idType = book.Id.GetType().Name;
                Assert.AreEqual(idType, "String");

                string titleType = book.Title.GetType().Name;
                Assert.AreEqual(titleType, "String");

                string bodyType = book.ISBN.GetType().Name;
                Assert.AreEqual(bodyType, "String");

                string authorType = book.Author.GetType().Name;
                Assert.AreEqual(authorType, "String");

                string yearType = book.Year.GetType().Name;
                Assert.AreEqual(yearType, "Int32");

                string priceType = book.Price.GetType().Name;
                Assert.AreEqual(priceType, "Decimal");

                string numberOfPagesType = book.NumberOfPages.GetType().Name;
                Assert.AreEqual(numberOfPagesType, "Int32");

                string quantityType = book.Quantity.GetType().Name;
                Assert.AreEqual(quantityType, "Int32");

                string numberOfPurchasesType = book.NumberOfPurchases.GetType().Name;
                Assert.AreEqual(numberOfPurchasesType, "Int32");

                string quantityLimitType = book.QuantityLimit.GetType().Name;
                Assert.AreEqual(quantityLimitType, "Int32");
            }
        }

        [Test]
        public void Retrieve_Inventory_Books_Types_With_Invalid_User_Test()
        {
            // Log in With Admin User
            var request = new RestRequest(BaseURL + "/Identity/Login", Method.POST);
            var newUser = new
            {
                username = "user",
                password = "123"
            };
            request.AddJsonBody(newUser);
            var logInResponse = _client.Execute(request);
            var token = new JsonDeserializer().Deserialize<LoginResponseModel>(logInResponse);
            _token = token.Token;
            Assert.AreEqual(HttpStatusCode.OK, logInResponse.StatusCode);

            // See inventory
            request = new RestRequest(BaseURL + "/Management/Inventory", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
