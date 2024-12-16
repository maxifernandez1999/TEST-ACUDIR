using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using Acudir.Test.Core.Domain.Interfaces;
using Newtonsoft.Json;

namespace Acudir.Test.Core.Infrastructure.Repository
{
    public class PersonRepository: IPersonRepository
    {
        public PersonRepository()
        {
            
        }
        private readonly string _filePath = "Test.json"; // agregar en appsetting.json

        #region GetAllAsync
        public async Task<IEnumerable<PersonDTO>> GetAllWithParamsAsync(PersonParams parameters)
        {
            var data = await File.ReadAllTextAsync(_filePath);

            var people = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(data) ?? new List<PersonDTO>();
            var mQuery = SetQueryOptions(people, parameters);

            return mQuery;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var data = await File.ReadAllTextAsync(_filePath);

            return JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(data) ?? new List<PersonDTO>();
        }
        #endregion

        private IEnumerable<PersonDTO> SetQueryOptions(IEnumerable<PersonDTO> data, PersonParams parameters)
        {

            if (!string.IsNullOrEmpty(parameters.Id.ToString()))
            {
                data = data.Where(x => x.Id == parameters.Id);
            }

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                data = data.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(parameters.LastName))
            {
                data = data.Where(x => x.LastName.ToLower().Contains(parameters.LastName));
            }

            if (!string.IsNullOrEmpty(parameters.Age.ToString()))
            {
                data = data.Where(x => x.Age == parameters.Age);
            }

            if (!string.IsNullOrEmpty(parameters.Address))
            {
                data = data.Where(x => x.Address.ToLower().Contains(parameters.Address));
            }

            if (!string.IsNullOrEmpty(parameters.Phone))
            {
                data = data.Where(x => x.Phone.ToLower().Contains(parameters.Phone));
            }

            return data;

        }
        #region GetByIdAsync
        public async Task<PersonDTO?> GetByIdAsync(int id)
        {
            var data = await File.ReadAllTextAsync(_filePath);
            var people = JsonConvert.DeserializeObject<List<PersonDTO>>(data) ?? new List<PersonDTO>();
            return people.FirstOrDefault(p => p.Id == id);
        }
        #endregion

        #region AddAsync
        public async Task<PersonDTO?> AddAsync(PersonDTO person)
        {
            var people = (await GetAllAsync()).ToList();
            person.Id = people.Any() ? people.Max(p => p.Id) + 1 : 1;
            people.Add(person);
            await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(people, Formatting.Indented));

            return person;
        }
        #endregion

        #region UpdateAsync
        public async Task<PersonDTO> UpdateAsync(PersonDTO person)
        {

            var people = (await GetAllAsync()).ToList();

            var index = people.FindIndex(p => p.Id == person.Id);

            if (index != -1)
            {
                people[index] = person;

                await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(people, Formatting.Indented));
                return person;
            }

            return new PersonDTO();

        }


        #endregion
    }
}
