using MailKit.Net.Smtp;  // MailKit's SmtpClient
using MimeKit;
using System.Net;
using System.Threading.Tasks;
using Nhom7_webTourdulich.Repositories;  // Ensure IEmailService namespace is correctly referenced
using System;

namespace Nhom7_webTourdulich.Services
{
    public class EmailService : IEmailService
    {
        // SMTP configuration for Gmail
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 465;  // Use port 465 for SSL
        private readonly string _emailUsername = "chithanhphamj@gmail.com"; // Replace with your Gmail address
        private readonly string _emailPassword = "cdkalrzsoraxbybi"; // Replace with your Gmail app password

        // Method to send reset password email
        public async Task SendResetPasswordEmail(string email, string resetLink)
        {
            // Create the SMTP client to connect to Gmail's SMTP server
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    // Set a cancellation token for timeout
                    var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                    var cancellationToken = cancellationTokenSource.Token;

                    // Connect to the Gmail SMTP server using SSL
                    await smtpClient.ConnectAsync(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.SslOnConnect, cancellationToken);

                    // Authenticate using the email and app-specific password
                    var credentials = new NetworkCredential(_emailUsername, _emailPassword);
                    await smtpClient.AuthenticateAsync(credentials, cancellationToken);

                    // Create the email message
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Your App Name", _emailUsername));
                    message.To.Add(new MailboxAddress("", email)); // Recipient email address
                    message.Subject = "Reset Password Request"; // Subject line

                    // Email body with the reset link
                    var body = new TextPart("plain")
                    {
                        Text = $"Please click the following link to reset your password: {resetLink}"
                    };

                    message.Body = body;

                    // Send the email asynchronously
                    await smtpClient.SendAsync(message, cancellationToken);

                    // Disconnect after sending the email
                    await smtpClient.DisconnectAsync(true, cancellationToken);
                }
                catch (Exception ex)
                {
                    // Handle any errors (e.g., log the error or rethrow)
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    throw;  // Optional: Re-throw the error
                }
            }
        }
    }
}
