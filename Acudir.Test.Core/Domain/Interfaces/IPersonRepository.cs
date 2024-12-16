using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;

namespace Acudir.Test.Core.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<PersonDTO?> GetByIdAsync(int id);
        Task<IEnumerable<PersonDTO>> GetAllWithParamsAsync(PersonParams paramaters);
        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task<PersonDTO?> AddAsync(PersonDTO persona);
        Task<PersonDTO> UpdateAsync(PersonDTO persona);
    }
}
