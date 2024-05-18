
using miniQuizlet.Models;

namespace miniQuizlet.Services;

public interface IAccountService
{
    public bool signUp(User user);
    public bool login();
    public bool existEmail(string email);
    public bool addActiveAccount(ActiveAccount activeAccount);
    public ActiveAccount getActiveAccountByCode(string securityCode);
    public bool removeActiveAccount(ActiveAccount activeAccount);
}
