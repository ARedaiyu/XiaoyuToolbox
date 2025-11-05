using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace XiaoyuToolbox.Views.Calculator;

public partial class AverageCalculatorView : UserControl
{
    public AverageCalculatorView()
    {
        InitializeComponent();
    }
}

public partial class AverageCalculatorViewModel : ObservableObject
{
    [ObservableProperty] private string path;
    [ObservableProperty] private string hint;
    [ObservableProperty] private string inputText;

    [ObservableProperty] private string averageText;
    [ObservableProperty] private string medianText;
    [ObservableProperty] private string modeText;

    private void Clear()
    {
        Hint = string.Empty;

        AverageText = string.Empty;
        MedianText = string.Empty;
        ModeText = string.Empty;
    }

    private void Calculate(string[] lines)
    {
        List<int> numbers = [];
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (int.TryParse(line, out int number))
            {
                numbers.Add(number);
            }
            else
            {
                Hint = $"第{i + 1}行不是有效的数字：{line}";
                return;
            }
        }

        if (numbers.Count == 0)
        {
            Hint = "没有有效的数字";
            return;
        }

        numbers.Sort();

        double average = numbers.Average();
        AverageText = $"{average}";

        double median;
        int n = numbers.Count;
        if (n % 2 == 1)
        {
            median = numbers[n / 2];
        }
        else
        {
            median = (numbers[n / 2 - 1] + numbers[n / 2]) / 2.0;
        }
        MedianText = $"{median:F2}";

        List<Pair> grouped = [.. numbers.GroupBy(x => x)
            .Select(p => new Pair { Value = p.Key, Count = p.Count() })
            .OrderByDescending(p => p.Count)];

        int maxCount = grouped[0].Count;
        List<int> modes = [.. grouped.Where(p => p.Count == maxCount).Select(p => p.Value)];

        string modeResult = string.Empty;
        foreach (int mode in modes)
        {
            modeResult += $"{mode}\n";
        }

        ModeText = modeResult;
    }

    [RelayCommand]
    private void ChooseFile()
    {
        OpenFileDialog dialog = new()
        {
            Filter = "文本文件 (*.txt)|*.txt",
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            Path = dialog.FileName;
        }
    }

    [RelayCommand]
    private void CalculateFromFile()
    {
        Clear();

        string[] lines;
        try
        {
            lines = File.ReadAllLines(Path);
        }
        catch
        {
            Hint = "读取文件失败，请检查文件是否存在或损坏";
            return;
        }

        Calculate(lines);
    }

    [RelayCommand]
    private void CalculateFromTextBox()
    {
        Clear();

        string[] lines;
        try
        {
            lines = InputText.Split('\n');
        }
        catch
        {
            Hint = "文本内容为空或者无效";
            return;
        }

        Calculate(lines);
    }
}

public class Pair
{
    public int Value { get; set; }
    public int Count { get; set; }
}
