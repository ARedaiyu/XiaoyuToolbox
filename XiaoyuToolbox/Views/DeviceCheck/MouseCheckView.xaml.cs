using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace XiaoyuToolbox.Views.DeviceCheck;

public partial class MouseCheckView : UserControl
{
    private MouseCheckViewModel ViewModel => DataContext as MouseCheckViewModel;

    public MouseCheckView()
    {
        InitializeComponent();
    }

    private void UserControlPreviewMouseDownOrUp(object sender, MouseButtonEventArgs e)
    {
        ViewModel.SetLeft(e.LeftButton == MouseButtonState.Pressed);
        ViewModel.SetMiddle(e.MiddleButton == MouseButtonState.Pressed);
        ViewModel.SetRight(e.RightButton == MouseButtonState.Pressed);
    }

    private async void UserControlPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (e.Delta > 0)
        {
            await ViewModel.PulseWheelUpAsync();
        }
        else if (e.Delta < 0)
        {
            await ViewModel.PulseWheelDownAsync();
        }
    }
}

public partial class MouseCheckViewModel : ObservableObject
{
    private readonly Brush activeColor = Brushes.LightGray;
    private readonly Brush inactiveColor = Brushes.White;

    private readonly int delay = 100;

    [ObservableProperty] private Brush leftColor;
    [ObservableProperty] private Brush rightColor;
    [ObservableProperty] private Brush middleColor;
    [ObservableProperty] private Brush wheelUpColor;
    [ObservableProperty] private Brush wheelDownColor;

    public MouseCheckViewModel()
    {
        LeftColor = inactiveColor;
        RightColor = inactiveColor;
        MiddleColor = inactiveColor;
        WheelUpColor = inactiveColor;
        WheelDownColor = inactiveColor;
    }

    public void SetLeft(bool pressed)
    {
        LeftColor = pressed ? activeColor : inactiveColor;
    }

    public void SetRight(bool pressed)
    {
        RightColor = pressed ? activeColor : inactiveColor;
    }

    public void SetMiddle(bool pressed)
    {
        MiddleColor = pressed ? activeColor : inactiveColor;
    }

    public async Task PulseWheelUpAsync()
    {
        WheelUpColor = activeColor;
        await Task.Delay(delay);
        WheelUpColor = inactiveColor;
    }

    public async Task PulseWheelDownAsync()
    {
        WheelDownColor = activeColor;
        await Task.Delay(delay);
        WheelDownColor = inactiveColor;
    }
}
