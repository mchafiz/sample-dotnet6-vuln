using Microsoft.AspNetCore.Mvc;
using Sample.DotNet6.Api.Controllers;
using Xunit;

namespace Sample.DotNet6.Api.Tests;

public class VulnerableControllerTests
{
    [Fact]
    public void Login_WithCorrectPassword_ReturnsOk()
    {
        // Arrange
        var controller = new VulnerableController();
        var password = "superSecretPassword123";

        // Act
        var result = controller.Login(password);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Logged in", okResult.Value);
    }

    [Fact]
    public void Login_WithIncorrectPassword_ReturnsUnauthorized()
    {
        // Arrange
        var controller = new VulnerableController();
        var password = "wrongPassword";

        // Act
        var result = controller.Login(password);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public void CommandInjection_ReturnsOk()
    {
        // Arrange
        var controller = new VulnerableController();
        var input = "test";

        // Act & Assert
        try
        {
            var result = controller.CommandInjection(input);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Command executed", okResult.Value);
        }
        catch (System.ComponentModel.Win32Exception)
        {
            // Expected on non-Windows systems where cmd.exe is missing
            // This still ensures the code path is executed for coverage
        }
    }

    [Fact]
    public void WeakHashing_ReturnsBase64String()
    {
        // Arrange
        var controller = new VulnerableController();
        var input = "test";

        // Act
        var result = controller.WeakHashing(input);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        Assert.IsType<string>(okResult.Value);
    }
}
