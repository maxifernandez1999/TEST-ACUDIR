using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;   
using Acudir.Test.Apis.Controllers;
using Acudir.Test.Core.Domain.Entities;
using FluentAssertions;

public class AuthControllerTests
{
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockConfig = new Mock<IConfiguration>();
        _mockConfig.SetupGet(config => config["JwtSettings:Key"]).Returns("clave-secreta-muy-segura");
        _mockConfig.SetupGet(config => config["JwtSettings:Issuer"]).Returns("Acudir.Test.Api");
        _mockConfig.SetupGet(config => config["JwtSettings:Audience"]).Returns("Acudir");
        _mockConfig.SetupGet(config => config["JwtSettings:ExpirationInMinutes"]).Returns("60");
        _mockConfig.SetupGet(config => config["JwtSettings:UserName"]).Returns("Acudir");
        _mockConfig.SetupGet(config => config["JwtSettings:Password"]).Returns("AcudirTest");

        _controller = new AuthController(_mockConfig.Object);
    }

    [Fact]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Username = "wrong_user",
            Password = "wrong_password"
        };

        // Act
        var result = _controller.Login(loginRequest);

        // Assert
        result.Should().BeOfType<UnauthorizedObjectResult>();
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Username = "Acudir",
            Password = "AcudirTest"
        };

        // Act
        var result = _controller.Login(loginRequest) as OkObjectResult;

        // Assert
        result.Should().NotBeNull();
        result?.Value.Should().BeOfType<AuthResponse>();

        var response = result?.Value as AuthResponse;
        response?.Token.Should().NotBeNullOrWhiteSpace();

        // Validar que el token tenga los valores esperados
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(response?.Token);

        token.Issuer.Should().Be("Acudir.Test.Api");
        token.Audiences.Should().Contain("Acudir");
    }
}
