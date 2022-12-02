using lab4.Data;
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
        return books;
    }

    [HttpGet]
    [Route("getById/{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return Ok(book);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Book>> Add(Book book)
    {
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

        book.Id = updatedBook.Id;
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
}