using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using MediatR;

namespace Acudir.Test.Core.Infrastructure.Queries.Person
{
    public record GetAllPeopleQuery(PersonParams paramaters) : IRequest<IEnumerable<PersonDTO>>;

}
