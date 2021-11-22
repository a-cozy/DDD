using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Prism.Ioc;
using Unity;

using CommonService.ImageToData;

namespace MainModel
{
    public class MainSomething : IMainSomething, IDisposable
    {
        public BitmapSource OpenImage { get; set; }

        public string UUID { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusMessage { get; private set; } = $"クリック";

        public string FileName { get; private set; }

        public event EventHandler ExitApp;

        public event EventHandler SendSts;

        private readonly ILoadData _LoadData;

        public MainSomething(IUnityContainer container)
        {
            //_LoadData = container.Resolve<ILoadData>();
            //_LoadData.EndLoadData += (s, e) =>
            //{
            //    if (s is LoadData ld)
            //    {
            //        FileName = ld.FileName;
            //    }
            //};

            //UUID = Guid.NewGuid().ToString();

            //_LoadData.RequestEvent();
        }
        /// <summary>
        /// ファイルを開く
        /// </summary>
        /// <param name="path"></param>
        public void DoOpenFile(string path)
        {
            _ = Service.ConvertBitmapToGrayScale(new Bitmap(path), out ushort[] data, out int width, out int height);
        }
        /// <summary>
        /// なんかする
        /// </summary>
        public void DoSomething()
        {
            StatusMessage = "完了";
            SendSts?.Invoke(this, new EventArgs());
        }
        public void RequestEvent() 
            => SendSts?.Invoke(this, new EventArgs());
        /// <summary>
        /// なんかする
        /// </summary>
        public void DoCmd(string path)
        {
            FileName = path;
            SendSts?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// アプリ終了
        /// </summary>
        public void ExitCmd()
        {
            ExitApp?.Invoke(this, new EventArgs());
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }

    public interface IMainSomething
    {
        event EventHandler SendSts;

        void DoSomething();

        void RequestEvent();
        /// <summary>
        /// なんかする
        /// </summary>
        /// <param name="path"></param>
        void DoCmd(string path);
        /// <summary>
        /// ファイルを開く
        /// </summary>
        /// <param name="path"></param>
        void DoOpenFile(string path);
        /// <summary>
        /// アプリ終了コマンド
        /// </summary>
        void ExitCmd();
        /// <summary>
        /// アプリ終了イベント
        /// </summary>
        event EventHandler ExitApp;
    }
}
