namespace Business.Helpers;

public static class IdGenerator
{
    public static string GenerateUniqueIdentifier()
    {
        return Guid.NewGuid().ToString();
    }
}