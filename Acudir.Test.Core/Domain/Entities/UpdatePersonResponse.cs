
using Acudir.Test.Core.Application.DTOs;

namespace Acudir.Test.Core.Domain.Entities
{
    public class UpdatePersonResponse : ResponseBase
    {
        public PersonDTO? Person { get; set; }
    }
}
