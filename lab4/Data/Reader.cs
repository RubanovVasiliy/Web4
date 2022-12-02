using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace lab4.Data;

public class Reader
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Fullname { get; set; }

    [Required] public string Birthday { get; set; } = DateTime.Now.ToString(CultureInfo.InvariantCulture);
}