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
using System.Windows.Media.Animation;  

namespace qiquanui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {

        private double oldx, oldy, originalHeight, originalWidth, list1h, list1w, grid2h, grid2w, grid3w, grid3h, list1hper, list1wper, grid2hper, grid2wper, grid3hper, grid3wper, top1w, top1wper,canvas1h,canvas1hper;
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X;
                double y = e.GetPosition(this).Y;
                this.Left += (x - oldx);
                this.Top += (y - oldy);
            }
            
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            oldx = e.GetPosition(this).X;
            oldy = e.GetPosition(this).Y;
            
        }
        private void Window_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                maxButton.Style = Resources["normalSty"] as Style;
                this.Opacity = 1;
                Border1.Visibility = Visibility.Hidden;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                maxButton.Style = Resources["maxSty"] as Style;
                this.Opacity = 0.95;
                this.Left = 50;
                this.Top = 50;
                Border1.Visibility = Visibility.Visible;
                
            }
            
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Left = 50;
            this.Top = 20;
            originalHeight = this.Height;
            originalWidth = this.Width;
            list1h = futuresMarketListView.Height;
            list1w = futuresMarketListView.Width;
  
            grid2h = Grid2.Height;
            grid2w = Grid2.Width;
            grid3h = Grid3.Height;
            grid3w = Grid3.Width;
            top1w = Top1.Width;
            canvas1h = Canvas1.Height;
        }



        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                maxButton.Style = Resources["normalSty"] as Style;
                this.Opacity = 1;
                Border1.Visibility = Visibility.Collapsed;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                maxButton.Style = Resources["maxSty"] as Style;
                this.Left = 50;
                this.Top = 50;
                this.Opacity = 0.95;
                Border1.Visibility = Visibility.Visible;
            }

        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool ResizeControl()
        {
            list1wper =  (this.Width - (originalWidth - list1w)) / (originalWidth - (originalWidth - list1w ));
            grid2wper = grid3wper = top1wper=(this.Width - (originalWidth - grid2w)) / (originalWidth - (originalWidth - grid2w));
            list1hper = (this.Height - (originalHeight - list1h)) / (originalHeight - (originalHeight - list1h));
             grid2hper = grid3hper = canvas1hper=(this.Height - (originalHeight - list1h + Top1.Height)) / (originalHeight - (originalHeight - list1h + Top1.Height));
            Border1.Height = this.Height - 14.0;
            Border1.Width = this.Width - 14.0;
            futuresMarketListView.Width = list1w * list1wper;
            futuresMarketListView.Height = list1h * list1hper;
            
            Grid2.Width = grid2w * grid2wper;
            Grid3.Width = grid3w * grid3wper;
            Top1.Width = top1w * top1wper;
            Canvas1.Height = canvas1h * canvas1hper;
            return true;
        }
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ResizeControl();
        }

        private void Canvas1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Canvas1.Width == 27.0)
            {
                DoubleAnimation animate = new DoubleAnimation();
                animate.To = 120;
                animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate.DecelerationRatio = 1;
             //   animate.AccelerationRatio = 0.33;
                Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
               
            }
        }

         private void  Canvas1_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animate = new DoubleAnimation();
            animate.To = 27.0;
            animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
         //   animate.DecelerationRatio = 0.33;
            animate.AccelerationRatio = 1;
            Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
        }


    }
}
