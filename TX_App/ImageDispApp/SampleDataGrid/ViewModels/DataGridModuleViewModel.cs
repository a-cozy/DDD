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

namespace SampleDataGrid.ViewModels
{
    public class DataGridModuleViewModel : BindableBase
    {
        /// <summary>
        /// 表示画像
        /// </summary>
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }
        /// <summary>
        /// 倍率
        /// </summary>
        private int _ZoomRate;
        public int ZoomRate
        {
            get { return _ZoomRate; }
            set { SetProperty(ref _ZoomRate, value); }
        }

        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set { SetProperty(ref _FileName, value); }
        }
        /// <summary>
        /// 画像ローダー
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// 画像倍率調整I/F
        /// </summary>
        private readonly IScaleAdjuster _Adjuter;

        public DataGridModuleViewModel(IUnityContainer service)
        {
            _ImageSource = new BitmapImage();

            _LoadImage = service.Resolve<ILoadImager>();
            _LoadImage.CmpLoadImage += (s, e) =>
            {
                if (s is LoadImager li)
                {
                    ImageSource = li.DispImage;

                    FileName = li.ImgPath;
                }
            };

            _Adjuter = service.Resolve<IScaleAdjuster>();
            _Adjuter.ChangeZoomRate += (s, e) =>
            {
                if (s is ScaleAdjuster sa)
                {
                    ZoomRate = (int)sa.ZoomRate;
                }
            };
            FileName = string.Empty;
            ZoomRate = 10;

        }
    }
}
