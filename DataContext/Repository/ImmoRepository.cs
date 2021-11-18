using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataContext.Repository.IRepository;
using DbAccess.Data;
using Microsoft.EntityFrameworkCore;
using DTO;
using Serilog;

namespace DataContext.Repository
{
    public class ImmoRepository : IImmoRepository
    {
        private readonly IMapper _mapper;
        private readonly ImmoDbContext _context;

        public ImmoRepository(IMapper mapper, ImmoDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ImmoDTO> CreateImmo(CreateImmoDTO createImmoDTO)
        {
            Immo immo = _mapper.Map<CreateImmoDTO, Immo>(createImmoDTO);
            immo.CreatedBy = "";
            immo.CreatedOn = DateTime.Now;
            var addedImmo = await _context.Immos.AddAsync(immo);
            await _context.SaveChangesAsync();
            return _mapper.Map<Immo, ImmoDTO>(addedImmo.Entity);
        }

        public async Task<ImmoDTO> GetImmo(string province)
        {
            try
            {
                ImmoDTO immo = _mapper.Map<Immo, ImmoDTO>(
                    await _context.Immos.FirstOrDefaultAsync(x => x.Province == province)/*Include(x => x.Images).*/);
                return immo;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "The Immo failed to load");
                return null;
            }
        }

        public async Task<IEnumerable<ImmoDTO>> GetAllImmos()
        {
            try
            {
                IEnumerable<ImmoDTO> immoDTOs =
                     _mapper.Map<IEnumerable<Immo>, IEnumerable<ImmoDTO>>(_context.Immos.Include(x => x.Images));
                return immoDTOs;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "The Immo's failed to load");
                return null;
            }
        }

        public async Task<ImmoDTO> UpdateImmo(int immoId, ImmoDTO immoDTO)
        {
            try
            {
                if (immoId == immoDTO.ImmoId)
                {
                    Immo immoDetails = await _context.Immos.FindAsync(immoId);
                    Immo immo = _mapper.Map<ImmoDTO, Immo>(immoDTO, immoDetails);
                    immo.UpdatedBy = "";
                    immo.UpdatedOn = DateTime.Now;
                    var updatedImmo = _context.Immos.Update(immo);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<Immo, ImmoDTO>(updatedImmo.Entity);
                }
                else
                {
                    Log.Error("The Immo's 'Id' does not match the immoDTO.ImmoId'");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "The Immo failed to update");
                return null;
            }
        }

        public async Task<int> DeleteImmo(int immoId)
        {
            var immoDetails = await _context.Immos.FindAsync(immoId);
            if (immoDetails != null)
            {
                var allImages = await _context.Images.Where(x => x.ImmoId == immoId).ToListAsync();
                _context.Images.RemoveRange(allImages);

                _context.Immos.Remove(immoDetails);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

    }
}
