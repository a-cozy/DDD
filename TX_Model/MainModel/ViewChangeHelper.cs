
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    /// <summary>
    /// ビュー変更
    /// </summary>
    public class ViewChangeHelper : IViewChangeHelper
    {
        /// <summary>
        /// 現在View
        /// </summary>
        public string CurrentView { get; private set; }
        /// <summary>
        /// 逆View
        /// </summary>
        public string OppositeView { get; private set; }
        /// <summary>
        /// View変更
        /// </summary>
        public event EventHandler ChangeView;
        /// <summary>
        /// View変更
        /// </summary>
        public event EventHandler Changelable;
        /// <summary>
        /// View変更サポートクラス
        /// </summary>
        public ViewChangeHelper()
        {
            CurrentView = "OneDispView";
            OppositeView = "QuarterDispView";
        }
        public void SetOppsitView()
        {
            var tmp = CurrentView;
            switch (CurrentView)
            {
                case ("OneDispView"):
                    OppositeView = "OneDispView";
                    CurrentView = "QuarterDispView";
                    break;
                case ("QuarterDispView"):
                    OppositeView = "QuarterDispView";
                    CurrentView = "OneDispView";
                    break;
                default:
                    throw new Exception($"{nameof(ViewChangeHelper)}have been exception!");
            }
            if (tmp != CurrentView)
            {
                ChangeView?.Invoke(this, new EventArgs());
                Changelable?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// 現在のView取得
        /// </summary>
        public void RequestCurrentlbl()
            => Changelable?.Invoke(this, new EventArgs());


    }
    public interface IViewChangeHelper
    {
        /// <summary>
        /// View変更
        /// </summary>
        event EventHandler ChangeView;
        /// <summary>
        /// View変更
        /// </summary>
        event EventHandler Changelable;
        /// <summary>
        /// 現在のView取得
        /// </summary>
        void RequestCurrentlbl();
        /// <summary>
        /// View変更
        /// </summary>
        /// <param name="viewname"></param>
        void SetOppsitView();
    }
}
