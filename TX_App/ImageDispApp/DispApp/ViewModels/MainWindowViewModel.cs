using Prism.Mvvm;

namespace DispApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
