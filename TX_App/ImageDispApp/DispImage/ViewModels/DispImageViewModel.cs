using MainModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Unity;

namespace DispImage.ViewModels
{
    public class DispImageViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }
        /// <summary>
        /// 画像ローダー
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service"></param>
        public DispImageViewModel(IUnityContainer service)
        {
            Message = "FileName";

            _ImageSource = new BitmapImage();
            _LoadImage = service.Resolve<ILoadImager>();
            _LoadImage.CmpLoadImage += (s, e) =>
            {
                if (s is LoadImager li)
                {
                    ImageSource = li.DispImage;
                }
            };
        }
    }
}
