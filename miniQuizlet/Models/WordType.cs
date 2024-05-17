using System;
using System.Collections.Generic;

namespace miniQuizlet.Models;

public partial class WordType
{
    public int WordTypeId { get; set; }

    public string WordTypeName { get; set; } = null!;

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
