namespace SendMail.Util.Interfaces;

public interface IEmailSend
{
    Task SendEmailAsync(string email, string subject, string message);
}