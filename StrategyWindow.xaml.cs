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
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AmCharts.Windows.Core;
using AmCharts.Windows.Line;
using System.Threading;
using System.Timers;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using RiskControl;

namespace qiquanui
{

    #region class TopStrategy
    public class TopStrategy : INotifyPropertyChanged
    {
        private string name;
        private int number;
        private string vaR;
        private string groupName;

        private string expectEarn;
        public double expectEarnPer;
        private string price;
        public double pricePer;
        private string guarantee;
        public double guaranteePer;
        private string fee;
        public double feePer;
        private string maxEarn;
        public double maxEarnPer;
        private string maxLose;
        public double maxLosePer;
        private string earnRate;
        public double earnRatePer;
        private string loseRate;
        public double loseRatePer;

        private bool ifchoose;
        public bool IfChoose
        {
            get { return ifchoose; }
            set
            {

                ifchoose = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IfChoose"));
            }
        }



        public int LineNo { get; set; }
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("GroupName"));
            }
        }

        public string ExpectEarn
        {
            get { return expectEarn; }
            set
            {
                expectEarn = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ExpectEarn"));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Number"));
            }
        }
        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(new PropertyChangedEventArgs("Price")); }
        }
        public string Guarantee
        {
            get { return guarantee; }
            set { guarantee = value; OnPropertyChanged(new PropertyChangedEventArgs("Guarantee")); }
        }
        public string Fee
        {
            get { return fee; }
            set
            {
                fee = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Fee"));
            }
        }

        public string MaxEarn
        {
            get { return maxEarn; }
            set
            {
                maxEarn = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MaxEarn"));
            }
        }
        public string MaxLose
        {
            get { return maxLose; }
            set { maxLose = value; OnPropertyChanged(new PropertyChangedEventArgs("MaxLose")); }
        }
        public string LoseRate
        {
            get { return loseRate; }
            set
            {
                loseRate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoseRate"));
            }
        }
        public string EarnRate
        {
            get { return earnRate; }
            set
            {
                earnRate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EarnRate"));
            }
        }
        public string VaR
        {
            get { return vaR; }
            set
            {
                vaR = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VaR"));
            }
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        #endregion
    }
    #endregion


    /// <summary>
    /// StrategyWindow.xaml 的交互逻辑
    /// </summary>

    public partial class StrategyWindow : Window
    {
        private double originalHeight, originalWidth, Top1_StrategyLabw, Top1_StrategyLabwper, groupListVieww, groupListViewwper, groupListViewh, groupListViewhper;
        private Storyboard groupCanvasStoryboard, groupCanvasStoryboard_Leave;
        private double windowShadowControlWidth;//窗口阴影控制宽度，有阴影时为0，无阴影时为7

        ObservableCollection<TopStrategy> qjoc;
        ObservableCollection<TopStrategy> cloc;
        MainWindow pwindow;
        public StrategyWindow(MainWindow _pWindow)
        {
            InitializeComponent();

            pwindow = _pWindow;
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
            pwindow.WindowState = WindowState.Normal;
            pwindow.strategyAndProfitTabItem.Visibility = Visibility.Hidden;
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



        #region 上涨
        private bool isUpPolyOpen = false;
        private void closeUpPoly()
        {
            isUpPolyOpen = false;
            //遮盖面板浮现
            openUpPoly.Visibility = Visibility.Visible;
            DoubleAnimation showOpenUpPloy = new DoubleAnimation();
            showOpenUpPloy.From = 0;
            showOpenUpPloy.To = 100;
            showOpenUpPloy.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            openUpPoly.BeginAnimation(Polygon.OpacityProperty, showOpenUpPloy);

            //单买看涨Canvas移回
            ThicknessAnimation buyAndUpMove = new ThicknessAnimation();
            buyAndUpMove.From = new Thickness(408, 355, 0, 0);
            buyAndUpMove.To = new Thickness(510, 355, 0, 0);
            buyAndUpMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndUpCanvas.BeginAnimation(Canvas.MarginProperty, buyAndUpMove);

            //牛市差价Canvas移回
            ThicknessAnimation bullSpreadMove = new ThicknessAnimation();
            bullSpreadMove.From = new Thickness(448, 385, 0, 0);
            bullSpreadMove.To = new Thickness(550, 385, 0, 0);
            bullSpreadMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bullSpreadCanvas.BeginAnimation(Canvas.MarginProperty, bullSpreadMove);

            //Canvas、button缩回
            DoubleAnimation shorten = new DoubleAnimation();
            shorten.From = 102;
            shorten.To = 0;
            shorten.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndUpCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            bullSpreadCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            buyAndUpBtn.BeginAnimation(Button.WidthProperty, shorten);
            bullSpreadBtn.BeginAnimation(Button.WidthProperty, shorten);
        }
        private void openUpPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isUpPolyOpen = true;

            if (isDownPolyOpen == true)
            {
                closeDownPoly();
            }
            if (isHighVolatilityPolyOpen == true)
            {
                closeHighVolatilityPoly();
            }
            if (isLowVolatilityPolyOpen == true)
            {
                closeLowVolatilityPoly();
            }
            if (isRisklessArbiPolyOpen == true)
            {
                closeRisklessArbiPoly();
            }

            //鼠标点击，隐藏上涨遮盖面板
            DoubleAnimation hideOpenUpPloy = new DoubleAnimation();
            hideOpenUpPloy.From = openUpPoly.Opacity;
            hideOpenUpPloy.To = 0;
            hideOpenUpPloy.Duration = new Duration(TimeSpan.Parse("0:0:1"));//设置动画时间，时：分：秒
            openUpPoly.BeginAnimation(Polygon.OpacityProperty, hideOpenUpPloy);
            openUpPoly.Visibility = Visibility.Collapsed;

            //单买看涨Canvas移出
            ThicknessAnimation buyAndUpMove = new ThicknessAnimation();
            buyAndUpMove.From = new Thickness(510, 355, 0, 0);
            buyAndUpMove.To = new Thickness(408, 355, 0, 0);
            buyAndUpMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndUpCanvas.BeginAnimation(Canvas.MarginProperty, buyAndUpMove);

            //牛市差价Canvas移出
            ThicknessAnimation bullSpreadMove = new ThicknessAnimation();
            bullSpreadMove.From = new Thickness(550, 385, 0, 0);
            bullSpreadMove.To = new Thickness(448, 385, 0, 0);
            bullSpreadMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bullSpreadCanvas.BeginAnimation(Canvas.MarginProperty, bullSpreadMove);

            //Canvas、button伸长
            DoubleAnimation extend = new DoubleAnimation();
            extend.From = 0;
            extend.To = 102;
            extend.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndUpCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            bullSpreadCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            buyAndUpBtn.BeginAnimation(Button.WidthProperty, extend);
            bullSpreadBtn.BeginAnimation(Button.WidthProperty, extend);
        }//选择“上涨”面板

        private void upBtn_Click(object sender, RoutedEventArgs e)
        {
            closeUpPoly();


            qjocPresent = StrategyType.Up;
            nowPresent = qjocPresent;
            new Thread(new ThreadStart(UpRefresh)).Start();

        }//点击“上涨”按钮


        private void UpRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            int no = 2;
            int which = 0;


            YK[] res = ComputeUpSituation(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = res[i].ykname;

            }

            BindingForOC(which, no);
            BindingForCharts(which, no);


            ResultTab_Thread(which);


            Lock = false;
        }



        delegate void ResultTabCallBack(int which);
        private void ResultTab_Thread(int which)
        {

            ResultTabCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new ResultTabCallBack(ResultTab_Thread);
                pwindow.Dispatcher.Invoke(d, new object[] { which });
            }
            else
            {


                ResultTab.SelectedIndex = which;

                ListView lv=null;
                if (which == 0)
                     lv=groupListView;
                if (which == 1)
                    lv=strategyListView;
                if (lv.Items.Count > 0)
                    lv.SelectedIndex = 0;
                else {
                    VolatilityChart.Visibility = Visibility.Hidden;
                    VolatilityChart2.Visibility = Visibility.Hidden;
                    ZoomInGL.Visibility = Visibility.Hidden;
                    ZoomInYK.Visibility = Visibility.Hidden;
                    for (int i = 0; i < 10; i++)
                        labels[i].Visibility = Visibility.Hidden;
                }
            }
        }



        /// <summary>
        /// 计算上涨情景
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeUpSituation(int Tot)
        {
            YK[] ans = new YK[Tot];

            ans[0] = ComputeBuyAndUp(1)[0];
            ans[1] = ComputeBull(1)[0];

            return ans;
        }



        double MaxRange;
        /// <summary>
        /// 计算单支看涨
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBuyAndUp(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMaxRateOfGrothTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.CallBuy] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "单买看涨", "上涨", MaxRange);
                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);
            }

            return ans;
        }


        /// <summary>
        /// 计算牛市差价
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBull(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMaxRateOfGrothTBox);

            //看涨牛市
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.CallBuy] = 1;
                    _num[j, (int)OptionType.CallSell] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "看涨组成的牛市", "上涨", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            //看跌牛市
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.PutBuy] = 1;
                    _num[j, (int)OptionType.PutSell] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "看跌组成的牛市", "上涨", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            //2比1牛市差价
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.CallSell] = 1;
                    _num[j, (int)OptionType.CallBuy] = 2;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "2比1牛市差价", "上涨", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            return ans;
        }




        private void buyAndUpBtn_Click(object sender, RoutedEventArgs e)
        {
            closeUpPoly();

            UpSingleRefresh();
        }//点击“单买看涨”按钮



        private void UpSingleRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            YK[] res = ComputeBuyAndUp(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[1, i] = res[i];
                ykm.yk[1, i].title = "Top " + (i + 1);
            }

            BindingForOC(1, no);
            BindingForCharts(1, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.UpSingle;

            Lock = false;

        }



        private void bullSpreadBtn_Click(object sender, RoutedEventArgs e)
        {
            closeUpPoly();


            UpBullRefresh();


        }//点击“牛市差价”按钮


        private void UpBullRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeBull(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;


            clocPresent = StrategyType.UpBull;


            Lock = false;

        }


        #endregion



        #region 下跌
        private bool isDownPolyOpen = false;
        private void closeDownPoly()
        {
            isDownPolyOpen = false;
            //遮盖面板浮现
            openDownPoly.Visibility = Visibility.Visible;
            DoubleAnimation showOpenDownPoly = new DoubleAnimation();
            showOpenDownPoly.From = 0;
            showOpenDownPoly.To = 100;
            showOpenDownPoly.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            openDownPoly.BeginAnimation(Polygon.OpacityProperty, showOpenDownPoly);

            //Canvas、button缩回
            DoubleAnimation shorten = new DoubleAnimation();
            shorten.From = 102;
            shorten.To = 0;
            shorten.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndDownCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            bearSpreadCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            buyAndDownBtn.BeginAnimation(Button.WidthProperty, shorten);
            bearSpreadBtn.BeginAnimation(Button.WidthProperty, shorten);
        }
        private void openDownPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDownPolyOpen = true;

            if (isUpPolyOpen == true)
            {
                closeUpPoly();
            }
            if (isHighVolatilityPolyOpen == true)
            {
                closeHighVolatilityPoly();
            }
            if (isLowVolatilityPolyOpen == true)
            {
                closeLowVolatilityPoly();
            }
            if (isRisklessArbiPolyOpen == true)
            {
                closeRisklessArbiPoly();
            }

            //鼠标点击，隐藏遮盖面板
            DoubleAnimation hideOpenDownPloy = new DoubleAnimation();
            hideOpenDownPloy.From = openDownPoly.Opacity;
            hideOpenDownPloy.To = 0;
            hideOpenDownPloy.Duration = new Duration(TimeSpan.Parse("0:0:1"));
            openDownPoly.BeginAnimation(Polygon.OpacityProperty, hideOpenDownPloy);
            openDownPoly.Visibility = Visibility.Collapsed;

            //Canvas、button伸长
            DoubleAnimation extend = new DoubleAnimation();
            extend.From = 0;
            extend.To = 102;
            extend.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndDownCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            bearSpreadCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            buyAndDownBtn.BeginAnimation(Button.WidthProperty, extend);
            bearSpreadBtn.BeginAnimation(Button.WidthProperty, extend);
        }//选择“下跌”面板

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            closeDownPoly();

            DownRefresh();

        }//点击“下跌”按钮      

        private void DownRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in DownSituation");
                return;
            }
            else Lock = true;

            qjoc.Clear();
            int no = 2;
            int which = 0;
            YK[] res = ComputeDownSituation(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = res[i].ykname;

            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = which;

            groupListView.SelectedIndex = 0;

            qjocPresent = StrategyType.Down;


            Lock = false;

        }


        private void buyAndDownBtn_Click(object sender, RoutedEventArgs e)
        {
            closeDownPoly();

            DownSingleRefresh();

        }//点击“单买看跌”按钮

        private void DownSingleRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            YK[] res = ComputeBuyAndDown(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[1, i] = res[i];
                ykm.yk[1, i].title = "Top " + (i + 1);
            }

            BindingForOC(1, no);
            BindingForCharts(1, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;


            clocPresent = StrategyType.DownSingle;


            Lock = false;
        }


        private void bearSpreadBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择熊市差价，遮盖面板浮现
            openDownPoly.Visibility = Visibility.Visible;
            DoubleAnimation showOpenDownPoly = new DoubleAnimation();
            showOpenDownPoly.From = 0;
            showOpenDownPoly.To = 100;
            showOpenDownPoly.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            openDownPoly.BeginAnimation(Polygon.OpacityProperty, showOpenDownPoly);

            //Canvas、button缩回
            DoubleAnimation shorten = new DoubleAnimation();
            shorten.From = 102;
            shorten.To = 0;
            shorten.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            buyAndDownCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            bearSpreadCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            buyAndDownBtn.BeginAnimation(Button.WidthProperty, shorten);
            bearSpreadBtn.BeginAnimation(Button.WidthProperty, shorten);


            DownBearRefresh();


        }//点击“熊市差价”按钮


        private void DownBearRefresh()
        {             ///熊市差价计算
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            YK[] res = ComputeBear(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[1, i] = res[i];
                ykm.yk[1, i].title = "Top " + (i + 1);
            }

            BindingForOC(1, no);
            BindingForCharts(1, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.DownBear;


            Lock = false;
        }


        /// <summary>
        /// 计算下跌情景
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeDownSituation(int Tot)
        {
            YK[] ans = new YK[Tot];

            ans[0] = ComputeBuyAndDown(1)[0];
            ans[1] = ComputeBear(1)[0];

            return ans;
        }




        /// <summary>
        /// 计算单支看跌
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBuyAndDown(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMaxRateOfDecreTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.PutBuy] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "单买看跌", "下跌", MaxRange);
                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);
            }

            return ans;
        }


        /// <summary>
        /// 计算熊市差价
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBear(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMaxRateOfDecreTBox);

            //看涨熊市
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.CallSell] = 1;
                    _num[j, (int)OptionType.CallBuy] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "看涨组成的熊市", "下跌", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            //看跌熊市
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.PutSell] = 1;
                    _num[j, (int)OptionType.PutBuy] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "看跌组成的熊市", "下跌", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            //2比1熊市差价
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.PutSell] = 1;
                    _num[j, (int)OptionType.PutBuy] = 2;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "2比1熊市差价", "下跌", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            return ans;
        }


        #endregion



        #region 高波动率
        private bool isHighVolatilityPolyOpen = false;
        private void closeHighVolatilityPoly()
        {
            isHighVolatilityPolyOpen = false;
            //遮盖面板浮现
            openHighVolatilityPoly.Visibility = Visibility.Visible;
            DoubleAnimation showOpenHighVolatilityPoly = new DoubleAnimation();
            showOpenHighVolatilityPoly.From = 0;
            showOpenHighVolatilityPoly.To = 100;
            showOpenHighVolatilityPoly.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            openHighVolatilityPoly.BeginAnimation(Polygon.OpacityProperty, showOpenHighVolatilityPoly);

            //底部宽跨Canvas移回
            ThicknessAnimation bottomWideMove = new ThicknessAnimation();
            bottomWideMove.From = new Thickness(366, 200, 0, 0);
            bottomWideMove.To = new Thickness(468, 200, 0, 0);
            bottomWideMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bottomWideCanvas.BeginAnimation(Canvas.MarginProperty, bottomWideMove);

            //底部跨式Canvas移回
            ThicknessAnimation bottomCrossMove = new ThicknessAnimation();
            bottomCrossMove.From = new Thickness(352, 242, 0, 0);
            bottomCrossMove.To = new Thickness(454, 242, 0, 0);
            bottomCrossMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bottomCrossCanvas.BeginAnimation(Canvas.MarginProperty, bottomCrossMove);

            //Canvas、buttom缩回
            DoubleAnimation shorten = new DoubleAnimation();
            shorten.From = 102;
            shorten.To = 0;
            shorten.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bottomCrossCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            bottomWideCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            bottomWideBtn.BeginAnimation(Button.WidthProperty, shorten);
            bottomCrossBtn.BeginAnimation(Button.WidthProperty, shorten);

        }

        private void openHighVolatilityPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isHighVolatilityPolyOpen = true;

            if (isUpPolyOpen == true)
            {
                closeUpPoly();
            }
            if (isDownPolyOpen == true)
            {
                closeDownPoly();
            }
            if (isLowVolatilityPolyOpen == true)
            {
                closeLowVolatilityPoly();
            }
            if (isRisklessArbiPolyOpen == true)
            {
                closeRisklessArbiPoly();
            }

            //鼠标点击，隐藏遮盖面板
            DoubleAnimation hideOpenHighVolatilityPoly = new DoubleAnimation();
            hideOpenHighVolatilityPoly.From = openHighVolatilityPoly.Opacity;
            hideOpenHighVolatilityPoly.To = 0;
            hideOpenHighVolatilityPoly.Duration = new Duration(TimeSpan.Parse("0:0:1"));
            openHighVolatilityPoly.BeginAnimation(Polygon.OpacityProperty, hideOpenHighVolatilityPoly);
            openHighVolatilityPoly.Visibility = Visibility.Collapsed;

            //底部宽跨Canvas移出
            ThicknessAnimation bottomWideMove = new ThicknessAnimation();
            bottomWideMove.From = new Thickness(468, 200, 0, 0);
            bottomWideMove.To = new Thickness(366, 200, 0, 0);
            bottomWideMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bottomWideCanvas.BeginAnimation(Canvas.MarginProperty, bottomWideMove);

            //底部跨式Canvas移出
            ThicknessAnimation bottomCrossMove = new ThicknessAnimation();
            bottomCrossMove.From = new Thickness(454, 242, 0, 0);
            bottomCrossMove.To = new Thickness(352, 242, 0, 0);
            bottomCrossMove.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bottomCrossCanvas.BeginAnimation(Canvas.MarginProperty, bottomCrossMove);

            //Canvas、button伸长
            DoubleAnimation extend = new DoubleAnimation();
            extend.From = 0;
            extend.To = 102;
            extend.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            bottomWideCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            bottomCrossCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            bottomCrossBtn.BeginAnimation(Button.WidthProperty, extend);
            bottomWideBtn.BeginAnimation(Button.WidthProperty, extend);
        }//选择“高波动率”面板

        private void highVolatilityBtn_Click(object sender, RoutedEventArgs e)
        {
            closeHighVolatilityPoly();

            HighRefresh();

        }//点击“高波动率”按钮

        private void HighRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            qjoc.Clear();
            int no = 2;
            int which = 0;
            YK[] res = ComputeHighWaveSituation(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = res[i].ykname;

            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = which;

            groupListView.SelectedIndex = 0;


            qjocPresent = StrategyType.High;


            Lock = false;
        }

        private void bottomWideBtn_Click(object sender, RoutedEventArgs e)
        {
            closeHighVolatilityPoly();


            HighWideRefresh();

        }//点击“底部宽跨”按钮

        private void HighWideRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeBottomWide(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;


            clocPresent = StrategyType.HighWide;


            Lock = false;


        }


        private void bottomCrossBtn_Click(object sender, RoutedEventArgs e)
        {
            closeHighVolatilityPoly();

            HighSharpRefresh();

        }//点击“底部跨式”按钮

        private void HighSharpRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeBottomSharp(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;


            clocPresent = StrategyType.HighSharp;


            Lock = false;
        }


        /// <summary>
        /// 计算高波动率情景
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeHighWaveSituation(int Tot)
        {
            YK[] ans = new YK[Tot];

            ans[0] = ComputeBottomWide(1)[0];
            ans[1] = ComputeBottomSharp(1)[0];

            return ans;
        }




        /// <summary>
        /// 计算底部宽跨
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBottomWide(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMaxRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
                for (int j = 0; j < TotLine; j++)
                    if (i != j)
                    {
                        //填充
                        int[,] _num = new int[TotLine + 1, 4];
                        _num[i, (int)OptionType.CallBuy] = 1;
                        _num[j, (int)OptionType.PutBuy] = 1;
                        //计算盈亏数据
                        YK yk = new YK(TotLine, _num, "底部宽跨", "高波动率", MaxRange);
                        yk.ComputeYK();
                        //排序
                        SortYKAnswer(ans, yk, Tot);
                    }

            return ans;
        }


        /// <summary>
        /// 计算底部跨式
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBottomSharp(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMaxRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.CallBuy] = 1;
                _num[i, (int)OptionType.PutBuy] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "底部跨式", "高波动率", MaxRange);
                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);
            }

            return ans;
        }




        #endregion



        #region 低波动率
        private bool isLowVolatilityPolyOpen = false;
        private void closeLowVolatilityPoly()
        {
            isLowVolatilityPolyOpen = false;
            //遮盖面板浮现
            openLowVolatilityPoly.Visibility = Visibility.Visible;
            DoubleAnimation showOpenLowVolatilityPoly = new DoubleAnimation();
            showOpenLowVolatilityPoly.From = 0;
            showOpenLowVolatilityPoly.To = 100;
            showOpenLowVolatilityPoly.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            openLowVolatilityPoly.BeginAnimation(Polygon.OpacityProperty, showOpenLowVolatilityPoly);

            //Canvas、button缩回
            DoubleAnimation shorten = new DoubleAnimation();
            shorten.From = 102;
            shorten.To = 0;
            shorten.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            topCrossCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            topWideCanvas.BeginAnimation(Canvas.WidthProperty, shorten);
            topWideBtn.BeginAnimation(Button.WidthProperty, shorten);
            topCrossBtn.BeginAnimation(Button.WidthProperty, shorten);
        }


        private void openLowVolatilityPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isLowVolatilityPolyOpen = true;

            if (isUpPolyOpen == true)
            {
                closeUpPoly();
            }
            if (isDownPolyOpen == true)
            {
                closeDownPoly();
            }
            if (isHighVolatilityPolyOpen == true)
            {
                closeHighVolatilityPoly();
            }
            if (isRisklessArbiPolyOpen == true)
            {
                closeRisklessArbiPoly();
            }

            //鼠标点击，隐藏低波动率面板
            DoubleAnimation hideOpenLowVolatilityPoly = new DoubleAnimation();
            hideOpenLowVolatilityPoly.From = openLowVolatilityPoly.Opacity;
            hideOpenLowVolatilityPoly.To = 0;
            hideOpenLowVolatilityPoly.Duration = new Duration(TimeSpan.Parse("0:0:1"));
            openLowVolatilityPoly.BeginAnimation(Polygon.OpacityProperty, hideOpenLowVolatilityPoly);
            openLowVolatilityPoly.Visibility = Visibility.Collapsed;

            //Canvas、button伸长
            DoubleAnimation extend = new DoubleAnimation();
            extend.From = 0;
            extend.To = 102;
            extend.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            topWideCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            topCrossCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            topCrossBtn.BeginAnimation(Button.WidthProperty, extend);
            topWideBtn.BeginAnimation(Button.WidthProperty, extend);
        }//选择“低波动率”面板

        private void lowVolatilityBtn_Click(object sender, RoutedEventArgs e)
        {
            closeLowVolatilityPoly();

            LowRefresh();

        }//点击“低波动率”按钮

        private void LowRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            qjoc.Clear();
            int no = 2;
            int which = 0;
            YK[] res = ComputeLowWaveSituation(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = res[i].ykname;

            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = which;

            groupListView.SelectedIndex = 0;

            qjocPresent = StrategyType.Low;


            Lock = false;
        }


        private void topWideBtn_Click(object sender, RoutedEventArgs e)
        {
            closeLowVolatilityPoly();


            LowWideRefresh();


        }//点击“顶部宽跨”按钮


        private void LowWideRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeTopWide(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.LowWide;


            Lock = false;
        }


        private void topCrossBtn_Click(object sender, RoutedEventArgs e)
        {
            closeLowVolatilityPoly();


            LowSharpRefresh();

        }//点击“顶部跨式”按钮


        private void LowSharpRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeTopSharp(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.LowSharp;


            Lock = false;

        }


        /// <summary>
        /// 计算高波动率情景
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeLowWaveSituation(int Tot)
        {
            YK[] ans = new YK[Tot];

            ans[0] = ComputeTopWide(1)[0];
            ans[1] = ComputeTopSharp(1)[0];

            return ans;
        }




        /// <summary>
        /// 计算底部宽跨
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeTopWide(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMiniRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
                for (int j = 0; j < TotLine; j++)
                    if (i != j)
                    {
                        //填充
                        int[,] _num = new int[TotLine + 1, 4];
                        _num[i, (int)OptionType.CallSell] = 1;
                        _num[j, (int)OptionType.PutSell] = 1;
                        //计算盈亏数据
                        YK yk = new YK(TotLine, _num, "顶部宽跨", "低波动率", MaxRange);
                        yk.ComputeYK();
                        //排序
                        SortYKAnswer(ans, yk, Tot);
                    }

            return ans;
        }


        /// <summary>
        /// 计算底部跨式
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeTopSharp(int Tot)
        {
            YK[] ans = new YK[Tot];

            GetRateText(setMiniRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.CallSell] = 1;
                _num[i, (int)OptionType.PutSell] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "顶部跨式", "低波动率", MaxRange);
                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);
            }

            return ans;
        }


        #endregion




        #region 无风险套利
        private bool isRisklessArbiPolyOpen = false;
        private void closeRisklessArbiPoly()
        {
            isRisklessArbiPolyOpen = false;
            //遮盖面板浮现
            openRisklessArbiPoly.Visibility = Visibility.Visible;
            DoubleAnimation showOpenRisklessArbiPoly = new DoubleAnimation();
            showOpenRisklessArbiPoly.From = 0;
            showOpenRisklessArbiPoly.To = 100;
            showOpenRisklessArbiPoly.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            openRisklessArbiPoly.BeginAnimation(Canvas.OpacityProperty, showOpenRisklessArbiPoly);

            //转换套利Ellipse移回
            ThicknessAnimation moveCA = new ThicknessAnimation();
            moveCA.From = new Thickness(470, 84, 0, 0);
            moveCA.To = new Thickness(580, 119, 0, 0);
            moveCA.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            conversionArbitrageEllipse.Visibility = Visibility.Visible;
            conversionArbitrageEllipse.BeginAnimation(MarginProperty, moveCA);
            //组合标的Ellipse移回
            ThicknessAnimation moveMSM = new ThicknessAnimation();
            moveMSM.From = new Thickness(524, 55, 0, 0);
            moveMSM.To = new Thickness(580, 119, 0, 0);
            moveMSM.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            multiSubjectMatterEllipse.Visibility = Visibility.Visible;
            multiSubjectMatterEllipse.BeginAnimation(MarginProperty, moveMSM);
            //箱型套利Ellipse移回
            ThicknessAnimation moveBA = new ThicknessAnimation();
            moveBA.From = new Thickness(589, 55, 0, 0);
            moveBA.To = new Thickness(580, 119, 0, 0);
            moveBA.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            boxArbitrageEllipse.Visibility = Visibility.Visible;
            boxArbitrageEllipse.BeginAnimation(MarginProperty, moveBA);
            //凸性套利Ellipse移回
            ThicknessAnimation moveCA2 = new ThicknessAnimation();
            moveCA2.From = new Thickness(642, 84, 0, 0);
            moveCA2.To = new Thickness(580, 119, 0, 0);
            moveCA2.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            convexityArbitrageEllipse.Visibility = Visibility.Visible;
            convexityArbitrageEllipse.BeginAnimation(MarginProperty, moveCA2);

            //eclipse缩小
            DoubleAnimation reduce = new DoubleAnimation();
            reduce.From = 47;
            reduce.To = 0;
            reduce.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            conversionArbitrageEllipse.BeginAnimation(Ellipse.WidthProperty, reduce);
            conversionArbitrageEllipse.BeginAnimation(Ellipse.HeightProperty, reduce);
            multiSubjectMatterEllipse.BeginAnimation(Ellipse.WidthProperty, reduce);
            multiSubjectMatterEllipse.BeginAnimation(Ellipse.HeightProperty, reduce);
            boxArbitrageEllipse.BeginAnimation(Ellipse.WidthProperty, reduce);
            boxArbitrageEllipse.BeginAnimation(Ellipse.HeightProperty, reduce);
            convexityArbitrageEllipse.BeginAnimation(Ellipse.WidthProperty, reduce);
            convexityArbitrageEllipse.BeginAnimation(Ellipse.HeightProperty, reduce);
        }

        private void openRisklessArbiPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isRisklessArbiPolyOpen = true;

            if (isUpPolyOpen == true)
            {
                closeUpPoly();
            }
            if (isDownPolyOpen == true)
            {
                closeDownPoly();
            }
            if (isHighVolatilityPolyOpen == true)
            {
                closeHighVolatilityPoly();
            }
            if (isLowVolatilityPolyOpen == true)
            {
                closeLowVolatilityPoly();
            }

            //鼠标点击，遮盖面板隐藏
            DoubleAnimation hideOpenRisklessArbiPoly = new DoubleAnimation();
            hideOpenRisklessArbiPoly.From = openRisklessArbiPoly.Opacity;
            hideOpenRisklessArbiPoly.To = 0;
            hideOpenRisklessArbiPoly.Duration = new Duration(TimeSpan.Parse("0:0:1"));
            openRisklessArbiPoly.BeginAnimation(Canvas.OpacityProperty, hideOpenRisklessArbiPoly);
            openRisklessArbiPoly.Visibility = Visibility.Collapsed;

            //转换套利Ellipse移出
            ThicknessAnimation moveCA = new ThicknessAnimation();
            moveCA.From = new Thickness(580, 119, 0, 0);
            moveCA.To = new Thickness(470, 84, 0, 0);
            moveCA.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            conversionArbitrageEllipse.Visibility = Visibility.Visible;
            conversionArbitrageEllipse.BeginAnimation(MarginProperty, moveCA);
            //组合标的Ellipse移出
            ThicknessAnimation moveMSM = new ThicknessAnimation();
            moveMSM.From = new Thickness(580, 119, 0, 0);
            moveMSM.To = new Thickness(524, 55, 0, 0);
            moveMSM.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            multiSubjectMatterEllipse.Visibility = Visibility.Visible;
            multiSubjectMatterEllipse.BeginAnimation(MarginProperty, moveMSM);
            //箱型套利Ellipse移出
            ThicknessAnimation moveBA = new ThicknessAnimation();
            moveBA.From = new Thickness(580, 119, 0, 0);
            moveBA.To = new Thickness(589, 55, 0, 0);
            moveBA.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            boxArbitrageEllipse.Visibility = Visibility.Visible;
            boxArbitrageEllipse.BeginAnimation(MarginProperty, moveBA);
            //凸性套利Ellipse移出
            ThicknessAnimation moveCA2 = new ThicknessAnimation();
            moveCA2.From = new Thickness(580, 119, 0, 0);
            moveCA2.To = new Thickness(642, 84, 0, 0);
            moveCA2.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            convexityArbitrageEllipse.Visibility = Visibility.Visible;
            convexityArbitrageEllipse.BeginAnimation(MarginProperty, moveCA2);

            //eclipse长大
            DoubleAnimation grow = new DoubleAnimation();
            grow.From = 0;
            grow.To = 47;
            grow.Duration = new Duration(TimeSpan.Parse("0:0:0.1"));
            conversionArbitrageEllipse.BeginAnimation(Ellipse.WidthProperty, grow);
            conversionArbitrageEllipse.BeginAnimation(Ellipse.HeightProperty, grow);
            multiSubjectMatterEllipse.BeginAnimation(Ellipse.WidthProperty, grow);
            multiSubjectMatterEllipse.BeginAnimation(Ellipse.HeightProperty, grow);
            boxArbitrageEllipse.BeginAnimation(Ellipse.WidthProperty, grow);
            boxArbitrageEllipse.BeginAnimation(Ellipse.HeightProperty, grow);
            convexityArbitrageEllipse.BeginAnimation(Ellipse.WidthProperty, grow);
            convexityArbitrageEllipse.BeginAnimation(Ellipse.HeightProperty, grow);

        }//选择“无风险套利”面板


        private void risklessArbiBtn_Click(object sender, RoutedEventArgs e)
        {

            closeRisklessArbiPoly();

            NoRefresh();

        }//点击“无风险套利”面板

        private void conversionArbitrageEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeRisklessArbiPoly();

            NoCARefresh();
        }//点击“转换套利”按钮

        private void multiSubjectMatterEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeRisklessArbiPoly(); 

            NoMSMRefresh();
        }//点击“组合标的”按钮

        private void boxArbitrageEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeRisklessArbiPoly();

            NoBARefresh();
        }//点击“箱型套利”按钮

        private void convexityArbitrageEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            closeRisklessArbiPoly();

            NoCA2Refresh();
        }//点击“凸性套利”按钮


        private void NoRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            int no = 4;
            int which = 0;
            YK[] res = ComputeNoSituation(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                if (res[i]!=null) 
                    ykm.yk[which, i].title = res[i].ykname;

            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = which;

            groupListView.SelectedIndex = 0;

            qjocPresent = StrategyType.NoRisk;


            Lock = false;

        }

        private void NoCARefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeNoCA(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                if (res[i]!=null) 
                    ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.NoCA;


            Lock = false;

        }

        private void NoMSMRefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in NoMSMR");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeNoMSM(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                if (res[i]!=null)
                    ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.NoMSM;


            Lock = false;

        }

        private void NoBARefresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeNoBA(no);

            for (int i = 0; i < no; i++)
            {
                ykm.yk[which, i] = res[i];
                if (res[i]!=null)
                    ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;

            clocPresent = StrategyType.NoBA;


            Lock = false;

        }

        private void NoCA2Refresh()
        {
            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            cloc.Clear();
            int no = 4;
            int which = 1;
            YK[] res = ComputeNoCA2(no);

            for (int i = 0; i < no; i++)      
            {
                ykm.yk[which, i] = res[i];
                if (res[i] != null)  ykm.yk[which, i].title = "Top " + (i + 1);
            }

            BindingForOC(which, no);
            BindingForCharts(which, no);

            ResultTab.SelectedIndex = 1;

            ListView lv = null;
            if (which == 0)
                lv = groupListView;
            if (which == 1)
                lv = strategyListView;
            if (lv.Items.Count > 0)
                lv.SelectedIndex = 0;
            else
            {
                VolatilityChart.Visibility = Visibility.Hidden;
                VolatilityChart2.Visibility = Visibility.Hidden;
                ZoomInGL.Visibility = Visibility.Hidden;
                ZoomInYK.Visibility = Visibility.Hidden;
                for (int i = 0; i < 10; i++)
                    labels[i].Visibility = Visibility.Hidden;
            }

            clocPresent = StrategyType.NoCA2;


            Lock = false;

        }


        private YK[] ComputeNoSituation(int Tot)
        {
            YK[] ans = new YK[Tot];

            ans[0] = ComputeNoCA(1)[0];
            ans[1] = ComputeNoMSM(1)[0];
            ans[2] = ComputeNoBA(1)[0];
            ans[3] = ComputeNoCA2(1)[0];

            return ans;
        }

        double NoRiskRateDefault=0.2;
        private YK[] ComputeNoCA(int Tot)
        {
            YK[] ans = new YK[Tot];

            //GetRateText(setMiniRateOfVolatiTBox);
            MaxRange = NoRiskRateDefault;
            string subject = nameInStrategyPanelComboBox.Text.Trim();

            for (int i = 0; i < TotLine; i++)
            {
                double c, k, p, s0;
                c = YKManager.ykOption[i, 0].BidPrice;
                k = YKManager.ykOption[i, 0].ExercisePrice;
                p = YKManager.ykOption[i, 2].AskPrice;
                s0 = YKManager.ykFuture[0].AskPrice;
                //if (c + k - p - s0 > 0)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    if (subject.Equals("上证50") || subject.Equals("沪深300"))
                    {
                        _num[i, (int)OptionType.CallBuy] = 3;
                        _num[i, (int)OptionType.PutSell] = 3;
                    }
                    else
                    {
                        _num[i, (int)OptionType.CallBuy] = 1;
                        _num[i, (int)OptionType.PutSell] = 1;
                    }
                    _num[TotLine, 1] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "转换套利", "无风险套利", MaxRange);
                    if (subject.Equals("上证50") || subject.Equals("沪深300"))
                        yk.szorhs = true;
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

                c = YKManager.ykOption[i, 0].AskPrice;
                k = YKManager.ykOption[i, 0].ExercisePrice;
                p = YKManager.ykOption[i, 2].BidPrice;
                s0 = YKManager.ykFuture[0].BidPrice;
                //if (p+s0-c-k > 0)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    if (subject.Equals("上证50") || subject.Equals("沪深300"))
                    {
                        _num[i, (int)OptionType.CallSell] = 3;
                        _num[i, (int)OptionType.PutBuy] = 3;
                    }
                    else
                    {
                        _num[i, (int)OptionType.CallSell] = 1;
                        _num[i, (int)OptionType.PutBuy] = 1;
                    }
                    _num[TotLine, 0] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "转换套利", "无风险套利", MaxRange);
                    if (subject.Equals("上证50") || subject.Equals("沪深300"))
                        yk.szorhs = true;
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }


            }

            return ans;
        }

        private YK[] ComputeNoMSM(int Tot)
        {
            YK[] ans = new YK[Tot];

            //GetRateText(setMiniRateOfVolatiTBox);
            MaxRange = NoRiskRateDefault;
            string subject = nameInStrategyPanelComboBox.Text.Trim();

            for (int i = 0; i < TotLine; i++)
            {
                double c, k, p, s0;
                c = YKManager.ykOption[i, 0].BidPrice;
                k = YKManager.ykOption[i, 0].ExercisePrice;
                p = YKManager.ykOption[i, 2].AskPrice;
                s0 = YKManager.ykFuture[0].AskPrice;
                //填充  A：买入一份看涨，卖出一份标的；
                int[,] _num = new int[TotLine + 1, 4];
                if (subject.Equals("上证50") || subject.Equals("沪深300"))
                {
                    _num[i, (int)OptionType.CallBuy] = 3;
                }
                else
                {
                    _num[i, (int)OptionType.CallBuy] = 1;
                }
                _num[TotLine, 1] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "组合标的", "无风险套利", MaxRange);
                if (subject.Equals("上证50") || subject.Equals("沪深300"))
                    yk.szorhs = true;

                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);


                //填充 B：买入一份看跌，买入一份标的
                _num = new int[TotLine + 1, 4];
                if (subject.Equals("上证50") || subject.Equals("沪深300"))
                {
                    _num[i, (int)OptionType.PutBuy] = 3;
                }
                else
                {
                    _num[i, (int)OptionType.PutBuy] = 1;
                }
                _num[TotLine, 0] = 1;
                //计算盈亏数据
                yk = new YK(TotLine, _num, "组合标的", "无风险套利", MaxRange);
                if (subject.Equals("上证50") || subject.Equals("沪深300"))
                    yk.szorhs = true;

                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);


            }

            return ans;
        }

        private YK[] ComputeNoBA(int Tot)
        {
            YK[] ans = new YK[Tot];

            //GetRateText(setMiniRateOfVolatiTBox);
            MaxRange = NoRiskRateDefault;

            string subject = nameInStrategyPanelComboBox.Text.Trim();

            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充  A：买入b和α 卖出a和β

                    int[,] _num = new int[TotLine + 1, 4];
                    if (subject.Equals("上证50") || subject.Equals("沪深300"))
                    {
                        _num[j, (int)OptionType.CallBuy] = 1;
                        _num[i, (int)OptionType.PutBuy] = 1;
                        _num[i, (int)OptionType.CallSell] = 1;
                        _num[j, (int)OptionType.PutSell] = 1;

                    }
                    else
                    {
                        _num[j, (int)OptionType.CallBuy] = 1;
                        _num[i, (int)OptionType.PutBuy] = 1;
                        _num[i, (int)OptionType.CallSell] = 1;
                        _num[j, (int)OptionType.PutSell] = 1;

                    }
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "箱型套利", "无风险套利", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);


                    //填充 B：买入一份看跌，买入一份标的
                    _num = new int[TotLine + 1, 4];
                    if (subject.Equals("上证50") || subject.Equals("沪深300"))
                    {
                        _num[i, (int)OptionType.CallBuy] = 1;
                        _num[j, (int)OptionType.PutBuy] = 1;
                        _num[j, (int)OptionType.CallSell] = 1;
                        _num[i, (int)OptionType.PutSell] = 1;

                    }
                    else
                    {
                        _num[i, (int)OptionType.CallBuy] = 1;
                        _num[j, (int)OptionType.PutBuy] = 1;
                        _num[j, (int)OptionType.CallSell] = 1;
                        _num[i, (int)OptionType.PutSell] = 1;

                    }
                    //计算盈亏数据
                    yk = new YK(TotLine, _num, "箱型套利", "无风险套利", MaxRange);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);


                }

            return ans;
        }




        private int ZhanZhuan(int n1, int n2)
        {
            int m, n, r = 0;
            m = n1;
            n = n2;
            while (n != 0)
            {
                r = m % n;
                m = n;
                n = r;
            }
            //Console.WriteLine("辗转相除法：" + n1 + " " + n2 + " " + n);
            return m;

        }

        private YK[] ComputeNoCA2(int Tot)
        {
            YK[] ans = new YK[Tot];

            //GetRateText(setMiniRateOfVolatiTBox);//!
            MaxRange = NoRiskRateDefault;

            string subject = nameInStrategyPanelComboBox.Text.Trim();

            for (int i = 0; i < TotLine - 2; i++)
                for (int j = i + 1; j < TotLine - 1; j++)
                    for (int k = j + 1; k < TotLine; k++)
                    {
                        int k1, k2, k3;
                        k1 = YKManager.ykOption[i, 0].ExercisePrice;
                        k2 = YKManager.ykOption[j, 0].ExercisePrice;
                        k3 = YKManager.ykOption[k, 0].ExercisePrice;
                        int n1, n2, n3;
                        n1 = k3 - k2;
                        n2 = k3 - k1;
                        n3 = k2 - k1;
                        int gys = ZhanZhuan(ZhanZhuan(n1, n2), n3);
                        n1 = n1 / gys;
                        n2 /= gys;
                        n3 /= gys;
                        int[,] _num;
                        YK yk;
                        

                        double c1, c2, c3;
                        c1 = YKManager.ykOption[i, (int)OptionType.CallBuy].BidPrice;
                        c2 = YKManager.ykOption[j, (int)OptionType.CallSell].AskPrice;
                        c3 = YKManager.ykOption[k, (int)OptionType.CallSell].AskPrice;

                        //if (n2 * c2 > n1 * c1 + n3 * c3)
                        {
                            //填充 B：
                            _num = new int[TotLine + 1, 4];
                            if (subject.Equals("上证50") || subject.Equals("沪深300"))
                            {
                                _num[j, (int)OptionType.CallSell] = n2 * 1;
                                _num[i, (int)OptionType.CallBuy] = n1 * 1;
                                _num[k, (int)OptionType.CallBuy] = n3 * 1;

                            }
                            else
                            {
                                _num[j, (int)OptionType.CallSell] = n2 * 1;
                                _num[i, (int)OptionType.CallBuy] = n1;
                                _num[k, (int)OptionType.CallBuy] = n3;
                            }

                            //计算盈亏数据
                            yk = new YK(TotLine, _num, "凸性套利1", "无风险套利", MaxRange);
                            yk.ComputeYK();
                            //排序
                            SortYKAnswer(ans, yk, Tot);
                        }

                    }

            for (int i = 0; i < TotLine - 2; i++)
                for (int j = i + 1; j < TotLine - 1; j++)
                    for (int k = j + 1; k < TotLine; k++)
                    {
                        int k1, k2, k3;
                        k1 = YKManager.ykOption[i, 0].ExercisePrice;
                        k2 = YKManager.ykOption[j, 0].ExercisePrice;
                        k3 = YKManager.ykOption[k, 0].ExercisePrice;
                        int n1, n2, n3;
                        n1 = k3 - k2;
                        n2 = k3 - k1;
                        n3 = k2 - k1;
                        int gys = ZhanZhuan(ZhanZhuan(n1, n2), n3);
                        n1 = n1 / gys;
                        n2 /= gys;
                        n3 /= gys;

                        if (n2 == 2 && n1 == 1 && n3 == 1) continue;


                        int[,] _num;
                        YK yk;



                        double c1, c2, c3;
                        c1 = YKManager.ykOption[i, (int)OptionType.CallBuy].BidPrice;
                        c2 = YKManager.ykOption[j, (int)OptionType.CallSell].AskPrice;
                        c3 = YKManager.ykOption[k, (int)OptionType.CallSell].AskPrice;

                        
                            //填充 B：
                            _num = new int[TotLine + 1, 4];
   
                                _num[j, (int)OptionType.CallSell] = 2;
                                _num[i, (int)OptionType.CallBuy] = 1;
                                _num[k, (int)OptionType.CallBuy] = 1;

                            //计算盈亏数据
                            yk = new YK(TotLine, _num, "凸性套利2", "无风险套利", MaxRange);
                            yk.ComputeYK();
                            //排序
                            SortYKAnswer(ans, yk, Tot);
                        

                    }


            return ans;
        }




        #endregion


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            initialing = true;


            GetChoice();
            optionsTraderInStrategyPanelComboBox.Items.Clear();
            optionsTraderInStrategyPanelComboBox.Items.Add("中金所");
            optionsTraderInStrategyPanelComboBox.Items.Add("郑商所");
            optionsTraderInStrategyPanelComboBox.Items.Add("上期所");

            for (int i = 0; i < optionsTraderInStrategyPanelComboBox.Items.Count; i++)
                if (((string)optionsTraderInStrategyPanelComboBox.Items[i]).Equals(Trader))
                {
                    optionsTraderInStrategyPanelComboBox.SelectedIndex = i;
                    break;
                }
            if (optionsTraderInStrategyPanelComboBox.SelectedIndex == -1)
                optionsTraderInStrategyPanelComboBox.SelectedIndex = 0;

            for (int i = 0; i < nameInStrategyPanelComboBox.Items.Count; i++)
                if (((string)nameInStrategyPanelComboBox.Items[i]).Equals(Subject))
                {
                    nameInStrategyPanelComboBox.SelectedIndex = i;
                    break;
                }
            if (nameInStrategyPanelComboBox.SelectedIndex == -1)
                nameInStrategyPanelComboBox.SelectedIndex = 0;

            for (int i = 0; i < dueDateComboBox.Items.Count; i++)
                if (((string)dueDateComboBox.Items[i]).Equals(Duedate))
                {
                    dueDateComboBox.SelectedIndex = i;
                    break;
                }
            if (dueDateComboBox.SelectedIndex == -1)
                dueDateComboBox.SelectedIndex = 0;

            labels = new Label[10];
            for (int i = 0; i < 10; i++)
            {
                labels[i] = new Label();
                labels[i].Foreground = Brushes.White;
                LeftChartsCanvas.Children.Add(labels[i]);
                labels[i].Visibility = Visibility.Hidden;
            }

            StrategyInitial();
            initialing = false;

            //刷新走势图
            System.Timers.Timer timer2 = new System.Timers.Timer(3000);
            timer2.Elapsed += new ElapsedEventHandler(RefreshChart);
            timer2.Start();

        }


        delegate void RefreshChartCallBack(object sender, ElapsedEventArgs e);
        private void RefreshChart(object sender, ElapsedEventArgs e)
        {
            RefreshChartCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new RefreshChartCallBack(RefreshChart);
                this.Dispatcher.Invoke(d, new object[] { sender, e });
            }
            else
            {
                VolatilityChart4.InvalidateMeasure();
            }
        }



        Label[] labels;
        LineChartGraph temp1, temp2;
        public string Trader, Subject, Duedate;
        delegate void VoidCallBack();
        public void GetChoice()
        {

            VoidCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new VoidCallBack(GetChoice);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                Trader = pwindow.traderComboBox.Text.Trim();
                Subject = pwindow.subjectMatterComboBox.Text.Trim();
                Duedate = pwindow.dueDateComboBox.Text.Trim();
            }

        }






        #region 三个下拉选单

        private void optionsTraderInStrategyPanelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string trader;
            trader = (string)e.AddedItems[0];
            trader = trader.Trim();
            optionsTraderInStrategyPanelComboBox.Text = trader;
            nameInStrategyPanelComboBox.Items.Clear();
            if (trader.Equals("上期所"))
            {
                nameInStrategyPanelComboBox.Items.Add("金");
                nameInStrategyPanelComboBox.Items.Add("铜");
            }
            if (trader.Equals("大商所"))
            {
                nameInStrategyPanelComboBox.Items.Add("豆粕");
            }
            if (trader.Equals("中金所"))
            {
                nameInStrategyPanelComboBox.Items.Add("上证50");
                nameInStrategyPanelComboBox.Items.Add("沪深300");
            }
            if (trader.Equals("郑商所"))
            {
                nameInStrategyPanelComboBox.Items.Add("白糖");
            }
            nameInStrategyPanelComboBox.SelectedItem = nameInStrategyPanelComboBox.Items[0];

        }

        private void nameInStrategyPanelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            string _subject = (string)e.AddedItems[0];
            nameInStrategyPanelComboBox.Text = _subject;
            string _option = "";
            for (int i = 0; i < MainWindow.NameSubject.Length; i++)
                if (MainWindow.NameSubject[i].Equals(_subject))
                    _option = MainWindow.NameOption[i];
            string duedatesql = "select duedate from staticdata where instrumentname='" + _option + "' group by duedate";
            DataTable dt = DataControl.QueryTable(duedatesql);
            dueDateComboBox.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _duedate = (string)dt.Rows[i][0];
                if (_duedate.Equals("1407")) continue;
                if (_duedate.Equals("1503") && (_subject.Equals("上证50") || _subject.Equals("沪深300"))) continue;
                dueDateComboBox.Items.Add(_duedate);
            }
            dueDateComboBox.SelectedIndex = 0;

        }

        private void dueDateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            string trader = optionsTraderInStrategyPanelComboBox.Text.Trim();
            string duedata = (string)e.AddedItems[0];
            string subject = nameInStrategyPanelComboBox.Text.Trim();

            dueDateComboBox.Text = duedata;

            if (trader.Equals("") || duedata.Equals("") || subject.Equals(""))
                return;

            if (!initialing)
                StrategyInitial();
        }
        #endregion








        bool initialing = true;
        YKManager ykm = new YKManager();
        bool Lock = false;
        int TotLine = 0;
        private void StrategyInitial()
        {
            string trader = optionsTraderInStrategyPanelComboBox.Text.Trim();
            string duedata = dueDateComboBox.Text.Trim();
            string subject = nameInStrategyPanelComboBox.Text.Trim();
            if (trader.Equals("") || duedata.Equals("") || subject.Equals(""))
                return;

            TotLine = ykm.Initial( nameInStrategyPanelComboBox.Text.Trim(), dueDateComboBox.Text.Trim());
            cloc = new ObservableCollection<TopStrategy>();
            qjoc = new ObservableCollection<TopStrategy>();
            groupListView.DataContext = qjoc;
            strategyListView.DataContext = cloc;
            qjocPresent = StrategyType.No;
            clocPresent = StrategyType.No;
            //LegendMask.Visibility = Visibility.Visible;
            VolatilityChart.Visibility = Visibility.Hidden;
            VolatilityChart2.Visibility = Visibility.Hidden;
            VolatilityChart3.Visibility = Visibility.Hidden;
            VolatilityChart4.Visibility = Visibility.Hidden;

            ZoomInGL.Visibility = Visibility.Hidden;
            ZoomInYK.Visibility = Visibility.Hidden;
            ZoomInYKs.Visibility = Visibility.Hidden;
            ZoomInZS.Visibility = Visibility.Hidden;

            for (int i = 0; i < labels.Length; i++)
                labels[i].Visibility = Visibility.Hidden;

            ///走势图初始化
            //stockChart.Charts[0].Collapse();
            //DateTime present = DataManager.now;
            //present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
            //stockChart.StartDate = present;
            //new Thread(new ThreadStart(TrendInitial)).Start();
            //timer = new System.Timers.Timer(60000);
            //timer.Elapsed += new ElapsedEventHandler(refreshTrend);
            //timer.Start();//每分钟刷新一次走势图

        }



        /*
        private void TrendInitial()
        {
            trm = new Trend();
            trm.initial(YKManager.SubjectID);
            BindingStockData = trm.Data;
            TrendInitialCallBack(trm);
        }//初始化走势图（载入历史数据）

        Trend trm;
        System.Timers.Timer timer;
        delegate void trendInitialCallBack(Trend tr);
        private void TrendInitialCallBack(Trend tr)
        {
            trendInitialCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new trendInitialCallBack(TrendInitialCallBack);
                this.Dispatcher.Invoke(d, new object[] { tr });
            }
            else
            {
                //stockSet1.ItemsSource = tr.Data;
            }
        }//走势图数据绑定


        delegate void RefreshCallBack(object sender, ElapsedEventArgs e);
        private void refreshTrend(object sender, ElapsedEventArgs e)
        {
            RefreshCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new RefreshCallBack(refreshTrend);
                pwindow.Dispatcher.Invoke(d, new object[] { sender, e });
            }
            else
            {

               // DataRow row = (DataRow)DataManager.All[YKManager.SubjectID];
               // double _open = Math.Round((double)row["OpenPrice"], 1);
               // double _close = Math.Round((double)row["LastPrice"], 1);
               // double _high = Math.Round((double)row["HighestPrice"], 1);
               // double _low = Math.Round((double)row["LowestPrice"], 1);
               // double _volume = Math.Round((double)row["OpenInterest"],1);
               // DateTime present = DataManager.now;
               // present = new DateTime(present.Year, present.Month, present.Day, present.Hour, present.Minute, 0);
               // trm.Data.Add(new StockInfo
               //{
               //    date = present,
               //    open = _open,
               //    high = _high,
               //    low = _low,
               //    close = _close,
               //    volume = _volume
               //});
                //stockChart.Scroll(1);
            }

        }//每分钟刷新一次走势图
        */





        /// <summary>
        /// 当份数改变时刷新价格等数据
        /// </summary>
        /// <param name="ts"></param>
        private void RefreshTSPrice(TopStrategy ts)
        {
            int number = ts.Number;
            ts.EarnRate = (1 * ts.earnRatePer).ToString("f1") + "%";
            ts.ExpectEarn = (number * ts.expectEarnPer).ToString("n0");
            ts.Fee = (number * ts.feePer).ToString("f1");
            ts.Guarantee = (number * ts.guaranteePer).ToString("n0");
            ts.LoseRate = (1 * ts.loseRatePer).ToString("f1") + "%";
            ts.MaxEarn = (number * ts.maxEarnPer).ToString("n0");
            ts.MaxLose = (number * ts.maxLosePer).ToString("n0");
            ts.Price = (number * ts.pricePer).ToString("f1");
        }

        /// <summary>
        /// 将ykm中的数据绑定到两个列表
        /// </summary>
        /// <param name="which"></param>
        /// <param name="num"></param>

        delegate void BindingOCCallBack(int which, int num);
        private void BindingForOC(int which, int num)
        {

            BindingOCCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new BindingOCCallBack(BindingForOC);
                pwindow.Dispatcher.Invoke(d, new object[] { which, num });
            }
            else
            {

                ObservableCollection<TopStrategy> oc;
                if (which == 0)
                    oc = qjoc;
                else
                    oc = cloc;
                TopStrategy ts;

                for (int i = 0; i < num; i++)
                {
                    if (i >= oc.Count) ts = new TopStrategy();
                    else ts = oc[i];
                    if (ykm.yk[which,i]==null) continue;
                    ts.earnRatePer = ykm.yk[which, i].EarnRate;
                    ts.expectEarnPer = ykm.yk[which, i].ExpectEarn;
                    ts.feePer = 0;//!
                    //计算购买一手时的手续费
                    foreach (OrderUnit item in ykm.yk[which,i].OrderList)
                    {
                        if (item.Number > 0)
                            ts.feePer += SomeCalculate.calculateFees(item.InstrumentID, item.Number, item.AskPrice);
                        else
                            ts.feePer += SomeCalculate.calculateFees(item.InstrumentID, item.Number, item.BidPrice);
                    }


                    ts.guaranteePer = 0;
                    foreach (OrderUnit item in ykm.yk[which, i].OrderList)
                    {
                        if (item.Number > 0)
                            ts.guaranteePer += SomeCalculate.caculateMargin(item.InstrumentID, item.Number, true,item.AskPrice);
                        else
                            ts.guaranteePer += SomeCalculate.caculateMargin(item.InstrumentID, item.Number, false,item.BidPrice);
                     }


                    ts.loseRatePer = ykm.yk[which, i].LoseRate;
                    ts.maxEarnPer = ykm.yk[which, i].MaxEarn;
                    ts.maxLosePer = ykm.yk[which, i].MaxLose;
                    ts.Name = ykm.yk[which, i].ykname;
                    ts.GroupName = ykm.yk[which, i].ykgroupname;
                    ts.Number = 1;
                    ts.pricePer = ykm.yk[which, i].Price;
                    WHICH = which;
                    new Thread(new ThreadStart(setVAR)).Start();
                    ts.LineNo = i;
                    RefreshTSPrice(ts);

                    if (i >= oc.Count) oc.Add(ts);
                }

                for (int i = num; i < oc.Count; i++)
                    oc.RemoveAt(i);
            }
        }
        static int WHICH = 0;




        /// <summary>
        /// 计算正态值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="lastprice"></param>
        /// <param name="ykmax"></param>
        /// <returns></returns>
        public static double ComputeZT(int x, double lastprice, double ykmax)
        {
            double u = lastprice, _o = ykmax * 100, o = _o;
            if (u >= 1000) o = 3 * _o;
            if (u >= 5000) o = 5 * _o;
            if (u >= 10000) o = 7 * _o;
            if (u >= 30000) o = 10 * _o;
            return 1.0 / (Math.Sqrt(2 * Math.PI) * o) * Math.Exp(-Math.Pow((x - u), 2) / (2 * (Math.Pow(o, 2))));
        }

        /// <summary>
        /// 绘制盈亏图和概率图
        /// </summary>
        /// <param name="which"></param>
        /// <param name="no"></param>
        public void BindingForChart(int which, int no)
        {

            BindingOCCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new BindingOCCallBack(BindingForChart);
                pwindow.Dispatcher.Invoke(d, new object[] { which, no });
            }
            else
            {
                VolatilityChart.Visibility = Visibility.Visible;
                VolatilityChart2.Visibility = Visibility.Visible;
                ZoomInGL.Visibility = Visibility.Visible;
                ZoomInYK.Visibility = Visibility.Visible;

                ObservableCollection<XY> data = new ObservableCollection<XY>();
                ObservableCollection<XY> data2 = new ObservableCollection<XY>();
                ObservableCollection<XY> data3 = new ObservableCollection<XY>();
                ObservableCollection<XY> data4 = new ObservableCollection<XY>();
                ObservableCollection<XY> dataTrans1 = new ObservableCollection<XY>();
                ObservableCollection<XY> dataTrans2 = new ObservableCollection<XY>();

                ObservableCollection<XY> coor = new ObservableCollection<XY>();
                YK yk = ykm.yk[which, no];
                if (yk == null) return;
                ChartWindow.Probability = yk.probability;
                ChartWindow.LeftEdge = yk.LeftEdge;
                ChartWindow.RightEdge = yk.RightEdge;
                int left = yk.LeftEdge, right = yk.RightEdge;
                double lastprice = yk.ykfuture[0].LastPrice;
                double max1 = -1e10;
                double max2 = -1e10;
                double min2 = 1e10;

                int now = 0;
                bool positive = yk.probability[0].positive;
                for (int j = left; j <= right; j += yk.ykstep)
                {
                    //概率图
                    double tempX = (int)j;
                    double tempY = StrategyWindow.ComputeZT((int)j, lastprice, yk.ykmax);

                    coor.Add(new XY() { X = tempX, Y = tempY });

                    if ((int)j == yk.probability[now].x)
                    {
                        Console.WriteLine(yk.probability[now].percent);
                        positive = yk.probability[now + 1].positive;
                        now++;
                        data.Add(new XY() { X = tempX, Y = tempY });
                        data2.Add(new XY() { X = tempX, Y = tempY });
                    }
                    else if (positive)
                        data.Add(new XY() { X = tempX, Y = tempY });
                    else
                        data2.Add(new XY() { X = tempX, Y = tempY });
                    if (tempY > max1)
                        max1 = tempY;




                    //盈亏图
                    tempX = (int)j;
                    XY point = yk.points[(int)((j - left) / yk.ykstep)];
                    if (point.Y == 0)
                    {
                        data3.Add(point);
                        data4.Add(point);
                    }
                    else if (point.Y > 0)
                        data3.Add(point);
                    else
                        data4.Add(point);

                    if (point.Y > max2)
                        max2 = point.Y;
                    if (point.Y < min2)
                        min2 = point.Y;
                }

                if (yk.ykname.Equals("转换套利") || yk.ykname.Equals("箱型套利"))
                {
                    dataTrans1.Add(new XY(left, max1 * 1.2));
                }
                else
                {
                    dataTrans1.Add(new XY(left, max1 * 1.2));

                    if (max2 < 0) max2 = 0;
                    if (min2 > 0) min2 = 0;
                    dataTrans2.Add(new XY(left, max2 + (max2 - min2) * 0.2));

                   
                }

                System.Windows.Data.Binding coorBinding = new System.Windows.Data.Binding();    //X坐标轴绑定

                System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding();   //数据绑定
                System.Windows.Data.Binding dataBinding2 = new System.Windows.Data.Binding();   //数据绑定
                System.Windows.Data.Binding dataBinding3 = new System.Windows.Data.Binding();   //数据绑定
                System.Windows.Data.Binding dataBinding4 = new System.Windows.Data.Binding();   //数据绑定
                System.Windows.Data.Binding dataBindingTrans2 = new System.Windows.Data.Binding();   //数据绑定
                System.Windows.Data.Binding dataBindingTrans1 = new System.Windows.Data.Binding();   //数据绑定


                Bindings[0] = coorBinding;
                Bindings[1] = dataBinding;
                Bindings[2] = dataBinding2;
                Bindings[3] = dataBinding3;
                Bindings[4] = dataBinding4;



                coorBinding.Source = coor;

                dataBinding.Source = data;
                dataBinding2.Source = data2;
                dataBinding3.Source = data3;
                dataBinding4.Source = data4;
                dataBindingTrans1.Source = dataTrans1;
                dataBindingTrans2.Source = dataTrans2;

                this.Transparent1.SetBinding(SerialGraph.DataItemsSourceProperty, dataBindingTrans1);
                this.Transparent1.SeriesIDMemberPath = "X";
                this.Transparent1.ValueMemberPath = "Y";


                this.Transparent2.SetBinding(SerialGraph.DataItemsSourceProperty, dataBindingTrans2);
                this.Transparent2.SeriesIDMemberPath = "X";
                this.Transparent2.ValueMemberPath = "Y";

                this.VolatilityChart.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
                this.VolatilityChart.IDMemberPath = "X";

                this.VolatilityGraph.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding);
                this.VolatilityGraph.SeriesIDMemberPath = "X";
                this.VolatilityGraph.ValueMemberPath = "Y";

                this.VolatilityGraph2.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding2);
                this.VolatilityGraph2.SeriesIDMemberPath = "X";
                this.VolatilityGraph2.ValueMemberPath = "Y";


                this.VolatilityChart2.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
                this.VolatilityChart2.IDMemberPath = "X";

                this.VolatilityGraph3.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding3);
                this.VolatilityGraph3.SeriesIDMemberPath = "X";
                this.VolatilityGraph3.ValueMemberPath = "Y";

                this.VolatilityGraph4.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding4);
                this.VolatilityGraph4.SeriesIDMemberPath = "X";
                this.VolatilityGraph4.ValueMemberPath = "Y";

                //设置概率标签
                for (int i = 0; i < yk.probability.Count; i++)
                {
                    Label temp = labels[i];
                    //[style]
                    temp.Content = yk.probability[i].percent+"%";
                    temp.FontSize = 14;

                    if (yk.probability[i].positive)
                        temp.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFCB2525"));
                    else
                        temp.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF25CB5A"));
                    int l = 0, r = 0;
                    if (i==0) 
                        l=yk.LeftEdge;
                    else
                        l=yk.probability[i-1].x;
                    if (i==yk.probability.Count-1)
                        r=yk.RightEdge;
                    else
                        r=yk.probability[i].x;
                    temp.Margin=new Thickness(25-25+1.0*((l+r)/2-yk.LeftEdge)/(yk.RightEdge-yk.LeftEdge)*256,10,0,0);
                    temp.Height = 30;
                    temp.VerticalAlignment = VerticalAlignment.Top;
                    temp.Visibility = Visibility.Visible;

                }
                for (int i = yk.probability.Count; i < 10; i++)
                    labels[i].Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// 绘制盈亏对比图
        /// </summary>
        /// <param name="which"></param>
        /// <param name="count"></param>
        public void BindingForCharts(int which, int count)
        {

            BindingOCCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new BindingOCCallBack(BindingForCharts);
                pwindow.Dispatcher.Invoke(d, new object[] { which, count });
            }
            else
            {

                if (ykm.yk[which, 0] == null)
                {
                    ZoomInYKs.Visibility = Visibility.Hidden;
                    VolatilityChart3.Visibility = Visibility.Hidden;

                    return;

                }
                ZoomInYKs.Visibility = Visibility.Visible;
                VolatilityChart3.Visibility = Visibility.Visible;
                LegendMask.Visibility = Visibility.Hidden;

                BindingCount = count;
                BindingTitles = new string[BindingCount];
                ObservableCollection<XY> data = new ObservableCollection<XY>();
                ObservableCollection<XY> data2 = new ObservableCollection<XY>();

                ObservableCollection<XY> coor = new ObservableCollection<XY>();
                coor = ykm.yk[which, 0].points;
                System.Windows.Data.Binding coorBinding = new System.Windows.Data.Binding();    //X坐标轴绑定
                Bindings[5] = coorBinding;
                coorBinding.Source = coor;
                VolatilityChart3.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
                VolatilityChart3.IDMemberPath = "X";
                VolatilityChart3.Graphs.Clear();
                double max = -1e10;

                for (int i = 0; i < count; i++)
                {
                    if (ykm.yk[which, i] == null) continue;
                    YK yk = ykm.yk[which, i];

                    data = yk.points;

                    System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding();   //数据绑定
                    Bindings[6 + i] = dataBinding;
                    BindingTitles[i] = yk.title;

                    dataBinding.Source = data;

                    LineChartGraph test = new LineChartGraph();

                    test.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding);
                    test.SeriesIDMemberPath = "X";
                    test.ValueMemberPath = "Y";
                    test.Title = yk.title;
                    test.LineThickness = 2;
                    ///[Style]
                    switch (i)
                    {
                        case 0:
                            test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFC160EE"));//紫色
                            break;
                        case 1:
                            test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE0E02B"));//黄色
                            break;
                        case 2:
                            test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF1DB2DE"));//蓝色
                            break;
                        case 3:
                            test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF1CC963"));//青色
                            break;

                    }

                    VolatilityChart3.Graphs.Add(test);

                }
            }

            BindingForTrend();
        }

        public void BindingForTrend()
        {
            if (VolatilityChart4.Visibility == Visibility.Visible) return;
            VolatilityChart4.Visibility = Visibility.Visible;
            ZoomInZS.Visibility = Visibility.Visible;

            string id = YKManager.SubjectID;
            Trend2 tr2 = new Trend2();
            tr2.initial(id,this);

            System.Windows.Data.Binding coorBinding = tr2.CoorBinding;    //X坐标轴绑定
            System.Windows.Data.Binding dataBinding = tr2.DataBinding;    //X坐标轴绑定
            Bindings[10] = coorBinding;
            Bindings[11] = dataBinding;
            Bindings[12] = null;

            VolatilityChart4.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
            VolatilityChart4.IDMemberPath = "X";
            VolatilityChart4.Graphs.Clear();
            LineChartGraph test = new LineChartGraph();

            test.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding);
            test.SeriesIDMemberPath = "X";
            test.ValueMemberPath = "Y";
            //test.Title = yk.title;
            test.LineThickness = 1;
            ///[Style]
            
            test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFC160EE"));//紫色
                    

            VolatilityChart4.Graphs.Add(test);

        }

        private void groupListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            int no = groupListView.Items.IndexOf(e.AddedItems[0]);
            BindingForChart(0, no);
        }

        private void strategyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            int no = strategyListView.Items.IndexOf(e.AddedItems[0]);
            BindingForChart(1, no);
        }

        /// <summary>
        /// 得到用户输入的关于最大幅度的百分数
        /// </summary>
        /// <param name="tb">该TextBox的指针</param>
        /// <returns></returns>
        delegate void RateTextCallBack(TextBox tb);
        private void GetRateText(TextBox tb)
        {

            RateTextCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new RateTextCallBack(GetRateText);
                pwindow.Dispatcher.Invoke(d, new object[] { tb });
            }
            else
            {
                double _max;
                string x = tb.Text;
                if (x == null || x.Equals("")) _max = 0.3;
                else _max = Double.Parse(x.Trim()) / 100;

                MaxRange = _max;
            }
        }


        /// <summary>
        /// 根据yk.ExpectEarn将其插入ans中，取前Tot位
        /// </summary>
        /// <param name="ans"></param>
        /// <param name="yk"></param>
        /// <param name="Tot"></param>
        private void SortYKAnswer(YK[] ans, YK yk, int Tot)
        {
            //无风险套利必须全为盈利
            if (yk.ykgroupname.Equals("无风险套利"))
            {
                if (yk.allPositive == false)
                    return;
            }
            //排序
            for (int k = 0; k < Tot; k++)
                if (ans[k] == null)
                {
                    ans[k] = yk;
                    break;
                }
                else
                    if (yk.ExpectEarn > ans[k].ExpectEarn)
                    {
                        for (int j = Tot - 1; j > k; j--)
                            ans[j] = ans[j - 1];
                        ans[k] = yk;
                        break;
                    }
        }

        //刷新按钮
        public enum StrategyType : int { No, Up, UpSingle, UpBull, Down, DownSingle, DownBear, High, HighSharp, HighWide, Low, LowSharp, LowWide, NoRisk, NoCA, NoMSM, NoBA, NoCA2 };
        public StrategyType nowPresent;
        public StrategyType qjocPresent;
        public StrategyType clocPresent;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ykm.Initial( nameInStrategyPanelComboBox.Text.Trim(), dueDateComboBox.Text.Trim());
            if (ResultTab.SelectedIndex == 0)
                nowPresent = qjocPresent;
            else nowPresent = clocPresent;
            switch (nowPresent)
            {
                case StrategyType.Up:
                    UpRefresh();
                    break;
                case StrategyType.UpSingle:
                    UpSingleRefresh();
                    break;
                case StrategyType.UpBull:
                    UpBullRefresh();
                    break;
                case StrategyType.Down:
                    DownRefresh();
                    break;
                case StrategyType.DownSingle:
                    DownSingleRefresh();
                    break;
                case StrategyType.DownBear:
                    DownBearRefresh();
                    break;
                case StrategyType.High:
                    HighRefresh();
                    break;
                case StrategyType.HighWide:
                    HighWideRefresh();
                    break;
                case StrategyType.HighSharp:
                    HighSharpRefresh();
                    break;
                case StrategyType.Low:
                    LowRefresh();
                    break;
                case StrategyType.LowWide:
                    LowWideRefresh();
                    break;
                case StrategyType.LowSharp:
                    LowSharpRefresh();
                    break;
                case StrategyType.NoRisk:
                    NoRefresh();
                    break;
                case StrategyType.NoCA:
                    NoCA2Refresh();
                    break;
                case StrategyType.NoMSM:
                    NoMSMRefresh();
                    break;
                case StrategyType.NoCA2:
                    NoCA2Refresh();
                    break;
                case StrategyType.NoBA:
                    NoBARefresh();
                    break;

            }
            MessagesControl.showMessage("数据已刷新!");
        }

        //份数改变
        private void lotGNUAD_ValueChanged(object sender, RoutedEventArgs e)
        {
            int selectedLineNo = Convert.ToInt32(((sender as qiquanui.NumericUpAndDownUserControl).Tag).ToString());    //获取行号
            if (qjoc[selectedLineNo].Number <= 0) qjoc[selectedLineNo].Number = 1;
            else 
            RefreshTSPrice(qjoc[selectedLineNo]);
        }

        private void lotSNUAD_ValueChanged(object sender, RoutedEventArgs e)
        {
            int selectedLineNo = Convert.ToInt32(((sender as qiquanui.NumericUpAndDownUserControl).Tag).ToString());    //获取行号
            if (cloc[selectedLineNo].Number <= 0) cloc[selectedLineNo].Number = 1;
            else 
            RefreshTSPrice(cloc[selectedLineNo]);
        }


        private void VolatilityChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }


        //下单
        private void placeOrderGButton_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            for (int i=0;i<qjoc.Count;i++)
                if (qjoc[i].IfChoose)
                {
                    int index = i;
                    List<OrderUnit> l = ykm.yk[0, index].OrderList;
                    foreach (OrderUnit item in l)
                    {
                        bool buy = true;
                        item.Number *= qjoc[index].Number;
                        double price = 0;
                        if (item.Number > 0)
                        {
                            buy = true;
                            price = item.AskPrice;
                        }
                        else
                        {
                            buy = false;
                            item.Number *= -1;
                            price = item.BidPrice;
                        }
                        
                        MainWindow.otm.AddTrading(item.InstrumentID, buy, item.Number, price);
                        if (!changed)
                            MessagesControl.showMessage("已加入交易区!");
                        changed = true;

                    }
                }
            //this.WindowState = WindowState.Minimized;
            if (changed)
            {
                pwindow.WindowState = WindowState.Normal;

            }
            else

                MessagesControl.showMessage("未勾选任何行!");

        }

        private void placeOrderSButton_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            for (int i = 0; i < cloc.Count; i++)
               if (cloc[i].IfChoose)
               {
                   int index = i;
                   List<OrderUnit> l = ykm.yk[1, index].OrderList;
                   foreach (OrderUnit item in l)
                   {
                       bool buy = true;
                       item.Number *= cloc[index].Number;
                       double price = 0;
                       if (item.Number > 0)
                       {
                           buy = true;
                           price = item.AskPrice;
                       }
                       else
                       {
                           buy = false;
                           item.Number *= -1;
                           price = item.BidPrice;
                       }

                       MainWindow.otm.AddTrading(item.InstrumentID, buy, item.Number, price);
                       if (!changed)
                           MessagesControl.showMessage("已加入交易区!");
                       changed = true;


                   }
               }
            //this.WindowState = WindowState.Minimized;
            if (changed)
                pwindow.WindowState = WindowState.Normal;
            else
                MessagesControl.showMessage("未勾选任何行!");
        }


        private void setVAR()
        {
            int which = WHICH;
            ObservableCollection<TopStrategy> oc;
            if (which==0) 
                oc=qjoc;
            else 
                oc=cloc;
            for (int no = 0; no < oc.Count; no++)
            {
                if (ykm.yk[which, no] == null) continue;
                int count = ykm.yk[which, no].OrderList.Count;
                string[] ids = new string[count];
                int[] nums = new int[count];
                for (int i = 0; i < count; i++)
                {
                    ids[i] = ykm.yk[which, no].OrderList[i].InstrumentID;
                    nums[i] = Math.Abs(ykm.yk[which, no].OrderList[i].Number);
                }
                double res = VarCom(count, ids, nums);
    
                oc[no].VaR = "" + res;
                
            }
        }

        //计算VAR
        public double VarCom(int nCount, string[] InstrumentName, int[] Num, double dCov = 0.995)
        //cov是置信度0—1  风险实验室是用户自己输入的  程一豪说一般0.995
        //nConut是组合一共有多少支期权
        //组合每支期权合约数组
        //购买的每支期权多少份
        {
            //MWCellArray InputNum = new MWCellArray(1, nCount);
            //MWCellArray InputID = new MWCellArray(1, nCount);
            //for (int i = 0; i < nCount; i++)
            //{
            //    InputNum[i + 1] = Num[i];
            //    InputID[i + 1] = "'" + InstrumentName[i] + "'";
            //}
            //RiskControl.Class1 output1 = new Class1();
            //MWNumericArray x0 = (MWNumericArray)output1.VaR(InputID, InputNum, nCount, dCov);
            //double ComVar = Math.Round((double)x0, 3);
            //return ComVar;
            return 0;
        }


        Binding[] Bindings = new Binding[13];
        //0~4概率盈亏 5~9盈亏对比
        int BindingCount = 0;
        string[] BindingTitles;
        //盈亏图条数
        public ObservableCollection<StockInfo> BindingStockData { get; set; }
        //走势图


        private void ZoomInGL_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow.CHARTTYPE = 0;
            ChartWindow.Bindings = this.Bindings;
            ChartWindow cw = new ChartWindow(pwindow);
            cw.Show();
        }

        private void ZoomInYK_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow.CHARTTYPE = 1;
            ChartWindow.Bindings = this.Bindings;
            ChartWindow cw = new ChartWindow(pwindow);
            cw.Show();
        }

        private void ZoomInYKs_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow.CHARTTYPE = 2;
            ChartWindow.Bindings = this.Bindings;
            ChartWindow.BindingCount = this.BindingCount;
            ChartWindow.BindingTitles = this.BindingTitles;
            ChartWindow cw = new ChartWindow(pwindow);
            cw.Show();

        }

        private void ZoomInZS_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow.CHARTTYPE = 3;
            //ChartWindow.BindingStockData = this.BindingStockData;
            ChartWindow.Bindings = this.Bindings;
            ChartWindow cw = new ChartWindow(pwindow);
            cw.Show();

        }

        private void chooseAllGCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)chooseAllGCheckBox.IsChecked)
            {
                for (int i = 0; i < qjoc.Count; i++)
                    qjoc[i].IfChoose = true;
            }
            else
            {
                for (int i = 0; i < qjoc.Count; i++)
                    qjoc[i].IfChoose = false;

            }
        }

        private void chooseAllSCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)chooseAllSCheckBox.IsChecked)
            {
                for (int i = 0; i < cloc.Count; i++)
                    cloc[i].IfChoose = true;
            }
            else
            {
                for (int i = 0; i < cloc.Count; i++)
                    cloc[i].IfChoose = false;

            }

        }







    }
}
