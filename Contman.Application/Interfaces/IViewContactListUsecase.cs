using Contact = Contman.Core.Models.Contact;

namespace Contman.Application.Interfaces
{
    public interface IViewContactListUsecase
    {
        Task<List<Contact>> ExecuteAsync(string filter="");
    }
}
