using Azure.Core;
using SisMot.Models;

namespace SisMot.Repositories;

public interface IRequestMotelRepository
{
    Task<bool> AddMotelWithRequest(Motel motel, PersonRequest request);
}