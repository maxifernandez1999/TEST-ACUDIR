using Acudir.Test.Core.Domain.Interfaces;

namespace Acudir.Test.Core.Infrastructure.Services
{
    public interface IPersonService
    {
        IPersonRepository GetPersonRepository();
    }
}
