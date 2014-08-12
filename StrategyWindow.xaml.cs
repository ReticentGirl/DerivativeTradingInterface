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
        private double windowShadowControlWidth;//窗口阴影控制宽度，有阴影时为0，无阴影时为7

        MainWindow pWindow;
        public StrategyWindow(MainWindow _pWindow)
        {
            InitializeComponent();

            pWindow = _pWindow;
            originalHeight = this.Height;
            originalWidth = this.Width;

            Top1_StrategyLabw = Top1_StrategyLab.Width;
            groupListViewh = groupListView.Height;
            groupListVieww = groupListView.Width;

            


            windowShadowControlWidth = 0;

            groupCanvasStoryboard = (Storyboard)this.FindResource("groupCanvasAnimate");
            groupCanvasStoryboard_Leave = (Storyboard)this.FindResource("groupCanvasAnimate_Leave");
        }
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isWindowMax == false)
            {
                this.DragMove();
            }
            else if (isWindowMax == true) ;

        }//窗口移动


        private void Top1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxButton_Click_1(null, null);
        } //双击标题栏最大化、还原
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            if (this.ActualHeight > SystemParameters.WorkArea.Height || this.ActualWidth > SystemParameters.WorkArea.Width)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                MaxButton_Click_1(null, null);
            }

            ResizeControl();


        } //拉伸窗口调用ResizeControl()
        private bool ResizeControl()
        {
            Border1.Width = this.Width - 14.0;
            Border1.Height = this.Height - 14.0;

            Top1_StrategyLabwper = (this.Width - (originalWidth - Top1_StrategyLabw)) / (originalWidth - (originalWidth - Top1_StrategyLabw));
            
             
            
            //groupListViewwper = (this.Width - (originalWidth - groupListVieww)) / (originalWidth - (originalWidth - groupListVieww));
            
            //groupListViewhper = (this.Height - (originalHeight - groupListViewh)) / (originalHeight - (originalHeight - groupListViewh));
           

            //groupListView.Width = groupListViewwper * groupListVieww + 2 * windowShadowControlWidth;
            //groupListView.Height = groupListViewhper * groupListViewh + 2 * windowShadowControlWidth;


            Top1_StrategyLab.Width = Top1_StrategyLabwper * Top1_StrategyLabw + 2 * windowShadowControlWidth;

            return true;
        }
        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        bool isWindowMax = false;
        private void MaxButton_Click_1(object sender, RoutedEventArgs e)
        {

            if (isWindowMax == false)
            {
                Rect rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
                windowShadowControlWidth = 7;
                this.BorderThickness = new Thickness(0);
                maxButton.Style = Resources["normalSty"] as Style;
                this.Opacity = 1;
                Border1.Visibility = Visibility.Hidden;


                Rect rc = SystemParameters.WorkArea;//获取工作区大小
                this.Left = 0;//设置位置
                this.Top = 0;


                this.Width = rc.Width;
                this.Height = rc.Height;
                isWindowMax = true;

            }
            else if (isWindowMax == true)
            {
                windowShadowControlWidth = 0;
                this.BorderThickness = new Thickness(7);
                maxButton.Style = Resources["maxSty"] as Style;
                this.Left = rcnormal.Left;
                this.Top = rcnormal.Top;
                this.Width = rcnormal.Width;
                this.Height = rcnormal.Height;
                this.Left = 50;
                this.Top = 50;
                this.Opacity = 0.95;

                Border1.Visibility = Visibility.Visible;
                isWindowMax = false;

            }

        } //最大化窗口按钮
        private bool closeStoryBoardCompleted = false;
        private DoubleAnimation closeAnimation1;
        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            pWindow.WindowState = WindowState.Normal;
            pWindow.strategyAndProfitTabItem.Visibility = Visibility.Hidden;
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
           // groupCanvasStoryboard.Begin(this);
        }

        private void groupCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
           // groupCanvasStoryboard_Leave.Begin(this);
        }

        private void openUpPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void upBtn_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
