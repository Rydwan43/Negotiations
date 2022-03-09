using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Negotiations.Application.Models;

namespace Negotiations.Application.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto userDto);
        string GenerateJwt(LoginUserDto userDto);
    }
}