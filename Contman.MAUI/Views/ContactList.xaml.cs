
using Contman.Application.Interfaces;
using Contman.MAUI.ViewModels;
using System.Collections.ObjectModel;
using Contact = Contman.Core.Models.Contact;

namespace Contman.MAUI.Views;

public partial class ContactList : ContentPage
{
    private readonly ContactListViewModel _contactListViewModel;
    //private readonly IViewContactListUsecase _viewContactListUsecase;
    //private readonly IDeleteContactUsecase _deleteContactUsecase;

    public ContactList(ContactListViewModel contactListViewModel)
    {
        InitializeComponent();
        _contactListViewModel = contactListViewModel;

        BindingContext = _contactListViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _contactListViewModel.LoadContactListAsync();
    }

    /*
    public ContactList(IViewContactListUsecase viewContactListUsecase, 
        IDeleteContactUsecase deleteContactUsecase)
	{
		InitializeComponent();
        _viewContactListUsecase = viewContactListUsecase;
        _deleteContactUsecase = deleteContactUsecase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SearchBar.Text = string.Empty;
        this.LoadContactList();
    }

    private async void lsvContactList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(lsvContactList.SelectedItem != null)
        {
            var selectedContact = lsvContactList.SelectedItem as Contact;
            //await DisplayAlert("Message", $"Hello {selectedContact.Name}", "Ok");
            await Shell.Current.GoToAsync($"{nameof(EditContact)}?id={selectedContact.Id}");
        }
    }

    private void lsvContactList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        lsvContactList.SelectedItem = null;
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContact));
    }

    private async void MenuItem_DeleteClicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;

        await _deleteContactUsecase.ExecuteAsync(contact.Id);
        LoadContactList();
    }

    private async void LoadContactList()
    {
        var contactList = new ObservableCollection<Contact>(await _viewContactListUsecase.ExecuteAsync());
        lsvContactList.ItemsSource = contactList;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchBar = sender as SearchBar;
        var contactList = new ObservableCollection<Contact>(await _viewContactListUsecase.ExecuteAsync(searchBar.Text));
        lsvContactList.ItemsSource = contactList;
    }
    */

}