using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
      
        protected string CurrentIdentityUserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

       
        protected int CurrentEmployeeId
        {
            get
            {
                var employeeIdClaim = User.FindFirstValue("EmployeeID");
                return int.TryParse(employeeIdClaim, out var id) ? id : 0;
            }
        }

        protected string? CurrentUserName =>
            User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;

        protected IActionResult Success(object data, int statuscode = 200, string message = "Success")
        {
            return Ok(new
            {
                statusCode = statuscode,
                success = true,
                message,
                data
            });
        }

        protected IActionResult Error(string message, int statuscode = 500)
        {
            return BadRequest(new
            {
                statusCode = statuscode,
                success = false,
                message
            });
        }
    }
}
