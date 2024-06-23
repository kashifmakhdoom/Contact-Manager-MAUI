using Contman.MAUI.ViewModels;
using Contact = Contman.Core.Models.Contact;
namespace Contman.MAUI.Views;

[QueryProperty(nameof(ContactId), "id")]
public partial class EditContact : ContentPage
{
    private Contact _contact;
    private readonly ContactViewModel _contactViewModel;
   
    public EditContact(ContactViewModel contactViewModel)
    {
        InitializeComponent();
        _contactViewModel = contactViewModel;

        // Set the binding context
        BindingContext = contactViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _contactViewModel.IsEditMode = true;

    }
    public string ContactId
    {
        set
        {
            if(!string.IsNullOrEmpty(value) && int.TryParse(value, out int contactId))
            {
                LoadContact(contactId);
            }
        }
    }

    private async void LoadContact(int id)
    {
        await _contactViewModel.LoadContact(id);
    }
}