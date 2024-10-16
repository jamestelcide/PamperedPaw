﻿using System.ComponentModel.DataAnnotations;

namespace PamperedPaw.Core.Domain.Entities
{
    /// <summary>
    /// PetGuest domain model class
    /// </summary>
    public class PetGuest
    {
        [Key]
        public string PetID { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet name can not be blank")]
        public string PetName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet species can not be blank")]
        public string PetSpecies { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet breed can not be blank")]
        public string PetBreed { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner name can not be blank")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner number can not be blank")]
        public string OwnerNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner address can not be blank")]
        public string OwnerAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Spa Activity can not be blank")]
        public string SpaActivity { get; set; } = string.Empty;

        public string? AdditionalNotes { get; set; }

        [Required(ErrorMessage = "Appointment start time can not be blank")]
        public DateTime AppointmentStartTime { get; set; }

        [Required(ErrorMessage = "Pet pick up time can not be blank")]
        public DateTime PetPickupTime { get; set; }
    }
}
