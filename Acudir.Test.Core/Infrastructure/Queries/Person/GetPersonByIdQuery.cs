using Acudir.Test.Core.Application.DTOs;
using MediatR;

namespace Acudir.Test.Core.Infrastructure.Queries.Person
{
    public record GetPersonByIdQuery(int id) : IRequest<PersonDTO>;

}
