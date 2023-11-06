using Azure.Core;
using SisMot.Data;
using SisMot.Models;
using SisMot.Repositories;

namespace SisMot.Services;

public class RequestMotelService : IRequestMotelRepository
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMotelRepository _motelRepository;
    private readonly DbsisMotContext _context;

    public RequestMotelService(
        DbsisMotContext context,
        IRequestRepository requestRepository, 
        IMotelRepository motelRepository)
    {
        _context = context;
        _requestRepository = requestRepository;
        _motelRepository = motelRepository;
    }

    public async Task<bool> AddMotelWithRequest(Motel motel, PersonRequest request)
    {
        if (motel != null && request != null)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //motelid en la tabla request
                await _motelRepository.CreateMotel(motel);
                request.MotelId = motel.Id;
                await _requestRepository.NewRequest(request);
                await transaction.CommitAsync();
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