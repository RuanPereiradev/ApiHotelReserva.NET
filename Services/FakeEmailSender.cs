using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Services
{
    public class FakeEmailSender<TUser> : IEmailSender<TUser> where TUser : class
    {
        public Task SendConfirmationLinkAsync(TUser user, string email, string confirmationLink)
        {
            Console.WriteLine($"[FakeEmailSender] Link de confirmação para {email}: {confirmationLink}");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(TUser user, string email, string resetLink)
        {
            Console.WriteLine($"[FakeEmailSender] Link de redefinição de senha para {email}: {resetLink}");
            return Task.CompletedTask;
        }

        public Task SendEmailAsync(TUser user, string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"[FakeEmailSender] Email para {email} - Assunto: {subject}");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(TUser user, string email, string resetCode)
        {
            throw new NotImplementedException();
        }
    }
}
