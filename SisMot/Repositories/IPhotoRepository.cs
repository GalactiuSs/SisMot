using SisMot.Models;

namespace SisMot.Repositories
{
    public interface IPhotoRepository
    {
        Task LoadedImages(List<IFormFile> photos, int motelId);
        Task<List<MotelPhoto>> GetPhotos(int motelId); 
    }
}