using BussinessLogic.Models;
using EFProject.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicProiectMVC.Models.EntitiesValidation
{
    public class ValidateUser
    {
        public static User IsValid(string _username, string _password)
        {
              var userRepository = new UserRepository();
              var UserCheck = userRepository.GetUserByUsernamePassword(_username, _password);
             var modelViewUserCheck= AutoMapper.Mapper.Map<BussinessLogic.Models.User,Models.User>(UserCheck);
             return modelViewUserCheck;
        }
    }
}