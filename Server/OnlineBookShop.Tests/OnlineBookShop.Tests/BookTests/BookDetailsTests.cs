using System;
using System.Net;
using NUnit.Framework;
using OnlineBookShop.Tests.Models;
using RestSharp;
using RestSharp.Serialization.Json;

namespace OnlineBookShop.Tests.BookTests
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


        [TestCase("title", "description", "summary", "1234567891011",
            "https://images.unsplash.com/photo-1598618443855-232ee0f819f6?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=667&q=80",
            "author", 100, 10, 1000, 20, 40, 5, TestName = "Get_Book_Whit_Valid_ID")]
        public void Test_Get_Book_Whit_Valid_ID(
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
            Assert.IsTrue(responseModel != null);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // 2 Get Book Details

            request = new RestRequest(BaseURL + $"/Book/{responseModel.BookId}", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

            response = _client.Execute(request);
            var book = new JsonDeserializer().Deserialize<BookDetailsViewModel>(response);

            string idType = book.Id.GetType().Name;
            Assert.AreEqual(idType, "String");

            string titleType = book.Title.GetType().Name;
            Assert.AreEqual(titleType, "String");

            string descriptionType = book.Description.GetType().Name;
            Assert.AreEqual(descriptionType, "String");

            string summaryDescriptionType = book.SummaryDescription.GetType().Name;
            Assert.AreEqual(summaryDescriptionType, "String");

            string bookImageType = book.BookImage.GetType().Name;
            Assert.AreEqual(bookImageType, "String");

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

        [Test]
        public void Test_Get_Book_Whit_UnValid_ID()
        {
            var request = new RestRequest(BaseURL + $"/Book/{Guid.NewGuid().ToString()}", Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _token));

             var response = _client.Execute(request);
             Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
