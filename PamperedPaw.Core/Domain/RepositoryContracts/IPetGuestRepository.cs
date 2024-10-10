using PamperedPaw.Core.Domain.Entities;
using System.Linq.Expressions;

namespace PamperedPaw.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing PetGuest entity
    /// </summary>
    public interface IPetGuestRepository
    {
        /// <summary>
        /// Adds a PetGuest object to the data store
        /// </summary>
        /// <param name="petGuest">PetGuest object to add</param>
        /// <returns>Returns the PetGuest object after adding it to the table</returns>
        Task<PetGuest> AddPetAsync(PetGuest petGuest);

        /// <summary>
        /// Returns all PetGuest objects in the dat store
        /// </summary>
        /// <returns>List of PetGuest objects from table</returns>
        Task<List<PetGuest>> GetAllPetsAsync();

        /// <summary>
        /// Returns a PetGuest object based on the given PetID
        /// </summary>
        /// <param name="petID">PetID (string) to search</param>
        /// <returns>A PetGuest object or null</returns>
        Task<PetGuest?> GetPetByIDAsync(string petID);

        /// <summary>
        /// Returns all PetGuest objects based on the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>All matching PetGuest objects with a given condition</returns>
        Task<List<PetGuest>> GetFilteredPetAsync(Expression<Func<PetGuest, bool>> predicate);

        /// <summary>
        /// Updates a PetGuest object based on the given PetID
        /// </summary>
        /// <param name="petGuest">PetGuest object to update</param>
        /// <returns></returns>
        Task<PetGuest> UpdatePetAsync(PetGuest petGuest);

        /// <summary>
        /// Deletes a PetGuest object based on the PetID
        /// </summary>
        /// <param name="petID">PetID (string) to search</param>
        /// <returns>Returns true if delete is successful, otherwise returns false</returns>
        Task<bool> DeletePetByIDAsync(string petID);
    }
}
