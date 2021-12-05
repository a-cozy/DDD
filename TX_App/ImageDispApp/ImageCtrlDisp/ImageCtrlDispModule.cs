using ImageCtrlDisp.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ImageCtrlDisp
{
    public class ImageCtrlDispModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            _ = regionMan.RegisterViewWithRegion(nameof(ImageCtrlDispModule), typeof(Views.ImageCtrlDispView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<ViewA>();
            //containerRegistry.RegisterForNavigation<ViewB>();
        }
    }
}