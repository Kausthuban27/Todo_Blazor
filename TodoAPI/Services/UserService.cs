using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoAPI.Interface;
using TodoAPI.Model;
using TodoAPI.ViewModel;

namespace TodoAPI.Services
{
    public class UserService : IUser
    {
        private readonly TodoDbContext _db;
        private readonly IMapper _map;
        public UserService(TodoDbContext db, IMapper mapper)
        {
            _db = db;
            _map = mapper;
        }
        public bool AddUser(UserData user)
        {
            var userDetails = _map.Map<User>(user);
            if(userDetails != null)
            {
                var userData = _db.Users.Where(u => u.Username == userDetails.Username);
                if(!userData.Any())
                {
                    _db.Add(userDetails);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool GetUser(string username, string password)
        {
            var user = _db.Users.Where(u => u.Username == username && u.Password == password);
            if(user != null && user.Any()) 
            {
                return true;
            }
            return false;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
