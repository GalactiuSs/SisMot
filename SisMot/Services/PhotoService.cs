    using SisMot.Data;
using SisMot.Models;
using SisMot.Repositories;

namespace SisMot.Services
{
    public class PhotoService : IPhotoRepository
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DbsisMotContext _context;

        public PhotoService(IWebHostEnvironment hostingEnvironment, DbsisMotContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public Task<List<MotelPhoto>> GetPhotos(int motelId)
        {
            throw new NotImplementedException();
        }

        public async Task LoadedImages(List<IFormFile> photos, int motelId)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            foreach (var photo in photos)
            {
                if (photo.Length > 0 && photo.ContentType.StartsWith("image/"))
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(path, uniqueFileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }

                        MotelPhoto motelPhoto = new MotelPhoto()
                        {
                            PathPhotoMotel = Path.Combine("uploads", uniqueFileName),
                            MotelId = motelId,
                        };

                        await _context.MotelPhotos.AddAsync(motelPhoto);
                    }
                    catch(Exception ex)
                    {
                        throw ex!;
                    }
                }
            }
            await _context.SaveChangesAsync();      
        }
    }
}