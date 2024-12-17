using Bookish_Project.Model;
using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookish_Project.Service
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
