using DispImageWindow;
using MainModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ImageCtrlDisp.ViewModels
{
    public class ImageCtrlDispViewModel : BindableBase
    {
        private readonly ILoadImager _LoadImager;

        private readonly IRegionManager _RegionManager;

        public ImageCtrlDispViewModel(IUnityContainer service)
        {
            _RegionManager = service.Resolve<IRegionManager>();
            _RegionManager.RegisterViewWithRegion(
                nameof(DispImageWindowModule), typeof(DispImageWindow.Views.DispImageWindow));

            _LoadImager = service.Resolve<ILoadImager>();
            _LoadImager.ClearImage += (s, e) =>
            {
                var dd = _RegionManager.Regions.ToList().Find(
                    p => p.Name == nameof(DispImageWindowModule));

                dd.RemoveAll();

                _RegionManager.RegisterViewWithRegion(
                    nameof(DispImageWindowModule), typeof(DispImageWindow.Views.DispImageWindow));

            };
        }
    }
}
