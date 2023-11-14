using Microsoft.EntityFrameworkCore;
using SisMot.Data;
using SisMot.Models;
using SisMot.Repositories;

namespace SisMot.Services;

public class RequestService : IRequestRepository
{
    private readonly DbsisMotContext _context;

    public RequestService(DbsisMotContext context) => _context = context;

    public async Task<bool> NewRequest(PersonRequest request)
    {
        if (request != null)
        {
            await _context.PersonRequests.AddAsync(request);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> EditRequest(int id, PersonRequest request)
    {
        var findRequest = await GetRequest(id);
        if (findRequest != null)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<List<PersonRequest>> GetAcceptedRequests()
    {
        var allRequests = await _context.PersonRequests.Where(r => r.StatusRequest.Equals(1)).ToListAsync();
        return allRequests;
    }

    public async Task<List<PersonRequest>> GetAllRequest()
    {
        var allRequests = await _context.PersonRequests.Include(m => m.Person).Where(r => r.StatusRequest.Equals(0)).ToListAsync(); /*Where(r => r.StatusRequest.Equals(0)).ToListAsync();*/
        return allRequests;

    }

    public async Task<PersonRequest> GetRequest(int id)
    {
        var getRequest = await _context.PersonRequests.FirstOrDefaultAsync(r => r.Id.Equals(id));
        if (getRequest != null)
            return getRequest;
        return null!;
    }

    public async Task<bool> AcceptRequest(int requestId, int newStatus)
    {
        var getRequest = await _context.PersonRequests.FirstOrDefaultAsync(r => r.Id.Equals(requestId));
        if (getRequest != null)
        {
            getRequest.StatusRequest = byte.Parse(newStatus.ToString());
            _context.Entry(getRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    
    public async Task<bool> RejectRequest(int requestId, int newStatus)
    {
        var getRequest = await _context.PersonRequests.FirstOrDefaultAsync(r => r.Id.Equals(requestId));
        if (getRequest != null)
        {
            getRequest.StatusRequest = byte.Parse(newStatus.ToString());
            _context.Entry(getRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}