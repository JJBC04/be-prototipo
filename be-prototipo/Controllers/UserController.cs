using be_prototipo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace be_prototipo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;//Inyeccion de dependencias del contexto
        public UserController(ApplicationDbContext context)
        {
            this._context= context;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            try
            {
                var users = await _context.Users.FirstOrDefaultAsync(x => x.Id==id);
                return users != null ? Ok(users) : Ok("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login(Login user)
        {
            try
            {
                var userAvailable = await _context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefaultAsync();
                if (userAvailable != null)
                {
                    var userValue = await _context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefaultAsync();
                    return Ok(userValue);
                }
                return Ok("Failure");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            try
            {
                var existingUser = await _context.Users.FirstAsync(u => u.Id == id);
                if (existingUser != null)
                {
                    existingUser.Age = user.Age;
                    existingUser.MobileNumber =user.MobileNumber;
                    existingUser.Fullname = user.Fullname;
                    existingUser.Email = user.Email;
                    existingUser.Genre =user.Genre;
                    existingUser.Password = user.Password;
                    await _context.SaveChangesAsync();
                    return Ok(existingUser);
                }
                return Ok("Failure");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var existingUser = await _context.Users.FirstAsync(u => u.Id == id);
                if (existingUser != null)
                {
                    _context.Users.Remove(existingUser);
                    await _context.SaveChangesAsync();
                    return Ok("Delete");
                }
                return NotFound("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
