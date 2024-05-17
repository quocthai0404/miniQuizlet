using miniQuizlet.Models;

namespace miniQuizlet.Services;

public class AccountServiceImpl : IAccountService
{
    private DatabaseContext db;
    public AccountServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public bool existEmail(string email)
    {
        return db.Users.SingleOrDefault(u => u.Email == email)!=null;
    }

    public bool login()
    {
        throw new NotImplementedException();
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
