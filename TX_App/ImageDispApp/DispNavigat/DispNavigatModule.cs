using DispNavigat.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace DispNavigat
{
    public class DispNavigatModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewTypeB>();
            containerRegistry.RegisterForNavigation<ViewTypeA>();
        }
    }
}