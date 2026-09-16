using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Course
{
    [Key]
    [Column("CourseId")]   // 🔥 MAP TO DB COLUMN
    public int CourseId { get; set; }

    public string CourseName { get; set; }

    public bool IsActive { get; set; }
}