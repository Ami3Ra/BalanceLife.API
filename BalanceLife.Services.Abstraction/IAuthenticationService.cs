using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared.CommonResponses;
using BalanceLife.Shared.DTOs.IdentityDTOs.cs;

namespace BalanceLife.Services.Abstraction
{
    public interface IAuthenticationService
    {
        //Login
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

        //Register
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);

        //CheckEmail
        Task<bool> CheckEmailAsync(string email);

        //GetUserByEmail
        Task<Result<UserDTO>> GetUserByEmailAsync(string email);
    }
}
