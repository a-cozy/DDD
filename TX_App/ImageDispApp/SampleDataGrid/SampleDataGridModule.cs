using SampleDataGrid.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace SampleDataGrid
{
    public class SampleDataGridModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionMan = containerProvider.Resolve<IRegionManager>();
            _ = regionMan.RegisterViewWithRegion(nameof(SampleDataGridModule), typeof(Views.DataGridModule));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}