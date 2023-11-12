using Azure.Core;
using SisMot.Data;
using SisMot.Models;
using SisMot.Repositories;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SisMot.Models.CustomModels;

namespace SisMot.Services;

public class RequestMotelService : IRequestMotelRepository
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMotelRepository _motelRepository;
    private readonly DbsisMotContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPhotoRepository _photoRepository;

    public RequestMotelService(
        DbsisMotContext context,
        IRequestRepository requestRepository, 
        IMotelRepository motelRepository,
        IHttpContextAccessor httpContextAccessor,
        IPhotoRepository photoRepository)
    {
        _context = context;
        _requestRepository = requestRepository;
        _motelRepository = motelRepository;
        _httpContextAccessor = httpContextAccessor;
        _photoRepository = photoRepository;
    }

    public async Task<bool> AddMotelWithRequest(Motel motel, PersonRequest request, List<IFormFile> photos)
    {
        if (motel != null && request != null)
        {
            var userAuthenticaded = _httpContextAccessor.HttpContext!.User;
            var userId = userAuthenticaded.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                motel.PersonId = int.Parse(userId);
                await _motelRepository.CreateMotel(motel);
                await _photoRepository.LoadedImages(photos, motel.Id);
                request.MotelId = motel.Id;
                request.PersonId = int.Parse(userId);
                await _requestRepository.NewRequest(request);
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return false;
    }

    public async Task<MotelPhotosDTO> GetRequestWithDetails(int requestId)
    {
        var findMotelByRequestId = await _context.PersonRequests.Where(r => r.Id.Equals(requestId))
            .Select(r => r.MotelId).FirstOrDefaultAsync();
        var findMotel = await _context.Motels.Where(m => m.Id.Equals(findMotelByRequestId))
            .Select(m => new Motel()
            {
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                PhoneNumber = m.PhoneNumber,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                Id = m.Id
            }).FirstOrDefaultAsync();
        var joinMotelWithPhotos = await _context.MotelPhotos.Where(photo => photo.MotelId.Equals(findMotel.Id))
            .Select(motelPathPhoto => motelPathPhoto.PathPhotoMotel).ToListAsync();
        MotelPhotosDTO motelPhotosDto = new()
        {
            MotelName = findMotel.Name,
            MotelDescription = findMotel.Description,
            MotelPrice = findMotel.Price,
            MotelPhoneNumber = findMotel.PhoneNumber,
            MotelLatitude = findMotel.Latitude,
            MotelLongitude = findMotel.Longitude,
            MotelPhotos = joinMotelWithPhotos
        };
        return motelPhotosDto;
    }
}