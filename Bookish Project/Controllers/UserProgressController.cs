using Bookish_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookish_Project.Service;

namespace Bookish_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgressController : ControllerBase
    {
        private readonly IUserProgressService _userProgressService;
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public UserProgressController(IUserProgressService userProgressService, ApplicationDbContext context)
        {
            _userProgressService = userProgressService;
            _context = context;
        }

        // GET: api/UserProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProgress>>> GetUserProgress()
        {
            var userProgress = await _context.UserProgress
                .Include(up => up.Book)  // Include Book details
                .Include(up => up.User)  // Include User details
                .ToListAsync();

            return Ok(userProgress);
        }

        // GET: api/UserProgress/
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProgress>> GetUserProgress(int id)
        {
            var userProgress = await _context.UserProgress
                .Include(up => up.Book)   // Include Book details
                .Include(up => up.User)   // Include User details
                .FirstOrDefaultAsync(up => up.Id == id);

            if (userProgress == null)
            {
                return NotFound();
            }

            return Ok(userProgress);
        }


        // POST: api/UserProgress
        [HttpPost]
        public async Task<ActionResult<UserProgress>> PostUserProgress(UserProgress userProgress)
        {
            // Ensure valid Book and User by checking their IDs
            var book = await _context.Books.FindAsync(userProgress.BookId);
            var user = await _context.Users.FindAsync(userProgress.UserId);

            // If book or user not found, return an error
            if (book == null || user == null)
            {
                return BadRequest("Invalid Book or User ID.");
            }

            // Attach the Book and User to the UserProgress entity
            userProgress.Book = book;
            userProgress.User = user;

            // Add the UserProgress record to the database
            _context.UserProgress.Add(userProgress);
            await _context.SaveChangesAsync();

            // Return the newly created UserProgress object with associated Book and User data
            return CreatedAtAction(nameof(GetUserProgress), new { id = userProgress.Id }, userProgress);
        }

        // DELETE: api/UserProgress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProgress(int id)
        {
            // Find the UserProgress by id
            var userProgress = await _context.UserProgress.FindAsync(id);

            if (userProgress == null)
            {
                return NotFound();  // If not found, return 404 (Not Found)
            }

            // Remove the UserProgress record from the database
            _context.UserProgress.Remove(userProgress);

            // Save the changes to persist the deletion
            await _context.SaveChangesAsync();

            // Return a NoContent response (successful deletion, no content to return)
            return NoContent();
        }

    }
}


