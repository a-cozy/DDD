//using CommonDialogs;
//using MainModel;
using CommonDialogs;
using MainModel;
using MenuBar.Views;
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
    public class MenuBarViewModel : BindableBase
    {
        /// <summary>
        /// アプリケーション終了コマンド
        /// </summary>
        public DelegateCommand ExitApp { get; private set; }
        /// <summary>
        /// 画像を開くコマンド
        /// </summary>
        public DelegateCommand OpenCmd { get; private set; }
        /// <summary>
        /// 画像を開くコマンド
        /// </summary>
        public DelegateCommand ClearCmd { get; private set; }
        /// <summary>
        /// 作成者のダイアログ表示コマンド
        /// </summary>
        public DelegateCommand ShowDialogAboutUs { get; private set; }
        /// <summary>
        /// メインサポートクラスI/F
        /// </summary>
        private readonly IMainSomething _MainSomething;
        /// <summary>
        /// 画像ロードクラス I/F
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// コモンダイアログ表示サービスを表します。
        /// </summary>
        private readonly ICommonDialogService _ComDialogService = null;
        /// <summary>
        /// 確認ダイアログ表示サービス
        /// </summary>
        private readonly IDialogService _PrismDialogService = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public MenuBarViewModel(IUnityContainer service)
        {
            _LoadImage = service.Resolve<ILoadImager>();
            _ComDialogService = service.Resolve<ICommonDialogService>();
            _MainSomething = service.Resolve<IMainSomething>();

            ExitApp = new DelegateCommand(()=>
            {
                _MainSomething.ExitCmd();
            });

            OpenCmd = new DelegateCommand(() =>
            {
                using (OpenFileDialogSettings settings = new OpenFileDialogSettings())
                {
                    settings.Filter = "イメージファイル(*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*png|すべてのファイル(*.*)|*.*";

                    if (_ComDialogService.ShowDialog(settings) && !string.IsNullOrEmpty(settings.FileName))
                    {
                        _LoadImage.OpenFile(settings.FileName);
                    }
                    else
                    {
                        return;
                    }
                };
            });

            ClearCmd = new DelegateCommand(() => 
            {
                _LoadImage.DoClear();
            });

            _PrismDialogService = service.Resolve<IDialogService>();
            ShowDialogAboutUs = new DelegateCommand(() =>
            {
                IDialogParameters param = new DialogParameters();
                _PrismDialogService.ShowDialog(nameof(AboutUsDialog), param, x=> 
                {
                    Debug.WriteLine("AboutUsDialog");
                });
            });
        }
    }
}
