using PublicShapefileService.Common.Models;
using PublicShapefileService.EmailClient.Services;
using System;
using System.Net.Mail;

namespace PublicShapefileService.EmailClient.Models
{
    public class RejectMail : BaseMail, IMailMessage
    {
        public RejectMail() : base()
        {
        }

        public void SendMail(ShapefileRequest solicitant)
        {
            MailMessage _rejectedMail = new MailMessage();
            _rejectedMail.IsBodyHtml = true;

            try
            {
                _rejectedMail.From = new MailAddress(_UserName, "Apele Romane");
                _rejectedMail.To.Add(new MailAddress(solicitant.SolicitantEmail, solicitant.SolicitantName));
                _rejectedMail.Subject = "Cerere Refuzata";

#warning check templates link
                string _Body = System.IO.File.ReadAllText("../../Templates/Reject.html");
                _Body = _Body.Replace("__REASON__", solicitant.OperatorResolution.ResolutionDetails);
                _rejectedMail.Body = _Body;

                _SmtpClient.Send(_rejectedMail);
                Console.WriteLine("Reject Mail send");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 
    }
}
