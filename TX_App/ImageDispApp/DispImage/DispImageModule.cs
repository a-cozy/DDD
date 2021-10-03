using DispImage.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DispImage
{
    public class DispImageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            //_ = regionMan.RegisterViewWithRegion(nameof(DispImageModule), typeof(Views.DispImage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}