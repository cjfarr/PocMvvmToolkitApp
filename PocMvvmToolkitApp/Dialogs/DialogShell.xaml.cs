namespace PocMvvmToolkitApp.Dialogs
{
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml.Controls;

    public sealed partial class DialogShell : UserControl
    {
        private List<ExtendedContentDialog> allDialogs;
        private List<ExtendedContentDialog> visibleDialogs;

        public DialogShell()
        {
            this.InitializeComponent();
            this.allDialogs = new List<ExtendedContentDialog>();
            this.visibleDialogs = new List<ExtendedContentDialog>();

            ////Doesn't work
            this.dialogShield.PointerEntered += this.OnModalShieldPointerEntered;
            this.dialogShield.Tapped += this.OnModalShieldTapped;
        }

        private void OnModalShieldTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ////Doesn't block click through
            e.Handled = true;
        }

        private void OnModalShieldPointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        public IReadOnlyList<ExtendedContentDialog> AllDialogs => this.allDialogs.AsReadOnly();

        public IReadOnlyList<ExtendedContentDialog> VisibleDialogs => this.visibleDialogs.AsReadOnly();

        public void ShowDialog(ExtendedContentDialog dialog)
        {
            this.allDialogs.Add(dialog);
            IEnumerable<ExtendedContentDialog> dialogsToHide = this.visibleDialogs.Where(d => !d.IsFullScreen && !d.AlwaysVisible);
            foreach (ExtendedContentDialog dialogToHide in dialogsToHide)
            {
                this.visibleDialogs.Remove(dialogToHide);
            }

            this.visibleDialogs.Add(dialog);
            this.dialogBorder.Child = dialog;
        }

        public void RemoveDialog(ExtendedContentDialog dialog)
        {
            this.visibleDialogs.Remove(dialog);
            this.allDialogs.Remove(dialog);
        }
    }
}
