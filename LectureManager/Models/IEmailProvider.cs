using SendGrid;
using SendGrid.Helpers.Mail;

namespace LectureManager.Models
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string toEmail, 
                            string fromEmail, 
                            string subject, 
                            string body, 
                            string htmlContent);
    }

    public class EmailProviderSendGrid : IEmailProvider
    {
        private readonly IConfiguration _config;

        public EmailProviderSendGrid(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, 
                                         string fromEmail, 
                                         string subject, 
                                         string body, 
                                         string htmlContent)
        {
            // Setup messsage
            var message = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = htmlContent
            };

            // Send the message
            message.AddTo(new EmailAddress(toEmail));

            /*// Setup SendGrid Key
            var apiKey = _config.GetSection("SendGridKey").Value;

            // Setup client using the key
            var client = new SendGridClient(apiKey);

            // Check if the email was sent correctly
            var response = await client.SendEmailAsync(message).ConfigureAwait(false);*/
        }
    }
}
