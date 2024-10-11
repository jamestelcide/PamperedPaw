using PamperedPaw.Core.Domain.Entities;

namespace PamperedPaw.Core.DTO
{
    /// <summary>
    /// Dto class that is used as a return type for methods of PetGuestService
    /// </summary>
    public class PetGuestResponseDto
    {
        public string PetID { get; set; } = string.Empty;
        public string PetName { get; set; } = string.Empty;
        public string PetSpecies { get; set; } = string.Empty;
        public string PetBreed { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerNumber { get; set; } = string.Empty;
        public string OwnerAddress { get; set; } = string.Empty;
        public string SpaActivity { get; set; } = string.Empty;
        public string? AdditionalNotes { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime PetPickupTime { get; set; }

        /// <summary>
        /// Compares the current object data with the parameter object
        /// </summary>
        /// <param name="obj">The PetGuestResponse object to compare</param>
        /// <returns>True or False, indicating whether all PetGuest details are matched with the specified parameter object</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }
            if (obj.GetType() != typeof(PetGuestResponseDto)) { return false; }

            PetGuestResponseDto petGuest = (PetGuestResponseDto)obj;

            return PetID == petGuest.PetID &&
            PetName == petGuest.PetName &&
            PetSpecies == petGuest.PetSpecies &&
            PetBreed == petGuest.PetBreed &&
            OwnerName == petGuest.OwnerName &&
            OwnerNumber == petGuest.OwnerNumber &&
            OwnerAddress == petGuest.OwnerAddress &&
            SpaActivity == petGuest.SpaActivity &&
            AdditionalNotes == petGuest.AdditionalNotes &&
            AppointmentStartTime == petGuest.AppointmentStartTime &&
            PetPickupTime == petGuest.PetPickupTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PetID, PetName);
        }

        public PetGuestUpdateRequestDto ToPetGuestUpdateRequest()
        {
            return new PetGuestUpdateRequestDto()
            {
                PetID = PetID,
                PetName = PetName,
                PetSpecies = PetSpecies,
                PetBreed = PetBreed,
                OwnerName = OwnerName,
                OwnerNumber = OwnerNumber,
                OwnerAddress = OwnerAddress,
                SpaActivity = SpaActivity,
                AdditionalNotes = AdditionalNotes,
                AppointmentStartTime = AppointmentStartTime,
                PetPickupTime = PetPickupTime
            };
        }
    }

    public static class PetGuestExtension
    {
        public static PetGuestResponseDto ToPetGuestResponse(this PetGuest petGuest)
        {
            return new PetGuestResponseDto()
            {
                PetID = petGuest.PetID,
                PetName = petGuest.PetName,
                PetSpecies = petGuest.PetSpecies,
                PetBreed = petGuest.PetBreed,
                OwnerName = petGuest.OwnerName,
                OwnerNumber = petGuest.OwnerNumber,
                OwnerAddress = petGuest.OwnerAddress,
                SpaActivity = petGuest.SpaActivity,
                AdditionalNotes = petGuest.AdditionalNotes,
                AppointmentStartTime = petGuest.AppointmentStartTime,
                PetPickupTime = petGuest.PetPickupTime
            };
        }
    }
}
