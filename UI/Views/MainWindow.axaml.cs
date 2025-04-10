using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.UI.ViewModels;

namespace MusicPlayer.UI.Views;

public partial class MainWindow : Window
{
    
    private Player _player => ServiceLocator.Instance.GetRequiredService<Player>();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(); // Associe le ViewModel à la fenêtre

    }

    private void Button_Previous(object? sender, RoutedEventArgs e)
    {
        _player.PreviousSong();
    }
    
    private void Button_Play(object? sender, RoutedEventArgs e)
    {
        _player.PlayDaMusic();
    }
    
    private void Button_Next(object? sender, RoutedEventArgs e)
    {
        _player.NextSong();
    }
}