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
        private double originalHeight, originalWidth, Top1_StrategyLabw, Top1_StrategyLabwper, groupListVieww, groupListViewwper, groupListViewh, groupListViewhper, topLeftGridw, topLeftGridwper, topLeftGridh, topLeftGridhper, TopRightAndTopCenterGridw, TopRightAndTopCenterGridwper, TopRightAndTopCenterGridh, TopRightAndTopCenterGridhper, TopCenterTabControlw, TopCenterTabControlwper, TopCenterTabControlh, TopCenterTabControlhper, TopRightCanvasw, TopRightCanvaswper, TopRightCanvash, TopRightCanvashper, probabilityChartw, probabilityChartwper, probabilityCharth, probabilityCharthper, ButtomRightGridw, ButtomRightGridwper, ButtomRightGridh, ButtomRightGridhper, groupListViewTabControlw, groupListViewTabControlwper, groupListViewTabControlh, groupListViewTabControlhper, specificConditionGridw, specificConditionGridwper, specificConditionGridh, specificConditionGridhper, specificStrategyGridw, specificStrategyGridwper, specificStrategyGridh, specificStrategyGridhper, strategyListVieww, strategyListViewwper, strategyListViewh, strategyListViewhper, ComparasionChartGridh, ComparasionChartGridhper, ComparasionChartGridw, ComparasionChartGridwper, TendencyChartGridh, TendencyChartGridhper, TendencyChartGridw, TendencyChartGridwper, topLeftGridtlw, topLeftGridtlwper, topLeftGridtlh, topLeftGridtlhper, topLeftGridtrw, topLeftGridtrwper, topLeftGridtrh, topLeftGridtrhper, topLeftGridblw, topLeftGridblwper, topLeftGridblh, topLeftGridblhper, topLeftGridbrw, topLeftGridbrwper, topLeftGridbrh, topLeftGridbrhper;
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

            topLeftGridw = topLeftGrid.Width;
            topLeftGridh = topLeftGrid.Height;
            TopRightAndTopCenterGridw = TopRightAndTopCenterGrid.Width;
            TopRightAndTopCenterGridh = TopRightAndTopCenterGrid.Height;
            TopCenterTabControlw=TopCenterTabControl.Width;
            TopCenterTabControlh=TopCenterTabControl.Height;
            TopRightCanvasw=TopRightCanvas.Width;
            TopRightCanvash=TopRightCanvas.Height;
            probabilityChartw=probabilityChart.Width;
            probabilityCharth=probabilityChart.Height;
            ButtomRightGridw = ButtomRightGrid.Width;
            ButtomRightGridh = ButtomRightGrid.Height;
            groupListViewTabControlw = groupListViewTabControl.Width;
            groupListViewTabControlh = groupListViewTabControl.Height;
            specificConditionGridw = specificConditionGrid.Width;
            specificConditionGridh = specificConditionGrid.Height;
            specificStrategyGridw = specificStrategyGrid.Width;
            specificStrategyGridh = specificStrategyGrid.Height;
            strategyListVieww = strategyListView.Width;
            strategyListViewh = strategyListView.Height;
            ComparasionChartGridh = ComparasionChartGrid.Height;
            ComparasionChartGridw = ComparasionChartGrid.Width;
            TendencyChartGridw = TendencyChartGrid.Width;
            TendencyChartGridh = TendencyChartGrid.Height;


            topLeftGridbrw = topLeftGridbr.Width;
            topLeftGridbrh = topLeftGridbr.Height;
            topLeftGridtlw = topLeftGridtl.Width;
            topLeftGridtlh = topLeftGridtl.Height;
            topLeftGridtrw = topLeftGridtr.Width;
            topLeftGridtrh = topLeftGridtr.Height;
            topLeftGridblw = topLeftGridbl.Width;
            topLeftGridblh = topLeftGridbl.Height;


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
            Border1.Width = this.Width - 14.0 + 2 * windowShadowControlWidth;
            Border1.Height = this.Height - 14.0 + 2 * windowShadowControlWidth;

            Top1_StrategyLabwper = (this.Width - (originalWidth - Top1_StrategyLabw)) / (originalWidth - (originalWidth - Top1_StrategyLabw));
            specificStrategyGridwper = specificConditionGridwper = (this.Width - (originalWidth - specificConditionGridw - ButtomRightGridw)) / (originalWidth - (originalWidth - specificConditionGridw - ButtomRightGridw));
            specificStrategyGridhper = specificConditionGridhper = (this.Height - (originalHeight - specificStrategyGridh - topLeftGridtlh - topLeftGridblh)) / (originalHeight - (originalHeight - specificStrategyGridh - topLeftGridtlh - topLeftGridblh));
            groupListViewTabControlwper = (this.Width - (originalWidth - groupListViewTabControlw - ButtomRightGridw)) / (originalWidth - (originalWidth - groupListViewTabControlw - ButtomRightGridw));
            groupListViewTabControlhper = (this.Height - (originalHeight - groupListViewTabControlh - topLeftGridtlh - topLeftGridblh)) / (originalHeight - (originalHeight - groupListViewTabControlh - topLeftGridtlh - topLeftGridblh));
            strategyListViewwper=groupListViewwper = (this.Width - (originalWidth - groupListVieww - ButtomRightGridw)) / (originalWidth - (originalWidth - groupListVieww - ButtomRightGridw));
            strategyListViewhper = groupListViewhper = (this.Height - (originalHeight - groupListViewh - topLeftGridtlh - topLeftGridblh)) / (originalHeight - (originalHeight - groupListViewh - topLeftGridtlh - topLeftGridblh));
            topLeftGridwper = (this.Width - (originalWidth - topLeftGridw - TopRightAndTopCenterGridw)) / (originalWidth - (originalWidth - topLeftGridw - TopRightAndTopCenterGridw));
            topLeftGridhper = (this.Height - (originalHeight - topLeftGridh - groupListViewh)) / (originalHeight - (originalHeight - topLeftGridh - groupListViewh));
            TopRightAndTopCenterGridwper = (this.Width - (originalWidth - topLeftGridw - TopRightAndTopCenterGridw)) / (originalWidth - (originalWidth - topLeftGridw - TopRightAndTopCenterGridw));
            TopRightAndTopCenterGridhper = (this.Height - (originalHeight - TopRightAndTopCenterGridh - groupListViewh)) / (originalHeight - (originalHeight - TopRightAndTopCenterGridh - groupListViewh));
            TendencyChartGridwper=ComparasionChartGridwper = TopCenterTabControlwper = (this.Width - (originalWidth - TopCenterTabControlw - TopRightCanvasw - topLeftGridw)) / (originalWidth - (originalWidth - TopCenterTabControlw - TopRightCanvasw - topLeftGridw));
            TopCenterTabControlhper = (this.Height - (originalHeight - ComparasionChartGridh - groupListViewh)) / (originalHeight - (originalHeight - ComparasionChartGridh - groupListViewh));
            probabilityChartwper = TopRightCanvaswper = (this.Width - (originalWidth - TopCenterTabControlw - TopRightCanvasw - topLeftGridw)) / (originalWidth - (originalWidth - TopCenterTabControlw - TopRightCanvasw - topLeftGridw));
            probabilityCharthper = TopRightCanvashper = (this.Height - (originalHeight - probabilityCharth - ButtomRightGridh)) / (originalHeight - (originalHeight - probabilityCharth - ButtomRightGridh));
            ButtomRightGridwper = (this.Width - (originalWidth - groupListVieww - ButtomRightGridw)) / (originalWidth - (originalWidth - groupListVieww - ButtomRightGridw));
            ButtomRightGridhper = (this.Height - (originalHeight - ButtomRightGridh - probabilityCharth)) / (originalHeight - (originalHeight - ButtomRightGridh - probabilityCharth));
            TendencyChartGridhper=ComparasionChartGridhper = (this.Height - (originalHeight - groupListViewh - ComparasionChartGridh)) / (originalHeight - (originalHeight - groupListViewh - ComparasionChartGridh));
            
            topLeftGridblwper=topLeftGridbrwper=topLeftGridtrwper= topLeftGridtlwper = (this.Width - (originalWidth - topLeftGridtlw - topLeftGridtrw - TopCenterTabControlw - TopRightCanvasw)) / (originalWidth - (originalWidth - topLeftGridtlw - topLeftGridtrw - TopCenterTabControlw - TopRightCanvasw));
            topLeftGridblhper = topLeftGridbrhper = topLeftGridtrhper = topLeftGridtlhper = (this.Height - (originalHeight - strategyListViewh - topLeftGridblh - topLeftGridtlh)) / (originalHeight - (originalHeight - strategyListViewh - topLeftGridblh - topLeftGridtlh));


            groupListView.Width = groupListViewwper * groupListVieww + 2 * windowShadowControlWidth;
            groupListView.Height = groupListViewhper * groupListViewh+windowShadowControlWidth;
            strategyListView.Width = strategyListVieww * strategyListViewwper + 2 * windowShadowControlWidth;
            strategyListView.Height = strategyListViewh * strategyListViewhper + windowShadowControlWidth;
            specificStrategyGrid.Width = specificStrategyGridw * specificStrategyGridwper + 2 * windowShadowControlWidth;
            specificStrategyGrid.Height = specificStrategyGridh * specificStrategyGridhper + windowShadowControlWidth;
            specificConditionGrid.Width = specificConditionGridw * specificConditionGridwper + 2 * windowShadowControlWidth;
            specificConditionGrid.Height = specificConditionGridh * specificConditionGridhper + windowShadowControlWidth;
            groupListViewTabControl.Width = groupListViewTabControlw * groupListViewTabControlwper + 2 * windowShadowControlWidth;
            groupListViewTabControl.Height = groupListViewTabControlh * groupListViewTabControlhper + windowShadowControlWidth;
            topLeftGrid.Width = topLeftGridw * topLeftGridwper;
            topLeftGrid.Height = topLeftGridh * topLeftGridhper + windowShadowControlWidth;
            TopRightAndTopCenterGrid.Width = TopRightAndTopCenterGridw * TopRightAndTopCenterGridwper + 2 * windowShadowControlWidth;
            TopRightAndTopCenterGrid.Height = TopRightAndTopCenterGridh * TopRightAndTopCenterGridhper + windowShadowControlWidth;
            TopCenterTabControl.Width = TopCenterTabControlw * TopCenterTabControlwper +  windowShadowControlWidth;
            TopCenterTabControl.Height = TopCenterTabControlh * TopCenterTabControlhper + windowShadowControlWidth;
            probabilityChart.Width = probabilityChartw * probabilityChartwper +  windowShadowControlWidth;
            probabilityChart.Height = probabilityCharth * probabilityCharthper + windowShadowControlWidth;
            TopRightCanvas.Width = TopRightCanvasw * TopRightCanvaswper + windowShadowControlWidth;
            TopRightCanvas.Height = TopRightCanvash * TopRightCanvashper + windowShadowControlWidth;
            ButtomRightGrid.Width = ButtomRightGridw * ButtomRightGridwper ;
            ButtomRightGrid.Height = ButtomRightGridh * ButtomRightGridhper + windowShadowControlWidth;
            TendencyChartGrid.Height = TendencyChartGridh * TendencyChartGridhper + windowShadowControlWidth;
            TendencyChartGrid.Width = TendencyChartGridw * TendencyChartGridwper + windowShadowControlWidth;
            ComparasionChartGrid.Height = ComparasionChartGridhper * ComparasionChartGridh +windowShadowControlWidth;
            ComparasionChartGrid.Width = ComparasionChartGridw * ComparasionChartGridwper+windowShadowControlWidth;


            topLeftGridbl.Width = topLeftGridblw * topLeftGridblwper;
            topLeftGridbl.Height = topLeftGridblh * topLeftGridblhper + 1 / 2 * windowShadowControlWidth;
            topLeftGridtl.Width = topLeftGridtlw * topLeftGridtlwper;
            topLeftGridtl.Height = topLeftGridtlh * topLeftGridtlhper + 1 / 2 * windowShadowControlWidth;
            topLeftGridtr.Width = topLeftGridtrw * topLeftGridtrwper;
            topLeftGridtr.Height = topLeftGridtrh * topLeftGridtrhper + 1 / 2 * windowShadowControlWidth;
            topLeftGridbr.Width = topLeftGridbrw * topLeftGridbrwper;
            topLeftGridbr.Height = topLeftGridbrh * topLeftGridbrhper + 1 / 2 * windowShadowControlWidth;


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
    }
}
