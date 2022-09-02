namespace PocMvvmToolkitApp.Dialogs
{
    using PocMvvmToolkitApp.Dialogs.ViewModels;

    public sealed partial class NoArgsDialog : ExtendedContentDialog
    {
        private NoArgsDialogViewModel viewModel => DataContext as NoArgsDialogViewModel;

        public NoArgsDialog()
        {
            this.InitializeComponent();
        }
    }
}
