using PamperedPaw.Core.DTO;

namespace PamperedPaw.Core.ServiceContracts
{
    /// <summary>
    /// Business logic for manipulating PetGuest entity
    /// </summary>
    public interface IPetGuestService
    {
        /// <summary>
        /// Adds a new PetGuest into the list of PetGuests
        /// </summary>
        /// <param name="petGuestAddRequest"></param>
        /// <returns></returns>
        Task<PetGuestResponseDto> AddPetAsync(PetGuestAddRequestDto? petGuestAddRequestDto);

        /// <summary>
        /// Returns all PetGuests
        /// </summary>
        /// <returns>Returns a list of objects of type PetGuest</returns>
        Task<List<PetGuestResponseDto>> GetAllPetsAsync();

        /// <summary>
        /// Returns the PetGuest object based on the given PetID
        /// </summary>
        /// <param name="petID">PetID to search</param>
        /// <returns>Returns matching PetGuest object</returns>
        Task<PetGuestResponseDto?> GetPetByIDAsync(string petID);

        /// <summary>
        /// Returns all PetGuest objects that matches with the given search field and search string
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">Search string to search</param>
        /// <returns>Returns all matching PetGuests based on the given search field and search string</returns>
        Task<List<PetGuestResponseDto>> GetFilteredPetsAsync(string searchBy, string? searchString);

        /// <summary>
        /// Updates a PetGuest object based on the given PetID
        /// </summary>
        /// <param name="petGuestUpdateRequest">PetGuest details to update, including PetID</param>
        /// <returns>Returns PetGuestResponse object after updating</returns>
        Task<PetGuestResponseDto> UpdatePetsAsync(PetGuestUpdateRequestDto? petGuestUpdateRequestDto);

        /// <summary>
        /// Deletes a PetGuest object based on the given PetID
        /// </summary>
        /// <param name="petID">PetID to delete</param>
        /// <returns>If delete is successful returns true, otherwise returns false</returns>
        Task<bool> DeletePetAsync(string? petID);
        
    }
}
