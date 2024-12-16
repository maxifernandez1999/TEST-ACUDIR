using Acudir.Test.Core.Application.DTOs;

namespace Acudir.Test.Core.Domain.Entities
{
    public class GetPersonResponse : ResponseBase
    {
        public List<PersonDTO> people = new List<PersonDTO>();
    }
}
