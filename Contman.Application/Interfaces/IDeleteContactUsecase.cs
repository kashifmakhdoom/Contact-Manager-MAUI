using Contact = Contman.Core.Models.Contact;

namespace Contman.Application.Interfaces
{
    public interface IDeleteContactUsecase
    {
        Task ExecuteAsync(int id);
    }
}
