using MessageBoxLib.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MessageBoxLib
{
    public class MessageBoxLibModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<NotifiedMessageBox, ViewModels.NotifiedMessageBoxViewModel>();
            containerRegistry.RegisterDialog<ConfirmedMessageBox, ViewModels.ConfirmedMessageBoxViewModel>();
        }
    }
}