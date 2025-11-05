using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XiaoyuToolbox.Common;

namespace XiaoyuToolbox.Views.Calculator;

public partial class BaseConversion1View : UserControl
{
    public BaseConversion1View()
    {
        InitializeComponent();
    }
}

public partial class BaseItem : ObservableObject
{
    [ObservableProperty] private int baseValue;
    [ObservableProperty] private string baseName;

    private string text;
    public string Text
    {
        get => text;
        set
        {
            if (IsTextValid(value))
            {
                SetProperty(ref text, value);
            }
        }
    }

    private static bool IsTextValid(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return true;
        }

        int startIndex = 0;
        if (text[0] == '-' || text[0] == '+')
        {
            startIndex = 1;
        }

        for (int i = startIndex; i < text.Length; i++)
        {
            char ch = text[i];
            if (!char.IsDigit(ch) && !Util.IsLatinLetter(ch))
            {
                return false;
            }
        }

        return true;
    }
}

public partial class BaseConverter1ViewModel : ObservableObject
{
    public ObservableCollection<BaseItem> BaseItems { get; } =
    [
        new BaseItem() { BaseName = "二进制", BaseValue = 2 },
        new BaseItem() { BaseName = "八进制", BaseValue = 8 },
        new BaseItem() { BaseName = "十进制", BaseValue = 10 },
        new BaseItem() { BaseName = "十六进制", BaseValue = 16 }
    ];

    [RelayCommand]
    private void Convert(BaseItem source)
    {
        if (source == null)
        {
            return;
        }

        try
        {
            BigInteger value = BigIntegerConverter.Parse(source.Text, source.BaseValue);

            foreach (BaseItem item in BaseItems)
            {
                string text = BigIntegerConverter.ToString(value, item.BaseValue);
                item.Text = text;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

public static class BigIntegerConverter
{
    public static BigInteger Parse(string value, int fromBase)
    {
        value = value.Trim().ToUpper();
        bool isNegative = value.StartsWith('-');
        if (value.StartsWith('-') || value.StartsWith('+'))
        {
            value = value[1..];
        }

        if (value.Length == 0)
        {
            throw new ArgumentException("数字为空");
        }

        BigInteger result = BigInteger.Zero;
        foreach (char ch in value)
        {
            int digit;
            if (char.IsDigit(ch))
            {
                digit = ch - '0';
            }
            else if (ch >= 'A' && ch <= 'Z')
            {
                digit = ch - 'A' + 10;
            }
            else
            {
                throw new ArgumentException($"无效字符：{ch}");
            }

            if (digit >= fromBase)
            {
                throw new ArgumentException($"字符{ch}超出{fromBase}进制范围");
            }

            result = result * fromBase + digit;
        }

        if (isNegative)
        {
            result = -result;
        }
        return result;
    }

    public static string ToString(BigInteger value, int toBase)
    {
        if (value == BigInteger.Zero)
        {
            return "0";
        }

        if (toBase < 2 || toBase > 36)
        {
            throw new ArgumentException($"无效基数：{toBase}");
        }

        StringBuilder sb = new();
        BigInteger current = BigInteger.Abs(value);

        while (current > 0)
        {
            BigInteger remainder = current % toBase;
            char digitChar = remainder < 10 ?
                (char)('0' + remainder) :
                (char)('A' + remainder - 10);
            sb.Insert(0, digitChar);
            current /= toBase;
        }

        if (value < 0)
        {
            sb.Insert(0, '-');
        }

        return sb.ToString();
    }
}
