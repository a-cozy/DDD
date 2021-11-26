using MenuBar.ViewModels;
using MenuBar.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MenuBar
{
    public class MenuBarModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            //_ = regionMan.RegisterViewWithRegion(nameof(MenuBarModule), typeof(Views.MenuButton));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AboutUsDialog, AboutUsDialogViewModel>();
        }
    }
}