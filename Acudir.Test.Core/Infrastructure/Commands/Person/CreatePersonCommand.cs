using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using MediatR;

namespace Acudir.Test.Core.Infrastructure.Commands.Person
{
    public record CreatePersonCommand(PersonDTO person) : IRequest<CreatePersonResponse>;
}
