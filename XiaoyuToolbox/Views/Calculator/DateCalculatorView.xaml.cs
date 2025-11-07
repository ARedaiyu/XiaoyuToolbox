using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;

namespace XiaoyuToolbox.Views.Calculator;

public partial class DateCalculatorView : UserControl
{
    public DateCalculatorView()
    {
        InitializeComponent();
    }
}

public partial class DateModel(DateTime dateTime, bool todayVisible = true) : ObservableObject
{
    private string yearText = dateTime.Year.ToString();
    public string YearText
    {
        get => yearText;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetProperty(ref yearText, value);
                return;
            }
            if (int.TryParse(value, out int year))
            {
                if (year > 0)
                {
                    SetProperty(ref yearText, value);
                }
            }
        }
    }

    private string monthText = dateTime.Month.ToString();
    public string MonthText
    {
        get => monthText;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetProperty(ref monthText, value);
                return;
            }
            if (int.TryParse(value, out int month))
            {
                if (month >= 1 && month <= 12)
                {
                    SetProperty(ref monthText, value);
                }
            }
        }
    }

    private string dayText = dateTime.Day.ToString();
    public string DayText
    {
        get => dayText;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetProperty(ref dayText, value);
                return;
            }
            if (int.TryParse(value, out int day))
            {
                if (day >= 1 && day <= 31)
                {
                    SetProperty(ref dayText, value);
                }
            }
        }
    }

    private readonly bool todayVisible = todayVisible;

    public Visibility TodayVisibility => todayVisible ? Visibility.Visible : Visibility.Collapsed;
    public bool IsDateReadonly => !todayVisible;

    [RelayCommand]
    private void Today()
    {
        YearText = DateTime.Today.Year.ToString();
        MonthText = DateTime.Today.Month.ToString();
        DayText = DateTime.Today.Day.ToString();
    }

    public DateTime ToDateTime()
    {
        return new DateTime(int.Parse(YearText), int.Parse(MonthText), int.Parse(DayText));
    }
}

public partial class DateCalculatorViewModel : ObservableObject
{
    [ObservableProperty] private DateModel baseDate = new(DateTime.Today);
    [ObservableProperty] private DateModel resultDate = new(DateTime.Today, false);
    [ObservableProperty] private DateModel startDate = new(DateTime.Today);
    [ObservableProperty] private DateModel endDate = new(DateTime.Today);

    private string offsetDays = 0.ToString();
    public string OffsetDays
    {
        get => offsetDays;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                SetProperty(ref offsetDays, value);
                return;
            }
            if (int.TryParse(value, out int days))
            {
                if (days >= 0)
                {
                    SetProperty(ref offsetDays, value);
                }
            }
        }
    }

    [ObservableProperty] private int diffDays;

    [ObservableProperty] private bool isAdd = true;
    public bool IsMinus
    {
        get => !IsAdd;
        set => IsAdd = !value;
    }

    [RelayCommand]
    private void CalculateNewDate()
    {
        try
        {
            DateTime baseDateTime = BaseDate.ToDateTime();
            ResultDate = new DateModel(baseDateTime.AddDays(int.Parse(OffsetDays) * (IsAdd ? 1 : -1)), false);
        }
        catch
        {
            MessageBox.Show("计算错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void CalculateDiffDays()
    {
        try
        {
            DateTime startDateTime = StartDate.ToDateTime();
            DateTime endDateTime = EndDate.ToDateTime();
            DiffDays = (int)(endDateTime - startDateTime).TotalDays;
        }
        catch
        {
            MessageBox.Show("计算错误", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
