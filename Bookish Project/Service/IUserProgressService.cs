using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookish_Project.Service
{
    public interface IUserProgressService
    {
        Task<IEnumerable<UserProgress>> GetUserProgressAsync();
        Task<UserProgress> GetUserProgressByIdAsync(int id);
        Task<UserProgress> AddUserProgressAsync(UserProgress userProgress);
    }
}
