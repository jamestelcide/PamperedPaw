namespace PamperedPaw.Core.Helpers
{
    public static class PetIDGenerationHelper
    {
        public static string GeneratePetID()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);

            string newUsername = $"PP-{randomNumber}";
            return newUsername;
        }
    }
}
