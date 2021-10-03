using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized;
using Prism.Common;

namespace DispApp
{
    class DisposeBehavior : IRegionBehavior
    {
        public const string Key = nameof(DisposeBehavior);

        public IRegion Region { get; set; }

        public void Attach()
        {
            Region.Views.CollectionChanged += (s, e) =>
            {
                if(e.Action==NotifyCollectionChangedAction.Remove)
                {
                    Action<IDisposable> callDispose = d => d.Dispose();
                    foreach(var o in e.OldItems)
                    {
                        MvvmHelpers.ViewAndViewModelAction(o, callDispose);
                    }
                };
            };
        }

    }
}
