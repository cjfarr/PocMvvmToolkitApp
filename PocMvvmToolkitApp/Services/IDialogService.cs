namespace PocMvvmToolkitApp.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using PocMvvmToolkitApp.Dialogs;
    using PocMvvmToolkitApp.Dialogs.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDialogService
    {
        event DialogStackChangedEvent DialogStackChanged;

        void RegisterDialog<TViewModelInterface, TViewModel, TDialog>(ServiceCollection services)
            where TViewModelInterface : IDialogViewModel
            where TViewModel : DialogViewModel
            where TDialog : ExtendedContentDialog, new();

        Task LaunchDialog<TViewModelInterface>(bool useFullScreen = false)
            where TViewModelInterface : IDialogViewModel;

        Task LaunchDialog<TViewModelInterface, TInputArgs>(TInputArgs inputArgs, bool useFullScreen = false)
            where TViewModelInterface : IDialogInputViewModel;

        Task<TReturnArgs> LaunchDialog<TViewModelInterface, TReturnArgs>(bool useFullScreen = false)
            where TViewModelInterface : IDialogReturnViewModel;

        Task<TReturnArgs> LaunchDialog<TViewModelInterface, TInputArgs, TReturnArgs>(TInputArgs inputArgs, bool useFullScreen = false)
            where TViewModelInterface : IDialogInputAndReturnViewModel;

        Task HideDialog(ExtendedContentDialog dialog);
    }
}
