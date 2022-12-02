namespace lab4.Dto;

public class CreateBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Article { get; set; }
    public int YearOfPublication { get; set; }
    public int Quantity { get; set; }
}