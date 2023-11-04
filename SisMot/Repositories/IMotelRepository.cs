using Microsoft.AspNetCore.Mvc;
using SisMot.Models;

namespace SisMot.Repositories
{
    public interface IMotelRepository
    {
        Task<bool> CreateMotel(Motel motel);
        Task<bool> UpdateMotel(int id, Motel motel);
        Task<bool> DeleteMotel(int id);
        Task<List<Motel>> GetAllMotels();
        Task<Motel> GetMotel(int id);
    }
}
