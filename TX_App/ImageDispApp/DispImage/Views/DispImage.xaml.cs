using Prism.Events;
using System;
using System.Collections.Generic;
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

namespace DispImage.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class DispImage : UserControl
    {
        public DispImage(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            eventAggregator.GetEvent<PubSubEvent<object>>().Subscribe((obj) =>
            {
                if (obj is DragDeltaEventArgs ddea)
                {
                    var dd = ddea.Source as Thumb;
                    double expectposi = Canvas.GetLeft(thumb1) + ddea.HorizontalChange;
                    if (expectposi > 0 && expectposi < canvas.ActualWidth)
                    {
                        Canvas.SetLeft(thumb1, expectposi);
                    }
                }
            });
            
        }
    }
}
