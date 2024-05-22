using Microsoft.Toolkit.Mvvm.ComponentModel;
namespace LetsTalk.Contracts.Pages
{
    public interface IMVMPage<out T> where T : ObservableObject
    {
        T ViewModel { get; }
    }
}
