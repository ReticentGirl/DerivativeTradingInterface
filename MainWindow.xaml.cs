using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;


namespace qiquanui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {

         DataManager dm;
         private double originalHeight, originalWidth, list1h, list1w, grid3w, grid3h, list1hper, list1wper, grid3hper, grid3wper, top1w, top1wper, canvas1h, canvas1hper, multipleTabControlw, multipleTabControlwper, tradingListVieww, tradingListViewwper, optionsHoldDetailListVieww, optionsHoldDetailListViewwper, historyListVieww, historyListViewwper, userManageListVieww, userManageListViewwper, statusBar1w, statusBar1wper, canvas2h, canvas2hper, Grid1w, Grid1h, Grid1wper, Grid1hper, optionsMarketListVieww, optionsMarketListViewwper, optionsMarketListViewh, optionsMarketListViewhper, TopCanvas1h, TopCanvas1w, TopCanvas1wper, TopCanvas1hper, TopCanvasButtomGridw, TopCanvasButtomGridwper, optionsMarketTitleGridw, optionsMarketTitleGridwper, titileBorder4w, titileBorder4wper, profitListVieww, profitListViewwper, darkRectangleh, darkRectanglehper, darkRectanglew, darkRectanglewper, subjectMatterMarketGridw, subjectMatterMarketGridwper;


        TradingManager otm;   //维护交易区的指针

        HistoryManager hm;  //维护历史记录区的指针

        BasicInforAndPromptManager bip;   //维护基本信息和提示的指针

        UserManager um;

        PositionsManager pm;   //维护持仓区

         private double windowShadowControlWidth;//窗口阴影控制宽度，有阴影时为0，无阴影时为7





        private Storyboard grid1Storyboard, grid1Storyboard_Leave, canvas1Storyboard, canvas1Storyboard_Leave, grid3Storyboard, grid3Storyboard_Leave, canvas2Storyboard_Leave, canvas2Storyboard, optionsMarketTitleGridStoryboard, optionsMarketTitleGridStoryboard_Leave, TopCanvasButtomGridStoryboard_Leave, TopCanvasButtomGridStoryboard, strategyOfOptionsCanvasStoryboard_Leave, strategyOfOptionsCanvasStoryboard, strategyOfFuturesCanvasStoryboard_Leave, strategyOfFuturesCanvasStoryboard, basicInforAndPromptGridStoryboard_Leave, basicInforAndPromptGridStoryboard;


        void dataStart()
        {
            dm = new DataManager(this);
        }

        //void tradingThreadStart()
        //{
        //    otm = new TradingManager(this);
        //}


        Thread dataThread;

        // Thread tradingThread;
        void ctpStart()
        {
            new MktData().Run();
        }
        private Rect rc = SystemParameters.WorkArea;
        public MainWindow()
        {

            //new Thread(new ThreadStart(dataStart)).Start();
            dataStart();

            //datathread = new thread(new threadstart(dmupdate));
            //datathread.start();

            //tradingthread = new thread(new threadstart(tradingthreadstart));    //交易区线程启动
            //tradingthread.start();
            //new  Thread(new ThreadStart(ctpStart)).Start();
            //窗口最大化时显示任务栏
            


            InitializeComponent();

            otm = new TradingManager(this);

       

            bip = new BasicInforAndPromptManager(this, dm, otm);

            um = new UserManager(this);

            pm = new PositionsManager(this, um, otm);

            hm = new HistoryManager(this, pm,um);

            um.GetPositonManagerPoint(pm);

            um.initOpeningBalance();   //一定要放在um pm 初始化之后

            pm.initPositionAveragePrice();

            //otm.OnAdd();

            //设置窗口距离显示屏边界距离
            this.Left = 50;
            this.Top = 20;
            originalHeight = this.Height;
            originalWidth = this.Width;

            //获取控件初始长宽
            list1h = futuresMarketListView.Height;
            list1w = futuresMarketListView.Width;
            grid3h = Grid3.Height;
            grid3w = Grid3.Width;
            top1w = Top1.Width;
            canvas1h = Canvas1.Height;
            canvas2h = Canvas2.Height;
            tradingListVieww = tradingListView.Width;
            multipleTabControlw = multipleTabControl.Width;
            optionsHoldDetailListVieww = holdDetailListView.Width;
            historyListVieww = historyListView.Width;
            userManageListVieww = userManageListView.Width;
            statusBar1w = statusBar1.Width;
            optionsMarketListVieww = optionsMarketListView.Width;
            optionsMarketListViewh = optionsMarketListView.Height;
            TopCanvas1w = TopCanvas1.Width;
            TopCanvas1h = TopCanvas1.Height;
            TopCanvasButtomGridw = TopCanvasButtomGrid.Width;
            optionsMarketTitleGridw = optionsMarketTitleGrid.Width;
            titileBorder4w = titileBorder4.Width;
            Grid1w = Grid1.Width;
            Grid1h = Grid1.Height;
            profitListVieww = profitListView.Width;
            subjectMatterMarketGridw = subjectMatterMarketGrid.Width;

            darkRectanglew = darkRectangle.Width;
            darkRectangleh = darkRectangle.Height;

            windowShadowControlWidth = 0;

            //初始化伸缩面板动画
            grid1Storyboard = (Storyboard)this.FindResource("grid1Animate");
            grid1Storyboard_Leave = (Storyboard)this.FindResource("grid1Animate_Leave");
            canvas1Storyboard = (Storyboard)this.FindResource("canvas1Animate");
            canvas1Storyboard_Leave = (Storyboard)this.FindResource("canvas1Animate_Leave");

            grid3Storyboard = (Storyboard)this.FindResource("Grid3Animate");
            grid3Storyboard_Leave = (Storyboard)this.FindResource("Grid3Animate_Leave");

            canvas2Storyboard = (Storyboard)this.FindResource("canvas2Animate");
            canvas2Storyboard_Leave = (Storyboard)this.FindResource("canvas2Animate_Leave");

            optionsMarketTitleGridStoryboard = (Storyboard)this.FindResource("optionsMarketTitleGridAnimate");
            optionsMarketTitleGridStoryboard_Leave = (Storyboard)this.FindResource("optionsMarketTitleGridAnimate_Leave");

            TopCanvasButtomGridStoryboard = (Storyboard)this.FindResource("TopCanvasButtomGridAnimate");
            TopCanvasButtomGridStoryboard_Leave = (Storyboard)this.FindResource("TopCanvasButtomGridAnimate_Leave");

            strategyOfOptionsCanvasStoryboard = (Storyboard)this.FindResource("strategyOfOptionsCanvasAnimate");
            strategyOfOptionsCanvasStoryboard_Leave = (Storyboard)this.FindResource("strategyOfOptionsCanvasAnimate_Leave");

            strategyOfFuturesCanvasStoryboard = (Storyboard)this.FindResource("strategyOfFuturesCanvasAnimate");
            strategyOfFuturesCanvasStoryboard_Leave = (Storyboard)this.FindResource("strategyOfFuturesCanvasAnimate_Leave");

            basicInforAndPromptGridStoryboard = (Storyboard)this.FindResource("basicInforAndPromptGridAnimate");
            basicInforAndPromptGridStoryboard_Leave = (Storyboard)this.FindResource("basicInforAndPromptGridAnimate_Leave");

            typeComboBox.SelectedIndex = 0;
            typeInStrategyPanelComboBox.SelectedIndex = 1;
            
        }

