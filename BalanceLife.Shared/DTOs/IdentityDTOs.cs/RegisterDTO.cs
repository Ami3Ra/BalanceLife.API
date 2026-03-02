using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.IdentityDTOs.cs
{
    public record RegisterDTO([EmailAddress]string Email,string DisplayName,string UserName,string Password);
    
}
