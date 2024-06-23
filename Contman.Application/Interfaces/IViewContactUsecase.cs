using Contact = Contman.Core.Models.Contact;

namespace Contman.Application.Interfaces
{
    public interface IViewContactUsecase
    {
        Task<Contact> ExecuteAsync(int id);
    }
}
