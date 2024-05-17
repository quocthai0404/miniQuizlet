using System;
using System.Collections.Generic;

namespace miniQuizlet.Models;

public partial class ActiveAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public DateTime Expired { get; set; }

    public bool Active { get; set; }

    public string SecurityCode { get; set; } = null!;
}
