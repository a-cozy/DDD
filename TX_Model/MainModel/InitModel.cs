using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModel
{
    public class InitModel : IInitModel
    {

        public string CurrentDir { get; private set; }

        public event EventHandler DoInit;

        public InitModel()
        {
            //if(!string.IsNullOrEmpty(TmpConf.Default.CurrentDir))
            //{
            //    CurrentDir = TmpConf.Default.CurrentDir;
            //}
        }

        public void SetDir(string path)
        {
            TmpConf.Default.CurrentDir = path;
            TmpConf.Default.Save();
            CurrentDir = TmpConf.Default.CurrentDir;
            DoInit?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 要求
        /// </summary>
        public void Request()
        {
            if (!string.IsNullOrEmpty(TmpConf.Default.CurrentDir))
            {
                CurrentDir = TmpConf.Default.CurrentDir;
                DoInit?.Invoke(this, new EventArgs());
            }
        }
    }

    public interface IInitModel
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler DoInit;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        void SetDir(string path);
        /// <summary>
        /// 要求
        /// </summary>
        void Request();
    }


}
