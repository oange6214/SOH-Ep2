using Microsoft.AspNetCore.Mvc;
using SohatNotebook.DataService.Data;
using SohatNotebook.Entities.DbSet;
using SohatNotebook.Entities.Dtos.Incoming;

namespace SohatNotebook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // Get all
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _context.Users.Where(x => x.Status == 1).ToList();
        return Ok(users);
    }

    // Post
    [HttpPost]
    public IActionResult AddUser(UserDto userDto)
    {
        var user = new User()
        {
            LastName = userDto.LastName,
            FirstName = userDto.FirstName,
            Email = userDto.Email,
            DateOfBirth = Convert.ToDateTime(userDto.DateOfBirth),
            Phone = userDto.Phone,
            Country = userDto.Country,
            Status = 1
        };
        
        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(); // return a 201
    }

    // Get
    [HttpGet]
    [Route("GetUser")]
    public IActionResult GetUser(Guid id)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);

        return Ok(user);
    }

}