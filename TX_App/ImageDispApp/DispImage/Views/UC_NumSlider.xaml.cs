using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DispImage
{
    /// <summary>
    /// UC_NumSlider.xaml の相互作用ロジック
    /// </summary>
    public partial class UC_NumSlider : UserControl
    {
        public UC_NumSlider()
        {
            InitializeComponent();
        }

        public string ImageFileName
        {
            get { return (string)GetValue(ImageFileNameProperty); }
            set { SetValue(ImageFileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageFileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageFileNameProperty =
            DependencyProperty.Register("ImageFileName", typeof(string), typeof(UC_NumSlider), new PropertyMetadata(null));

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
