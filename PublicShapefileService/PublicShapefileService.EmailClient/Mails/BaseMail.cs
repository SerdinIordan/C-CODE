using PublicShapefileService.EmailClient.Services;
using System;
using System.Configuration;
using System.Net.Mail;

namespace PublicShapefileService.EmailClient.Models
{
    public class BaseMail : ISmtpClient
    {
        public BaseMail()
        {
            ReadInformationsFromWebConfig();
            AssignInformationsToClient();
        }

        public SmtpClient _SmtpClient = new SmtpClient();

        public string _Host;
        public string _Port;
        public string _UserName;
        public string _Password;
        public string[] _Operators;

        public void ReadInformationsFromWebConfig()
        {
            try
            {
                _Host = ConfigurationManager.AppSettings["_Host"];
                _Port = ConfigurationManager.AppSettings["_Port"];
                _UserName = ConfigurationManager.AppSettings["_Username"];
                _Password = ConfigurationManager.AppSettings["_Password"];
                _Operators = ConfigurationManager.AppSettings["_Operators"].Split(',');
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                //Console.WriteLine("HOST:" + _Host);
                //Console.WriteLine("Port:" + _Port);
                //Console.WriteLine("UserName:" + _UserName);
                //Console.WriteLine("Password:" + _Password);
                //Console.ReadLine();
            }
        }

        public void AssignInformationsToClient()
        {
            try
            {
                _SmtpClient.Host = _Host;
                _SmtpClient.Port = Convert.ToInt32(_Port);
                _SmtpClient.Credentials = new System.Net.NetworkCredential(_UserName, _Password);
                _SmtpClient.EnableSsl = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
