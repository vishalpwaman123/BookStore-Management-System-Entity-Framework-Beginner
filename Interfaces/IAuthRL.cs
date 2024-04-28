using BookStoreManagementSystem.Models;
using System.Threading.Tasks;

namespace BookStoreManagementSystem.Interfaces
{
    public interface IAuthRL
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);
        public Task<SignInResponse> SignIn(SignInRequest request);
    }
}
