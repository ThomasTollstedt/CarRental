using Microsoft.AspNetCore.Identity.UI.Services;

namespace CarRental.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // This dummy sender doesn't actually send emails.
            // It just fulfills the IEmailSender contract.
            return Task.CompletedTask;
        }

    }
}
