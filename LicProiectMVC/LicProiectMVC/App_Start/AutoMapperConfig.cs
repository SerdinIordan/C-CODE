using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicProiectMVC.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            //sursa modelul din frontend catre destinatie model backend
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<BussinessLogic.Models.User, Models.User>();
                cfg.CreateMap<BussinessLogic.Models.Student, Models.Student>();
                cfg.CreateMap<BussinessLogic.Models.Course, Models.Course>();
                cfg.CreateMap<BussinessLogic.Models.StudentCourse, Models.StudentCourse>();
            });

        }
    }
}