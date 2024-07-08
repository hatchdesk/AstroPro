namespace Web.Application.Options
{
    public class EmailSettingsOption
    {
        public string SmtpServer { get; set; } = default!;
        public int SmtpPort { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string From { get; set;} = default!;
    }
}
