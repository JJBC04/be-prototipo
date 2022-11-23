using be_prototipo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace be_prototipo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext _contextPet;//Inyeccion de dependencias del contexto
        public PetController(ApplicationDbContext context)
        {
            this._contextPet= context;
        }

        // GET api/<PetController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPet([FromRoute] int id)
        {
            try
            {
                var pet = await _contextPet.Pets.FirstOrDefaultAsync(x => x.Id== id);
                if (pet != null)
                {
                    return Ok(pet);
                }
                return Ok("Pet not found");

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET api/<PetController>
        [HttpGet]
        public async Task<IActionResult> GetPetUser()
        {
            try
            {
                var pet = await _contextPet.Pets.ToListAsync();
                return Ok(pet);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        // POST api/<PetController>
        [HttpPost]
        public async Task<IActionResult> AddPet([FromBody] Pets pet)
        {
            try
            {
                await _contextPet.Pets.AddAsync(pet);
                await _contextPet.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, pet);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<PetController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PetController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
