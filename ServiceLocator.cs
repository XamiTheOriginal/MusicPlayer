using System;

namespace MusicPlayer;

public static class ServiceLocator
{
    public static IServiceProvider? Instance { get; private set; }

    public static void Init(IServiceProvider? provider)
    {
        Instance = provider;
    }
}