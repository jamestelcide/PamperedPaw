using Microsoft.Extensions.Logging;
using PamperedPaw.Core.Domain.Entities;
using PamperedPaw.Core.Domain.RepositoryContracts;
using PamperedPaw.Core.DTO;
using PamperedPaw.Core.Helpers;
using PamperedPaw.Core.ServiceContracts;
using Serilog;

namespace PamperedPaw.Core.Services
{
    public class PetGuestService : IPetGuestService
    {
        private readonly IPetGuestRepository _petGuestRepository;
        private readonly ILogger<PetGuestService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public PetGuestService(IPetGuestRepository petGuestRepository, 
            ILogger<PetGuestService> logger, IDiagnosticContext diagnosticContext)
        {
            _petGuestRepository = petGuestRepository;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public async Task<PetGuestResponseDto> AddPetAsync(PetGuestAddRequestDto? petGuestAddRequestDto)
        {
            _logger.LogInformation("AddPetAsync of PetGuestService");

            if (petGuestAddRequestDto == null)
            {
                throw new ArgumentNullException(nameof(petGuestAddRequestDto));
            }

            var existingPetGuest = await _petGuestRepository.GetAllPetsAsync();
            if (existingPetGuest.Any(p => p.PetName == petGuestAddRequestDto.PetName && 
                p.PetBreed == petGuestAddRequestDto.PetBreed && p.PetSpecies == petGuestAddRequestDto.PetSpecies &&
                p.AppointmentStartTime == petGuestAddRequestDto.AppointmentStartTime))
            {
                throw new ArgumentException($"A Pet with the same Name, Breed, Species, and Appointment Time has already been entered.");
            }

            PetGuest petGuest = petGuestAddRequestDto.ToPetGuest();

            string newPetID = PetIDGenerationHelper.GeneratePetID();

            while (existingPetGuest.Any(p => p.PetID == newPetID))
            {
                newPetID = PetIDGenerationHelper.GeneratePetID();
            }

            petGuest.PetID = newPetID;

            await _petGuestRepository.AddPetAsync(petGuest);
            return petGuest.ToPetGuestResponse();
        }

        public async Task<List<PetGuestResponseDto>> GetAllPetsAsync()
        {
            _logger.LogInformation("GetAllPetsAsync of PetGuestService");

            var petGuests = await _petGuestRepository.GetAllPetsAsync();

            return petGuests.Select(temp => temp.ToPetGuestResponse()).ToList();
        }

        public async Task<PetGuestResponseDto?> GetPetByIDAsync(string petID)
        {
            _logger.LogInformation("GetPetByIDAsync of PetGuestService");

            if (petID == null) { return null; }
            PetGuest? petFromResponseList = await _petGuestRepository.GetPetByIDAsync(petID);

            if (petFromResponseList == null) { return null; }
            return petFromResponseList.ToPetGuestResponse();
        }

        public async Task<List<PetGuestResponseDto>> GetFilteredPetsAsync(string searchBy, string? searchString)
        {
            _logger.LogInformation("GetFilteredPetsAsync of PetGuestService");

            List<PetGuest> petGuests;

            petGuests = searchBy switch
            {
                nameof(PetGuestResponseDto.PetID) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.PetID != null &&
                p.PetID.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.PetName) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.PetName != null &&
                p.PetName.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.PetSpecies) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.PetSpecies != null &&
                p.PetSpecies.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.PetBreed) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.PetBreed != null &&
                p.PetBreed.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.OwnerName) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.OwnerName != null &&
                p.OwnerName.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.OwnerNumber) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.OwnerNumber != null &&
                p.OwnerNumber.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.OwnerAddress) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.OwnerAddress != null &&
                p.OwnerAddress.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.SpaActivity) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.SpaActivity != null &&
                p.SpaActivity.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.AdditionalNotes) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.AdditionalNotes != null &&
                p.AdditionalNotes.Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.AppointmentStartTime) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.AppointmentStartTime.ToString().Contains(searchString ?? string.Empty)),

                nameof(PetGuestResponseDto.PetPickupTime) => await
                _petGuestRepository.GetFilteredPetAsync(p =>
                p.PetPickupTime.ToString().Contains(searchString ?? string.Empty))
            };
            return petGuests.Select(p => p.ToPetGuestResponse()).ToList();
        }

        public async Task<PetGuestResponseDto> UpdatePetsAsync(PetGuestUpdateRequestDto? petGuestUpdateRequestDto)
        {
            _logger.LogInformation("UpdatePetsAsync of PetGuestService");

            if(petGuestUpdateRequestDto == null)
            {
                throw new ArgumentNullException(nameof(petGuestUpdateRequestDto));
            }

            PetGuest? matchingPet = await _petGuestRepository.GetPetByIDAsync(petGuestUpdateRequestDto.PetID);

            if (matchingPet == null)
            {
                throw new ArgumentNullException(nameof(petGuestUpdateRequestDto.PetID), "PetID not found");
            }

            matchingPet.PetName = petGuestUpdateRequestDto.PetName;
            matchingPet.PetSpecies = petGuestUpdateRequestDto.PetSpecies;
            matchingPet.OwnerName = petGuestUpdateRequestDto.OwnerName;
            matchingPet.OwnerNumber = petGuestUpdateRequestDto.OwnerNumber;
            matchingPet.OwnerAddress = petGuestUpdateRequestDto.OwnerAddress;
            matchingPet.SpaActivity = petGuestUpdateRequestDto.SpaActivity;
            matchingPet.AdditionalNotes = petGuestUpdateRequestDto.AdditionalNotes;
            matchingPet.AppointmentStartTime = petGuestUpdateRequestDto.AppointmentStartTime;
            matchingPet.PetPickupTime = petGuestUpdateRequestDto.PetPickupTime;

            await _petGuestRepository.UpdatePetAsync(matchingPet);
            return matchingPet.ToPetGuestResponse();
        }

        public async Task<bool> DeletePetAsync(string? petID)
        {
            _logger.LogInformation("DeletePetAsync of PetGuestService");

            if (petID == null)
            {
                throw new ArgumentNullException(nameof(petID));
            }

            PetGuest? petGuest = await _petGuestRepository.GetPetByIDAsync(petID);
            if (petGuest == null) { return false; }

            await _petGuestRepository.DeletePetByIDAsync(petID);

            return true;
        }
    }
}
