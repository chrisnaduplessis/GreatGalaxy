namespace GreatGalaxy.Common.Utilities
{
    public static class IdGenerator
    {
        public static int GenerateId()
        {
            // In a real application, you would want to use a more robust method of generating unique IDs,
            // such as a database auto-incrementing primary key or a GUID. For simplicity, we'll just use a random number here.
            return new Random().Next(1, int.MaxValue);
        }
    }
}
