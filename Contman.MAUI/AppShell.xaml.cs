
using Contman.MAUI.Views;

namespace Contman.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Add page routes
            Routing.RegisterRoute(nameof(ContactList), typeof(ContactList));
            Routing.RegisterRoute(nameof(AddContact), typeof(AddContact));
            Routing.RegisterRoute(nameof(EditContact), typeof(EditContact));
        }
    }
}
