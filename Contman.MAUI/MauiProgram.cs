using CommunityToolkit.Maui;
using Contman.Application.Interfaces;
using Contman.Application.Interfaces.Datastore;
using Contman.Application.Usecases;
using Contman.Infrastructure.Implementations.Datastore;
using Contman.MAUI.Views;
using Microsoft.Extensions.Logging;

namespace Contman.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
                

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Repositories
            builder.Services.AddSingleton<IContactRepository, ContactInMemoryRepository>();

            // Usecases
            builder.Services.AddSingleton<IViewContactListUsecase, ViewContactListUsecase>();
            builder.Services.AddSingleton<IViewContactUsecase, ViewContactUsecase>();
            builder.Services.AddTransient<IAddContactUsecase, AddContactUsecase>();
            builder.Services.AddTransient<IEditContactUsecase, EditContactUsecase>();
            builder.Services.AddTransient<IDeleteContactUsecase, DeleteContactUsecase>();

            // Pages
            builder.Services.AddSingleton<ContactList>();
            builder.Services.AddSingleton<AddContact>();
            builder.Services.AddSingleton<EditContact>();

            return builder.Build();
        }
    }
}
