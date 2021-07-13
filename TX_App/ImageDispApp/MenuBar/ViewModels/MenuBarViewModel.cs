﻿//using CommonDialogs;
//using MainModel;
using CommonDialogs;
using MainModel;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
        /// メインサポートクラスI/F
        /// </summary>
        private readonly IMainSomething _MainSomething;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILoadImager _LoadImage;
        /// <summary>
        /// コモンダイアログ表示サービスを表します。
        /// </summary>
        private readonly ICommonDialogService _ComDialogService = null;
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
        }
    }
}
