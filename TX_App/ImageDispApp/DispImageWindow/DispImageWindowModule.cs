using DispImageWindow.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DispImageWindow
{
    public class DispImageWindowModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            _ = regionMan.RegisterViewWithRegion(nameof(DispImageWindowModule), typeof(Views.DispImageWindow));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}