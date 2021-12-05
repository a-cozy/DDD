using DispImageWindow;
using MainModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ImageCtrlDisp.ViewModels
{
    public class ImageCtrlDispViewModel : BindableBase
    {

        //private readonly IRegionManager _RegionManager;

        public DelegateCommand<string> ShowViewCommand { get; }


        public ImageCtrlDispViewModel(IUnityContainer service)

        {
            Debug.WriteLine($"{nameof(ImageCtrlDispViewModel)}is callsed");

            //_RegionManager = service.Resolve<IRegionManager>();

            this.ShowViewCommand = new DelegateCommand<string>(this.ShowView);

        }
        public void ShowView(string viewName)
        {
            //_RegionManager.RequestNavigate("ContentRegion", viewName);
        }
    }
}
