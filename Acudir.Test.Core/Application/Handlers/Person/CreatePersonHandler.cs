using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using Acudir.Test.Core.Infrastructure.Commands.Person;
using Acudir.Test.Core.Infrastructure.Services;
using MediatR;
using Newtonsoft.Json;

namespace Acudir.Test.Core.Application.Handlers.Person
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponse>
    {
        private readonly IPersonService _personService;

        public CreatePersonHandler(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));

        }

        public async Task<CreatePersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _personService.GetPersonRepository().AddAsync(request.person);

                return new CreatePersonResponse { Code = 1, Message = "Person has been added.", Person = person };
            }
            catch (Exception ex)
            {
                return new CreatePersonResponse { Code = -1, Message = ex.Message, Person = new PersonDTO() };
            }
            
        }
    }
}
