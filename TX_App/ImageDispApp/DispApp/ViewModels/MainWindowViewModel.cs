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

        private int _WindowWidth = 640;
        public int WindowWidth
        {
            get { return _WindowWidth; }
            set { SetProperty(ref _WindowWidth, value); }
        }

        private int _WindowHeight = 480;
        public int WindowHeight
        {
            get { return _WindowHeight; }
            set { SetProperty(ref _WindowHeight, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
