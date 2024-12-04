namespace EVM.Services.Extensions;

public static class RandomExtensions
{
    public static string NextString(this Random source, int stringLength)
    {
        const string allowedChars = "abcdefghijkmnopqrstuvwxyz0123456789";
        var chars = new char[stringLength];

        for (var i = 0; i < stringLength; i++)
        {
            chars[i] = allowedChars[source.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }
}