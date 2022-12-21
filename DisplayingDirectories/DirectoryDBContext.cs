using Microsoft.EntityFrameworkCore;
using DisplayingDirectories.Models;

namespace DisplayingDirectories
{
    public class DirectoryDBContext: DbContext
    {
        public DirectoryDBContext(DbContextOptions<DirectoryDBContext> options) : base(options) { }
        public DbSet<Folder> Folders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Folder>()
                .Property(p => p.FolderType)
                .HasConversion(
                    v => v.ToString(),
                    v => (FolderType)Enum.Parse(typeof(FolderType), v));

            FolderType directory = FolderType.Directory;

            modelBuilder.Entity<Folder>().HasData(
                new Folder { Id = 1, Name = "Creating Digital Images", FolderType = directory, ParentId = null },
                
                new Folder { Id = 2, Name = "Resources", FolderType = directory, ParentId = 1 },
                new Folder { Id = 3, Name = "Evidence", FolderType = directory, ParentId = 1 },
                new Folder { Id = 4, Name = "Graphic Products", FolderType = directory, ParentId = 1 },
                
                new Folder { Id = 5, Name = "Primary Sources", FolderType = directory, ParentId = 2 },
                new Folder { Id = 6, Name = "Secondary Sources", FolderType = directory, ParentId = 2 },
                
                new Folder { Id = 7, Name = "Process", FolderType = directory, ParentId = 4 },
                new Folder { Id = 8, Name = "Final Product", FolderType = directory, ParentId = 4 }
                );
        }
    }
}
