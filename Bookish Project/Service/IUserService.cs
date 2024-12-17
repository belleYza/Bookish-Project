using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookish_Project.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);
    }
}
