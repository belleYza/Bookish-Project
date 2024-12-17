using Bookish_Project.Data;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookish_Project.Service
{
    public class UserProgressService : IUserProgressService
    {
        private readonly ApplicationDbContext _context;

        public UserProgressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProgress>> GetUserProgressAsync()
        {
            return await _context.UserProgress
                .Include(up => up.Book)  // Include the related Book entity
                .Include(up => up.User)  // Include the related User entity
                .ToListAsync();
        }

        public async Task<UserProgress> GetUserProgressByIdAsync(int id)
        {
            return await _context.UserProgress
                .Include(up => up.Book)  // Include the related Book entity
                .Include(up => up.User)  // Include the related User entity
                .FirstOrDefaultAsync(up => up.Id == id);
        }


        public async Task<UserProgress> AddUserProgressAsync(UserProgress userProgress)
        {
            Console.WriteLine($"Adding UserProgress with BookId: {userProgress.BookId}, UserId: {userProgress.UserId}");

            // Check for valid BookId and UserId
            if (userProgress.BookId <= 0 || userProgress.UserId <= 0)
            {
                throw new ArgumentException("Invalid BookId or UserId.");
            }

            var book = await _context.Books.FindAsync(userProgress.BookId);
            var user = await _context.Users.FindAsync(userProgress.UserId);

            if (book == null)
            {
                throw new ArgumentException($"Book with ID {userProgress.BookId} does not exist.");
            }

            if (user == null)
            {
                throw new ArgumentException($"User with ID {userProgress.UserId} does not exist.");
            }

            _context.UserProgress.Add(userProgress);
            await _context.SaveChangesAsync();

            return userProgress;
        }
    }
}

