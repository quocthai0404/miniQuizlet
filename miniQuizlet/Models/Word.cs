using System;
using System.Collections.Generic;

namespace miniQuizlet.Models;

public partial class Word
{
    public int WordId { get; set; }

    public string WordText { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public string Translation { get; set; } = null!;

    public string? Pronunciation { get; set; }

    public string Example { get; set; } = null!;

    public int StudySetId { get; set; }

    public virtual StudySet StudySet { get; set; } = null!;

    public virtual ICollection<WordType> WordTypes { get; set; } = new List<WordType>();
}
