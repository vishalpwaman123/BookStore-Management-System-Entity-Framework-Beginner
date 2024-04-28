using BookStoreManagementSystem.Context;
using BookStoreManagementSystem.Interfaces;
using BookStoreManagementSystem.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreManagementSystem.Services
{
    public class BookRL : IBookRL
    {
        private IConfiguration _configuration;
        private DataContext _dataContext;

        public BookRL(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<InsertBookResponse> InsertBook(InsertBookRequest request)
        {
            InsertBookResponse response = new InsertBookResponse();

            Account account = new Account(
                                _configuration["CloudinarySettings:CloudName"],
                                _configuration["CloudinarySettings:ApiKey"],
                                _configuration["CloudinarySettings:ApiSecret"]);

            var path = request.File.OpenReadStream();
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(request.File.FileName, path),
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            Book book = new Book()
            {
                BookName = request.BookName,
                BookType = request.BookType,
                BookPrice = request.BookPrice,
                BookDetails = request.BookDetails,
                BookAuthor = request.BookAuthor,
                Quantity = request.Quantity,
                BookImageUrl = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId.ToString()
            };

            _dataContext.Books.Add(book);
            var result = await _dataContext.SaveChangesAsync();

            if (result == 1)
            {
                response.IsSuccess = true;
                response.Message = "Add Book Successful";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong !";
            }

            return response;
        }

        public async Task<GetBookResponse> GetBook(int pageNumber, int numberOfRecordsPerPage)
        {
            GetBookResponse response = new GetBookResponse();
            int totalRecords = _dataContext.Books.Count();

            var result = _dataContext.Books
                .OrderBy(b => b.BookID)
                .Skip((pageNumber - 1) * numberOfRecordsPerPage)
                .Take(numberOfRecordsPerPage)
                .ToList();

            response.CurrentPage = pageNumber;
            response.TotalRecords = totalRecords;
            response.TotalPage = Convert.ToInt32(Math.Ceiling((double)totalRecords / numberOfRecordsPerPage));
            response.Data = result;

            if (result.Count > 0)
            {
                response.IsSuccess = true;
                response.Message = "Successful";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong !";
            }

            return response;
        }

        public async Task<UpdateBookResponse> UpdateBook(UpdateBookRequest request)
        {
            UpdateBookResponse response = new UpdateBookResponse();
            string url = string.Empty;
            string publicId = string.Empty;

            if (request.UpdateImage)
            {
                Account account = new Account(
                                _configuration["CloudinarySettings:CloudName"],
                                _configuration["CloudinarySettings:ApiKey"],
                                _configuration["CloudinarySettings:ApiSecret"]);


                Cloudinary cloudinary = new Cloudinary(account);
                var deletionParams = new DeletionParams(request.PublicID)
                {
                    ResourceType = ResourceType.Image
                };

                var deletionResult = cloudinary.Destroy(deletionParams);
                string Result = deletionResult.Result.ToString();
                if (Result.ToLower() != "ok")
                {
                    response.IsSuccess = false;
                    response.Message = "Something Went To Wrong In Cloudinary Destroy Method";
                    return response;
                }

                var path = request.File2.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(request.File2.FileName, path),
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                url = uploadResult.Url.ToString();
                publicId = uploadResult.PublicId.ToString();
            }

            var book = await _dataContext.Books.FindAsync(request.BookID);
            if (book == null)
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong !";
            }
            else
            {
                book.BookID = request.BookID;
                book.BookName = request.BookName;
                book.BookDetails = request.BookDetails;
                book.BookAuthor = request.BookAuthor;
                book.BookPrice = request.BookPrice;
                book.BookType = request.BookType;
                book.Quantity = request.Quantity;

                if (request.UpdateImage)
                {
                    book.BookImageUrl = url;
                    book.PublicId = publicId;
                }

                var result = await _dataContext.SaveChangesAsync();

                if (result == 1)
                {
                    response.IsSuccess = true;
                    response.Message = "Update Book Successful";
                }
            }

            return response;
        }

        public async Task<DeleteBookResponse> DeleteBook(DeleteBookRequest request)
        {
            DeleteBookResponse response = new DeleteBookResponse();
            Account account = new Account(
                                _configuration["CloudinarySettings:CloudName"],
                                _configuration["CloudinarySettings:ApiKey"],
                                _configuration["CloudinarySettings:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            var deletionParams = new DeletionParams(request.PublicID)
            {
                ResourceType = ResourceType.Image
            };

            var deletionResult = cloudinary.Destroy(deletionParams);
            string Result = deletionResult.Result.ToString();
            if (Result.ToLower() != "ok")
            {
                response.IsSuccess = false;
                response.Message = "Something Went To Wrong In Cloudinary Destroy Method";
                return response;
            }

            var book = await _dataContext.Books.FindAsync(request.BookID);
            if (book == null)
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong !";
            }
            else
            {
                _dataContext.Books.Remove(book);
                var result = await _dataContext.SaveChangesAsync();

                if (result == 1)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete Book Successful";
                }
            }

            return response;
        }
    }
}
