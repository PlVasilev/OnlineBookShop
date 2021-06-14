using System.Net;
using NUnit.Framework;
using OnlineBookShop.Tests.Models;
using RestSharp;
using RestSharp.Serialization.Json;

namespace OnlineBookShop.Tests.ManagementTests
{
    class CreateBookTests
    {
        const string BaseURL = "https://localhost:5001";
        RestClient _client;
        private string _token;

        public CreateBookTests()
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

        [TestCase("T", "description", "summary", "1234567891011",
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_title")]
        [TestCase("title", "D", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, 10, 20, 5, TestName = "Create_Book_Create_Book_Invalid_description")]
        [TestCase("title", "description", "S", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_summaryDescription")]
        [TestCase("title", "description", "summary", "1234567891011333", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_isbn")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "some.img", 
            "author", 100, 10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_bookImage")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "A", 100, 10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_author")]
        [TestCase("title", "description", "summary", "1234567891011",
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "Author", 10000, 10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_year")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, -10, 1000, 10, 20, 5, TestName = "Create_Book_Invalid_price")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "author", 100, 10, 0, 10, 20, 5, TestName = "Create_Book_Invalid_numberOfPages")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, -10, 20, 5, TestName = "Create_Book_Invalid_quantity")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, 10, -20, 5, TestName = "Create_Book_Invalid_quantityLimit")]
        [TestCase("title", "description", "summary", "1234567891011", 
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80", 
            "author", 100, 10, 1000, 10, 20, -5, TestName = "Create_Book_Invalid_numberOfPurchases")]
        public void Test_Create_Book_With_Invalid_Data(
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
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestCase("title", "description", "summary", "1234567891011",
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "author", 100, 10, 1000, 20, 10, 5, TestName = "Create_Book_Invalid_quantityLimit_to_quantity")]
        public void Test_Create_Book_Whit_Invalid_Quantity_QuantityLimit(
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
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [TestCase("title", "description", "summary", "1234567891011",
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "author", 100, 10, 1000, 20, 40, 5, TestName = "Create_Book_Whit_Valid_Data")]
        public void Test_Create_Book_Whit_Valid_Data(
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
            Assert.IsTrue(responseModel != null);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
