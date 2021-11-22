using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    public class ImageScaleControlor : IImageScaleControlor
    {
        /// <summary>
        /// 画像ロードクラス I/F
        /// </summary>
        private readonly IImageArrayToBitmap _ImageInf;
        /// <summary>
        /// 
        /// </summary>
        public ImageScaleControlor()
        {

        }
    }

    public interface IImageScaleControlor
    {

    }
}
