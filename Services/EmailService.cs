using Microsoft.Extensions.Options;
using OrderManagementSystem.Helpers;
using OrderManagementSystem.Interfaces.ServiceInterfaces;
using System.Net;
using System.Net.Mail;

namespace OrderManagementSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_emailSettings.Username) || string.IsNullOrWhiteSpace(_emailSettings.Password))
                {
                    _logger.LogInformation("[Email Service Mock] Email to {ToEmail} | Subject: {Subject} | Body: {Body}", toEmail, subject, body);
                    await Task.CompletedTask;
                    return;
                }

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
                {
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                    EnableSsl = _emailSettings.EnableSsl
                };

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Successfully sent email to {ToEmail}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {ToEmail}", toEmail);
            }
        }

        public async Task SendWelcomeEmailAsync(string toEmail, string username)
        {
            string subject = "Welcome to Order Management System!";
            string body = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px;'>
                    <h2>Welcome, {username}!</h2>
                    <p>Thank you for registering with Order Management System.</p>
                    <p>Your account has been successfully created and configured.</p>
                    <hr />
                    <p style='color: #666; font-size: 12px;'>Order Management System System Notification</p>
                </div>";

            await SendEmailAsync(toEmail, subject, body);
        }

        public async Task SendEmployeeCredentialsEmailAsync(string toEmail, string fullName, string username, string tempPassword)
        {
            string subject = "Your Order Management System Account";
            string body = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; max-width: 600px;'>
                    <h2>Hello, {fullName}!</h2>
                    <p>Your employee account has been created on the Order Management System. 
                       Use the credentials below to log in.</p>
                    <div style='background:#f4f4f4; border-radius:8px; padding:16px; margin:16px 0;'>
                        <p><strong>Login URL:</strong> /api/auth/login</p>
                        <p><strong>Username / Email:</strong> {username}</p>
                        <p><strong>Temporary Password:</strong> <code style='background:#e0e0e0;padding:2px 6px;border-radius:4px;'>{tempPassword}</code></p>
                    </div>
                    <p style='color:#c0392b;'><strong>Important:</strong> Please change your password after your first login.</p>
                    <hr />
                    <p style='color: #666; font-size: 12px;'>Order Management System — System Notification</p>
                </div>";

            await SendEmailAsync(toEmail, subject, body);
        }
    }
}

