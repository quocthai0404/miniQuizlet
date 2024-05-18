namespace miniQuizlet.Services;

public interface IMailService
{
    public bool Send(string from, string to, string subject, string body);
}
