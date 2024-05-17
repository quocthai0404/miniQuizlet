using System;
using System.Collections.Generic;

namespace miniQuizlet.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public DateTime BirthDay { get; set; }

    public bool Gender { get; set; }

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public override string ToString()
    {
        return $"{{{nameof(UserId)}={UserId.ToString()}, {nameof(Email)}={Email}, {nameof(Password)}={Password}, {nameof(Fullname)}={Fullname}, {nameof(BirthDay)}={BirthDay.ToString()}, {nameof(Gender)}={Gender.ToString()}";
    }
}
