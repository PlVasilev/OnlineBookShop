namespace OnlineBookShop.Tests.ManagementTests
{
    using System;
    using System.Net;
    using NUnit.Framework;
    using Models;
    using RestSharp;
    using RestSharp.Serialization.Json;

    class UpdateBookTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;
        private string _token;

        public UpdateBookTests()
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

        [TestCase("title", "description", "summary", "1234567891011",
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "author", 100, 10, 1000, 20, 40, 5, TestName = "Update_Book_Whit_Valid_Id")]
        public void Test_Update_Book_Whit_Valid_Id(
            string title,
            string description,
            string summaryDescription,
            string isbn,
            string bookImage,
            string author,
            int year,
            decimal price,
            int numberOfPages,
            int quantity,
            int quantityLimit,
            int numberOfPurchases)
        {
            // 1 Create Book
            var request = new RestRequest(BaseURL + "/Management/CreateBook", Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var newBook = new
            {
                title,
                description,
                summaryDescription,
                isbn,
                bookImage,
                author,
                year,
                price,
                numberOfPages,
                quantity,
                quantityLimit,
                numberOfPurchases
            };
            request.AddJsonBody(newBook);

            IRestResponse response = _client.Execute(request);
            var responseModel = new JsonDeserializer().Deserialize<CreateBookResponseModel>(response);
            var bookId = responseModel.BookId;

            // 2 UpdateBook

            request = new RestRequest(BaseURL + "/Management/UpdateBook", Method.PUT);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var updatedBook = new
            {
                id = bookId,
                description = "new Description",
                summaryDescription = "new summeryDescription",
                bookImage = "https://images.unsplash.com/photo-1507738978512-35798112892c?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1050&q=80",
                price = 1000,
                quantity= 1000,
                quantityLimit = 2000,

            };

            request.AddJsonBody(updatedBook);
            response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase("title", "description", "summary", "1234567891011",
           "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
           "author", 100, 10, 1000, 20, 40, 5, TestName = "Update_Book_Whit_Invalid_Id")]
        public void Test_Update_Book_Whit_Invalid_Id(
           string title,
           string description,
           string summaryDescription,
           string isbn,
           string bookImage,
           string author,
           int year,
           decimal price,
           int numberOfPages,
           int quantity,
           int quantityLimit,
           int numberOfPurchases)
        {
            // 1 Create Book
            var request = new RestRequest(BaseURL + "/Management/CreateBook", Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var newBook = new
            {
                title,
                description,
                summaryDescription,
                isbn,
                bookImage,
                author,
                year,
                price,
                numberOfPages,
                quantity,
                quantityLimit,
                numberOfPurchases
            };
            request.AddJsonBody(newBook);

            IRestResponse response = _client.Execute(request);
            var responseModel = new JsonDeserializer().Deserialize<CreateBookResponseModel>(response);
            var bookId = responseModel.BookId;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // 2 UpdateBook

            request = new RestRequest(BaseURL + "/Management/UpdateBook", Method.PUT);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            var updatedBook = new
            {
                id = Guid.NewGuid().ToString(),
                description = "new Description",
                summaryDescription = "new summeryDescription",
                bookImage = "https://images.unsplash.com/photo-1507738978512-35798112892c?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1050&q=80",
                price = 1000,
                quantity = 1000,
                quantityLimit = 2000,

            };

            request.AddJsonBody(updatedBook);
            response = _client.Execute(request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

