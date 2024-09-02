using Authorization.Core.Models;
using Refit;

namespace Authorization.Core.Interfaces
{
    public interface IUserRequests
    {
        [Get("/users")]
        Task<IEnumerable<User>> GetAllAsync();
    }
}