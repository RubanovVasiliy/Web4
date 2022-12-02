using lab4.Data;
using lab4.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly MyDbContext _context;

    public BooksController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<List<Book>>> GetAll()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books);
    }

    [HttpGet]
    [Route("getById/{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Book>> Add(CreateBookDto dto)
    {
        var book = new Book()
        {
            Quantity = dto.Quantity,
            Article = dto.Article,
            Author = dto.Author,
            Title = dto.Title,
            YearOfPublication = dto.YearOfPublication
        };
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book>> Update(Book updatedBook)
    {
        var book = await _context.Books.FindAsync(updatedBook.Id);
        if (book == null)
        {
            return NotFound();
        }

        book.Article = updatedBook.Article;
        book.Author = updatedBook.Author;
        book.Title = updatedBook.Title;
        book.Quantity = updatedBook.Quantity;
        book.YearOfPublication = updatedBook.YearOfPublication;
        await _context.SaveChangesAsync();
        return Ok(updatedBook);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book>> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return Ok(book);
    }

    [HttpGet]
    [Route("getBooksInStock")]
    public async Task<ActionResult<List<Book>>> GetBooksInStock()
    {
        var books = await _context.Books.Where(b => b.Quantity > 0).ToListAsync();
        return Ok(books);
    }

    [HttpGet]
    [Route("getByTitle/{word}")]
    public async Task<ActionResult<List<Book>>> GetByTitle(string word)
    {
        var book = await _context.Books
            .Where(b => b.Title.ToLower().Contains(word.ToLower()))
            .ToListAsync();
        return Ok(book);
    }
}