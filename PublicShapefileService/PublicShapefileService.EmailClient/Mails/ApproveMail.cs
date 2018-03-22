using PublicShapefileService.Common.Models;
using PublicShapefileService.EmailClient.Services;
using System;
using System.Net.Mail;

namespace PublicShapefileService.EmailClient.Models
{
    public class ApproveMail : BaseMail, IMailMessage
    {
        public ApproveMail() : base()
        {
        }

        public void SendMail(ShapefileRequest solicitant)
        {
            MailMessage _aprovedMail = new MailMessage();
            _aprovedMail.IsBodyHtml = true;

            try
            {
                _aprovedMail.From = new MailAddress(_UserName, "Apele Romane");
                _aprovedMail.To.Add(new MailAddress(solicitant.SolicitantEmail, solicitant.SolicitantName));
                _aprovedMail.Subject = "Cerere Aprobata";

#warning check templates link
                string _Body = System.IO.File.ReadAllText("../../Templates/Approve.html");

#warning  TODO - Change LINK with "solicitant.DownloadLink.PublicId"
                _Body = _Body.Replace("__LINK__", "https://www.google.ro");
                _aprovedMail.Body = _Body;

                _SmtpClient.Send(_aprovedMail);
                Console.WriteLine("Approve Mail send");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
