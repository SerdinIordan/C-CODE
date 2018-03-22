using PublicShapefileService.EmailClient.Models;
using PublicShapefileService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicShapefileService.BusinessLogic.ForwardFlux
{

    /// <summary>
    /// Manages the process of sending the request/response emails
    /// </summary>
    class MailManager
    {
        //Last step of the forward flux. Sends the request email to the operator for later aproval/rejection
        public void SendRequest(ShapefileRequest _request)
        {
            RequestMail forwardMail = new RequestMail();
            forwardMail.SendMail(_request);
        }

        //Last step of the return flux. Sends the response email to the requesting party
        public void SendResponse(OperatorResolution _resolution)
        {
            switch (_resolution.Resolution)
            {
                case true:
                    ApproveMail approveMail = new ApproveMail();
                    approveMail.SendMail(_resolution.ShapefileRequest);
                    break;
                case false:
                    RejectMail rejectMail = new RejectMail();
                    rejectMail.SendMail(_resolution.ShapefileRequest);
                    break;
            }

        }
    }
}
