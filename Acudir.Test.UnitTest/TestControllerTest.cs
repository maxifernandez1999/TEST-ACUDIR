using Acudir.Test.Apis.Controllers;
using Acudir.Test.Core.Application.DTOs;
using Acudir.Test.Core.Domain.Entities;
using Acudir.Test.Core.Infrastructure.Commands.Person;
using Acudir.Test.Core.Infrastructure.Queries.Person;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Acudir.Test.UnitTest
{
    public class TestControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TestController _controller;

        public TestControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TestController(_mediatorMock.Object);
        }

        #region GetAll Tests
        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenPeopleFound()
        {
            // Arrange
            var people = new List<PersonDTO> { new PersonDTO { Id = 1, Name = "John", LastName = "Doe" } };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPeopleQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(people);

            PersonParams parameters = new PersonParams
            {
                Id = null,
                Name = null,
                LastName = null,
                Age = null,
                Address = null,
                Phone = null
            };

            // Act
            var result = await _controller.GetAll(parameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PersonDTO>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFound_WhenPeopleNotFound()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPeopleQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new List<PersonDTO>());

            // Act
            PersonParams parameters = new PersonParams
            {
                Id = null,
                Name = null,
                LastName = null,
                Age = null,
                Address = null,
                Phone = null
            };
            var result = await _controller.GetAll(parameters);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No people were found.", notFoundResult.Value);
        }
        #endregion

        #region Create Tests
        [Fact]
        public async Task Create_ReturnsOkResult_WhenPersonAdded()
        {
            // Arrange
            var createPersonDTO = new CreatePersonRequestDto
            {
                Name = "John",
                LastName = "Doe",
                Age = 30,
                Address = "123 Main St",
                Phone = "555-5555"
            };

            var personResult = new CreatePersonResponse
            {
                Code = 1,
                Message = "Person has been added.",
                Person = new PersonDTO
                {
                    Id = 1,
                    Name = "John",
                    LastName = "Doe",
                    Age = 30,
                    Address = "123 Main St",
                    Phone = "555-5555"
                }

            };


            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(personResult);
            // Act
            var result = await _controller.Add(createPersonDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreatePersonResponse>(okResult.Value);
            Assert.Equal(personResult.Person.Id, returnValue.Person.Id);
            Assert.Equal(personResult.Person.Name, returnValue.Person.Name);
            Assert.Equal(personResult.Person.LastName, returnValue.Person.LastName);
            Assert.Equal(personResult.Person.Age, returnValue.Person.Age);
            Assert.Equal(personResult.Person.Address, returnValue.Person.Address);
            Assert.Equal(personResult.Person.Phone, returnValue.Person.Phone);
        }
        #endregion

        #region Update Tests
        [Fact]
        public async Task Update_ReturnsOkResult_WhenPersonUpdated()
        {
            // Arrange
            var updatePersonDTO = new PersonDTO
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                Age = 30,
                Address = "123 Main St",
                Phone = "555-5555"
            };

            var personResult = new UpdatePersonResponse
            {
                Code = 1,
                Message = "Person updated successfully.",
                Person = new PersonDTO
                {
                    Id = 1,
                    Name = "Maxi",
                    LastName = "Doe",
                    Age = 30,
                    Address = "123 Main St",
                    Phone = "555-5555"
                }

            };


            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(personResult);

            // Act
            var result = await _controller.Update(updatePersonDTO.Id, updatePersonDTO.Name, updatePersonDTO.LastName, updatePersonDTO.Age, updatePersonDTO.Address, updatePersonDTO.Phone);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdatePersonResponse>(okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenNoChangesDetected()
        {
            // Arrange
            var updatePersonDTO = new PersonDTO
            {
                Id = 1,
                Name = "John",
                LastName = "Doe",
                Age = 30,
                Address = "123 Main St",
                Phone = "555-5555"
            };

            var personResult = new UpdatePersonResponse
            {
                Code = 0,
                Message = "No changes detected.",
                Person = new PersonDTO
                {
                    Id = 0,
                    Name = null,
                    LastName = null,
                    Age = 0,
                    Address = null,
                    Phone = null
                }
                
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(personResult);

            // Act
            var result = await _controller.Update(updatePersonDTO.Id, updatePersonDTO.Name, updatePersonDTO.LastName, updatePersonDTO.Age, updatePersonDTO.Address, updatePersonDTO.Phone);

            // Assert
            var badRequestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdatePersonResponse>(badRequestResult.Value);
        }
        #endregion
    }
}