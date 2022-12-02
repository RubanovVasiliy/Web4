using System.ComponentModel.DataAnnotations;

namespace lab4.Data;

public class Reader
{
    [Key]
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Birthday { get; set; }
}