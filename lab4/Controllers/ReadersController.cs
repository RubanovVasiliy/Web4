using lab4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab4.Controllers;

[ApiController]
[Route("readers")]
public class ReadersController : ControllerBase
{
    private readonly MyDbContext _context;
    
    public ReadersController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<List<Reader>>> GetAll()
    {
        var readers = await _context.Readers.ToListAsync();
        return readers;
    }

    [HttpGet]
    [Route("getById/{id:int}")]
    public async Task<ActionResult<Reader>> GetById(int id)
    {
        var reader = await _context.Readers.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }
        return Ok(reader);
    }

    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Reader>> Add(Reader reader)
    {
        await _context.Readers.AddAsync(reader);
        await _context.SaveChangesAsync();
        return Ok(reader);
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Reader>> Update(Reader updatedReader)
    {
        var reader = await _context.Readers.FindAsync(updatedReader.Id);
        if (reader == null)
        {
            return NotFound();
        }

        reader.Fullname = updatedReader.Fullname;
        reader.Birthday = updatedReader.Birthday;

        await _context.SaveChangesAsync();
        return Ok(updatedReader);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Reader>> Delete(int id)
    {
        var reader = await _context.Readers.FindAsync(id);
        if (reader == null)
        {
            return NotFound();
        }

        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync();

        return Ok(reader);
    }
}