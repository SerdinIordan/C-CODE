using PublicShapefileService.Common.Models;
using PublicShapefileService.BusinessLogic.ReturnFlux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.BusinessLogic.Interfaces
{

    // interface used for IoC container implementation
    public interface IManager
    {
        void StartForwardFlux(ShapefileRequest _request);
        DownloadPackage StartReturnFlux(OperatorResolution _resolution);
    }
}
