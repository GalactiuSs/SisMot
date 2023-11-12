using SisMot.Models;

namespace SisMot.Repositories;

public interface IRequestRepository
{
    Task<bool> NewRequest(PersonRequest request);
    Task<bool> EditRequest(int id, PersonRequest request);
    Task<List<PersonRequest>> GetAllRequest();
    Task<List<PersonRequest>> GetAcceptedRequests();
    Task<PersonRequest> GetRequest(int id);
    Task<bool> AcceptRequest(int requestId, int newStatus);
    Task<bool> RejectRequest(int requestId, int newStatus);
}