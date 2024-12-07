using Application.Dto_s.UserDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IRegisterationService
    {

       public Task<(bool Success, string Message)> RegisterAsync(RegistrationRequestDto request);
       public Task<(bool Success, string Message, string Token)> LoginAsync(LoginRequestDto requestDto);
       public Task<(bool Success, string Message)> LogoutAsync();
       public Task<(bool Success, string Message)> UserApprovalAsync();
       public Task<(bool Success, string Message)> UserDetailsAsync();
       public Task<(bool Success, string Message)> UserProfileAsync();
      

    }
}
