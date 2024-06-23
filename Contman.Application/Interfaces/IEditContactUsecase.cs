using Contact = Contman.Core.Models.Contact;

namespace Contman.Application.Interfaces
{
    public interface IEditContactUsecase
    {
        Task ExecuteAsync(int id, Contact contact);
    }
}
