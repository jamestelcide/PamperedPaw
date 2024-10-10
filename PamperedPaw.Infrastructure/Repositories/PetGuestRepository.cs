using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PamperedPaw.Core.Domain.Entities;
using PamperedPaw.Core.Domain.RepositoryContracts;
using PamperedPaw.Infrastructure.DbContext;
using System.Linq.Expressions;

namespace PamperedPaw.Infrastructure.Repositories
{
    public class PetGuestRepository : IPetGuestRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PetGuestRepository> _logger;

        public PetGuestRepository(ApplicationDbContext db, ILogger<PetGuestRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<PetGuest> AddPetAsync(PetGuest petGuest)
        {
            _logger.LogInformation("AddPetAsync of PetGuestRepository");

            _db.PetGuests.Add(petGuest);
            await _db.SaveChangesAsync();

            return petGuest;
        }

        public async Task<List<PetGuest>> GetAllPetsAsync()
        {
            _logger.LogInformation("GetAllPetsAsync of PetGuestRepository");

            return await _db.PetGuests.ToListAsync();
        }

        public async Task<PetGuest?> GetPetByIDAsync(string petID)
        {
            _logger.LogInformation("GetPetByIDAsync of PetGuestRepository");

            return await _db.PetGuests.FirstOrDefaultAsync(temp => temp.PetID == petID);
        }

        public async Task<List<PetGuest>> GetFilteredPetAsync(Expression<Func<PetGuest, bool>> predicate)
        {
            _logger.LogInformation("GetFilteredPetAsync of PetGuestRepository");

            return await _db.PetGuests.Where(predicate).ToListAsync();
        }

        public async Task<PetGuest> UpdatePetAsync(PetGuest petGuest)
        {
            _logger.LogInformation("UpdatePetAsync of PetGuestRepository");

            PetGuest? matchingPet = await _db.PetGuests
                .FirstOrDefaultAsync(temp => temp.PetID == petGuest.PetID);

            if (matchingPet == null) { return petGuest; }

            matchingPet.PetName = petGuest.PetName;
            matchingPet.PetSpecies = petGuest.PetSpecies;
            matchingPet.PetBreed = petGuest.PetBreed;
            matchingPet.OwnerName = petGuest.OwnerName;
            matchingPet.OwnerNumber = petGuest.OwnerNumber;
            matchingPet.OwnerAddress = petGuest.OwnerAddress;
            matchingPet.SpaActivity = petGuest.SpaActivity;
            matchingPet.AdditionalNotes = petGuest.AdditionalNotes;
            matchingPet.AppointmentStartTime = petGuest.AppointmentStartTime;
            matchingPet.PetPickupTime = petGuest.PetPickupTime;

            await _db.SaveChangesAsync();

            return matchingPet;
        }

        public async Task<bool> DeletePetByIDAsync(string petID)
        {
            _logger.LogInformation("DeletePetByIDAsync of PetGuestRepository");

            _db.PetGuests.RemoveRange(_db.PetGuests.Where(temp => temp.PetID == petID));
            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
