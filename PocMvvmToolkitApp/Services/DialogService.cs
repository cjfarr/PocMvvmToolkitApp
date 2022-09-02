namespace PocMvvmToolkitApp.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using PocMvvmToolkitApp.Args;
    using PocMvvmToolkitApp.Dialogs;
    using PocMvvmToolkitApp.Dialogs.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;

    public delegate void DialogStackChangedEvent(DialogStackChangedEventArgs args);

    public class DialogService : IDialogService
    {   
        private readonly Dictionary<Type, Type> dialogToViewModelMap;
        private readonly Dictionary<Type, Type> viewModelToDialogMap;
        private readonly Dictionary<Type, Type> interfaceToViewModelMap;

        public event DialogStackChangedEvent DialogStackChanged;

        public DialogService()
        {
            this.dialogToViewModelMap = new Dictionary<Type, Type>();
            this.viewModelToDialogMap = new Dictionary<Type, Type>();
            this.interfaceToViewModelMap = new Dictionary<Type, Type>();
        }

        public void RegisterDialog<TViewModelInterface, TViewModel, TDialog>(ServiceCollection services)
            where TViewModelInterface : IDialogViewModel
            where TViewModel : DialogViewModel
            where TDialog : ExtendedContentDialog, new()
        {
            this.dialogToViewModelMap[typeof(TDialog)] = typeof(TViewModelInterface);
            this.viewModelToDialogMap[typeof(TViewModelInterface)] = typeof(TDialog);
            this.interfaceToViewModelMap[typeof(TViewModelInterface)] = typeof(TViewModel);

            services.AddTransient(typeof(TViewModelInterface), typeof(TViewModel));
            services.AddTransient(typeof(TDialog), typeof(TDialog));
        }

        public async Task LaunchDialog<TViewModelInterface>(bool useFullScreen = false) 
            where TViewModelInterface : IDialogViewModel
        {
            Type dialogType = this.GetDialogType<TViewModelInterface>();

            SemaphoreSlim semaphore = new SemaphoreSlim(0);
            
            ExtendedContentDialog dialog = Ioc.Default.GetService(dialogType) as ExtendedContentDialog;
            IDialogViewModel viewModel = Ioc.Default.GetService<TViewModelInterface>();

            dialog.DataContext = viewModel;
            dialog.Title = viewModel.Title;
            dialog.IsFullScreen = useFullScreen;

            string styleKey = useFullScreen ? "ExtendedFullScreenDialog" : "ExtendedContentDialog";
            dialog.Style = Application.Current.Resources[styleKey] as Style;

            Action<ExtendedContentDialog> onClosing = null;

            onClosing = (ExtendedContentDialog d) =>
            {
                dialog.Closing -= onClosing;
                semaphore.Release();
            };

            dialog.Closing += onClosing;
            
            this.DialogStackChanged?.Invoke(new DialogStackChangedEventArgs()
            {
                Dialog = dialog,
                Context = DialogStackChangedContext.Showing
            });
            
            await semaphore.WaitAsync();
        }

        public async Task LaunchDialog<TViewModelInterface, TInputArgs>(TInputArgs inputArgs, bool useFullScreen = false) 
            where TViewModelInterface : IDialogInputViewModel
        {
            Type dialogType = this.GetDialogType<TViewModelInterface>();

            SemaphoreSlim semaphore = new SemaphoreSlim(0);

            ExtendedContentDialog dialog = Ioc.Default.GetService(dialogType) as ExtendedContentDialog;
            IDialogInputViewModel viewModel = Ioc.Default.GetService<TViewModelInterface>();
            viewModel.SetInputArgs(inputArgs);

            dialog.DataContext = viewModel;
            dialog.Title = viewModel.Title;
            dialog.IsFullScreen = useFullScreen;

            string styleKey = useFullScreen ? "ExtendedFullScreenDialog" : "ExtendedContentDialog";
            dialog.Style = Application.Current.Resources[styleKey] as Style;

            Action<ExtendedContentDialog> onClosing = null;

            onClosing = (ExtendedContentDialog d) =>
            {
                dialog.Closing -= onClosing;
                semaphore.Release();
            };

            this.DialogStackChanged?.Invoke(new DialogStackChangedEventArgs()
            {
                Dialog = dialog,
                Context = DialogStackChangedContext.Showing
            });

            dialog.Closing += onClosing;
            await semaphore.WaitAsync();
        }

        public Task<TReturnArgs> LaunchDialog<TViewModelInterface, TReturnArgs>(bool useFullScreen = false) 
            where TViewModelInterface : IDialogReturnViewModel
        {
            throw new NotImplementedException();
        }

        public Task<TReturnArgs> LaunchDialog<TViewModelInterface, TInputArgs, TReturnArgs>(TInputArgs inputArgs, bool useFullScreen = false) 
            where TViewModelInterface : IDialogInputAndReturnViewModel
        {
            throw new NotImplementedException();
        }

        private Type GetDialogType<TViewModel>()
        {
            Type key = typeof(TViewModel);

            if (!this.viewModelToDialogMap.ContainsKey(key))
            {
                throw new Exception($"{key.Name} was not mapped to a dialog");
            }

            return this.viewModelToDialogMap[key];
        }

        public async Task HideDialog(ExtendedContentDialog dialog)
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(0);
            this.DialogStackChanged?.Invoke(new DialogStackChangedEventArgs()
            {
                Context = DialogStackChangedContext.Closing,
                Dialog = dialog
            });

            await semaphore.WaitAsync();
        }
    }
}
