using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XiaoyuToolbox.Common;

namespace XiaoyuToolbox.Views.Encoding;

public partial class TextToUnicodeView : UserControl
{
    public TextToUnicodeView()
    {
        InitializeComponent();
    }
}

public partial class TextToUnicodeViewModel : ObservableObject
{
    [ObservableProperty] private string textToConvert = string.Empty;
    [ObservableProperty] private string convertedText = string.Empty;

    [RelayCommand]
    private void Convert()
    {
        if (string.IsNullOrEmpty(TextToConvert))
        {
            Util.ShowMessageBoxOK("请输入要转换的文本。", MessageBoxImage.Information);
            return;
        }

        string result = string.Empty;
        foreach (Rune rune in TextToConvert.EnumerateRunes())
        {
            string strChar = rune.ToString();
            if (rune.Value == 10)
            {
                strChar = "\\n";
            }
            else if (rune.Value == 13)
            {
                strChar = "\\r";
            }

            result += string.Format("'{0}' -> U+{1:X4} ({1}){2}", strChar, rune.Value, Environment.NewLine);
        }

        ConvertedText = result;
    }
}
