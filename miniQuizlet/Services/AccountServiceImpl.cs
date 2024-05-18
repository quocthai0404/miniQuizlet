using miniQuizlet.Models;

namespace miniQuizlet.Services;

public class AccountServiceImpl : IAccountService
{
    private DatabaseContext db;
    public AccountServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public bool addActiveAccount(ActiveAccount activeAccount)
    {
        try
        {
            db.ActiveAccounts.Add(activeAccount);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
        
    }

    public bool existEmail(string email)
    {
        return db.Users.SingleOrDefault(u => u.Email == email)!=null;
    }

    public ActiveAccount getActiveAccountByCode(string securityCode)
    {
        return db.ActiveAccounts.SingleOrDefault(a => a.SecurityCode == securityCode);
    }

    public bool login()
    {
        throw new NotImplementedException();
    }

    public bool removeActiveAccount(ActiveAccount activeAccount)
    {
        try
        {
            db.ActiveAccounts.Remove(activeAccount);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool signUp(User user)
    {

        try
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }

    }
}
