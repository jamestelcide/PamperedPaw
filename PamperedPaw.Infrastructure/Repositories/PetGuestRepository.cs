using PamperedPaw.Core.Domain.Entities;
using PamperedPaw.Core.Domain.RepositoryContracts;
using System.Linq.Expressions;

namespace PamperedPaw.Infrastructure.Repositories
{
    public class PetGuestRepository : IPetGuestRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PetGuestRepository> _logger;

        public Task<PetGuest> AddPetAsync(PetGuest petGuest)
        {
            throw new NotImplementedException();
        }

        public Task<PetGuest> DeletePetByIDAsync(string petID)
        {
            throw new NotImplementedException();
        }

        public Task<List<PetGuest>> GetAllPetsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PetGuest> GetFilteredPetAsync(Expression<Func<PetGuest, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PetGuest?> GetPetByIDAsync(string petID)
        {
            throw new NotImplementedException();
        }

        public Task<PetGuest> UpdatePetAsync(PetGuest petGuest)
        {
            throw new NotImplementedException();
        }
    }
}
