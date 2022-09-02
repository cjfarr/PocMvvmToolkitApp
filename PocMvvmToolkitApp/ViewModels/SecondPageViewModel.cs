namespace PocMvvmToolkitApp.ViewModels
{
    using Microsoft.Toolkit.Mvvm.Input;
    using PocMvvmToolkitApp.Args;
    using Services;
    using System.Windows.Input;

    public class SecondPageViewModel : PageViewModel, ISecondPageViewModel
    {
        private readonly ITestService testService;

        private ICommand goBackCommand;

        public SecondPageViewModel(
            ITestService testService,
            INavigationService navigationService) 
            : base(navigationService)
        {
            this.testService = testService;
        }

        public string Message
        {
            get => $"Second Page TestService.GetHashCode() = {this.testService.GetHashCode()}";
        }

        public ICommand GoBackCommand
        {
            get => this.goBackCommand ?? (this.goBackCommand = new RelayCommand(() =>
            {
                if (this.navigationService.CanGoBack)
                {
                    this.navigationService.GoBack();
                };
            }));
        }

        public override void OnNavigatedTo(object parameter)
        {
            base.OnNavigatedTo(parameter);
            if (parameter != null)
            {
                System.Diagnostics.Debug.WriteLine("SecondPage received args: " + parameter.ToString());
            }

            System.Diagnostics.Debug.WriteLine("SecondPageViewModel.OnNavigatedTo called");
        }

        public override void OnNavigatedFrom(object parameter)
        {
            System.Diagnostics.Debug.WriteLine("SecondPageViewModel.OnNavigatedFrom called");
        }
    }
}
