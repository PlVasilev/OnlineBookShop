

namespace OnlineBookShop.Tests.CartTests
{
    using System.Collections.Generic;
    using System.Net;
    using NUnit.Framework;
    using Models;
    using RestSharp;
    using RestSharp.Serialization.Json;
    class GetCartTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;
        private string _token;


        public GetCartTests()
        {
            _client = new RestClient(BaseURL);
        }


        [Test]
        public void Retrieve_All_CartBooks_Types_With_Valid_User_Test()
        {
            // Log in With User
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
            request = new RestRequest(BaseURL + "/Cart/GetCart", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var cartBooks = new JsonDeserializer().Deserialize<List<CartBooksViewModel>>(response);

            foreach (var cartBook in cartBooks)
            {
                string cartIdType = cartBook.CartId.GetType().Name;
                Assert.AreEqual(cartIdType, "String");

                string titleType = cartBook.Title.GetType().Name;
                Assert.AreEqual(titleType, "String");

                string bookIdType = cartBook.BookId.GetType().Name;
                Assert.AreEqual(bookIdType, "String");

                string authorType = cartBook.Author.GetType().Name;
                Assert.AreEqual(authorType, "String");

                string priceType = cartBook.Price.GetType().Name;
                Assert.AreEqual(priceType, "Decimal");

                string quantityType = cartBook.Quantity.GetType().Name;
                Assert.AreEqual(quantityType, "Int32");

                string totalPriceType = cartBook.TotalPrice.GetType().Name;
                Assert.AreEqual(totalPriceType, "Int32");

            }
        }

        [Test]
        public void Retrieve_All_CartBooks_Types_With_UnValid_User_Test()
        {
            // See inventory
            var request = new RestRequest(BaseURL + "/Cart/GetCart", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));
            var response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