        //protected override void OnExit(ExitEventArgs e)
        //{
        //    Console.WriteLine("OnExit");
        //    base.OnExit(e);
        //} 

        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isWindowMax == false)
            {
                this.DragMove();
            }
            else if (isWindowMax == true);

        }//窗口移动


        private void Top1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxButton_Click_1(null,null);
        } //双击标题栏最大化、还原
        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        } //最小化窗口按钮
        
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

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
          
            this.Close();
            
        }//关闭窗口按钮

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
        } //窗口关闭效果

        private bool closeStoryBoardCompleted = false;
        private DoubleAnimation closeAnimation1;
        private void closeWindow_Completed(object sender, EventArgs e)
        {
            closeStoryBoardCompleted = true;
            this.Close();
        }

        private bool ResizeControl()
        {
            

            optionsMarketTitleGridwper = TopCanvasButtomGridwper = TopCanvas1wper = Grid1wper = list1wper = optionsMarketListViewwper = (this.Width - (originalWidth - list1w)) / (originalWidth - (originalWidth - list1w));
            profitListViewwper = userManageListViewwper = historyListViewwper = optionsHoldDetailListViewwper = tradingListViewwper = multipleTabControlwper = grid3wper = top1wper = (this.Width - (originalWidth - grid3w)) / (originalWidth - (originalWidth - grid3w));
            list1hper = (this.Height - (originalHeight - list1h)) / (originalHeight - (originalHeight - list1h));
            optionsMarketListViewhper = (this.Height - (originalHeight - optionsMarketListViewh)) / (originalHeight - (originalHeight - optionsMarketListViewh));
            TopCanvas1hper = Grid1hper = grid3hper = (this.Height - (originalHeight - Grid1h)) / (originalHeight - (originalHeight - Grid1h));
            titileBorder4wper = (this.Width - (originalWidth - titileBorder4w)) / (originalWidth - (originalWidth - titileBorder4w));
            subjectMatterMarketGridwper = (this.Width - (originalWidth - subjectMatterMarketGridw)) / (originalWidth - (originalWidth - subjectMatterMarketGridw));


            darkRectanglewper = (this.Width - (originalWidth - darkRectanglew)) / (originalWidth - (originalWidth - darkRectanglew));
            darkRectanglehper = (this.Height - (originalHeight - darkRectangleh)) / (originalHeight - (originalHeight - darkRectangleh));

            statusBar1wper = this.Width / originalWidth;
            canvas2hper = canvas1hper = (this.Height - (originalHeight - canvas1h)) / (originalHeight - (originalHeight - canvas1h));


            Border1.Height = this.Height - 14.0 + 2 * windowShadowControlWidth;
            Border1.Width = this.Width - 14.0 + 2 * windowShadowControlWidth;
            Grid1.Width = futuresMarketListView.Width = list1w * list1wper + 2 * windowShadowControlWidth;
            futuresMarketListView.Height = list1h * list1hper + 2 * windowShadowControlWidth;
            Grid1.Height = Grid1h * Grid1hper + 2 * windowShadowControlWidth;
            historyListView.Width = historyListVieww * historyListViewwper + 2 * windowShadowControlWidth;
            userManageListView.Width = userManageListVieww * userManageListViewwper + 2 * windowShadowControlWidth;


            Grid3.Width = grid3w * grid3wper + 2 * windowShadowControlWidth;
            Top1.Width = top1w * top1wper + 2 * windowShadowControlWidth;
            Canvas1.Height = canvas1h * canvas1hper + 2 * windowShadowControlWidth;
            Canvas2.Height = canvas2h * canvas2hper + 2 * windowShadowControlWidth;
            tradingListView.Width = tradingListVieww * tradingListViewwper + 2 * windowShadowControlWidth;
            multipleTabControl.Width = multipleTabControlw * multipleTabControlwper + 2 * windowShadowControlWidth;
            holdDetailListView.Width = optionsHoldDetailListVieww * optionsHoldDetailListViewwper + 2 * windowShadowControlWidth;
            profitListView.Width = profitListViewwper * profitListVieww + 2 * windowShadowControlWidth;
            statusBar1.Width = statusBar1w * statusBar1wper + 2 * windowShadowControlWidth;
            optionsMarketListView.Width = optionsMarketListVieww * optionsMarketListViewwper + 2 * windowShadowControlWidth;
            optionsMarketListView.Height = optionsMarketListViewh * optionsMarketListViewhper + 2 * windowShadowControlWidth;
            TopCanvas1.Width = TopCanvas1w * TopCanvas1wper + 2 * windowShadowControlWidth;
            TopCanvas1.Height = TopCanvas1h * TopCanvas1hper + 2 * windowShadowControlWidth;
            TopCanvasButtomGrid.Width = TopCanvasButtomGridw * TopCanvasButtomGridwper + 2 * windowShadowControlWidth;
            optionsMarketTitleGrid.Width = optionsMarketTitleGridwper * optionsMarketTitleGridw + 2 * windowShadowControlWidth;
            titileBorder4.Width = titileBorder4wper * titileBorder4w + 2 * windowShadowControlWidth;
            subjectMatterMarketGrid.Width = subjectMatterMarketGridw * subjectMatterMarketGridwper + 2 * windowShadowControlWidth;

            darkRectangle.Width = darkRectanglew * darkRectanglewper + 2 * windowShadowControlWidth;
            darkRectangle.Height = darkRectangleh * darkRectanglehper + 2 * windowShadowControlWidth;

            Canvas1Border1.Height = Canvas1.Height + 2 * windowShadowControlWidth;

            Canvas2Border1.Height = Canvas2.Height + 2 * windowShadowControlWidth;

            return true;
        } //拉伸窗口时改变各个控件大小
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            if (this.ActualHeight > SystemParameters.WorkArea.Height || this.ActualWidth > SystemParameters.WorkArea.Width)
            {
                this.WindowState = System.Windows.WindowState.Normal;
                MaxButton_Click_1(null, null);
            }

                ResizeControl();
            
            
        } //拉伸窗口调用ResizeControl()


        private void Grid1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //grid1Storyboard.Begin(this);
        } //鼠标进入行情区，区域增亮

        private void Grid1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //grid1Storyboard_Leave.Begin(this);
        } //鼠标离开行情区，区域亮度还原

        private void Grid3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //grid3Storyboard.Begin(this);
        } //鼠标进入操作区，区域增亮

        private void Grid3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //grid3Storyboard_Leave.Begin(this);
        } //鼠标离开操作区，区域亮度还原
        private void optionsMarketTitleGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //optionsMarketTitleGridStoryboard.Begin(this);

        } //鼠标进入期权对应标的物行情区，区域增亮

        private void optionsMarketTitleGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  optionsMarketTitleGridStoryboard_Leave.Begin(this);
        } //鼠标离开期权对应标的物行情区，区域亮度还原

        private void basicInforAndPromptGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  basicInforAndPromptGridStoryboard.Begin(this);
        }

        private void basicInforAndPromptGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  basicInforAndPromptGridStoryboard_Leave.Begin(this);
        }

        private void futuresComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

            optionsMarketListView.Visibility = Visibility.Hidden;
            optionsMarketTitleGrid.Visibility = Visibility.Hidden;
            futuresMarketListView.Visibility = Visibility.Visible;
            subjectMatterMarketGrid.Visibility = Visibility.Hidden;
            titileBorder4.Visibility = Visibility.Hidden;
            dueDateLabel.Visibility = Visibility.Hidden;
            dueDateComboBox.Visibility = Visibility.Hidden;

        } //行情区，“衍生品种类”选择“期货”，隐藏“标的期货”、cvx指数、期权行情，显示期货行情

        private void optionsComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

            futuresMarketListView.Visibility = Visibility.Hidden;
            optionsMarketListView.Visibility = Visibility.Visible;
            optionsMarketTitleGrid.Visibility = Visibility.Visible;
            subjectMatterMarketGrid.Visibility = Visibility.Visible;
            titileBorder4.Visibility = Visibility.Visible;
            dueDateLabel.Visibility = Visibility.Visible;
            dueDateComboBox.Visibility = Visibility.Visible;

        } //行情区，“衍生品种类”选择“期权”，显示“标的商品”、cvx指数、期权行情，隐藏期货行情
        private void TopCanvasButtomGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // TopCanvasButtomGridStoryboard.Begin(this);
        }

        private void TopCanvasButtomGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //TopCanvasButtomGridStoryboard_Leave.Begin(this);

        }
        private void strategyOfOptionsCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  strategyOfOptionsCanvasStoryboard.Begin(this);
        }

        private void strategyOfOptionsCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //strategyOfOptionsCanvasStoryboard_Leave.Begin(this);
        }

        private void strategyOfFuturesCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // strategyOfFuturesCanvasStoryboard.Begin(this);
        }

        private void strategyOfFuturesCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //strategyOfFuturesCanvasStoryboard_Leave.Begin(this);
        }


        //WPF的定时器使用DispatcherTimer类对象，用来为darkrectangle的逐渐消失计时
        private System.Windows.Threading.DispatcherTimer dTimer = new DispatcherTimer();
        //WPF的定时器使用DispatcherTimer类对象，用来为leftCanvas的逐渐展开计时
        private System.Windows.Threading.DispatcherTimer lcTimer_show = new DispatcherTimer();
        //WPF的定时器使用DispatcherTimer类对象，用来为leftCanvas的逐渐收缩计时
        private System.Windows.Threading.DispatcherTimer lcTimer = new DispatcherTimer();
        //WPF的定时器使用DispatcherTimer类对象，用来为rightCanvas的逐渐展开计时
        private System.Windows.Threading.DispatcherTimer rcTimer_show = new DispatcherTimer();
        //WPF的定时器使用DispatcherTimer类对象，用来为rightCanvas的逐渐收缩计时
        private System.Windows.Threading.DispatcherTimer rcTimer = new DispatcherTimer();

        private bool isLeftCanvasExpanding = false;
        private bool isRightCanvasExpanding = false;
        private bool isLeftCanvasHiding = false;
        private bool isRightCanvasHiding = false;
        private void dTimer_Tick(object sender, EventArgs e)//darkrectangle消失定时器
        {
            darkRectangle.Visibility = Visibility.Hidden;
            dTimer.Stop();
        }
        private void lcTimer_show_Tick(object sender, EventArgs e)//左版面扩展定时器
        {
            isLeftCanvasExpanding = false;
            lcTimer_show.Stop();
        }
        private void lcTimer_Tick(object sender, EventArgs e)//左版面收缩定时器
        {
            isLeftCanvasHiding = false;
            lcTimer.Stop();
        }
        private void rcTimer_show_Tick(object sender, EventArgs e)//右版面扩展定时器
        {
            isRightCanvasExpanding = false;
            rcTimer_show.Stop();
        }
        private void rcTimer_Tick(object sender, EventArgs e)//右版面收缩定时器
        {
            isRightCanvasHiding = false;
            rcTimer.Stop();
        }

        public void darkRectangleHidden()//黑幕隐藏
        {

            if (((Canvas1.Width == 29.0 || Canvas2.Width == 29.0) && ((isLeftCanvasHiding == true || isRightCanvasHiding == true) && !(isLeftCanvasHiding == true && isRightCanvasHiding == true))) || ((isLeftCanvasHiding == false && isRightCanvasHiding == false) && (((Canvas2.Width > 29.0) && (Canvas2.Width <= 60.0)) || ((Canvas1.Width > 29.0) && (Canvas1.Width <= 292.0)))) || ((isLeftCanvasHiding == true && isRightCanvasHiding == true) && (Canvas1.Width == 292.0 && Canvas2.Width == 60.0)))
            {
                Storyboard s = new Storyboard();
                this.Resources.Add(Guid.NewGuid().ToString(), s);
                DoubleAnimation da = new DoubleAnimation();
                Storyboard.SetTarget(da, darkRectangle);
                Storyboard.SetTargetProperty(da, new PropertyPath("Opacity", new object[] { }));
                da.From = 1;
                da.To = 0;
                s.Duration = new Duration(TimeSpan.FromSeconds(1));
                s.Children.Add(da);
                s.Begin();

                dTimer.Tick += new EventHandler(dTimer_Tick);
                dTimer.Interval = new TimeSpan(0, 0, 1);
                dTimer.Start();
            }
        }
        public void darkRectangleShow()//黑幕显现
        {
            if ((Canvas1.Width == 29.0 && Canvas2.Width == 29.0 && ((isLeftCanvasExpanding == true || isRightCanvasExpanding == true) && !(isLeftCanvasExpanding == true && isRightCanvasExpanding == true))) || ((isLeftCanvasHiding == false && isRightCanvasHiding == false) && (isLeftCanvasExpanding == true && isRightCanvasExpanding == true) && (((Canvas2.Width >= 29.0) && (Canvas2.Width < 60.0)) || ((Canvas1.Width >= 29.0) && (Canvas1.Width < 292.0)))))
            {
                darkRectangle.Visibility = Visibility.Visible;

                Storyboard s = new Storyboard();
                this.Resources.Add(Guid.NewGuid().ToString(), s);
                DoubleAnimation da = new DoubleAnimation();
                Storyboard.SetTarget(da, darkRectangle);
                Storyboard.SetTargetProperty(da, new PropertyPath("Opacity", new object[] { }));
                da.From = 0;
                da.To = 1;
                s.Duration = new Duration(TimeSpan.FromSeconds(1));
                s.Children.Add(da);
                s.Begin();
            }
        }

        public void darkRectangle_Click(object sender, RoutedEventArgs e)//点击黑幕后两侧伸缩面板缩回，黑幕消失
        {
            darkRectangleHidden();

            CloseLeftCanvas();
            CloseRightCanvas();
        }
        //左伸缩板的展开和收缩
        public void openLeftCanvas()
        {
            if (Canvas1.Width == 29.0 || (isLeftCanvasHiding == true && Canvas1.Width <= 292.0 && Canvas1.Width >= 29.0))
            {
                lcTimer_show.Tick += new EventHandler(lcTimer_show_Tick);
                lcTimer_show.Interval = TimeSpan.FromSeconds(0.4);
                lcTimer_show.Start();
                isLeftCanvasExpanding = true;
                DoubleAnimation animate = new DoubleAnimation();
                animate.To = 292.0;
                animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate.DecelerationRatio = 1;
                //   animate.AccelerationRatio = 0.33;
                Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
                LeftImage.Source = new BitmapImage(new Uri("Resources/left.png", UriKind.Relative));
                darkRectangleShow();
                Canvas1Border1.Visibility = Visibility.Visible;
                canvas1Storyboard.Begin(this);
            }

        }
        public void CloseLeftCanvas()
        {
            if (Canvas1.Width == 292.0 || (isLeftCanvasExpanding == true && Canvas1.Width <= 292.0 && Canvas1.Width >= 29.0))
            {
                lcTimer.Tick += new EventHandler(lcTimer_Tick);
                lcTimer.Interval = TimeSpan.FromSeconds(0.4);
                lcTimer.Start();
                isLeftCanvasHiding = true;
                DoubleAnimation animate = new DoubleAnimation();
                animate.To = 29.0;
                animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                //   animate.DecelerationRatio = 0.33;
                animate.AccelerationRatio = 1;
                Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
                LeftImage.Source = new BitmapImage(new Uri("Resources/right.png", UriKind.Relative));
                darkRectangleHidden();
                Canvas1Border1.Visibility = Visibility.Hidden;
                canvas1Storyboard_Leave.Begin(this);
            }
        }

        private void StategyCanvasButtom_Click(object sender, RoutedEventArgs e)
        {
            if (Canvas1.Width == 29.0)
            {
                openLeftCanvas();
            }
            else if (Canvas1.Width == 292.0)
            {
                CloseLeftCanvas();
            }
        }


        private void RiskCanvasButtom_Click(object sender, RoutedEventArgs e)
        {
            if (Canvas2.Width == 29.0)
            {
                openRightCanvas();

                RiskWindow riskWindow = new RiskWindow(this);
                riskWindow.Show();
                this.WindowState = WindowState.Minimized;

            }
            else if (Canvas2.Width == 60.0)
            {
                CloseRightCanvas();
            }

        }
        //右伸缩板的展开和收缩
        public void openRightCanvas()
        {
            if (Canvas2.Width == 29.0 || (isRightCanvasHiding == true && Canvas2.Width <= 60.0 && Canvas2.Width >= 29.0))
            {
                rcTimer_show.Tick += new EventHandler(rcTimer_show_Tick);
                rcTimer_show.Interval = TimeSpan.FromSeconds(0.4);
                rcTimer_show.Start();
                isRightCanvasExpanding = true;
                DoubleAnimation animate1 = new DoubleAnimation();
                animate1.To = 60.0;
                animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate1.DecelerationRatio = 1;
                //   animate.AccelerationRatio = 0.33;
                Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
                canvas2Storyboard.Begin(this);
                RightImage.Source = new BitmapImage(new Uri("Resources/right.png", UriKind.Relative));
                
                darkRectangleShow();


                Canvas2Border1.Visibility = Visibility.Visible;
                canvas2Storyboard.Begin(this);
            }
        }
        public void CloseRightCanvas()
        {
            if (Canvas2.Width == 60.0 || (isRightCanvasExpanding == true && Canvas2.Width <= 60.0 && Canvas2.Width >= 29.0))
            {
                rcTimer.Tick += new EventHandler(rcTimer_Tick);
                rcTimer.Interval = TimeSpan.FromSeconds(0.4);
                rcTimer.Start();
                isRightCanvasHiding = true;
                DoubleAnimation animate1 = new DoubleAnimation();
                animate1.To = 29.0;
                animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                //   animate.DecelerationRatio = 0.33;
                animate1.AccelerationRatio = 1;
                Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
                RightImage.Source = new BitmapImage(new Uri("Resources/left.png", UriKind.Relative));
                darkRectangleHidden();
                Canvas2Border1.Visibility = Visibility.Hidden;
                canvas2Storyboard_Leave.Begin(this);
            }
        }



        private void predictComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            maxAmountOfUpAndDownLabel.Visibility = Visibility.Visible;
            maxAmountOfUpAndDownTextBox.Visibility = Visibility.Visible;
            startStrategyBtn.IsEnabled = true;
            volatilityLabel.Visibility = Visibility.Hidden;
            volatilityComboBox.Visibility = Visibility.Hidden;
        } //策略实验室，“情景选择”选择“上涨”，显示“最高涨跌幅”，隐藏“波动率”

        private void predictComboBoxItem2_Selected(object sender, RoutedEventArgs e)
        {
            maxAmountOfUpAndDownLabel.Visibility = Visibility.Visible;
            maxAmountOfUpAndDownTextBox.Visibility = Visibility.Visible;
            startStrategyBtn.IsEnabled = true;
            volatilityLabel.Visibility = Visibility.Hidden;
            volatilityComboBox.Visibility = Visibility.Hidden;
        } //策略实验室，“情景选择”选择“下跌”，显示“最高涨跌幅”，隐藏“波动率”

        private void predictComboBoxItem3_Selected(object sender, RoutedEventArgs e)
        {
            maxAmountOfUpAndDownLabel.Visibility = Visibility.Hidden;
            maxAmountOfUpAndDownTextBox.Visibility = Visibility.Hidden;
            volatilityLabel.Visibility = Visibility.Visible;
            volatilityComboBox.Visibility = Visibility.Visible;
            startStrategyBtn.IsEnabled = true;
        } //策略实验室，“情景选择”选择“波动”，隐藏“最高涨跌幅”，显示“波动率”

        private void predictComboBoxItem4_Selected(object sender, RoutedEventArgs e)
        {
            maxAmountOfUpAndDownLabel.Visibility = Visibility.Hidden;
            maxAmountOfUpAndDownTextBox.Visibility = Visibility.Hidden;
            volatilityLabel.Visibility = Visibility.Hidden;
            volatilityComboBox.Visibility = Visibility.Hidden;
            strategyAndProfitTabItem.Visibility = Visibility.Visible;
            CloseLeftCanvas();
            strategyAndProfitTabItem.IsSelected = true;
            startStrategyBtn.IsEnabled = false;
        } //策略实验室，“情景选择”选择“与标的物组合”，隐藏“最高涨跌幅”、“波动率”

        private void optionsMarketListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {

        }

        private void futuresInStrategyPanelComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            strategyOfOptionsCanvas.Visibility = Visibility.Hidden;
            strategyOfFuturesCanvas.Visibility = Visibility.Visible;
        } //策略实验室，“衍生品种类”选择期货，展开相关控件

        private void optionsInStrategyPanelComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            strategyOfOptionsCanvas.Visibility = Visibility.Visible;
            strategyOfFuturesCanvas.Visibility = Visibility.Hidden;
        } //策略实验室，“衍生品种类”选择期权，展开相关控件

        private void startStrategyBtn_Click(object sender, RoutedEventArgs e)
        {
            StrategyWindow strategyWindow = new StrategyWindow(this);
            strategyWindow.Show();
            this.WindowState = WindowState.Minimized;
            CloseLeftCanvas();
            CloseRightCanvas();
            

        } //策略实验室，点击“开始分析”

        //private void marketPriceOTCBI_Selected(object sender, RoutedEventArgs e)
        //{
        //    comboBoxItem1.Visibility = Visibility.Hidden;
        //} //期权交易区，“委托方式”选择“市价”，“托单方式”中“ROD”不可选





        private void placeOrderTBtn_Click(object sender, RoutedEventArgs e)         //交易区下单按钮
        {



            PlaceOrder placeOrder = new PlaceOrder();
            placeOrder.Show();

            PlaceOrderManager pom = new PlaceOrderManager(placeOrder);

            placeOrder.getPoint(hm, pom, dm, otm, this);

            if (tradingListView.Items.Count > 0)
            {
                for (int i = 0; i < tradingListView.Items.Count; i++)
                {

                    TradingData otd = (TradingData)tradingListView.Items[i];
                    if (otd.IfChooseOTGVCH == true)
                    {

                        string orderUserID = otd.UserID;   //投资账户

                        string orderInstrumentID = otd.InstrumentID;   //合约代码



                        string orderTradingType = "";  //交易类型   0 买  1 卖

                        switch (otd.TradingType)
                        {
                            case 0:
                                orderTradingType = "买";
                                break;
                            case 1:
                                orderTradingType = "卖";
                                break;
                           
                        }


                        int orderTradingNum = otd.TradingNum;  //交易手数

                        string orderClientageType = "";   //委托方式    0 市价  限价 1 

                        switch (otd.ClientageType)
                        {
                            case 0:
                                orderClientageType = "市价";
                                break;
                            case 1:
                                orderClientageType = "限价";
                                break;

                        }

                        double orderClientagePrice = 0;
                        if (otd.ClientageType == 1)   //限价
                        {
                            orderClientagePrice = Convert.ToDouble(otd.ClientagePrice); //委托价格
                        }


                        double orderMarketPrice = otd.MarketPrice;   //市场价格

                        string orderClientageCondition = "";   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消

                        switch (otd.ClientageCondition)
                        {
                            case 0:
                                orderClientageCondition = "ROD";
                                break;
                            case 1:
                                orderClientageCondition = "FOK";
                                break;
                            case 2:
                                orderClientageCondition = "IOC";
                                break;
                        }

                        double orderFinalPrice = 0;

                        if (otd.ClientageType == 0)   //市价
                            orderFinalPrice = orderMarketPrice;
                        else if (otd.ClientageType == 1)   //限价
                            orderFinalPrice = orderClientagePrice;


                        bool optionOrFuture = otd.OptionOrFuture;



                        pom.OnAdd(orderUserID, orderInstrumentID, orderTradingType, orderTradingNum, orderClientageType, orderClientagePrice, orderMarketPrice, orderClientageCondition, orderFinalPrice, optionOrFuture, otd.IsBuy, false);

                        if (otd.IsSetGradient == true)   //如果设置了等差
                        {
                            //int count = 0;  //计算等差次数
                            for (int count = 1; count <= 2; count++)
                            {

                                string g_clientageType = "限价";

                                if (otd.IsBuy == true)    //买卖  NUM正负不同
                                {
                                    int g_tradingNum = orderTradingNum - count * otd.ArithmeticProgression;

                                    if (g_tradingNum > 0)
                                    {

                                        double gp_clientagePrice = orderFinalPrice + count * otd.Accuracy;    //正的
                                        double gp_finalPrice = gp_clientagePrice;

                                        pom.OnAdd(orderUserID, orderInstrumentID, orderTradingType, g_tradingNum, g_clientageType, gp_clientagePrice, orderMarketPrice, orderClientageCondition, gp_finalPrice, optionOrFuture, otd.IsBuy, true);



                                        double gn_clientagePrice = orderFinalPrice - count * otd.Accuracy;    //负的
                                        double gn_finalPrice = gn_clientagePrice;

                                        pom.OnAdd(orderUserID, orderInstrumentID, orderTradingType, g_tradingNum, g_clientageType, gn_clientagePrice, orderMarketPrice, orderClientageCondition, gn_finalPrice, optionOrFuture, otd.IsBuy, true);

                                    }
                                    else
                                    {
                                        break;
                                    }

                                }
                                else if (otd.IsBuy == false)
                                {
                                    int g_tradingNum = orderTradingNum + count * otd.ArithmeticProgression;


                                    if (g_tradingNum < 0)
                                    {

                                        double gp_clientagePrice = orderFinalPrice + count * otd.Accuracy;    //正的
                                        double gp_finalPrice = gp_clientagePrice;

                                        pom.OnAdd(orderUserID, orderInstrumentID, orderTradingType, g_tradingNum, g_clientageType, gp_clientagePrice, orderMarketPrice, orderClientageCondition, gp_finalPrice, optionOrFuture, otd.IsBuy, true);



                                        double gn_clientagePrice = orderFinalPrice - count * otd.Accuracy;    //负的
                                        double gn_finalPrice = gn_clientagePrice;

                                        pom.OnAdd(orderUserID, orderInstrumentID, orderTradingType, g_tradingNum, g_clientageType, gn_clientagePrice, orderMarketPrice, orderClientageCondition, gn_finalPrice, optionOrFuture, otd.IsBuy, true);

                                    }
                                    else
                                    {
                                        break;
                                    }

                                }







                            }
                        }

                    }

                }
            }
        }


        private void deleteTBtn_Click(object sender, RoutedEventArgs e)      //交易区删除按钮
        {
            if (tradingListView.Items.Count > 0)
            {
                for (int i = tradingListView.Items.Count - 1; i >= 0; i--)    //倒回来写才删的干净
                {

                    TradingData otd = (TradingData)tradingListView.Items[i];
                    if (otd.IfChooseOTGVCH == true)
                    {
                        otm.TradingOC.RemoveAt(i);

                    }

                }
            }
        }







        
        private void tradingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)    //交易区窗口选择列改变事件
        {
            //System.Windows.MessageBox.Show("change");
            TradingData selectedItem = tradingListView.SelectedItem as TradingData;

            if (selectedItem != null)
            {
                bip.getSelectedItemPoint(selectedItem);
            }
            

            /*
            if (selectedItem != null)
            {


                string se_nowUserID = selectedItem.UserID;

                DataRow uDr = (DataRow)UserManager.userHash[se_nowUserID];



                string se_totalCapital = uDr["UserEquity"].ToString();                    //用户总资本
                string se_availableCapital = uDr["AvailableCapital"].ToString();           //可用资产
                string se_occupyCapital = uDr["UsedMargin"].ToString();                        //占用资产
                string se_nowInstrumentID = selectedItem.InstrumentID;
                string se_dayToEnd = "";                                //到期天数
                string se_dueDate = "";                                       //到期日
                string se_tickSize = "";                           //最小变动价位
                string se_maxTradeNum = "";                         //最大可交易手数


                DataRow nDr = (DataRow)DataManager.All[se_nowInstrumentID];


                se_dueDate = nDr["LastDate"].ToString();

                DateTime dt_dueDate = DateTime.ParseExact(se_dueDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                TimeSpan sp_dueDate = dt_dueDate - dm.now;

                double i_dueDate = Math.Round(sp_dueDate.TotalDays,1);

               // System.Windows.MessageBox.Show(i_dueDate.ToString());

                se_dayToEnd = i_dueDate.ToString();

                se_tickSize = nDr["MinUnit"].ToString();

                se_maxTradeNum = SomeCalculate.calculateCanBuyNum(selectedItem, otm.TradingOC).ToString();

                bip.changeInfo(se_nowUserID, se_totalCapital, se_availableCapital, se_occupyCapital, se_nowInstrumentID, se_dayToEnd, se_dueDate, se_tickSize, se_maxTradeNum);

            }

           */
             

        }
        
        

        ////TopComboBox
        #region 顶端各选单逻辑
        /// <summary>
        /// 顶端各选单逻辑 CREATED BY IVES DING
        /// </summary>



        void DmUpdate()
        {
            dm.Update();
        }

        public static string[] NameSubject = { "金", "铜", "白糖", "豆粕", "上证50", "沪深300" };
        public static string[] NameOption = { "金期权", "铜期权", "白糖期权", "豆粕期权", "上证50期权", "沪深300期权" };
        public static string[] NameFuture = { "金", "铜", "白糖", "豆粕", "上证50期货", "沪深300期货" };
        /// <summary>
        /// 标的期货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subjectMatterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //如果只是Clear导致的就忽略它
            if (e.AddedItems.Count == 0) return;

            string type = typeComboBox.Text.Trim();
            if (type.Equals("期货"))
            {
                dataThread = new Thread(new ThreadStart(DmUpdate));
                dataThread.Start();

                return;
            }

            string _subject = (string)e.AddedItems[0];
            subjectMatterComboBox.Text = _subject;
            string _option = "";
            for (int i = 0; i < NameSubject.Length; i++)
                if (NameSubject[i].Equals(_subject))
                    _option = NameOption[i];
            string duedatesql = "select duedate from staticdata where instrumentname='" + _option + "' group by duedate";
            DataTable dt = DataControl.QueryTable(duedatesql);
            dueDateComboBox.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _duedate = (string)dt.Rows[i][0];
                if (_duedate.Equals("1407")) continue;
                dueDateComboBox.Items.Add(_duedate);
            }
            dueDateComboBox.SelectedIndex = 0;

        }

        /// <summary>
        /// 交易商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void traderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count == 0 || e.AddedItems.Count == 0) return;
            string trader;
            trader = (string)e.AddedItems[0];
            trader = trader.Trim();
            type_change(typeComboBox.Text.Trim(), trader);
        }

        private void type_change(string _type, string _trader)
        {
            traderComboBox.Text = _trader;
            if (!_type.Equals("期权"))
            {
                //string duedatesql = "SELECT duedate FROM staticdata s  where exchangename='" + _trader + "' and optionorfuture=1 group by duedate ";
                //DataTable dt = DataControl.QueryTable(duedatesql);
                //dueDateComboBox.Items.Clear();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string _duedate = (string)dt.Rows[i][0];
                //    if (_duedate.Length == 4 && _duedate[0].Equals('1') && !_duedate.Equals("1407"))
                //        dueDateComboBox.Items.Add(_duedate);
                //}
                //dueDateComboBox.SelectedIndex = 0;

                string subjectlist;
                if (_trader.Equals("全部"))
                    subjectlist = "SELECT instrumentname FROM staticdata s where  instrumenttype='期货' and exchangename<>'大商所' and  duedate<>'1407' and duedate<>'no' group by instrumentname";
                else
                    subjectlist = "SELECT instrumentname FROM staticdata s where  instrumenttype='期货' and exchangename='" + _trader + "' and  duedate<>'1407' and duedate<>'no' group by instrumentname";

                DataTable dt = DataControl.QueryTable(subjectlist);
                subjectMatterComboBox.Items.Clear();
                subjectMatterComboBox.Items.Add("全部");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _subject = (string)dt.Rows[i][0];
                    subjectMatterComboBox.Items.Add(_subject);
                }
                subjectMatterComboBox.SelectedIndex = 0;

                return;
            }

            string trader = _trader;
            subjectMatterComboBox.Items.Clear();
            if (trader.Equals("上交所"))
            {
                dueDateComboBox.Items.Clear();
                return;
            }
            if (trader.Equals("上期所"))
            {
                subjectMatterComboBox.Items.Add("金");
                subjectMatterComboBox.Items.Add("铜");
            }
            if (trader.Equals("大商所"))
            {
                subjectMatterComboBox.Items.Add("豆粕");
            }
            if (trader.Equals("中金所"))
            {
                subjectMatterComboBox.Items.Add("上证50");
                subjectMatterComboBox.Items.Add("沪深300");
            }
            if (trader.Equals("郑商所"))
            {
                subjectMatterComboBox.Items.Add("白糖");
            }
            subjectMatterComboBox.SelectedItem = subjectMatterComboBox.Items[0];

        }

        private void dueDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            string type = typeComboBox.Text.Trim();
            string trader = traderComboBox.Text.Trim();
            string duedata = (string)e.AddedItems[0];
            string subject = subjectMatterComboBox.Text.Trim();

            if ((type.Equals("期权") && (trader.Equals("") || duedata.Equals("") || subject.Equals(""))) ||
                (type.Equals("期货") && (trader.Equals(""))))
                return;

            dataThread = new Thread(new ThreadStart(DmUpdate));
            dataThread.Start();
        }


        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string type = ((string)((ComboBoxItem)e.AddedItems[0]).Content).Trim();
            typeComboBox.Text = type;
            if (type.Equals("期货"))
            {
                traderComboBox.Items.Clear();
                traderComboBox.Items.Add("全部");
                traderComboBox.Items.Add("中金所");
                traderComboBox.Items.Add("上期所");
                traderComboBox.Items.Add("郑商所");
                traderComboBox.SelectedIndex = 0;
                type_change(type, "全部");

                this.addSubjectMatter.Visibility = Visibility.Hidden;  //加入标的商品 按钮隐藏
            }
            else
            {
                traderComboBox.Items.Clear();
                traderComboBox.Items.Add("中金所");
                traderComboBox.Items.Add("上期所");
                traderComboBox.Items.Add("郑商所");
                traderComboBox.Items.Add("上交所");
                traderComboBox.SelectedIndex = 0;
                type_change(type, "中金所");

                this.addSubjectMatter.Visibility = Visibility.Visible;   //加入标的商品 按钮显示
            }
        }



        #endregion




        private void typeOfTradingTComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)    //对买 卖 ComboBox 写动态 0 买  1 卖
        {

            for (int i = 0; i < tradingListView.Items.Count; i++)
            {
                TradingData otd = (TradingData)tradingListView.Items[i];

                bool becomeSame = false;   //改变了之后有相同的

                int old_tradingNum = otd.TradingNum;

                switch (otd.TradingType)
                {
                    case 0:
                        otd.IsBuy = true;
                        
                        otd.TradingNum = Math.Abs(old_tradingNum);
                        break;
                    case 1:
                        otd.IsBuy = false;
                        otd.TradingNum = -Math.Abs(old_tradingNum);
                        break;
                 
                }

                otd.TypeChangeCount += 1;

                if (otd.TypeChangeCount > 1)   //如果已经不是第一次改变，那就要判断是否有重复
                {
                    if (otd.OptionOrFuture == false)  //如果是期权
                    {
                        if (otm.TradingOC.Count() > 0)    //判断是否有相同的
                            for (int j = 0; j < otm.TradingOC.Count(); j++)
                            {
                                TradingData td = otm.TradingOC[j];
                                if (td.UserID == otd.UserID && td.InstrumentID == otd.InstrumentID && Convert.ToDouble(td.ExercisePrice) == Convert.ToDouble(otd.ExercisePrice) && td.IsBuy == otd.IsBuy &&
                                    td.CallOrPut.Equals(otd.CallOrPut) && td.OptionOrFuture == otd.OptionOrFuture && i != j)
                                    becomeSame = true;
                            }
                    }
                    else if (otd.OptionOrFuture == true)  //如果是期货
                    {
                        if (otm.TradingOC.Count() > 0)    //判断是否有相同的
                            for (int j = 0; j < otm.TradingOC.Count(); j++)
                            {
                                TradingData td = otm.TradingOC[j];

                                if (td.InstrumentID.Equals(otd.InstrumentID) && td.IsBuy == otd.IsBuy && td.OptionOrFuture == otd.OptionOrFuture && i != j)
                                {
                                    becomeSame = true;
                                }

                            }
                    }
                }



                if (becomeSame == true)
                {
                    otm.TradingOC.RemoveAt(i);
                }



            }

        }

        private void bidROMBtn_Click(object sender, RoutedEventArgs e)        //期权看涨 “买价”按钮  实际上是卖期权
        {


            int exercisePrice = Convert.ToInt32((sender as System.Windows.Controls.Button).Tag);  //获取按钮Tag

            string userID = this.userComboBox.Text;
            BuyOrSellForButtonForOption(exercisePrice,userID, "看涨", false, false);

        }

        private void askROMBtn_Click(object sender, RoutedEventArgs e)      //期权看涨 “卖价” 按钮  买期权
        {
            //System.Windows.MessageBox.Show("看涨  卖");
            int exercisePrice = Convert.ToInt32(((sender as System.Windows.Controls.Button).Tag));  //获取按钮Tag
            //System.Windows.MessageBox.Show(((sender as System.Windows.Controls.Button).Tag).GetType().ToString());
            string userID = this.userComboBox.Text;
            BuyOrSellForButtonForOption(exercisePrice, userID, "看涨", true, false);

        }

        private void bidDOMBtn_Click(object sender, RoutedEventArgs e)    //期权看跌 “买价”按钮 实际上是卖期权
        {
            //System.Windows.MessageBox.Show("看跌  买");
            int exercisePrice = Convert.ToInt32((sender as System.Windows.Controls.Button).Tag);  //获取按钮Tag
            string userID = this.userComboBox.Text;
            BuyOrSellForButtonForOption(exercisePrice, userID, "看跌", false, false);
        }

        private void askDOMBtn_Click(object sender, RoutedEventArgs e)     //期权看跌 “卖"  按钮   实际上是买期权
        {
            //System.Windows.MessageBox.Show("看跌  卖");
            int exercisePrice = Convert.ToInt32((sender as System.Windows.Controls.Button).Tag);  //获取按钮Tag
            string userID = this.userComboBox.Text;
            BuyOrSellForButtonForOption(exercisePrice, userID, "看跌", true, false);
        }




        private void askFMBtn_Click(object sender, RoutedEventArgs e)      //期货“卖价”按钮   买期货
        {
            string selectedInstrumentName = ((sender as System.Windows.Controls.Button).Tag).ToString();    //获取“卖”按钮Tag
            //System.Windows.MessageBox.Show(selectedInstrumentName);

            //future selectedFuture = ObservableOb[selectedInstrumentName];
            string userID = this.userComboBox.Text;
            BuyOrSellForButtonForFuture(selectedInstrumentName, userID, true, true);

        }

        private void bidFMBtn_Click(object sender, RoutedEventArgs e)    //期货“买”按钮  卖期货
        {
            string selectedInstrumentName = ((sender as System.Windows.Controls.Button).Tag).ToString();
            string userID = this.userComboBox.Text;
            BuyOrSellForButtonForFuture(selectedInstrumentName, userID, false, true);
        }




        public void BuyOrSellForButtonForOption(int _buttonTag, string _SelectedUserID, string _selectedCallOrPut, bool _isBuy, bool _optionOrFuture)
        {
            // this.tradingListView.Visibility = Visibility.Visible;

            this.tradingTabItem.IsSelected = true;

            bool haveSame = false;    //用以判断交易区中是否有相同的

            bool isValid = true;    //用来处理卖价 ，卖价 出现  - 的情况



            if (_optionOrFuture == false)   //如果是期权
            {
                int exercisePrice = _buttonTag;  //获取按钮Tag

                int buttonIndex = (int)dm.ep_no[(int)exercisePrice];    //根据哈希表获得行数

                option selectedOption = dm.ObservableObj[buttonIndex];


                //////////////////////////先判断点击的按钮有没有效 ///////////////////////////////////////////////////////////
                double selectedMarketPrice = 0;

                if (_isBuy == true && _selectedCallOrPut.Equals("看涨"))
                {
                    if (selectedOption.BidPrice1.Equals("-"))
                        isValid = false;
                    else
                        selectedMarketPrice = Convert.ToDouble(selectedOption.AskPrice1);
                }
                else if (_isBuy == false && _selectedCallOrPut.Equals("看涨"))
                {
                    if (selectedOption.AskPrice1.Equals("-"))
                        isValid = false;
                    else
                        selectedMarketPrice = Convert.ToDouble(selectedOption.BidPrice1);

                }
                else if (_isBuy == true && _selectedCallOrPut.Equals("看跌"))
                {
                    if (selectedOption.BidPrice2.Equals("-"))
                        isValid = false;
                    else
                        selectedMarketPrice = Convert.ToDouble(selectedOption.AskPrice2);
                }
                else if (_isBuy == false && _selectedCallOrPut.Equals("看跌"))
                {
                    if (selectedOption.AskPrice2.Equals("-"))
                        isValid = false;
                    else
                        selectedMarketPrice = Convert.ToDouble(selectedOption.BidPrice2);
                }


                //////////////////////////////////////////////////////////////////

                if (isValid == true)
                {
                    string selectedUserID = _SelectedUserID;

                    string selectedInstrumentID = "";

                    if (_selectedCallOrPut.Equals("看涨"))
                    {
                        selectedInstrumentID = selectedOption.instrumentid1;   //看涨
                    }
                    else if (_selectedCallOrPut.Equals("看跌"))
                    {
                        selectedInstrumentID = selectedOption.instrumentid2;   //看跌
                    }

                    string selectedCallOrPut = _selectedCallOrPut;

                    double selectedExercisePrice = Convert.ToDouble(selectedOption.ExercisePrice);

                    bool selectedIsBuy = _isBuy;

                    if (otm.TradingOC.Count() > 0)    //判断是否有相同的
                        for (int i = 0; i < otm.TradingOC.Count(); i++)
                        {
                            TradingData td = otm.TradingOC[i];
                            if (td.UserID == selectedUserID && td.InstrumentID == selectedInstrumentID && Convert.ToDouble(td.ExercisePrice) == selectedExercisePrice && td.IsBuy == selectedIsBuy &&
                                td.CallOrPut.Equals(selectedCallOrPut) && td.OptionOrFuture == _optionOrFuture)
                                haveSame = true;
                        }

                    if (haveSame == false)    //没有相同的  入列表中
                    {
                        //otm.TurnOver();
                        otm.OnAdd(selectedUserID, selectedInstrumentID, selectedCallOrPut, Convert.ToString(selectedExercisePrice), selectedMarketPrice, selectedIsBuy, _optionOrFuture);
                        //otm.TurnOver();
                    }
                }

            } ////////////////////////////////////////////////////////////////////////以上是期权处理

        }


        public void BuyOrSellForButtonForFuture(string _buttonTag, string _SelectedUserID, bool _isBuy, bool _optionOrFuture)
        {
            //this.tradingListView.Visibility = Visibility.Visible;

            this.tradingTabItem.IsSelected = true;

            bool haveSame = false;

            bool isValid = true;

            if (_optionOrFuture == true)   //如果是期货
            {

                int selectedIndex = (int)dm.id_no[_buttonTag];

                future selectedFuture = dm.ObservableOb[selectedIndex];

                double selectedMarketPrice = 0;

                if (_isBuy == true)
                {
                    if (selectedFuture.BidPrice1.Equals("-"))
                        isValid = false;
                    else
                        selectedMarketPrice = Convert.ToDouble(selectedFuture.AskPrice1);
                }
                else if (_isBuy == false)
                {
                    if (selectedFuture.AskPrice1.Equals("-"))
                        isValid = false;
                    else
                        selectedMarketPrice = Convert.ToDouble(selectedFuture.BidPrice1);
                }


                if (isValid == true)     //如果有效的话
                {
                    string selectedUserID = _SelectedUserID;

                    string selectedInstrumentID = selectedFuture.instrumentid;


                    string selectedCallOrPut = "-";     //因为是期货，所以没有看涨看跌

                    string selectedExercisePrice = "-";    //因为是期货，所以没有行权价

                    bool selectedIsBuy = _isBuy;

                    if (otm.TradingOC.Count() > 0)    //判断是否有相同的
                        for (int i = 0; i < otm.TradingOC.Count(); i++)
                        {
                            TradingData td = otm.TradingOC[i];

                            if (td.InstrumentID.Equals(selectedInstrumentID) && td.IsBuy == _isBuy && td.OptionOrFuture == _optionOrFuture)
                            {
                                haveSame = true;
                            }

                        }

                    if (haveSame == false)   //如果没有相同的
                    {
                        otm.OnAdd(selectedUserID, selectedInstrumentID, selectedCallOrPut, selectedExercisePrice, selectedMarketPrice, selectedIsBuy, _optionOrFuture);
                    }

                }
            }
        }

        private void cancelOrderBtn_Click(object sender, RoutedEventArgs e)   //历史记录区 “删除”按钮
        {

            //hm.OrderByTime();

            for (int i = hm.HistoryOC.Count() - 1; i >= 0; i--)
            {
                HistoryData selectedHd = hm.HistoryOC[i];
                if (selectedHd.HIfChoose == true && selectedHd.TradingState.Equals(HistoryManager.POST))
                {
                    hm.HistoryOC.RemoveAt(i);
                }
            }
        }

        private void formOfClientageTComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)   //交易区 市价 限价 ComboBOX 变换调用的函数
        {
            //System.Windows.MessageBox.Show("123");
            for (int i = 0; i < otm.TradingOC.Count(); i++)
            {
                TradingData cTd = otm.TradingOC[i];
                if (cTd.OptionOrFuture == true)  //如果是期货
                {
                    cTd.AboutFOK = Visibility.Collapsed;
                    cTd.AboutIOC = Visibility.Collapsed;
                    cTd.AboutROD = Visibility.Visible;
                    if (cTd.ClientageType == 0) //市价
                    {
                        cTd.ClientagePrice = "-";
                        cTd.IsEnableOfClientagePrice = false;
                    }
                    else if (cTd.ClientageType == 1)    //限价
                    {
                        cTd.ClientagePrice = cTd.MarketPrice.ToString();
                        cTd.IsEnableOfClientagePrice = true;
                    }
                }
                else     //如果是期权
                {
                    if (cTd.ClientageType == 0) //市价
                    {
                        cTd.AboutFOK = Visibility.Visible;
                        cTd.AboutIOC = Visibility.Visible;
                        cTd.AboutROD = Visibility.Collapsed;

                        cTd.ClientagePrice = "-";
                        cTd.IsEnableOfClientagePrice = false;
                    }
                    else if (cTd.ClientageType == 1)    //限价
                    {
                        cTd.AboutFOK = Visibility.Visible;
                        cTd.AboutIOC = Visibility.Visible;
                        cTd.AboutROD = Visibility.Visible;

                        cTd.ClientagePrice = cTd.MarketPrice.ToString();
                        cTd.IsEnableOfClientagePrice = true;
                    }

                }
            }
        }

        private void addSubjectMatter_Click(object sender, RoutedEventArgs e)            //加入标的的商品  按钮
        {
            if (this.typeComboBox.SelectedIndex == 1)
            {
                /////////////////////默认为买入
                bool haveSame = false;

                future add_Future = dm.ObservableOb[0];

                double add_MarketPrice = Convert.ToDouble(add_Future.AskPrice1);

                string add_userID = this.userComboBox.Text;            //先这样用着

                string add_instrumentID = add_Future.instrumentid;

                string add_callOrPut = "-";

                string add_exercisePrice = "-";

                bool add_isBuy = true;

                if (otm.TradingOC.Count() > 0)    //判断是否有相同的
                    for (int i = 0; i < otm.TradingOC.Count(); i++)
                    {
                        TradingData td = otm.TradingOC[i];

                        if (td.InstrumentID.Equals(add_instrumentID) && td.IsBuy == add_isBuy && td.OptionOrFuture == true)
                        {
                            haveSame = true;
                        }

                    }

                if (haveSame == false)   //如果没有相同的
                {
                    otm.OnAdd(add_userID, add_instrumentID, add_callOrPut, add_exercisePrice, add_MarketPrice, add_isBuy, true);
                }


            }
        }

        private void accuracyTTextBox_TextChanged(object sender, TextChangedEventArgs e)     //交易区 精确度 数值变化响应
        {
            for (int i = 0; i < otm.TradingOC.Count(); i++)
            {
                TradingData c_td = otm.TradingOC[i];

                DataRow nDr = (DataRow)DataManager.All[c_td.InstrumentID];

                double c_accuracyUnit = (double)nDr["MinUnit"];

                //double iii = 1.2 % 0.2;

                if ((int)(c_td.Accuracy * 100) % (int)(c_accuracyUnit * 100) != 0)   //乘100 是为了解决小数取余有问题的问题
                {
                    int time = (int)(c_td.Accuracy / c_accuracyUnit); //倍数

                    c_td.Accuracy = c_accuracyUnit * time;
                }
            }

        }

    
        private void chooseAllCheckBox_Click(object sender, RoutedEventArgs e)     //交易区 全选 勾选
        {
            System.Windows.Controls.CheckBox all_selected = sender as System.Windows.Controls.CheckBox;

            if (all_selected.IsChecked == true)
            {
                for (int i = 0; i < otm.TradingOC.Count(); i++)
                {
                    otm.TradingOC[i].IfChooseOTGVCH = true;
               }
            }
            else if (all_selected.IsChecked == false)
            {
                for (int i = 0; i < otm.TradingOC.Count(); i++)
                {
                    otm.TradingOC[i].IfChooseOTGVCH = false;
                }
            }
            
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            startStrategyBtn_Click(sender,e);
        }


        private void sighInBtn_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow sighInWindow = new SignInWindow(um);
            sighInWindow.Show();


            //string deleteBuySqlAndSell = String.Format("DELETE FROM Positions WHERE UserID='{0}'AND InstrumentID='{1}' AND IsBuy =0", "001107","HO1408-C-1300");

            //DataControl.InsertOrUpdate(deleteBuySqlAndSell);
        }//打开注册窗口

        private void Window_Closed_1(object sender, EventArgs e)   //窗体关闭前调用的函数
        {
            um.CalculateStaticFloatingProfitAndLossAndFinalBalance();
        }

        private void closeOutBtn_Click(object sender, RoutedEventArgs e)    //平仓按钮 
        {
            pm.HandleCloseOut();
        }

       














    }
}
