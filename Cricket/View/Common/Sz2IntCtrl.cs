using System.Windows;
using System.Windows.Controls;

namespace Cricket.View.Common
{
    public class Sz2IntCtrl : Control
    {
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Sz2IntCtrl), null);
    }
}
