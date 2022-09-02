namespace PocMvvmToolkitApp.Pages
{
    using Microsoft.Toolkit.Mvvm.DependencyInjection;
    using ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondPage : Page
    {
        private SecondPageViewModel viewModel => DataContext as SecondPageViewModel;

        public SecondPage()
        {
            this.InitializeComponent();
            this.DataContext = Ioc.Default.GetService<ISecondPageViewModel>();
        }
    }
}
