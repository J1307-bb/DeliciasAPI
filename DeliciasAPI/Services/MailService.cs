using DeliciasAPI.Context;
using System.Net.Mail;
using System.Net;
using DeliciasAPI.Interfaces;

namespace DeliciasAPI.Services
{
    public class MailService : IMailService
    {
        private readonly ApplicationDbContext _context;

        public MailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SendMail(string to, string subject, string body, string logoPath = null)
        {
            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
            smtp.Credentials = new NetworkCredential("delicias.webapp@outlook.com", "Delicias2024");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("delicias.webapp@outlook.com", "Delicias.com");
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Body = body;

            if (logoPath != null && File.Exists(logoPath))
            {
                LinkedResource inlineLogo = new LinkedResource(logoPath)
                {
                    ContentId = "logo",
                    TransferEncoding = System.Net.Mime.TransferEncoding.Base64
                };

                string htmlBody = body.Replace("[LOGO]", "cid:logo");
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, System.Net.Mime.MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(inlineLogo);
                mail.AlternateViews.Add(alternateView);
            }
            else
            {
                mail.Body = body;
            }


            smtp.Send(mail);
        }

    }
}
