using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            MessageBox.Show("请输入要转换的文本。", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
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

            result += string.Format("'{0}' -> U+{1:X4} ({1})\n", strChar, rune.Value);
        }

        ConvertedText = result;
    }
}
