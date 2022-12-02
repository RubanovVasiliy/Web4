using lab4.Data;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly MyDbContext _context;

    private static List<Book> books = new()
    {
        // new Book
        // {
        //     Id = 1,
        //     Title = "",
        //     Article = "",
        //     Author = "",
        //     YearOfPublication = 2000,
        //     Quantity = 5
        // }
    };


    public BooksController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book[]>> GetAll()
    {
        return Ok(await _context.GetAllBooks());
    }

    [HttpGet]
    [Route("getById/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _context.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Book>> Add(Book book)
    {
        return Ok(await _context.CreateBook(book));
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book>> Update(Book updatedBook)
    {
        var book = books.Find(b => b.Id == updatedBook.Id);
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

        return Ok(updatedBook);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Book>> Delete(int id)
    {
        var book = books.Find(b => b.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        books.Remove(book);
        return Ok(book);
    }
}