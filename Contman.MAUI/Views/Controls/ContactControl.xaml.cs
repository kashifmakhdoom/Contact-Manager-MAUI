
using Contman.MAUI.ViewModels;
using System.Runtime.CompilerServices;

namespace Contman.MAUI.Views.Controls;

public partial class ContactControl : ContentView
{

    public ContactControl()
    {
        InitializeComponent();
    }

    /*
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        try
        {
            if (IsEditMode)
            {
                btnSave.SetBinding(Button.CommandProperty, "EditContactCommand");
            }
            else
            {
                btnSave.SetBinding(Button.CommandProperty, "AddContactCommand");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex?.InnerException?.ToString());
            throw;
        }
    }
    */
}