using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lab4.Data;

public class Book
{
    [Key] public int Id { get; set; }
    [Required] public string Title { get; set; }
    [Required] public string Author { get; set; }
    [Required] public string Article { get; set; }
    [Required] public int YearOfPublication { get; set; }
    [Required] public int Quantity { get; set; }
    [JsonIgnore] public List<Reader> Readers { get; set; }
}