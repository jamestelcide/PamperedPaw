using System.ComponentModel.DataAnnotations;

namespace PamperedPaw.Core.Domain.Entities
{
    public class PetGuest
    {
        [Key]
        public string PetID { get; set; }
        public string PetName { get; set; }
        public string PetSpecies { get; set; }
        public string PetBreed { get; set; }
        public string OwnerName { get; set; }
        public string OwnerNumber { get; set; }
        public string OwnerAddress { get; set; }
        public string SpaActivity { get; set; }
        public string AdditionalNotes { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime PetPickupTime { get; set; }
    }
}
