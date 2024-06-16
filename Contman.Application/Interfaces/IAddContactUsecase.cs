using Contact = Contman.Core.Models.Contact;

namespace Contman.Application.Interfaces
{
    public interface IAddContactUsecase
    {
        Task ExecuteAsync(Contact contact);
    }
}
