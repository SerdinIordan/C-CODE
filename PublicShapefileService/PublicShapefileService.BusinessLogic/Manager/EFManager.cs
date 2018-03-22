using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using PublicShapefileService.Common.Models;
using PublicShapefileService.DataLayer;


namespace PublicShapefileService.BusinessLogic.Manager
{

    // Manager of Entity Framework methods to register data
    class EFManager
    {
        // second step of the forward flux. Registers the request from the first form
        public void RegisterRequest (ShapefileRequest _request)
        {
            var db = PublicShapefileServiceContext.Instance;
            db.SaveShapefileRequest(_request);
        }

        // second step of the return flux. Registers the resolution from the second form
        public void RegisterResolution (OperatorResolution _resolution)
        {
            var SHRequestId  = _resolution.ShapefileRequest.ShapefileRequestId;
            var db =PublicShapefileServiceContext.Instance;
            var ORId = db.SaveOperatorResolution(_resolution).OperatorResolutionId;

            var or = db.GetOperatorResolutionById(ORId);
            
        }
    }
}
