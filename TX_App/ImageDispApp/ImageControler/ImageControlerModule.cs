using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ImageControler
{
    public class ImageControlerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            _ = regionMan.RegisterViewWithRegion(nameof(ImageControlerModule), typeof(Views.ChangeScaler));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}