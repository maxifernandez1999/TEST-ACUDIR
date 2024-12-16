using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using Acudir.Test.Core.Infrastructure.Commands.Person;
using Acudir.Test.Core.Infrastructure.Queries.Person;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acudir.Test.Apis.Controllers
{
    [ApiController]
    [Route("people")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GetAllPerson
        /// <summary>
        /// get all people
        /// </summary>
        [HttpGet("getAll")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PersonDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll(
            [FromQuery] PersonParams filters)
        {
            try
            {
                var people = await _mediator.Send(new GetAllPeopleQuery(filters));

                if (people == null || !people.Any())
                {
                    return NotFound("No people were found.");
                }

                return Ok(people.ToList());
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region GetById

        [HttpGet("getById")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<PersonDTO>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(
            [FromQuery] int id)
        {
            try
            {
                var person = await _mediator.Send(new GetPersonByIdQuery(id));

                if (person == null) { return NotFound("No people were found."); }

                return Ok(person);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region CreatePerson
        /// <summary>
        /// create new person
        /// </summary>
        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(typeof(CreatePersonResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(
            [FromQuery] CreatePersonRequestDto personParams)
        {
            try
            {
                var personDTO = new PersonDTO()
                {
                    Name = personParams.Name,
                    LastName = personParams.LastName,
                    Age = personParams.Age,
                    Address = personParams.Address,
                    Phone = personParams.Phone
                };

                var result = await _mediator.Send(new CreatePersonCommand(personDTO));

                if (result.Code != 1)
                {
                    return BadRequest(result);   
                }            

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region UpdatePerson
        /// <summary>
        /// Update a person
        /// </summary>
        [HttpPut("update")]
        [Authorize]
        [ProducesResponseType(typeof(UpdatePersonResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(
            [FromQuery] int id,
            [FromQuery] string? name,
            [FromQuery] string? lastname,
            [FromQuery] int age,
            [FromQuery] string? address,
            [FromQuery] string? phone
            )
        {
            try
            {
                var personDTO = new PersonDTO()
                {
                    Id = id,
                    Name = name,
                    LastName = lastname,
                    Age = age,
                    Address = address,
                    Phone = phone
                };

                var result = await _mediator.Send(new UpdatePersonCommand(personDTO));

                if (result.Code == 0)
                {
                    return Ok(result);
                }
                if (result.Code == -1)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
    }
}

