using SisMot.Models;

namespace SisMot.Repositories;

public interface IRequestRepository
{
    Task<bool> NewRequest(PersonRequest request);
    Task<bool> EditRequest(int id, PersonRequest request);
    Task<List<PersonRequest>> GetAllRequest();
    Task<PersonRequest> GetRequest(int id);
}