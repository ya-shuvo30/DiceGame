public static class Utils
{
    public static string Pad(string text, int length)
    {
        return text.PadRight(length).Substring(0, length);
    }
}