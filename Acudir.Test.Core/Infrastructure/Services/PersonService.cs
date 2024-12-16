using Acudir.Test.Core.Domain.Interfaces;


namespace Acudir.Test.Core.Infrastructure.Services
{
    public class PersonService: IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(
            IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public IPersonRepository GetPersonRepository()
        {
            return _personRepository;
        }
    }
}
