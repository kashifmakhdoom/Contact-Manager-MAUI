using Contman.Application.Interfaces;
using Contman.Application.Usecases;
using Contact = Contman.Core.Models.Contact;
namespace Contman.MAUI.Views;

[QueryProperty(nameof(ContactId), "id")]
public partial class EditContact : ContentPage
{
    private Contact _contact;
    private readonly IViewContactUsecase _viewContactUsecase;
    private readonly IEditContactUsecase _editContactUsecase;

    public EditContact(
        IViewContactUsecase viewContactUsecase,
        IEditContactUsecase editContactUsecase
    )
	{
		InitializeComponent();
        _viewContactUsecase = viewContactUsecase;
        _editContactUsecase = editContactUsecase;
    }

    public string ContactId
    {
        set
        {
            _contact = _viewContactUsecase.ExecuteAsync(value).GetAwaiter().GetResult();

            if (_contact is not null)
            {
                ctrlContact.Name = _contact.Name;
                ctrlContact.Email = _contact.Email;
                ctrlContact.Phone = _contact.Phone;
                ctrlContact.Address = _contact.Address;
            };

        }
    }

    private async void ctrlContact_OnSave(object sender, EventArgs e)
    {
        _contact.Name = ctrlContact.Name;
        _contact.Email = ctrlContact.Email;
        _contact.Phone = ctrlContact.Phone;
        _contact.Address = ctrlContact.Address;

        await _editContactUsecase.ExecuteAsync(_contact.Id, _contact);

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