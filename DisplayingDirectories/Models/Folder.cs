using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisplayingDirectories.Models
{
    public enum FolderType
    {
        Directory,
        Other
    }
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        [Column(TypeName = "varchar(300)")]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public FolderType FolderType { get; set; }
        public int? ParentId { get; set; }
        public Folder? Parent { get; set; }
    }
}
