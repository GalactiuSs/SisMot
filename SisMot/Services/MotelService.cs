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
            var getMotels = await _context.Motels.Join(requestsList, m => m.Id, r => r.MotelId, 
                (m, r) => new Motel()
                {
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    PhoneNumber = m.PhoneNumber,
                    Latitude = m.Latitude,
                    Longitude = m.Longitude 
                }).Where(m => m.Status.Equals(1)).ToListAsync();
            return getMotels;
        }

        public async Task<MotelPhotosDTO> GetMotel(int id)
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

        public async Task<bool> UpdateMotel(MotelPhotosDTO motelPhotosDto)
        {
            var findMotel = await GetSingleMotel(motelPhotosDto.MotelId);
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
