namespace PocMvvmToolkitApp.Dialogs
{
    using ViewModels;

    public sealed partial class InputArgsDialog : ExtendedContentDialog
    {
        private InputArgsDialogViewModel viewModel => DataContext as InputArgsDialogViewModel;

        public InputArgsDialog()
        {
            this.InitializeComponent();
        }
    }
}
