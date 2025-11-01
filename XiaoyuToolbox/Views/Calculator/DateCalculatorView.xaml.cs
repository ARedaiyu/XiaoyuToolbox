using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;

namespace XiaoyuToolbox.Views.Calculator
{
    public partial class DateCalculatorView : UserControl
    {
        public DateCalculatorView()
        {
            InitializeComponent();
        }
    }

    public partial class DateModel : ObservableObject
    {
        [ObservableProperty] private int year;
        [ObservableProperty] private int month;
        [ObservableProperty] private int day;

        private readonly bool todayVisible;

        public Visibility TodayVisibility => todayVisible ? Visibility.Visible : Visibility.Collapsed;

        public DateModel(DateTime dateTime, bool todayVisible = true)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;

            this.todayVisible = todayVisible;
        }

        [RelayCommand]
        private void Today()
        {
            Year = DateTime.Today.Year;
            Month = DateTime.Today.Month;
            Day = DateTime.Today.Day;
        }
    }

    public partial class DateCalculatorViewModel : ObservableObject
    {
        [ObservableProperty] private DateModel baseDate = new(DateTime.Today);
        [ObservableProperty] private DateModel resultDate = new(DateTime.Today, false);
        [ObservableProperty] private DateModel startDate = new(DateTime.Today);
        [ObservableProperty] private DateModel endDate = new(DateTime.Today);

        [ObservableProperty] private int offsetDays;
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
                DateTime baseDateTime = new(BaseDate.Year, BaseDate.Month, BaseDate.Day);
                ResultDate = new DateModel(baseDateTime.AddDays(OffsetDays * (IsAdd ? 1 : -1)), false);
            }
            catch
            {
                MessageBox.Show("日期不正确", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        [RelayCommand]
        private void CalculateDiffDays()
        {
            try
            {
                DateTime startDateTime = new(StartDate.Year, StartDate.Month, StartDate.Day);
                DateTime endDateTime = new(EndDate.Year, EndDate.Month, EndDate.Day);
                DiffDays = (int)(endDateTime - startDateTime).TotalDays;
            }
            catch
            {
                MessageBox.Show("日期不正确", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
