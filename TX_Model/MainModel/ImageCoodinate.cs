using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{

    /// <summary>
    /// 画像の現在座標クラス
    /// </summary>
    public class ImageCoodinate: IImageCoodinate, INotifyPropertyChanged
    {
        /// <summary>
        /// 変更通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 現在座標Y
        /// </summary>
        private float _CurrentY;
        public float CurrentY
        {
            get => _CurrentY;
            set
            {
                if (_CurrentY == value)
                    return;
                _CurrentY = value;
                RaisePropertyChanged(nameof(CurrentY));
            }
        }
        /// <summary>
        /// 現在座標X
        /// </summary>
        private float _CurrentX;
        public float CurrentX
        {
            get => _CurrentX;
            set
            {
                if (_CurrentX == value)
                    return;
                _CurrentX = value;
                RaisePropertyChanged(nameof(CurrentX));
            }
        }
        /// <summary>
        /// 画像ローダー
        /// </summary>
        private readonly IScaleAdjuster _Scale;
        /// <summary>
        /// 画像の現在座標クラス
        /// </summary>
        public ImageCoodinate(IScaleAdjuster scale)
        {
            _Scale = scale;
            _Scale.CmpInitZoomRateImage += (s, e) => 
            {
                if(s is ScaleAdjuster sa)
                {
                    CurrentX = (float)Math.Round(sa.ImageWidth / 2F, 0, MidpointRounding.AwayFromZero);
                    CurrentY = (float)Math.Round(sa.ImageHeight / 2F, 0, MidpointRounding.AwayFromZero);
                }
            };
            _Scale.ChangeZoomRate += (s, e) =>
            {
                if (s is ScaleAdjuster sa)
                {
                    CurrentX = (float)Math.Round(sa.ImageWidth / 2F, 0, MidpointRounding.AwayFromZero);
                    CurrentY = (float)Math.Round(sa.ImageHeight / 2F, 0, MidpointRounding.AwayFromZero);
                }
            };
        }

        public void DoHorizontalChange(float x)
        {
            CurrentX = x;
        }
    }
    /// <summary>
    /// 画像の現在座標クラスI/F
    /// </summary>
    public interface IImageCoodinate
    {
        event PropertyChangedEventHandler PropertyChanged;

        void DoHorizontalChange(float x);


    }
}
