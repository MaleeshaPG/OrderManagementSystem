using Microsoft.AspNetCore.Authorization;
using OrderManagementSystem.Controllers;

namespace OrderManagementSystem.Tests;

public class ControllerAuthorizationTests
{
    [Theory]
    [InlineData(typeof(DepartmentController))]
    [InlineData(typeof(ItemController))]
    [InlineData(typeof(OrderController))]
    public void Controllers_RequireAuthentication(Type controllerType)
    {
        var authorizeAttribute = controllerType
            .GetCustomAttributes(typeof(AuthorizeAttribute), true)
            .Cast<AuthorizeAttribute>()
            .FirstOrDefault();

        Assert.NotNull(authorizeAttribute);
    }
}
