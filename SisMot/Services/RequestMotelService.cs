using Azure.Core;
using SisMot.Data;
using SisMot.Models;
using SisMot.Repositories;
using System.Security.Claims;

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
}