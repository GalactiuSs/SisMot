using Microsoft.AspNetCore.Mvc;
using SisMot.Models;
using SisMot.Models.CustomModels;

namespace SisMot.Repositories
{
    public interface IMotelRepository
    {
        Task<bool> CreateMotel(Motel motel);
        Task<bool> UpdateMotel(EditMotelDTO motelPhotosDto);
        Task<bool> DeleteMotel(int id);
        Task<List<Motel>> GetAllMotels();
        Task<List<EditMotelDTO>> GetMotelsWithPhotos();
        Task<EditMotelDTO> GetMotel(int id);
        Task<List<Motel>> GetMotelsByOwner(int id);
    }
}
