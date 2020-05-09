using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab1.Models;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentItemsController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentItemsController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/StudentItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentItem>>> GetStudentItems()
        {
            return await _context.StudentItems.ToListAsync();
        }

        // GET: api/StudentItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentItem>> GetStudentItem(long id)
        {
            var studentItem = await _context.StudentItems.FindAsync(id);

            if (studentItem == null)
            {
                return NotFound();
            }

            return studentItem;
        }

        // PUT: api/StudentItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentItem(long id, StudentItem studentItem)
        {
            if (id != studentItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentItem>> PostStudentItem(StudentItem studentItem)
        {
            //Validare
            if (studentItem.Age <0)
            {
                return BadRequest("Invalid value entered for the 'age' field!");
            }
            _context.StudentItems.Add(studentItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentItem", new { id = studentItem.Id }, studentItem);
        }

        // DELETE: api/StudentItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentItem>> DeleteStudentItem(long id)
        {
            var studentItem = await _context.StudentItems.FindAsync(id);
            if (studentItem == null)
            {
                return NotFound();
            }

            _context.StudentItems.Remove(studentItem);
            await _context.SaveChangesAsync();

            return studentItem;
        }

        private bool StudentItemExists(long id)
        {
            return _context.StudentItems.Any(e => e.Id == id);
        }
    }
}
