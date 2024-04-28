using BookStoreManagementSystem.Context;
using BookStoreManagementSystem.Interfaces;
using BookStoreManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreManagementSystem.Services
{
    public class AuthRL : IAuthRL
    {
        private readonly DataContext _dataContext;

        public AuthRL(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            var result = _dataContext.Users.Where(id => id.Username == request.UserName && id.Password == request.Password).FirstOrDefault();
            
            if (result != null)
            {
                response.IsSuccess = true;
                response.Message = "Successful";
                response.Data = new SignIn()
                {
                    UserId = result.UserId,
                    UserName = result.Username,
                    InsertionDate = result.InsertionDate.ToString("dd/MM/yyyy")
                };
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Login Unsuccessfully";
            }

            return response;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            User user = new User()
            {
                Username = request.UserName,
                Password = request.Password
            };

            if (request.Password.Equals(request.ConfigPassword))
            {
                _dataContext.Users.Add(user);
                var result = await _dataContext.SaveChangesAsync();

                if (result == 1)
                {
                    response.IsSuccess = true;
                    response.Message = "Successful";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                }
            }

            return response;
        }
    }
}
