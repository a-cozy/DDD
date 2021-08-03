using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TXCustomControlLib
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TXCustomControlLib"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TXCustomControlLib;assembly=TXCustomControlLib"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class TTextBox : TextBox
    {
        #region DependencyProperties

        /// <summary>
        /// 追加の表示値のプロパティ。
        /// </summary>
        public static readonly DependencyProperty SubTextProperty =
            DependencyProperty.Register(
                nameof(SubText),
                typeof(string),
                typeof(TTextBox),
                new UIPropertyMetadata(null)
            );

        /// <summary>
        /// 追加の表示するテキストを取得または設定します。
        /// </summary>
        public string SubText
        {
            get { return (string)GetValue(SubTextProperty); }
            set { SetValue(SubTextProperty, value); }
        }

        #endregion
        /// <summary>
        /// 生成
        /// </summary>
        static TTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TTextBox), new FrameworkPropertyMetadata(typeof(TTextBox)));
        }
        /// <summary>
        /// メッセージ
        /// </summary>
        public string HelpMessage
        {
            get { return (string)GetValue(HelpMessageProperty); }
            set { SetValue(HelpMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HelpMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HelpMessageProperty =
            DependencyProperty.Register("HelpMessage", typeof(string), typeof(TTextBox), new PropertyMetadata(null));

        public TTextBox()
        {
            LostFocus += (s,e)=> 
            {
                if (IsKeyboardFocusWithin == true) return;

                var binding = BindingOperations.GetBinding(this, TextProperty);

                if (binding != null && (binding.UpdateSourceTrigger == UpdateSourceTrigger.LostFocus || binding.UpdateSourceTrigger == UpdateSourceTrigger.Default))
                {
                    var bindingExpression = GetBindingExpression(TextProperty);
                    if (bindingExpression == null) return;

                    if (bindingExpression != null && (bindingExpression.BindingGroup?.Owner ?? null) is DataGrid)
                    {
                        Debug.WriteLine($"BindingGroup が {bindingExpression.BindingGroup.Owner.GetType()} に設定されています。");
                        Debug.WriteLine($"未反映状態 = {bindingExpression.BindingGroup.IsDirty}");
                    }

                    bindingExpression.UpdateSource();
                    bindingExpression.UpdateTarget();
                }
            };
        }

    }
}
