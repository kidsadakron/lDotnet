using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services.Interface
{
    public interface IUserService
    {
        string GenerateToken(int userId);
    }
}