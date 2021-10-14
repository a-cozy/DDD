using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MenuBar.ViewModels
{
    public class AboutUsDialogViewModel: BindableBase,IDialogAware
    {
        /// <summary>
        /// 表示メッセージ
        /// </summary>
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }
        /// <summary>
        /// 画像を開くコマンド
        /// </summary>
        public DelegateCommand OkCommand { get; private set; }

        public string Title => "確認画面";

        /// <summary>
        /// クローズ
        /// </summary>
        public event Action<IDialogResult> RequestClose;

        public AboutUsDialogViewModel(IUnityContainer service)
        {
            OkCommand = new DelegateCommand(() => 
            {
                IDialogParameters param = new DialogParameters();
                this.RequestClose.Invoke(new DialogResult());
            });
        }

        public bool CanCloseDialog()
        {
            Debug.WriteLine("Comfirming Can Close Dialog");
            return true;
            //throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            Debug.WriteLine("Comfirming On Dialog");
            //throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Debug.WriteLine("Comfirming Open Dialog");
            //throw new NotImplementedException();
        }
    }
}
