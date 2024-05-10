
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LetsTalk.Contracts.Pages;

interface IPageWithViewModel<out T> where T : ObservableRecipient
{
    T ViewModel
    {
        get;
    }
}
