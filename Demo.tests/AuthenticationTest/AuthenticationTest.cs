using Xunit;
using Moq;
using Demo.Controllers;
using Demo.DTOs;
using Demo.Models;
using Demo.Repository.UserRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class AuthenticationControllerTests
{
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly AuthenticationController _controller;

    public AuthenticationControllerTests()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _mockConfig = new Mock<IConfiguration>();

        _controller = new AuthenticationController(_mockUserRepo.Object, _mockConfig.Object);
    }

    [Fact]
    public void RegisterStudent_ReturnsConflict_WhenEmailAlreadyExists()
    {
        // Arrange
        var existingUser = new User { email = "test@example.com" };
        _mockUserRepo.Setup(repo => repo.GetAllUser()).Returns(new List<User> { existingUser });

        var registerDto = new RegisterDTO
        {
            name = "New Student",
            email = "test@example.com",
            password = "Password123"
        };

        // Act
        var result = _controller.RegisterStudent(registerDto);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        Assert.Equal(409, conflictResult.StatusCode);
    }

    [Fact]
    public void RegisterStudent_ReturnsOk_WhenRegistrationSuccessful()
    {
        // Arrange
        _mockUserRepo.Setup(repo => repo.GetAllUser()).Returns(new List<User>());
        _mockUserRepo.Setup(repo => repo.create(It.IsAny<User>())).Callback<User>(u => u.id = 1);

        var registerDto = new RegisterDTO
        {
            name = "New Student",
            email = "newstudent@example.com",
            password = "Password123"
        };

        // Act
        var result = _controller.RegisterStudent(registerDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = okResult.Value as IDictionary<string, object>;
        if (value == null)
        {
            var dynamicValue = okResult.Value;
            var message = dynamicValue.GetType().GetProperty("Message").GetValue(dynamicValue, null).ToString();
            var userId = (int)dynamicValue.GetType().GetProperty("UserId").GetValue(dynamicValue, null);

            Assert.Equal("Registration successful", message);
            Assert.Equal(1, userId);
        }
        else
        {
            Assert.Equal("Registration successful", value["Message"]);
            Assert.Equal(1, value["UserId"]);
        }
    }
}
