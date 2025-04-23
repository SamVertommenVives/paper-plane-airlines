using SendMail.Util.Interfaces;

namespace PaperPlaneAirlines.Controllers;

public class SendController
{
    private readonly IEmailSend _emailSender;
    
    public SendController(IEmailSend emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendConfirmationEmail(string emailAddress)
    {
        string subject = "Boeking bevestigd! 🚀";
        string body = "Hi! \n Bedankt voor het boeken van je vlucht!\nTot binnekort!\n\nHet PaperPlaneAirlines team";
        
        try
        {
            await _emailSender.SendEmailAsync(emailAddress, subject, body);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 
}