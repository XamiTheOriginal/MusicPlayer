using Avalonia.Controls;
using Avalonia.Interactivity;
using MusicPlayer.UI.ViewModels;

namespace MusicPlayer.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(); // Associe le ViewModel à la fenêtre

    }



    private void Button_Previous(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    
    private void Button_Play(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    
    private void Button_Next(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}