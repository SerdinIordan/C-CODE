using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicShapefileService.App_Start
{
    public class AutoMapperWebConfiguration
    {
        public static void RegisterMaps()
        {
            //sursa modelul din frontend catre destinatie model backend
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<Models.OperatorResolution, Common.Models.OperatorResolution>();
                cfg.CreateMap<Common.Models.ShapefileRequest, Models.ShapefileRequest>();
            });
            
        }
       
    }
}