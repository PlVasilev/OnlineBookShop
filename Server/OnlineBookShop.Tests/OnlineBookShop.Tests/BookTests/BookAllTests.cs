using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using OnlineBookShop.Tests.Models;
using RestSharp;
using RestSharp.Serialization.Json;

namespace OnlineBookShop.Tests.BookTests
{
    class BookAllTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;
        private string _token;


        public BookAllTests()
        {
            _client = new RestClient(BaseURL);
        }


        [Test]
        public void Retrieve_All_Books_Types_With_Valid_User_Test()
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
            request = new RestRequest(BaseURL + "/Book/All", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var bookInventory = new JsonDeserializer().Deserialize<List<BookListViewModel>>(response);

            foreach (var book in bookInventory)
            {
                string idType = book.Id.GetType().Name;
                Assert.AreEqual(idType, "String");

                string titleType = book.Title.GetType().Name;
                Assert.AreEqual(titleType, "String");

                string bodyType = book.BookImage.GetType().Name;
                Assert.AreEqual(bodyType, "String");

                string authorType = book.Author.GetType().Name;
                Assert.AreEqual(authorType, "String");

                string priceType = book.Price.GetType().Name;
                Assert.AreEqual(priceType, "Decimal");

                string quantityType = book.Quantity.GetType().Name;
                Assert.AreEqual(quantityType, "Int32");

                string numberOfPurchasesType = book.NumberOfPurchases.GetType().Name;
                Assert.AreEqual(numberOfPurchasesType, "Int32");

                string isLimitedType = book.IsLimited.GetType().Name;
                Assert.AreEqual(isLimitedType, "Boolean");
            }
        }

        [Test]
        public void Retrieve_All_Books_Types_With_No_User_Test()
        {
            var request = new RestRequest(BaseURL + "/Book/All", Method.GET);
            
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
