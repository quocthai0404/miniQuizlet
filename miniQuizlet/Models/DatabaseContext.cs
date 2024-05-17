using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace miniQuizlet.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<StudySet> StudySets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    public virtual DbSet<WordType> WordTypes { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.FolderId).HasName("PK__folder__C2FABF930A33C75F");

            entity.ToTable("folder");

            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.FolderName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("folderName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__folder__userId__4BAC3F29");
        });

        modelBuilder.Entity<StudySet>(entity =>
        {
            entity.HasKey(e => e.StudySetId).HasName("PK__StudySet__D2B334738AF72151");

            entity.ToTable("StudySet");

            entity.Property(e => e.StudySetId).HasColumnName("studySetId");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.StudySetName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("studySetName");

            entity.HasOne(d => d.Folder).WithMany(p => p.StudySets)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudySet__folder__4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__CB9A1CFFA79A4B8A");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.BirthDay).HasColumnName("birthDay");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.WordId).HasName("PK__Word__F80A4ACD7A124303");

            entity.ToTable("Word");

            entity.Property(e => e.WordId).HasColumnName("wordId");
            entity.Property(e => e.Definition)
                .IsUnicode(false)
                .HasColumnName("definition");
            entity.Property(e => e.Example)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("example");
            entity.Property(e => e.Pronunciation)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("pronunciation");
            entity.Property(e => e.StudySetId).HasColumnName("studySetId");
            entity.Property(e => e.Translation)
                .HasMaxLength(256)
                .HasColumnName("translation");
            entity.Property(e => e.WordText)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("wordText");

            entity.HasOne(d => d.StudySet).WithMany(p => p.Words)
                .HasForeignKey(d => d.StudySetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Word__studySetId__5165187F");

            entity.HasMany(d => d.WordTypes).WithMany(p => p.Words)
                .UsingEntity<Dictionary<string, object>>(
                    "WordWordType",
                    r => r.HasOne<WordType>().WithMany()
                        .HasForeignKey("WordTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Word_Word__wordT__571DF1D5"),
                    l => l.HasOne<Word>().WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Word_Word__wordI__5629CD9C"),
                    j =>
                    {
                        j.HasKey("WordId", "WordTypeId").HasName("PK__Word_Wor__56006AE76635B899");
                        j.ToTable("Word_WordType");
                        j.IndexerProperty<int>("WordId").HasColumnName("wordId");
                        j.IndexerProperty<int>("WordTypeId").HasColumnName("wordTypeId");
                    });
        });

        modelBuilder.Entity<WordType>(entity =>
        {
            entity.HasKey(e => e.WordTypeId).HasName("PK__WordType__E0A202AB69160F77");

            entity.ToTable("WordType");

            entity.Property(e => e.WordTypeId).HasColumnName("wordTypeId");
            entity.Property(e => e.WordTypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("wordTypeName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
