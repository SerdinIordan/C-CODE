using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicShapefileService.Common.Models;

namespace PublicShapefileService.BusinessLogic.ForwardFlux
{
    class Validator
    {
        //First step of the flux. Validating the operator
        public Boolean ValidateRequest(ShapefileRequest _request)
        {
            if (_request.SolicitantName == null)
            {
                return false;
            }

            if (_request.Locality == null)
            {
                return false;
            }
           
            if (_request.CUI == null)
            {
                return false;
            }
            if (_request.SolicitantEmail == null)
            {
                return false;
            }
            if (_request.RequestDetails == null)
            {
                return false;
            }
            if (_request.Layers == null)
            {
                return false;
            }
            return true;
        }
    }
}
