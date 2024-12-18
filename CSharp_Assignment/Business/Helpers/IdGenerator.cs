namespace Business.Helpers;

public static class IdGenerator
{
    public static string Generate_UniqueIdentifier()
    {
        return Guid.NewGuid().ToString();
    }
}