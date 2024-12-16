using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Infrastructure.Queries.Person;
using Acudir.Test.Core.Infrastructure.Services;
using MediatR;

namespace Acudir.Test.Core.Application.Handlers.Person
{
    public class GetAllPeopleHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<PersonDTO>>
    {
        private readonly IPersonService _personService;

        public GetAllPeopleHandler(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));

        }
        public async Task<IEnumerable<PersonDTO>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _personService.GetPersonRepository().GetAllWithParamsAsync(request.paramaters);
            }
            catch (Exception)
            {
                return new List<PersonDTO>();   
            }

        }
    }
}
