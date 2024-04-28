using BookStoreManagementSystem.Models;
using System.Threading.Tasks;

namespace BookStoreManagementSystem.Interfaces
{
    public interface IBookRL
    {
        public Task<InsertBookResponse> InsertBook(InsertBookRequest request);

        public Task<GetBookResponse> GetBook(int pageNumber, int numberOfRecordsPerPage);

        public Task<UpdateBookResponse> UpdateBook(UpdateBookRequest request);

        public Task<DeleteBookResponse> DeleteBook(DeleteBookRequest request);
    }
}
