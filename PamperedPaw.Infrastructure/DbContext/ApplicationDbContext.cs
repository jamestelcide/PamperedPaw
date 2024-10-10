using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PamperedPaw.Core.Domain.Entities;
using PamperedPaw.Core.Domain.IdentityEntities;

namespace PamperedPaw.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext() { }

        public virtual DbSet<PetGuest> PetGuests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PetGuest>().ToTable("PetGuests");

            string petGuestJson = System.IO.File.ReadAllText("Seed/petguests.json");
            List<PetGuest>? petGuests = System.Text.Json.JsonSerializer.Deserialize<List<PetGuest>>(petGuestJson);

            if (petGuests != null)
            {
                foreach (PetGuest petGuest in petGuests)
                {
                    builder.Entity<PetGuest>().HasData(petGuest);
                }
            }
        }
    }
}
