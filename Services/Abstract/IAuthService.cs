using AwsTextract.api.Models;

namespace AwsTextract.api.Services.Abstract
{
    public interface IAuthService
    {
        Task<UserManagerResponseViewModel> RegisterUserAsync(User userObj);
        Task<UserManagerResponseViewModel> LoginUserAsync(User userObj);
    }
}
