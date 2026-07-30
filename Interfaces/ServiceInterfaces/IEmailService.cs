namespace OrderManagementSystem.Interfaces.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendWelcomeEmailAsync(string toEmail, string username);
        Task SendEmployeeCredentialsEmailAsync(string toEmail, string fullName, string username, string tempPassword);
    }
}

