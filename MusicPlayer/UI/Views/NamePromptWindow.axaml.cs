using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace MusicPlayer.UI.Views;

public partial class NamePromptWindow : Window
{
    public string? PlaylistName { get; private set; }

    public NamePromptWindow()
    {
        InitializeComponent();
    }

    private void OnOkClick(object? sender, RoutedEventArgs e)
    {
        PlaylistName = this.FindControl<TextBox>("NameBox").Text;
        Close(PlaylistName);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }
}