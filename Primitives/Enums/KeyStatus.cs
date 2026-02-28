namespace Primitives.Enums;

public enum KeyStatus
{
    Active,
    Blocked
}

public static class KeyBlockStatusExtensions
{
    public static string ToMessage(this KeyStatus keyStatus)
    {
        return keyStatus switch
        {
            KeyStatus.Active => "Активный",
            KeyStatus.Blocked => "Заблокированный"
        };
    }
}