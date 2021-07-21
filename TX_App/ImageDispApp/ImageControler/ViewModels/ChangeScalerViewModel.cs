using MainModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ImageControler.ViewModels
{
    public class ChangeScalerViewModel : BindableBase
    {
        ///// <summary>
        ///// スケール値
        ///// </summary>
        private string _ScaleNum;
        public string ScaleNum
        {
            get => _ScaleNum;
            set
            {
                //if(string.IsNullOrEmpty(value)) 
                if (!string.IsNullOrEmpty(value)&&float.TryParse(value.ToString(),out float zoom))
                {
                    if (_ScaleNum == value)
                        return;
                    _ScaleNum = value;
                    RaisePropertyChanged();
                    _Adjuter.SetZoomValue(float.Parse(_ScaleNum));
                }
            }
        }
        /// <summary>
        /// 画像倍率調整I/F
        /// </summary>
        private readonly IScaleAdjuster _Adjuter;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ChangeScalerViewModel(IUnityContainer service)
        {
            _Adjuter = service.Resolve<IScaleAdjuster>();
            _Adjuter.CmpInitZoomRateImage += (s, e) =>
            {
                if (s is ScaleAdjuster sa)
                {
                    ScaleNum = sa.ZoomRate.ToString("00.00");
                }
            };
            ScaleNum = "0.00";
        }
    }
}
