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
    /// UC_ZoomSlider.xaml の相互作用ロジック
    /// </summary>
    public partial class UC_ZoomSlider : UserControl
    {
        public UC_ZoomSlider()
        {
            InitializeComponent();
        }

        public float MaxValueSlider
        {
            get { return (float)GetValue(MaxValueSliderProperty); }
            set { SetValue(MaxValueSliderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MaxValueSlider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueSliderProperty =
            DependencyProperty.Register("MaxValueSlider", typeof(float), typeof(UC_ZoomSlider), new PropertyMetadata(1F));

        public float MinValueSlider
        {
            get { return (float)GetValue(MinValueSliderProperty); }
            set { SetValue(MinValueSliderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MinValueSlider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueSliderProperty =
            DependencyProperty.Register("MinValueSlider", typeof(float), typeof(UC_ZoomSlider), new PropertyMetadata(0F));



        public float ScaleNum
        {
            get { return (float)GetValue(ScaleNumProperty); }
            set { SetValue(ScaleNumProperty, value); }
        }

        //// Using a DependencyProperty as the backing store for ScaleNum.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ScaleNumProperty =
        //    DependencyProperty.Register("ScaleNum", typeof(float), typeof(UC_ZoomSlider), new PropertyMetadata(0F));
        // Using a DependencyProperty as the backing store for ScaleNum.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleNumProperty =
            DependencyProperty.Register("ScaleNum",
                                        typeof(float),
                                        typeof(UC_ZoomSlider),
                                        new PropertyMetadata(0F,
                                            (s, e) => 
                                            {
                                                var data = (UC_ZoomSlider)s;
                                                var dddd = Math.Round(data.ScaleNum, 2).ToString("0.00");
                                                data.ScaleNum = float.Parse(dddd);
                                            }));



    }
}
