using SisMot.Models;
using SisMot.Models.CustomModels;

namespace SisMot.Repositories
{
    public interface IPhotoRepository
    {
        Task LoadedImages(List<IFormFile> photos, int motelId);
        Task<MotelPhotosDTO> GetPhotosByMotel(int motelId); 
    }
}