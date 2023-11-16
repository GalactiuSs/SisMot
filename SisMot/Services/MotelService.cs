using Microsoft.EntityFrameworkCore;
using SisMot.Data;
using SisMot.Models;
using SisMot.Models.CustomModels;
using SisMot.Repositories;

namespace SisMot.Services
{
    public class MotelService : IMotelRepository
    {
        private readonly DbsisMotContext _context;
        private readonly IRequestRepository _requestRepository;
        private readonly IPhotoRepository _photoRepository;

        public MotelService(DbsisMotContext context, IRequestRepository requestRepository, IPhotoRepository photoRepository)
        {
            _context = context;
            _requestRepository = requestRepository;
            _photoRepository = photoRepository;
        }

        public async Task<bool> CreateMotel(Motel motel)
        {
            if (motel != null)
            {
                await _context.Motels.AddAsync(motel);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMotel(int id)
        {
            var findMotel = await GetSingleMotel(id);
            if (findMotel != null)
            {
                findMotel.Status = 0;
                _context.Entry(findMotel).State = EntityState.Modified;
                return true;
            }
            return false;
        }

        public async Task<List<Motel>> GetAllMotels()
        {
            var requestsList = await _requestRepository.GetAcceptedRequests();
            var motelsList = await _context.Motels.Where(m => m.Status.Equals(1)).ToListAsync();
            var getMotels = motelsList.Join(requestsList, m => m.Id, r => r.MotelId, 
                (m, r) => new Motel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    PhoneNumber = m.PhoneNumber,
                    Latitude = m.Latitude,
                    Longitude = m.Longitude 
                }).ToList();
            return getMotels;
        }

        public async Task<List<EditMotelDTO>> GetMotelsWithPhotos()
        {
            List<EditMotelDTO> motelPhotosDtos = new();
            var motels = await GetAllMotels();
            foreach (var motel in motels)
            {
                var motelPhoto = await _photoRepository.GetPhotosByMotel(motel.Id);
                motelPhoto.MotelId = motel.Id;
                motelPhoto.MotelName = motel.Name;
                motelPhoto.MotelDescription = motel.Description;
                motelPhoto.MotelPrice = motel.Price;
                motelPhoto.MotelPhoneNumber = motel.PhoneNumber;
                motelPhoto.MotelLatitude = motel.Latitude;
                motelPhoto.MotelLongitude = motel.Longitude;
                motelPhotosDtos.Add(motelPhoto);
            }
            return motelPhotosDtos;
        }

        public async Task<EditMotelDTO> GetMotel(int id)
        {
            var getMotelPhotos = await _photoRepository.GetPhotosByMotel(id);
            var getMotel = await _context.Motels.Where(motel => motel.Id.Equals(id))
                .Select(motel => new Motel()
                {
                    Id = motel.Id,
                    Name = motel.Name,
                    Description = motel.Description,
                    Price = motel.Price,
                    PhoneNumber = motel.PhoneNumber,
                    Latitude = motel.Latitude,
                    Longitude = motel.Longitude
                }).FirstOrDefaultAsync();
            getMotelPhotos.MotelId = getMotel.Id;
            getMotelPhotos.MotelName = getMotel.Name;
            getMotelPhotos.MotelDescription = getMotel.Description;
            getMotelPhotos.MotelPrice = getMotel.Price;
            getMotelPhotos.MotelPhoneNumber = getMotel.PhoneNumber;
            getMotelPhotos.MotelLatitude = getMotel.Latitude;
            getMotelPhotos.MotelLongitude = getMotel.Longitude;
            if (getMotel != null)
                return getMotelPhotos;
            return null!;
        }

        private async Task<Motel> GetSingleMotel(int id)
        {
            var getMotel = await _context.Motels.FirstOrDefaultAsync(m => m.Id.Equals(id));
            return getMotel;
        }

        public async Task<bool> UpdateMotel(EditMotelDTO motelPhotosDto)
        {
            var findMotel = await GetSingleMotel(motelPhotosDto.MotelId);
            //  var finphotos = await _photoRepository.LoadedImages()
            if (findMotel != null)
            {
                findMotel.Name = motelPhotosDto.MotelName;
                findMotel.Description = motelPhotosDto.MotelDescription;
                findMotel.Price = motelPhotosDto.MotelPrice;
                findMotel.PhoneNumber = motelPhotosDto.MotelPhoneNumber;
                findMotel.Latitude = motelPhotosDto.MotelLatitude;
                findMotel.Longitude = motelPhotosDto.MotelLongitude;
                findMotel.LastUpdate = DateTime.Now;
                _context.Entry(findMotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Motel>> GetMotelsByOwner(int id)
        {
            var motels = await _context.PersonRequests
                .Where(r => r.PersonId.Equals(id) && r.StatusRequest.Equals(1))
                .Select(r => r.Motel)
                .Where(m => m.Status.Equals(1))
                .ToListAsync();
            return motels;
        }
    }
}
