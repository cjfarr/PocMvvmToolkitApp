namespace PocMvvmToolkitApp.Pages
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using PocMvvmToolkitApp.Args;
    using PocMvvmToolkitApp.Dialogs;
    using PocMvvmToolkitApp.Services;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public class RootPage : Page
    {
        private readonly IDialogService dialogService;
        private DialogShell dialogShell;

        public RootPage()
        {
            this.dialogService = Ioc.Default.GetService<IDialogService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.dialogService.DialogStackChanged += this.OnDialogStackChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.dialogService.DialogStackChanged -= this.OnDialogStackChanged;
        }

        private void OnDialogStackChanged(DialogStackChangedEventArgs args)
        {
            if (this.dialogShell == null && args.Context == DialogStackChangedContext.Showing)
            {

            }
        }
    }
}
