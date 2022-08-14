using Microsoft.Extensions.Logging;
using Moq;
using nagp_devops_dotnet.Controllers;
using System;
using Xunit;

namespace nagp_dotnet_test
{
    public class HomeControllerTests
    {
        private readonly HomeController _homeController;
        private readonly Mock<ILogger<HomeController>> _logger;

        public HomeControllerTests()
        {
            _logger = new Mock<ILogger<HomeController>>();
            _homeController = new HomeController(_logger.Object);
        }

        [Fact]
        public void TestFunction_Should_ReturnTrue_IfTruePassed()
        {
            // Act
            var res = _homeController.TestFunction(true);

            // Assert
            Assert.True(res);
        }

        [Fact]
        public void TestFunction_Should_ReturnFalse_IfFalsePassed()
        {
            // Act
            var res = _homeController.TestFunction(false);

            // Assert
            Assert.False(res);
        }
    }
}
