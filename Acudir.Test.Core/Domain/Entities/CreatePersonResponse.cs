using Acudir.Test.Core.Application.DTOs;

namespace Acudir.Test.Core.Domain.Entities
{
    public class CreatePersonResponse : ResponseBase
    {
        public PersonDTO? Person { get; set; }
    }
}
