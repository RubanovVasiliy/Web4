using Microsoft.EntityFrameworkCore;

namespace lab4.Data;

public sealed class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Reader> Readers => Set<Reader>();


    /// <summary>
    /// Book CRUD
    /// </summary>
    /// <param name="book"></param>
    public async Task<Book?> CreateBook(Book book)
    {
        await Books.AddAsync(book);
        await SaveChangesAsync();
        return book;
    }

    public async Task UpdateBook(Book book)
    {
        var bookItem = await Books.FirstOrDefaultAsync(p => p.Id == book.Id);
        if (bookItem == null)
        {
            return;
        }

        bookItem.Article = book.Article;
        bookItem.Author = book.Author;
        bookItem.Quantity = book.Quantity;
        bookItem.YearOfPublication = book.YearOfPublication;

        await SaveChangesAsync();
    }

    public async Task DeleteBook(int id)
    {
        var bookItem = await Books.FirstOrDefaultAsync(p => id == p.Id);
        if (bookItem == null)
        {
            return;
        }

        Books.Remove(bookItem);
        await SaveChangesAsync();
    }

    public async Task<Book?> GetBookById(int id)
        => await Books.FirstOrDefaultAsync(p => id == p.Id);

    public async Task<List<Book>> GetAllBooks()
        => await Books.ToListAsync();

    
    /// <summary>
    /// Readers CRUD
    /// </summary>
    /// <param name="reader"></param>
    public async Task CreateReader(Reader reader)
    {
        await Readers.AddAsync(reader);
        await SaveChangesAsync();
    }

    public async Task UpdateReader(Reader reader)
    {
        var readerItem = await Readers.FirstOrDefaultAsync(p => p.Id == reader.Id);
        if (readerItem == null)
        {
            return;
        }

        readerItem.Fullname = reader.Fullname;
        readerItem.Birthday = reader.Birthday;
        await SaveChangesAsync();
    }

    public async Task DeleteReader(int id)
    {
        var readerItem = await Readers.FirstOrDefaultAsync(p => id == p.Id);
        if (readerItem == null)
        {
            return;
        }

        Readers.Remove(readerItem);
        await SaveChangesAsync();
    }

    public async Task<Reader?> GetReaderById(int id)
        => await Readers.FirstOrDefaultAsync(p => id == p.Id);

    public async Task<List<Reader>> GetAllReaders()
        => await Readers.ToListAsync();

}