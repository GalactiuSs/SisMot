using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisMot.Data;
using SisMot.Models;
using SisMot.Repositories;

namespace SisMot.Services
{
    public class MotelService : IMotelRepository
    {
        private readonly DbsisMotContext _context;
        private readonly IRequestRepository _requestRepository;

        public MotelService(DbsisMotContext context, IRequestRepository requestRepository)
        {
            _context = context;
            _requestRepository = requestRepository;
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
            var findMotel = await GetMotel(id);
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

        public async Task<Motel> GetMotel(int id)
        {
            var getMotel = await _context.Motels.FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (getMotel != null)
                return getMotel;
            return null!;
        }

        public async Task<bool> UpdateMotel(int id, Motel motel)
        {
            var findMotel = await GetMotel(id);
            if (findMotel != null)
            {
                _context.Entry(motel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Motel>> GetMotelsByOwner(int id)
        {
            var motels = await _context.Motels.Where(motel => motel.PersonId == id).ToListAsync();
            return motels;
        }
    }
}
