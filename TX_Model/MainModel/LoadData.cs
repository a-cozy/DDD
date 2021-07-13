using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Unity;

namespace MainModel
{
    public class LoadData : ILoadData, IDisposable
    {
        public event EventHandler EndLoadData;

        public string FileName { get; private set; }

        public LoadData()
        {
            FileName = System.Environment.CurrentDirectory + "ZipData";

            EndLoadData?.Invoke(this, new EventArgs());
        }

        void IDisposable.Dispose()
        {
            //throw new NotImplementedException();
        }
        public void RequestEvent()
                => EndLoadData?.Invoke(this, new EventArgs());
    }

    public interface ILoadData
    {
        event EventHandler EndLoadData;

        void RequestEvent();
    }
}
