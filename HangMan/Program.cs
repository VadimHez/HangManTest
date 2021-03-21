using HangManServices;
using Microsoft.Extensions.DependencyInjection;

namespace HangMan
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IHangManService, HangManService>()
                .AddSingleton<IHangManUI, HangManUI>()
                .BuildServiceProvider();
            //

            var hangManUI = serviceProvider.GetService<IHangManUI>();
            hangManUI.StartApplication();
        }

    }
}
