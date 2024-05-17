namespace miniQuizlet.ViewModel_DTO;

public class UserFormBinding
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateTime BirthDay { get; set; }

    public bool Gender { get; set; }

    public string Repassword { get; set; }

    public bool Agree { get; set; }
}
