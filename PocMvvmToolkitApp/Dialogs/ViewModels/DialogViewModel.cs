namespace PocMvvmToolkitApp.Dialogs.ViewModels
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;

    public abstract class DialogViewModel : ObservableObject, IDialogViewModel
    {
        private string title;

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        /// <summary>
        /// Occurs when Primary Button is invoked
        /// </summary>
        /// <returns>CanClose true/false</returns>
        public virtual bool OnPrimaryButtonInvoked()
        {
            return true;
        }

        /// <summary>
        /// Occurs when Secondary Button is invoked
        /// </summary>
        /// <returns>CanClose true/false</returns>
        public virtual bool OnSecondaryButtonInvoked()
        {
            return true;
        }

        /// <summary>
        /// Occurs when Closing Button is invoked
        /// </summary>
        /// <returns>CanClose true/false</returns>
        public virtual bool OnClosingButtonInvoked()
        {
            return true;
        }

        /// <summary>
        /// Occurs when dialog is closing
        /// </summary>
        public virtual void OnClosing()
        {
        }
    }
}
