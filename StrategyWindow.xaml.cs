using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace qiquanui
{
    /// <summary>
    /// StrategyWindow.xaml 的交互逻辑
    /// </summary>
    
    public partial class StrategyWindow : Window
    {
        private double originalHeight, originalWidth, Top1_StrategyLabw, Top1_StrategyLabwper, groupListVieww, groupListViewwper, groupListViewh, groupListViewhper;
        private Storyboard groupCanvasStoryboard, groupCanvasStoryboard_Leave;
        public StrategyWindow()
        {
            InitializeComponent();

            originalHeight = this.Height;
            originalWidth = this.Width;

            Top1_StrategyLabw = Top1_StrategyLab.Width;
            groupListViewh = groupListView.Height;
            groupListVieww = groupListView.Width;

            groupCanvasStoryboard = (Storyboard)this.FindResource("groupCanvasAnimate");
            groupCanvasStoryboard_Leave = (Storyboard)this.FindResource("groupCanvasAnimate_Leave");
        }
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ResizeControl();
        }
        private bool ResizeControl()
        {
            Border1.Width = this.Width-14.0;
            Border1.Height = this.Height - 14.0;

            Top1_StrategyLabwper = (this.Width - (originalWidth - Top1_StrategyLabw)) / (originalWidth - (originalWidth - Top1_StrategyLabw));
            groupListViewwper = (this.Width - (originalWidth - groupListVieww)) / (originalWidth - (originalWidth - groupListVieww));
            groupListViewhper = (this.Height - (originalHeight - groupListViewh)) / (originalHeight - (originalHeight - groupListViewh));

            groupListView.Width = groupListViewwper * groupListVieww;
            groupListView.Height = groupListViewhper * groupListViewh;

            Top1_StrategyLab.Width = Top1_StrategyLabwper * Top1_StrategyLabw;

            return true;
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
                Border1.Visibility = Visibility.Hidden;
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
        private bool closeStoryBoardCompleted = false;
        private DoubleAnimation closeAnimation1;
        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
        private void closeWindow_Completed(object sender, EventArgs e)
        {
            closeStoryBoardCompleted = true;
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeStoryBoardCompleted)
            {
                closeAnimation1 = new DoubleAnimation();
                closeAnimation1.From = 1;    //初始值   1为  整个窗体     0.1表示窗体的十分之一
                closeAnimation1.To = 0;    //结束值    0为完全关闭
                closeAnimation1.Duration = new Duration(TimeSpan.Parse("0:0:0.3"));    //所用时间的间隔
                closeAnimation1.Completed += new EventHandler(closeWindow_Completed);
                ScaleTransform st = new ScaleTransform();

                st.CenterY = this.Height / 2;    //以纵向的方式缩小
                this.RenderTransform = st;
                st.BeginAnimation(ScaleTransform.ScaleYProperty, closeAnimation1);
                e.Cancel = true;
            }
        }

        private void groupCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            groupCanvasStoryboard.Begin(this);
        }

        private void groupCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            groupCanvasStoryboard_Leave.Begin(this);
        }
    }
}
