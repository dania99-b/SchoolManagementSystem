using Demo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [MaxLength(100)]
    [MinLength(2)]
    public string ?course_name { get; set; }

    [Range(10, 20)]
    public int ?course_capacity { get; set; }

    public int user_id { get; set; }
    [JsonIgnore]
    public User user { get; set; } // Better naming


    // Add navigation property
}