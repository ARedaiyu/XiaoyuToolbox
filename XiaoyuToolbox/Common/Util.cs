using System.Windows;

namespace XiaoyuToolbox.Common;

public static class Util
{
    private static readonly Dictionary<MessageBoxImage, string> messageBoxImageMap = new()
    {
        { MessageBoxImage.Error, "错误" },
        { MessageBoxImage.Warning, "警告" },
        { MessageBoxImage.Information, "信息" }
    };

    public static bool IsLatinLetter(char ch)
    {
        return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
    }

    public static void ShowMessageBoxOK(string message, MessageBoxImage image)
    {
        MessageBox.Show(message, messageBoxImageMap[image], MessageBoxButton.OK, image);
    }
}
