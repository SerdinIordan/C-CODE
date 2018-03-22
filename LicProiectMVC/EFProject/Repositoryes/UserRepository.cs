using BussinessLogic.Models;
using BussinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Repositoryes
{
    public class UserRepository:IUserService
    {
        private SchoolDBContext schoolDBFirstContext =
              SchoolDBContext.Instance;

        public User GetByID(int id)
        {
            var user = schoolDBFirstContext.User.Where(s => s.UserId == id).FirstOrDefault();
            return user;
        }
        public User GetUserByUsernamePassword(string Username, string Password)
        {
            var user = schoolDBFirstContext.User.Where(s => s.Username.Equals(Username)
            && s.Password.Equals(Password)).FirstOrDefault();
            return user;
        }

        public User Save(User user)
        {
            schoolDBFirstContext.User.Add(user);
            schoolDBFirstContext.SaveChanges();
            return user;
        }
        public User Update(User user)
        {
            schoolDBFirstContext.User.Update(user);
            schoolDBFirstContext.SaveChanges();
            return user;
        }
        public User Delete(User user)
        {
            schoolDBFirstContext.User.Remove(user);
            schoolDBFirstContext.SaveChanges();
            return user;
        }

        public List<User> GetAll()
        {
            var listUsers= schoolDBFirstContext.User.ToList();
            return listUsers;
        }
    }
}
