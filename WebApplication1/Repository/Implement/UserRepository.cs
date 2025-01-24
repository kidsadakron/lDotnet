using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Modals.Entities;
using WebApplication1.Models.Entities;
using WebApplication1.Repository.Interface;
using WebApplication1.Services.Implement;

namespace WebApplication1.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserModel GetUserById(int id)
        {
            UserModel user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            return user;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> users = _context.Users.ToList();
            return users;
        }

        public bool InsertUser(UserModel user)
        {
            bool status = false;
            try
            {
                user.Id = 0;
                _context.Users.Add(user);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return status;

        }

        public bool RemoveUserById(int id)
        {
            bool status = false;
            try
            {
                UserModel user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                _context.Users.Remove(user);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return status;
        }

        public bool UpdateUser(int id, string name)
        {
            bool status = false;
            try
            {
                UserModel _user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                _user.Name = name;
                _context.Users.Update(_user);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return status;
        }
    }
}