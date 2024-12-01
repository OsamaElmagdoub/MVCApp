using MVCApplicationTest.DAL.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVCApplicationTestPL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("", 587);
            client.EnableSsl= true;

            client.Credentials = new NetworkCredential("", "");

            client.Send("", email.To, email.Subject, email.Body);
        }
    }
}
