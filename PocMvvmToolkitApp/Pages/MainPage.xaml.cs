namespace PocMvvmToolkitApp
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using PocMvvmToolkitApp.Dialogs;
    using PocMvvmToolkitApp.Pages;
    using PocMvvmToolkitApp.Services;
    using PocMvvmToolkitApp.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : RootPage
    {
        private MainPageViewModel viewModel => DataContext as MainPageViewModel;

        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;

        public MainPage() : base()
        {
            this.InitializeComponent();

            this.navigationService = Ioc.Default.GetService<INavigationService>();
            this.dialogService = Ioc.Default.GetService<IDialogService>();

            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private void OnUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.dialogService.DialogStackChanged -= this.OnDialogStackChanged;
        }

        private void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.dialogService.DialogStackChanged += this.OnDialogStackChanged;
        }

        private void OnDialogStackChanged(Args.DialogStackChangedEventArgs args)
        {
            switch (args.Context)
            {
                case Args.DialogStackChangedContext.Showing:
                    if (this.dialogShell.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                    {
                        this.dialogShell.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.dialogShell.ShowDialog(args.Dialog);
                    }


                    break;
                case Args.DialogStackChangedContext.Closing:
                    if (this.dialogShell.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        this.dialogShell.RemoveDialog(args.Dialog);
                        if (this.dialogShell.AllDialogs.Count == 0)
                        {
                            this.dialogShell.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        }
                    }

                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.navigationService.AddFrame(this.nestedFrame, "NestedFrame");
            this.navigationService.Navigate<INestedPageViewModel>("NestedFrame");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.navigationService.RemoveFrame("NestedFrame");
        }
    }
}
