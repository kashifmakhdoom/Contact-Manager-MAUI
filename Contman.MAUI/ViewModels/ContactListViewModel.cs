using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contman.Application.Interfaces;
using Contman.MAUI.Views;
using System.Collections.ObjectModel;
using Contact = Contman.Core.Models.Contact;

namespace Contman.MAUI.ViewModels
{
    public partial class ContactListViewModel : ObservableObject
    {
        private readonly IViewContactListUsecase _viewContactListUsecase;
        private readonly IDeleteContactUsecase _deleteContactUsecase;

        private string _filterText;
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                LoadContactListAsync(_filterText);
            }
        }

        public ObservableCollection<Contact> ContactList
        {
            get; set;
        }

        public ContactListViewModel(
            IViewContactListUsecase viewContactListUsecase,
            IDeleteContactUsecase deleteContactUsecase
        )
        {
            _viewContactListUsecase = viewContactListUsecase;
            _deleteContactUsecase = deleteContactUsecase;

            ContactList = new ObservableCollection<Contact>();
        }

        public async Task LoadContactListAsync(string filterText = "")
        {
            ContactList.Clear();

            var contactList = await _viewContactListUsecase.ExecuteAsync(filterText);
            if (contactList is not null)
            {
                foreach (var contact in contactList)
                {
                    ContactList.Add(contact);
                }
            }
        }

        [RelayCommand]
        public async Task GoToAddContact()
        {
            await Shell.Current.GoToAsync($"{nameof(AddContact)}");
        }

        [RelayCommand]
        public async Task GoToEditContact(string id)
        {
            await Shell.Current.GoToAsync($"{nameof(EditContact)}?id={id}");
        }

        [RelayCommand]
        public async Task DeleteContact(int id)
        {
            await _deleteContactUsecase.ExecuteAsync(id);
            await LoadContactListAsync();
        }
    }
}
