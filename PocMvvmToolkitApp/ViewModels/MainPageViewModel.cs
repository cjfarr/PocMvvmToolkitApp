namespace PocMvvmToolkitApp.ViewModels
{
    using Microsoft.Toolkit.Mvvm.Input;
    using PocMvvmToolkitApp.Args;
    using PocMvvmToolkitApp.Dialogs;
    using PocMvvmToolkitApp.Dialogs.ViewModels;
    using Services;
    using System.Windows.Input;
    using Windows.UI.Xaml.Controls;

    public class MainPageViewModel : PageViewModel, IMainPageViewModel
    {
        protected readonly ITestService testService;
        protected readonly IDialogService dialogService;

        private ICommand goToSecondPageCommand;
        private ICommand showNoArgsDialogCommand;
        private ICommand showInputArgsDialogCommand;

        public MainPageViewModel(
            ITestService testService,
            INavigationService navigationService,
            IDialogService dialogService) : base(navigationService)
        {
            this.testService = testService;
            this.dialogService = dialogService;
        }

        public virtual string Message
        {
            get => $"Main Page. TestService.GetHashCode() = {this.testService.GetHashCode()}";
        }

        public ICommand GoToSecondPageCommand
        {
            get => this.goToSecondPageCommand ?? (this.goToSecondPageCommand = new RelayCommand(() =>
            {
                this.navigationService.Navigate<ISecondPageViewModel>(new SecondPageArgs() 
                { 
                    Id = 1, 
                    Name = "Testing Args"
                });
            }));
        }

        public ICommand ShowNoArgsDialogCommand
        {
            get => this.showNoArgsDialogCommand ?? (this.showNoArgsDialogCommand = new RelayCommand(this.OnShowNoArgsDialog));
        }

        public ICommand ShowInputArgsDialogCommand
        {
            get => this.showInputArgsDialogCommand ?? (this.showInputArgsDialogCommand = new RelayCommand(this.OnShowInputArgsDialog));
        }

        public override void OnNavigatedTo(object parameter)
        {
            base.OnNavigatedTo(parameter);
            System.Diagnostics.Debug.WriteLine("MainPageViewModel.OnNavigatedTo called");
        }

        public override void OnNavigatedFrom(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("MainPageViewModel.OnNavigatedFrom called");
        }

        private async void OnShowNoArgsDialog()
        {
            System.Diagnostics.Debug.WriteLine("Before No Args Dialog");
            await this.dialogService.LaunchDialog<INoArgsDialogViewModel>();
            System.Diagnostics.Debug.WriteLine("After No Args Dialog");
        }

        private async void OnShowInputArgsDialog()
        {
            System.Diagnostics.Debug.WriteLine("Before Input Args Dialog");

            await this.dialogService.LaunchDialog<IInputArgsDialogViewModel, DialogInputArgs>(new DialogInputArgs()
            {
                Title = "Input Only Dialog",
                InputMessage = "This was one of the Input Args"
            });

            System.Diagnostics.Debug.WriteLine("After Input Args Dialog");
        }
    }
}
