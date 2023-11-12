using Azure.Core;
using SisMot.Models;
using SisMot.Models.CustomModels;

namespace SisMot.Repositories;

public interface IRequestMotelRepository
{
    Task<bool> AddMotelWithRequest(Motel motel, PersonRequest request, List<IFormFile> photos);
    Task<MotelPhotosDTO> GetRequestWithDetails(int requestId);
}