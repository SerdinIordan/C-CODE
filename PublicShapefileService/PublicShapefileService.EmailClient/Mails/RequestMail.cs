using PublicShapefileService.Common.Models;
using PublicShapefileService.EmailClient.Services;
using System;
using System.Net.Mail;

namespace PublicShapefileService.EmailClient.Models
{
    public class RequestMail : BaseMail, IMailMessage
    {
        public RequestMail() : base()
        {
        }

        public void SendMail(ShapefileRequest solicitant) // de primit shapefilerequest
        {
            MailMessage _requestMail = new MailMessage();
            _requestMail.IsBodyHtml = true;

            try
            {
                _requestMail.From = new MailAddress(_UserName, "Apele Romane");
                foreach (string _operator in _Operators)
                {
                    _requestMail.To.Add(new MailAddress(_operator, "Operator"));
                }

                _requestMail.Subject = "Cerere";

//#warning check templates link
                string _Body = System.IO.File.ReadAllText("../../Templates/Request.html");
                _Body = _Body.Replace("__NumeSolicitant__", solicitant.SolicitantName);
                _Body = _Body.Replace("__CUI__", solicitant.CUI);
               // _Body = _Body.Replace("__Localitate__", solicitant.Locality);
                _Body = _Body.Replace("__EmailSolicitant__", solicitant.SolicitantEmail);
                _Body = _Body.Replace("__DetaliiCerere__", solicitant.RequestDetails);

//#warning replace with LINK/function/??
                //_Body = _Body.Replace("__RequestLink__", "Get link function with solicitant.ShapefileRequestId");

                _requestMail.Body = _Body;

                _SmtpClient.Send(_requestMail);
                Console.WriteLine("Request Mail send");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
