using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MenuShortCut
{
    public class MenuShortCutModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            _ = regionMan.RegisterViewWithRegion(nameof(MenuShortCutModule), typeof(Views.MenuShortCut));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}