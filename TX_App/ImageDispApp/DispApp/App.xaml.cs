using CommonDialogs;
using DispApp.Views;
using DispImage;
using MainModel;
using MenuBar;
using MessageBoxLib;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace DispApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICommonDialogService, CommonDialogService>();
            containerRegistry.RegisterSingleton<ILoadData, LoadData>();
            containerRegistry.RegisterSingleton<ILoadImager, LoadImager>();
            containerRegistry.RegisterSingleton<IMainSomething, MainSomething>();
            containerRegistry.RegisterSingleton<IViewChangeHelper, ViewChangeHelper>();
            containerRegistry.RegisterInstance(this.Container);

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //コモンダイアログ用
            moduleCatalog.AddModule<MessageBoxLibModule>();
            //メニューバー用
            moduleCatalog.AddModule<MenuBarModule>(InitializationMode.WhenAvailable);
            //センタ用
            moduleCatalog.AddModule<DispImageModule>(InitializationMode.WhenAvailable);

            base.ConfigureModuleCatalog(moduleCatalog);
        }
    }
}
