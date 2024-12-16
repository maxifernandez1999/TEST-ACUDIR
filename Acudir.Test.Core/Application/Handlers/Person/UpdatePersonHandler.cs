using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using Acudir.Test.Core.Infrastructure.Commands.Person;
using Acudir.Test.Core.Infrastructure.Services;
using MediatR;

namespace Acudir.Test.Core.Application.Handlers.Person
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, UpdatePersonResponse>
    {
        private readonly IPersonService _personService;

        public UpdatePersonHandler(IPersonService personService)
        {
            _personService = personService ?? throw new ArgumentNullException(nameof(personService));

        }

        public async Task<UpdatePersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var people = (await _personService.GetPersonRepository().GetAllAsync()).ToList();

                var index = people.ToList().FindIndex(p => p.Id == request.person.Id);

                if (index == -1)
                {
                    return new UpdatePersonResponse { Code = -1, Message = "Person not found.", Person = new PersonDTO() };
                }

                var existingPerson = people[index];
                if (existingPerson.Name == request.person.Name &&
                    existingPerson.LastName == request.person.LastName &&
                    existingPerson.Age == request.person.Age &&
                    existingPerson.Address == request.person.Address &&
                    existingPerson.Phone == request.person.Phone)
                {
                    return new UpdatePersonResponse { Code = 0, Message = "No changes detected.", Person = new PersonDTO() };
                }

                await _personService.GetPersonRepository().UpdateAsync(request.person);

                return new UpdatePersonResponse { Code = 1, Message = "Person updated successfully.", Person = request.person };

            }
            catch (Exception ex)
            {
                return new UpdatePersonResponse
                {
                    Code = -1,
                    Message = ex.Message,
                    Person = null,
                };
            }
            
           
        }
    }
}
