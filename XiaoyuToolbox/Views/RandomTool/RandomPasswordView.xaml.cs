using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;

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
    [ObservableProperty] private bool uppercasesIncluded = true;
    [ObservableProperty] private bool lowercasesIncluded = true;
    [ObservableProperty] private bool numbersIncluded = true;

    [ObservableProperty] private bool specialCharactersIncluded;
    [ObservableProperty] private bool hasExclusion = true;

    [ObservableProperty] private string specialCharacters;
    [ObservableProperty] private string exclusionCharacters;
    [ObservableProperty] private string passwordLengthText;
    [ObservableProperty] private string passwordNumberText;

    [ObservableProperty] private string passwords;
}
