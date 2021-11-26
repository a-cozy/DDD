using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    /// <summary>
    /// 画像コレクタ
    /// </summary>
    public class ImageCollector : IImageCollector, IDisposable
    {

        public event EventHandler ChangedImageCollection;
        /// <summary>
        /// 画像情報
        /// </summary>
        public ICollection<ImageDispInf> ImageDispInfs { get; private set; }
        /// <summary>
        /// SHA_256
        /// </summary>
        public string SHA_256 { get; private set; }
        /// <summary>
        /// 追加可否
        /// </summary>
        public bool CanAddCollection { get; private set; }
        /// <summary>
        /// 画像ロードクラス I/F
        /// </summary>
        private readonly IImageDisplay _ImageDisplay;
        /// <summary>
        /// データロードクラス I/F
        /// </summary>
        private readonly ILoadData _LoadData;
        /// <summary>
        /// 
        /// </summary>
        public ImageCollector(IImageDisplay lD, ILoadData ld)
        {
            _LoadData = ld;
            _LoadData.EndLoadData += (s, e) => 
            {
                SHA_256 = (s as LoadData).ID_SHA256;
            };

            _ImageDisplay = lD;
            _ImageDisplay.ChangedImage += (s, e) =>
            {
                if (CanAddCollection)
                {
                    DoAddImage((s as ImageDisplay).ImageDispInf);
                    CanAddCollection = false;
                }
            };
            ImageDispInfs = new List<ImageDispInf>();
        }
        /// <summary>
        /// 画像追加
        /// </summary>
        /// <param name="image"></param>
        public void DoAddImage(ImageDispInf image)
        {
            ImageDispInfs.Add(image);
            ChangedImageCollection?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 画像削除
        /// </summary>
        /// <param name="image"></param>
        public void DoRemoveImage(ImageDispInf image)
        {
            foreach (var tmpimage in ImageDispInfs)
            {
                if(tmpimage.Equals(image))
                {
                    ImageDispInfs.Remove(image);
                }
            }
            ChangedImageCollection?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 当該データが追加できるか？
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool QueryData(string path)
        {
            if (ImageDispInfs.Count() > 0)
            {
                _LoadData.DoLoadData(path);

                foreach (var inf in ImageDispInfs)
                {
                    if (inf.ID_sha256 == SHA_256)
                    {
                        CanAddCollection = false;
                        return false;
                    }
                }
                CanAddCollection = true;
                return true;
            }
            else
            {
                CanAddCollection = true;
                return true;
            }
        }
        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            Debug.WriteLine($"{nameof(ImageCollector)} was disposing");
        }
    }
    public interface IImageCollector
    {
        /// <summary>
        /// 画像変更
        /// </summary>
        event EventHandler ChangedImageCollection;
        /// <summary>
        /// 画像追加
        /// </summary>
        /// <param name="image"></param>
        void DoAddImage(ImageDispInf image);
        /// <summary>
        /// 画像削除
        /// </summary>
        /// <param name="image"></param>
        void DoRemoveImage(ImageDispInf image);
        /// <summary>
        /// 当該データが追加できるか？
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool QueryData(string path);

    }
}
