using System.Globalization;

namespace lab4.Dto;

public class CreateReaderDto
{
    public string Fullname { get; set; }
    public string Birthday { get; set; } = DateTime.Now.ToString(CultureInfo.InvariantCulture);
}