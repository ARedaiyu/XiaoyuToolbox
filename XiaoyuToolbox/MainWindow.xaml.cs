using System.Windows;
using System.Windows.Controls;
using XiaoyuToolbox.Common;
using XiaoyuToolbox.Views;
using XiaoyuToolbox.Views.Calculator;
using XiaoyuToolbox.Views.DeviceCheck;
using XiaoyuToolbox.Views.Encoding;
using XiaoyuToolbox.Views.RandomTool;

namespace XiaoyuToolbox;

public partial class MainWindow : Window
{
    private readonly List<MenuCategory> categories =
    [
        new MenuCategory("编码工具",
        [
            new MenuItemInfo("文本转Unicode", typeof(TextToUnicodeView))
        ]),
        new MenuCategory("计算器",
        [
            new MenuItemInfo("进制转换1", typeof(BaseConversion1View)),
            new MenuItemInfo("日期计算器", typeof(DateCalculatorView)),
            new MenuItemInfo("平均数计算器", typeof(AverageCalculatorView))
        ]),
        new MenuCategory("随机工具",
        [
            new MenuItemInfo("随机密码生成器", typeof(RandomPasswordView))
        ]),
        new MenuCategory("设备检测",
        [
            new MenuItemInfo("鼠标检测", typeof(MouseCheckView))
        ])
    ];

    public MainWindow()
    {
        InitializeComponent();
        FixWindowSize();

        AddMenuItems();
        MainViewMI.Click += (s, e) =>
        {
            ToolView.Content = new MainView();
        };

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

    private void AddMenuItems()
    {
        foreach (MenuCategory category in categories)
        {
            MenuItem categoryMenuItem = new() { Header = category.Name };
            foreach (MenuItemInfo item in category.Items)
            {
                MenuItem subItem = new() { Header = item.Title };
                subItem.Click += (s, e) =>
                {
                    ToolView.Content = Activator.CreateInstance(item.ViewType);
                };
                categoryMenuItem.Items.Add(subItem);
            }
            ToolsMI.Items.Add(categoryMenuItem);
        }
    }
}

public class MenuCategory(string name, List<MenuItemInfo> items)
{
    public string Name { get; } = name;
    public List<MenuItemInfo> Items { get; } = items;
}

public class MenuItemInfo(string title, Type viewType)
{
    public string Title { get; } = title;
    public Type ViewType { get; } = viewType;
}