using Contman.Application.Interfaces;
using Contact = Contman.Core.Models.Contact;

namespace Contman.MAUI.Views;

public partial class AddContact : ContentPage
{
    private readonly IAddContactUsecase _addContactUsecase;

    public AddContact(IAddContactUsecase addContactUsecase)
	{
		InitializeComponent();
        _addContactUsecase = addContactUsecase;
    }

    private async void ctrlContact_OnSave(object sender, EventArgs e)
    {
        await _addContactUsecase.ExecuteAsync(new Contact
        {
            Name = ctrlContact.Name,
            Email = ctrlContact.Email,
            Phone = ctrlContact.Phone,
            Address = ctrlContact.Address,
        });

        await Shell.Current.GoToAsync($"//{nameof(ContactList)}");
    }

    private void ctrlContact_OnCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(ContactList)}");
    }

    private void ctrlContact_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}