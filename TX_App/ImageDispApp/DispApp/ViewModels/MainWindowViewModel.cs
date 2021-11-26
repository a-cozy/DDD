using DispImage;
using MainModel;
using MenuBar;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unity;

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

        private readonly IMainSomething _MainSomething;

        private readonly IRegionManager _RegionManager;

        //private readonly IImageCollector _ImageCollector;

        private readonly IContainerExtension _ContainerExtension;

        public MainWindowViewModel(IUnityContainer service)
        {
            _ContainerExtension = service.Resolve<IContainerExtension>();

            //_MainSomething = service.Resolve<IMainSomething>();
            //_MainSomething.ExitApp += (s, e) =>
            //{
            //    Application.Current.MainWindow.Close();
            //};

            _RegionManager = service.Resolve<IRegionManager>();
            
            _RegionManager.RegisterViewWithRegion(
                nameof(MenuBarModule), typeof(MenuBar.Views.MenuBarView));

            _RegionManager.RegisterViewWithRegion(
                "MenuButtonModule", typeof(MenuBar.Views.MenuButton));

            _RegionManager.RegisterViewWithRegion(
                nameof(DispImage.DispImageModule), typeof(DispImage.Views.DispImage));


        }
        private void AddModule<T>(string resionName) where T : UserControl
        {
            var name = typeof(T).Name;

            var viewTarget = _RegionManager.Regions[resionName].
                Views.FirstOrDefault(x => x.GetType().Name == name);

            if(viewTarget==null)
            {
                var view = _ContainerExtension.Resolve<T>();
                _RegionManager.Regions[resionName].Add(view, name);
            }
        }
    }
}
