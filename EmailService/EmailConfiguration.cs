namespace EmailService
{
    /// <summary>
    /// Contains properties needed to configure sending email messages from the application.
    /// The .\appsettings.json file contains the values to populate these properties.
    /// Visit: https://code-maze.com/aspnetcore-send-email/
    /// </summary>
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}   