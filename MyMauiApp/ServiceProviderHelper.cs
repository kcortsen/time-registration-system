namespace MyMauiApp;

public static class ServiceProviderHelper
{
    public static IServiceProvider Current { get; private set; }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        Current = serviceProvider;
    }
}