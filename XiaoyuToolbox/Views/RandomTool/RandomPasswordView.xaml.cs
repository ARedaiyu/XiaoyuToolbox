using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using XiaoyuToolbox.Common;

namespace XiaoyuToolbox.Views.RandomTool;

public partial class RandomPasswordView : UserControl
{
    public RandomPasswordView()
    {
        InitializeComponent();
    }
}

public partial class RandomPasswordViewModel : ObservableObject
{
    private readonly Random random = new();

    [ObservableProperty] private bool uppercasesIncluded = true;
    [ObservableProperty] private bool lowercasesIncluded = true;
    [ObservableProperty] private bool numbersIncluded = true;

    [ObservableProperty] private bool specialCharactersIncluded;
    [ObservableProperty] private bool hasExclusion = true;

    [ObservableProperty] private string specialCharacters = "!@#$%^&*=";
    [ObservableProperty] private string exclusionCharacters = "Oo0IiLl1";

    private string passwordLengthText = 12.ToString();
    public string PasswordLengthText
    {
        get => passwordLengthText;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetProperty(ref passwordLengthText, value);
                return;
            }
            if (int.TryParse(value, out int length))
            {
                if (length > 0 && length <= 100)
                {
                    SetProperty(ref passwordLengthText, value);
                }
            }
        }
    }

    private string passwordNumberText = 10.ToString();
    public string PasswordNumberText
    {
        get => passwordNumberText;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetProperty(ref passwordNumberText, value);
                return;
            }
            if (int.TryParse(value, out int number))
            {
                if (number > 0 && number <= 10000)
                {
                    SetProperty(ref passwordNumberText, value);
                }
            }
        }
    }

    [ObservableProperty] private string passwords;

    private List<char> GetCharList()
    {
        List<char> chars = [];
        if (UppercasesIncluded)
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                chars.Add(c);
            }
        }
        if (LowercasesIncluded)
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                chars.Add(c);
            }
        }
        if (NumbersIncluded)
        {
            for (char c = '0'; c <= '9'; c++)
            {
                chars.Add(c);
            }
        }

        if (SpecialCharactersIncluded)
        {
            foreach (char c in SpecialCharacters)
            {
                if (!chars.Contains(c))
                {
                    chars.Add(c);
                }
            }
        }
        if (HasExclusion)
        {
            foreach (char c in ExclusionCharacters)
            {
                chars.Remove(c);
            }
        }

        return chars;
    }

    [RelayCommand]
    private void Generate()
    {
        if (!int.TryParse(PasswordLengthText, out int length) ||
            !int.TryParse(PasswordNumberText, out int number))
        {
            return;
        }

        List<char> chars = GetCharList();
        StringBuilder result = new();

        for (int i = 0; i < number; i++)
        {
            StringBuilder sb = new();
            for (int j = 0; j < length; j++)
            {
                sb.Append(chars[random.Next(chars.Count)]);
            }
            result.AppendLine(sb.ToString());
        }

        Passwords = result.ToString();
    }

    [RelayCommand]
    private void Export()
    {
        if (string.IsNullOrEmpty(Passwords))
        {
            return;
        }

        SaveFileDialog dialog = new()
        {
            Title = "保存文件",
            Filter = "文本文件 (*.txt)|*.txt",
            DefaultExt = ".txt",
            AddExtension = true,
            OverwritePrompt = true
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                File.WriteAllText(dialog.FileName, Passwords);
            }
            catch
            {
                Util.ShowMessageBoxOK("保存文件失败", MessageBoxImage.Error);
            }
        }
    }

    [RelayCommand]
    private void Clear()
    {
        Passwords = string.Empty;
    }
}
