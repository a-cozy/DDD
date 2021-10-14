using CommonDialogs;
using DispApp.Views;
using DispImage;
using DispImageWindow;
using ImageControler;
using ImageCtrlDisp;
using MainModel;
using MenuBar;
using MenuBar.ViewModels;
using MenuBar.Views;
using MessageBoxLib;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
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

            containerRegistry.RegisterSingleton<IInitModel,InitModel>();

            containerRegistry.RegisterSingleton<ILoadData, LoadData>();
            
            containerRegistry.RegisterSingleton<ILoadImager, LoadImager>();
            
            containerRegistry.RegisterSingleton<IScaleAdjuster, ScaleAdjuster>();
            
            containerRegistry.RegisterSingleton<IMainSomething, MainSomething>();
            
            containerRegistry.RegisterSingleton<IViewChangeHelper, ViewChangeHelper>();

            containerRegistry.RegisterSingleton<IImageCoodinate, ImageCoodinate>();

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
            ////画像制御用
            //moduleCatalog.AddModule<ImageControlerModule>(InitializationMode.WhenAvailable);
            ////画像制御用
            //moduleCatalog.AddModule<ImageCtrlDispModule>(InitializationMode.WhenAvailable);
            ////画像制御用
            //moduleCatalog.AddModule<DispImageWindowModule>(InitializationMode.WhenAvailable);
            


            //データグリッド用
            //moduleCatalog.AddModule<SampleDataGridModule>(InitializationMode.WhenAvailable);

            base.ConfigureModuleCatalog(moduleCatalog);
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            regionBehaviors.AddIfMissing(DisposeBehavior.Key, typeof(DisposeBehavior));
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
        }
    }
}
