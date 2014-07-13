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

        private double oldx, oldy, originalHeight, originalWidth, list1h, list1w, grid3w, grid3h, list1hper, list1wper, grid3hper, grid3wper, top1w, top1wper, canvas1h, canvas1hper, multipleTabControlw, multipleTabControlwper, optionsTradingListVieww, optionsTradingListViewwper, optionsHoldDetailListVieww, optionsHoldDetailListViewwper, historyListVieww, historyListViewwper, userManageListVieww, userManageListViewwper, statusBar1w, statusBar1wper, canvas2h, canvas2hper;

        private Storyboard futureMarketListViewStoryboard, futureMarketListViewStoryboard_Leave, canvas1Storyboard, canvas1Storyboard_Leave, grid3Storyboard, grid3Storyboard_Leave, canvas2Storyboard_Leave, canvas2Storyboard;
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            oldx = e.GetPosition(this).X;
            oldy = e.GetPosition(this).Y;
        }

        private void Top1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X;
                double y = e.GetPosition(this).Y;
                this.Left += (x - oldx);
                this.Top += (y - oldy);
            }
        }
        private void Top1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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

            grid3h = Grid3.Height;
            grid3w = Grid3.Width;
            top1w = Top1.Width;
            canvas1h = Canvas1.Height;
            canvas2h = Canvas2.Height;
            optionsTradingListVieww = optionsTradingListView.Width;
            multipleTabControlw = multipleTabControl.Width;
            optionsHoldDetailListVieww = optionsHoldDetailListView.Width;
            historyListVieww = historyListView.Width;
            userManageListVieww = userManageListView.Width;
            statusBar1w = statusBar1.Width;

            futureMarketListViewStoryboard = (Storyboard)this.FindResource("futureMarketAnimate");
            futureMarketListViewStoryboard_Leave = (Storyboard)this.FindResource("futureMarketAnimate_Leave");
            canvas1Storyboard = (Storyboard)this.FindResource("canvas1Animate");
            canvas1Storyboard_Leave = (Storyboard)this.FindResource("canvas1Animate_Leave");
  
            grid3Storyboard = (Storyboard)this.FindResource("Grid3Animate");
            grid3Storyboard_Leave = (Storyboard)this.FindResource("Grid3Animate_Leave");

            canvas2Storyboard = (Storyboard)this.FindResource("canvas2Animate");
            canvas2Storyboard_Leave = (Storyboard)this.FindResource("canvas2Animate_Leave");

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
            userManageListViewwper=historyListViewwper = optionsHoldDetailListViewwper = optionsTradingListViewwper = multipleTabControlwper = grid3wper = top1wper = (this.Width - (originalWidth - grid3w)) / (originalWidth - (originalWidth - grid3w));
            list1hper = (this.Height - (originalHeight - list1h)) / (originalHeight - (originalHeight - list1h));
            grid3hper  = (this.Height - (originalHeight - list1h)) / (originalHeight - (originalHeight - list1h));
            statusBar1wper = this.Width / originalWidth;
            canvas2hper=canvas1hper = (this.Height-(originalHeight-canvas1h)) / (originalHeight-(originalHeight-canvas1h));
            Border1.Height = this.Height - 14.0;
            Border1.Width = this.Width - 14.0;
            futuresMarketListView.Width = list1w * list1wper;
            futuresMarketListView.Height = list1h * list1hper;
            historyListView.Width = historyListVieww * historyListViewwper;
            userManageListView.Width = userManageListVieww * userManageListViewwper;
        

            Grid3.Width = grid3w * grid3wper;
            Top1.Width = top1w * top1wper;
            Canvas1.Height = canvas1h * canvas1hper;
            Canvas2.Height = canvas2h * canvas2hper;
            optionsTradingListView.Width = optionsTradingListVieww * optionsTradingListViewwper;
            multipleTabControl.Width = multipleTabControlw * multipleTabControlwper;
            optionsHoldDetailListView.Width = optionsHoldDetailListVieww * optionsHoldDetailListViewwper;
            statusBar1.Width = statusBar1w * statusBar1wper;
            return true;
        }
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ResizeControl();
        }

        private void Canvas1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Canvas1.Width == 29.0)
            {
                DoubleAnimation animate = new DoubleAnimation();
                animate.To = 120;
                animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate.DecelerationRatio = 1;
                //   animate.AccelerationRatio = 0.33;
                Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
                canvas1Storyboard.Begin(this);
            }
        }

        private void Canvas1_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animate = new DoubleAnimation();
            animate.To = 29.0;
            animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            //   animate.DecelerationRatio = 0.33;
            animate.AccelerationRatio = 1;
            Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
            canvas1Storyboard_Leave.Begin(this);
        }
        private void Canvas2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Canvas2.Width == 29.0)
            {
                DoubleAnimation animate1 = new DoubleAnimation();
                animate1.To = 120;
                animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate1.DecelerationRatio = 1;
                //   animate.AccelerationRatio = 0.33;
                Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
                canvas2Storyboard.Begin(this);
            }
        }

        private void Canvas2_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation animate1 = new DoubleAnimation();
            animate1.To = 29.0;
            animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            //   animate.DecelerationRatio = 0.33;
            animate1.AccelerationRatio = 1;
            Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
            canvas2Storyboard_Leave.Begin(this);
        }
         private void futuresMarketListView_MouseEnter(object sender, MouseEventArgs e)
         {

             futureMarketListViewStoryboard.Begin(this);
         }

         private void futuresMarketListView_MouseLeave(object sender, MouseEventArgs e)
         {
             futureMarketListViewStoryboard_Leave.Begin(this);
         }



         private void Grid3_MouseEnter(object sender, MouseEventArgs e)
         {
             grid3Storyboard.Begin(this);
         }

         private void Grid3_MouseLeave(object sender, MouseEventArgs e)
         {
             grid3Storyboard_Leave.Begin(this);
         }

         private void optionsTradingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {

         }

         

    }
}
