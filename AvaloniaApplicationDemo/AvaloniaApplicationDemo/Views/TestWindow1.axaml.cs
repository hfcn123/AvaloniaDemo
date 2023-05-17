using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationDemo.ViewModels;
using AvaloniaApplicationDemo.Models;

namespace AvaloniaApplicationDemo.Views;

public partial class TestWindow1 : Window
{
    public TestWindow1()
    {
        InitializeComponent();
        DataContext = new TestWindow1ViewModel();
   
    }
}