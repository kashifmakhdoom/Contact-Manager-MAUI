using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contman.Application.Interfaces;
using Contman.MAUI.Views;
using Contact = Contman.Core.Models.Contact;

namespace Contman.MAUI.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private Contact _contact;
        private bool _isEditMode;

        private readonly IViewContactUsecase _viewContactUsecase;
        private readonly IAddContactUsecase _addContactUsecase;
        private readonly IEditContactUsecase _editContactUsecase;

        public Contact Contact
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                SetProperty(ref _isEditMode, value);
            }
        }

        public bool IsNameProvided { get; set; }
        public bool IsEmailValid { get; set; }

        public ContactViewModel(
            IViewContactUsecase viewContactUsecase,
            IAddContactUsecase addContactUsecase,
            IEditContactUsecase editContactUsecase
        )
        {
            _viewContactUsecase = viewContactUsecase;
            _addContactUsecase = addContactUsecase;
            _editContactUsecase = editContactUsecase;
        }

        public async Task LoadContact(int id)
        {
            Contact = await _viewContactUsecase.ExecuteAsync(id);

        }


        [RelayCommand]
        public async Task UpsertContact()
        {
            if (await ValidateInput())
            {
                if (IsEditMode)
                    await _editContactUsecase.ExecuteAsync(_contact.Id, Contact);
                else
                    await _addContactUsecase.ExecuteAsync(Contact);

                //await Shell.Current.GoToAsync($"{nameof(ContactList)}");
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        public async Task BackToContactList()
        {
            await Shell.Current.GoToAsync($"{nameof(ContactList)}");
        }

        private async Task<bool> ValidateInput()
        {
            if (!IsNameProvided)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error", "Name is required!", "OK");
                return false;
            }
            if (!IsEmailValid)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error", "Email format is invalid!", "OK");
                return false;
            }
            /*if (!IsEmailProvided)
            {
                await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Error", "Email is required!", "OK");
                return false;
            }*/

            return true;
        }
    }
}
