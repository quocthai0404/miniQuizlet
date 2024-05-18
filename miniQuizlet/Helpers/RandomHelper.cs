namespace miniQuizlet.Helpers;

public class RandomHelper
{
    public static string generateSecurityCode()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}
