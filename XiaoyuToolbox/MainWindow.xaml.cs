using System.Windows;
using XiaoyuToolbox.Common;
using XiaoyuToolbox.Views;
using XiaoyuToolbox.Views.Calculator;
using XiaoyuToolbox.Views.Encoding;

namespace XiaoyuToolbox;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        FixWindowSize();
        AddMenuItemClicks();

        Title += $" v{ToolboxVersion.Current}";
        ToolView.Content = new MainView();
    }

    private void FixWindowSize()
    {
        WindowStyle = WindowStyle.SingleBorderWindow;

        Width += 16;
        Height += 39;

        MinWidth += 16;
        MinHeight += 39;
    }

    private void AddMenuItemClicks()
    {
        MainViewMI.Click += (s, e) => { ToolView.Content = new MainView(); };

        TextToUnicodeMI.Click += (s, e) => { ToolView.Content = new TextToUnicodeView(); };

        BaseConversion1MI.Click += (s, e) => { ToolView.Content = new BaseConversion1View(); };
        DateCalculatorMI.Click += (s, e) => { ToolView.Content = new DateCalculatorView(); };
        AverageCalculatorMI.Click += (s, e) => { ToolView.Content = new AverageCalculatorView(); };
    }
}