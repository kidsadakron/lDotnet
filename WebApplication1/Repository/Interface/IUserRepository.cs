using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Entities;

namespace WebApplication1.Repository.Interface
{
    public interface IUserRepository
    {
        List<UserModel> GetUsers();
        UserModel GetUserById(int id);
        bool RemoveUserById(int id);
        bool UpdateUser(int id, string name);
        bool InsertUser(UserModel user);
    }
}