﻿namespace OnLineBookStore.Server.Features.Management
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Infrastructure;

    [Authorize(Roles = "Admin")]
    public class ManagementController : ApiController
    {
        private readonly IManagementService _managementService;

        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpDelete]
        [Route( nameof(DeleteBook) +"/{id}")]
        public async Task<ActionResult> DeleteBook(string id)
        {
            var userId = this.User.GetId();
            var deleted = await _managementService.Delete(id, userId);
            if (!deleted)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPut] 
        [Route(nameof(UpdateBook))]
        public async Task<ActionResult<UpdateBookRequestModel>> UpdateBook(UpdateBookRequestModel model)
        {
            var userId = this.User.GetId();
            var updated = await _managementService.Update(
                model.Id,
                model.Description,
                model.SummaryDescription,
                model.BookImage,
                model.Price,
                model.Quantity,
                model.NumberOfPurchases,
                userId);

            if (!updated) return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route(nameof(CreateBook))]
        public async Task<ActionResult<CreateBookResponseModel>> CreateBook(CreateBookRequestModel model)
        {
            var userId = this.User.GetId();
            CreateBookResponseModel book = await _managementService.Create(
                model.Title,
                model.Description,
                model.SummaryDescription,
                model.ISBN,
                model.BookImage,
                model.Author,
                model.Year,
                model.Price,
                model.NumberOfPages,
                model.Quantity,
                model.NumberOfPurchases,
                userId);

            return Created(nameof(this.CreateBook), book);
        }
    }
}