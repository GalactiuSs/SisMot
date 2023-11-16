using SisMot.Models;
using SisMot.Models.CustomModels;

namespace SisMot.Repositories
{
    public interface IPhotoRepository
    {
        Task LoadedImages(List<IFormFile> photos, int motelId);
        Task<EditMotelDTO> GetPhotosByMotel(int motelId);
        Task<bool> RemoveImage(string imageId);
    }
}