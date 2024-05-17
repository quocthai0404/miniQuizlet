using System;
using System.Collections.Generic;

namespace miniQuizlet.Models;

public partial class Folder
{
    public int FolderId { get; set; }

    public string FolderName { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<StudySet> StudySets { get; set; } = new List<StudySet>();

    public virtual User User { get; set; } = null!;
}
