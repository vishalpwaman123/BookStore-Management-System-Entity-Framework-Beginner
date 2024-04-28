using BookStoreManagementSystem.Interfaces;
using BookStoreManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStoreManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRL _bookRL;

        public BookController(IBookRL bookRL)
        {
            _bookRL = bookRL;
        }

        [HttpPost]
        public async Task<ActionResult> InsertBook([FromForm] InsertBookRequest request)
        {
            InsertBookResponse response = new InsertBookResponse();
            try
            {
                response = await _bookRL.InsertBook(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetBook(int pageNumber, int numberOfRecordsPerPage)
        {
            GetBookResponse response = new GetBookResponse();
            try
            {
                response = await _bookRL.GetBook(pageNumber, numberOfRecordsPerPage);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook([FromForm] UpdateBookRequest request)
        {
            UpdateBookResponse response = new UpdateBookResponse();
            try
            {
                response = await _bookRL.UpdateBook(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBook(DeleteBookRequest request)
        {
            DeleteBookResponse response = new DeleteBookResponse();
            try
            {
                response = await _bookRL.DeleteBook(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
