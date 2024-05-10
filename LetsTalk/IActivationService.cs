using System.Threading.Tasks;

namespace LetsTalk.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);

}
