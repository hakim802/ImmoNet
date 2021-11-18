using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;

namespace DataContext.Repository.IRepository
{
    public interface IImmoRepository
    {
        Task<ImmoDTO> CreateImmo(CreateImmoDTO createImmoDTO);
        Task<int> DeleteImmo(int immoId);
        Task<IEnumerable<ImmoDTO>> GetAllImmos();
        Task<ImmoDTO> GetImmo(string province);
        Task<ImmoDTO> UpdateImmo(int immoId, ImmoDTO immoDTO);
    }
}