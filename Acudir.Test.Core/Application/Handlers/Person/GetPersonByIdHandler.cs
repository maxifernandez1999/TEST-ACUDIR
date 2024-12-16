using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Infrastructure.Queries.Person;
using Acudir.Test.Core.Infrastructure.Services;
using MediatR;

namespace Acudir.Test.Core.Application.Handlers.Person
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonDTO>
    {

        private readonly IPersonService _personService;

        public GetPersonByIdHandler(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));

        }
        public async Task<PersonDTO> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            //check nulls
            try
            {
                return await _personService.GetPersonRepository().GetByIdAsync(request.id);
            }
            catch (Exception)
            {
                return new PersonDTO();
            }
            
        }
    }
}
