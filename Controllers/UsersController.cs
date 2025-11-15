using ApiTeamProject.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTeamProject.Controllers
{

    public class UsersController : Controller
    {
        private readonly RestoranContext _context = new RestoranContext();


        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserDto>> GetAll() =>
        await _context.Users.Select(u => new UserDto
        {
            Id = u.Id,
            Username = u.Username,
            FullName = u.FullName,
            Role = u.Role,
            IsActive = (bool)u.IsActive,
            CreatedAt = (DateTime)u.CreatedAt
        }).ToListAsync();

        [HttpGet("GetById")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();
            return new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Role = u.Role,
                IsActive = (bool)u.IsActive,
                CreatedAt = (DateTime)u.CreatedAt
            };
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]RegisDto dto)
        {
            var u = new User
            {
                Username = dto.username,
                FullName = dto.fullname,
                Role = "waiter",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = dto.password
            };
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = u.Id }, u);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.First(o => o.Username == dto.username && o.PasswordHash == dto.password);
            if (user == null)
            { return NotFound(false); }
            return Ok(true);
        }



        [HttpPut("UpdateById")]
        public async Task<ActionResult> Update(int id, UserDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();

            u.Username = dto.Username;
            u.FullName = dto.FullName;
            u.Role = dto.Role;
            u.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("DeleteById")]
        public async Task<ActionResult> Delete(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();
            _context.Users.Remove(u);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
