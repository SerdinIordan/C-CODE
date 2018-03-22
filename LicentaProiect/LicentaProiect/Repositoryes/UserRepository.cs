using LicentaProiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicentaProiect.Repositoryes
{
    public class UserRepository
    {
        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;

        public User GetUserByID(int id)
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

        public User SaveUser(User user)
        {
            schoolDBFirstContext.User.Add(user);
            schoolDBFirstContext.SaveChanges();
            return user;
        }
        public User UpdateUser(User user)
        {
            schoolDBFirstContext.User.Update(user);
            schoolDBFirstContext.SaveChanges();
            return user;
        }
        public User RemoveUser(User user)
        {
            schoolDBFirstContext.User.Remove(user);
            schoolDBFirstContext.SaveChanges();
            return user;
        }


    }
}
