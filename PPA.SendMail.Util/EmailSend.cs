using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using SendMail.Util.Interfaces;

namespace SendMail.Util;

public class EmailSend : IEmailSend
{
    
    private readonly EmailSettings _emailSettings;

    public EmailSend(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        
        Console.WriteLine($"Sending email to {email}");
        
        var mail = new MailMessage(); // aanmaken van een mail-object
        mail.To.Add(new MailAddress(email));
        mail.From = new
            MailAddress("noreply.paperplaneairlines@gmail.com"); // hier komt jullie Gmail-adres
        mail.Subject = subject;
        mail.Body = message;
        mail.IsBodyHtml = true;
        try {
            using (var smtp = new SmtpClient(_emailSettings.MailServer))
            {
                smtp.Port = _emailSettings.MailPort;
                smtp.EnableSsl = true;
                smtp.Credentials =
                    new NetworkCredential(_emailSettings.Sender,
                        _emailSettings.Password);
                await smtp.SendMailAsync(mail);
            }
        }
        catch(Exception ex)
        { throw ex; }
    }
}