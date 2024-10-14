using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PamperedPaw.Core.Domain.Entities;
using PamperedPaw.Infrastructure.DbContext;

namespace PamperedPaw.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetGuestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetGuestController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of PetGuests from table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetGuest>>> GetPets()
        {
            var pets = await _context.PetGuests.OrderBy(temp => temp.PetName).ToListAsync();
            return pets;
        }

        [HttpGet("{petID}")]
        public async Task<ActionResult<PetGuest>> GetPet(string petID)
        {
            var pet = await _context.PetGuests.FirstOrDefaultAsync(temp => temp.PetID == petID);

            if (pet == null)
            {
                return Problem(detail: "Invalid PetID", statusCode: 400, title: "Pet Search");
            }

            return pet;
        }

        [HttpPut("{petID}")]
        public async Task<IActionResult> PutPet(string petID, [Bind(nameof(PetGuest.PetID), nameof(PetGuest.PetName))] PetGuest petGuest)
        {
            if (petID != petGuest.PetID) { return BadRequest(); }

            var existingPet = await _context.PetGuests.FindAsync(petID);
            if (existingPet == null) { return NotFound(); }

            existingPet.PetName = petGuest.PetName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!PetGuestExists(petID)) { return NotFound(); }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PetGuest>> PostPet([Bind(nameof(PetGuest.PetID), nameof(PetGuest.PetName))] PetGuest petGuest)
        {
            if(_context.PetGuests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PetGuests' is null.");
            }
            _context.PetGuests.Add(petGuest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { petID = petGuest.PetID }, petGuest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(string id)
        {
            var pet = await _context.PetGuests.FindAsync(id);
            if (pet == null) { return NotFound(); }

            _context.PetGuests.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetGuestExists(string id)
        {
            return (_context.PetGuests?.Any(p => p.PetID == id)).GetValueOrDefault();
        }
    }
    
}
