namespace DeliciasAPI.Interfaces
{
    public interface IMailService
    {
        public void SendMail(string to, string subject, string body, string logoPath = null);
    }
}
