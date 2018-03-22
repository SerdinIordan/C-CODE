using BussinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public interface IUserService:IEntityService<User>
    {
        User GetUserByUsernamePassword(string userName, string password);

    }
}
