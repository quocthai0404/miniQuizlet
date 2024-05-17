
using miniQuizlet.Models;

namespace miniQuizlet.Services;

public interface IAccountService
{
    public bool signUp(User user);
    public bool login();
    public bool existEmail(string email);
}
