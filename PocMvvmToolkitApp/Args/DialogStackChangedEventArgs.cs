namespace PocMvvmToolkitApp.Args
{
    using PocMvvmToolkitApp.Dialogs;

    public enum DialogStackChangedContext
    {
        Showing,
        Closing
    };

    public class DialogStackChangedEventArgs
    {
        public DialogStackChangedContext Context
        {
            get;
            set;
        }

        public ExtendedContentDialog Dialog
        {
            get;
            set;
        }
    }
}
