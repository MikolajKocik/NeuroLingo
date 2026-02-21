using System.Windows;

namespace NeuroLingo.WPF;

/// <summary>
/// Main window code-behind - minimal, only what's required by WPF.
/// All logic is in ViewModels, all UI is in XAML templates.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
