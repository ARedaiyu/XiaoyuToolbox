namespace XiaoyuToolbox.Common
{
    public static class Util
    {
        public static bool IsLatinLetter(char ch)
        {
            return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
        }
    }
}
