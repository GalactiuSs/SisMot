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

        public MotelService(DbsisMotContext context)
        {
            _context = context;
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
            var getMotels = await _context.Motels.ToListAsync();
            if (getMotels.Count > 0)
                return getMotels;
            return null!;
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
