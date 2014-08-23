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
            else if (isWindowMax == true);

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
        private void openUpPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            //选择情景，遮盖面板浮现
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


            if (Lock)
            {
                Console.WriteLine("Locked while in buyAndUpBtn_Click");
                return;
            }
            else Lock = true;

            qjoc.Clear();
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

            ResultTab.SelectedIndex = which;

            groupListView.SelectedIndex = 0;

            Lock = false;


        }//点击“上涨”按钮


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




        /// <summary>
        /// 计算单支看涨
        /// </summary>
        /// <param name="Tot"></param>
        /// <returns></returns>
        private YK[] ComputeBuyAndUp(int Tot)
        {
            YK[] ans = new YK[Tot];

            double _max = GetRateText(setMaxRateOfGrothTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.CallBuy] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "单买看涨", "上涨", _max);
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

            double _max = GetRateText(setMaxRateOfGrothTBox);

            //看涨牛市
            for (int i=0;i<TotLine-1;i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.CallBuy] = 1;
                    _num[j, (int)OptionType.CallSell] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "看涨组成的牛市", "上涨", _max);
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
                    YK yk = new YK(TotLine, _num, "看跌组成的牛市", "上涨", _max);
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
                    YK yk = new YK(TotLine, _num, "2比1牛市差价", "上涨", _max);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            return ans;
        }

        


        private void buyAndUpBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择单买看涨，遮盖面板浮现
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

            Lock = false;
        }//点击“单买看涨”按钮







        private void bullSpreadBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择牛市差价，遮盖面板浮现
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
            Lock = false;




        }//点击“牛市差价”按钮


        #endregion



        #region 下跌

        private void openDownPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            //选择情景，遮盖面板浮现
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

            Lock = false;


        }//点击“下跌”按钮      

        private void buyAndDownBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择单买看跌，遮盖面板浮现
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
            Lock = false;



        }//点击“单买看跌”按钮

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


            ///熊市差价计算
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
            Lock = false;



        }//点击“熊市差价”按钮

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

            double _max = GetRateText(setMaxRateOfDecreTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.PutBuy] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "单买看跌", "下跌", _max);
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

            double _max = GetRateText(setMaxRateOfDecreTBox);

            //看涨熊市
            for (int i = 0; i < TotLine - 1; i++)
                for (int j = i + 1; j < TotLine; j++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.CallSell] = 1;
                    _num[j, (int)OptionType.CallBuy] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "看涨组成的熊市", "下跌", _max);
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
                    YK yk = new YK(TotLine, _num, "看跌组成的熊市", "下跌", _max);
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
                    YK yk = new YK(TotLine, _num, "2比1熊市差价", "下跌", _max);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            return ans;
        }


        #endregion



        #region 高波动率

        private void openHighVolatilityPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            bottomWideCanvas.BeginAnimation(Canvas.WidthProperty,extend);
            bottomCrossCanvas.BeginAnimation(Canvas.WidthProperty, extend);
            bottomCrossBtn.BeginAnimation(Button.WidthProperty, extend);
            bottomWideBtn.BeginAnimation(Button.WidthProperty, extend);
        }//选择“高波动率”面板

        private void highVolatilityBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择情景，遮盖面板浮现
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

            Lock = false;


        }//点击“高波动率”按钮

        private void bottomWideBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择底部宽跨，遮盖面板浮现
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
            Lock = false;



        }//点击“底部宽跨”按钮

        private void bottomCrossBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择底部跨式，遮盖面板浮现
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
            Lock = false;


        }//点击“底部跨式”按钮


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

            double _max = GetRateText(setMaxRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
                for (int j = 0; j < TotLine; j++)
                    if (i != j)
                    {
                        //填充
                        int[,] _num = new int[TotLine + 1, 4];
                        _num[i, (int)OptionType.CallBuy] = 1;
                        _num[j, (int)OptionType.PutBuy] = 1;
                        //计算盈亏数据
                        YK yk = new YK(TotLine, _num, "底部宽跨", "高波动率", _max);
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

            double _max = GetRateText(setMaxRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
                {
                    //填充
                    int[,] _num = new int[TotLine + 1, 4];
                    _num[i, (int)OptionType.CallBuy] = 1;
                    _num[i, (int)OptionType.PutBuy] = 1;
                    //计算盈亏数据
                    YK yk = new YK(TotLine, _num, "底部跨式", "高波动率", _max);
                    yk.ComputeYK();
                    //排序
                    SortYKAnswer(ans, yk, Tot);
                }

            return ans;
        }




        #endregion
        


        #region 低波动率


        private void openLowVolatilityPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            //选择情景，遮盖面板浮现
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

            Lock = false;


        }//点击“低波动率”按钮

        private void topWideBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择顶部宽跨，遮盖面板浮现
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
            Lock = false;


        }//点击“顶部宽跨”按钮

        private void topCrossBtn_Click(object sender, RoutedEventArgs e)
        {
            //选择顶部跨式，遮盖面板浮现
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
            Lock = false;


        }//点击“顶部跨式”按钮

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

            double _max = GetRateText(setMiniRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
                for (int j = 0; j < TotLine; j++)
                    if (i != j)
                    {
                        //填充
                        int[,] _num = new int[TotLine + 1, 4];
                        _num[i, (int)OptionType.CallSell] = 1;
                        _num[j, (int)OptionType.PutSell] = 1;
                        //计算盈亏数据
                        YK yk = new YK(TotLine, _num, "顶部宽跨", "低波动率", _max);
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

            double _max = GetRateText(setMiniRateOfVolatiTBox);


            for (int i = 0; i < TotLine; i++)
            {
                //填充
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.CallSell] = 1;
                _num[i, (int)OptionType.PutSell] = 1;
                //计算盈亏数据
                YK yk = new YK(TotLine, _num, "顶部跨式", "低波动率", _max);
                yk.ComputeYK();
                //排序
                SortYKAnswer(ans, yk, Tot);
            }

            return ans;
        }


        #endregion




        #region 无风险套利

        private void openRisklessArbiPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            //选择情景，遮盖面板浮现
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
        }//点击“无风险套利”面板

        private void conversionArbitrageEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //选择转换套利，遮盖面板浮现
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
        }//点击“转换套利”按钮

        private void multiSubjectMatterEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //选择“组合标的”，遮盖面板浮现
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
        }//点击“组合标的”按钮

        private void boxArbitrageEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //选择箱型套利，遮盖面板浮现
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
        }//点击“箱型套利”按钮

        private void convexityArbitrageEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //选择凸性套利，遮盖面板浮现
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
        }//点击“凸性套利”按钮


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

            StrategyInitial();
            initialing = false;
        }

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

            TotLine = ykm.Initial(optionsTraderInStrategyPanelComboBox.Text.Trim(), nameInStrategyPanelComboBox.Text.Trim(), dueDateComboBox.Text.Trim());
            cloc = new ObservableCollection<TopStrategy>();
            qjoc = new ObservableCollection<TopStrategy>();
            groupListView.DataContext = qjoc;
            strategyListView.DataContext = cloc;
        }



        private void RefreshTSPrice(TopStrategy ts)
        {
            int number = ts.Number;
            ts.EarnRate = (number * ts.earnRatePer).ToString("f1") + "%";
            ts.ExpectEarn = (number * ts.expectEarnPer).ToString("n0");
            ts.Fee = (number * ts.feePer).ToString("f1");
            ts.Guarantee = (number * ts.guaranteePer).ToString("n0");
            ts.LoseRate = (number * ts.loseRatePer).ToString("f1") + "%";
            ts.MaxEarn = (number * ts.maxEarnPer).ToString("n0");
            ts.MaxLose = (number * ts.maxLosePer).ToString("n0");
            ts.Price = (number * ts.pricePer).ToString("f1");
        }

        /// <summary>
        /// 将ykm中的数据绑定到两个列表
        /// </summary>
        /// <param name="which"></param>
        /// <param name="num"></param>
        private void BindingForOC(int which, int num)
        {
            ObservableCollection<TopStrategy> oc;
            if (which == 0)
                oc = qjoc;
            else
                oc = cloc;
            oc.Clear();
            TopStrategy ts;

            for (int i = 0; i < num; i++)
            {
                ts = new TopStrategy();
                ts.earnRatePer = ykm.yk[which, i].EarnRate;
                ts.expectEarnPer = ykm.yk[which, i].ExpectEarn;
                ts.feePer = 0;//!
                ts.guaranteePer = 0;
                ts.loseRatePer = ykm.yk[which, i].LoseRate;
                ts.maxEarnPer = ykm.yk[which, i].MaxEarn;
                ts.maxLosePer = ykm.yk[which, i].MaxLose;
                ts.Name = ykm.yk[which, i].ykname;
                ts.GroupName = ykm.yk[which, i].ykgroupname;
                ts.Number = 1;
                ts.pricePer = ykm.yk[which, i].Price;
                ts.VaR = "0";
                RefreshTSPrice(ts);

                oc.Add(ts);
            }


        }





        /// <summary>
        /// 计算正态值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="lastprice"></param>
        /// <param name="ykmax"></param>
        /// <returns></returns>
        public static double ComputeZT(int x, double lastprice,double ykmax)
        {
            double u = lastprice, _o = ykmax * 100,o=_o;
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
        public void BindingForChart(int which,int no)
        {
            ObservableCollection<XY> data = new ObservableCollection<XY>();
            ObservableCollection<XY> data2 = new ObservableCollection<XY>();
            ObservableCollection<XY> data3 = new ObservableCollection<XY>();
            ObservableCollection<XY> data4 = new ObservableCollection<XY>();

            ObservableCollection<XY> coor = new ObservableCollection<XY>();
            YK yk = ykm.yk[which, no];
            int left = yk.LeftEdge, right = yk.RightEdge;
            double lastprice = yk.ykfuture[0].LastPrice;


            int now = 0;
            bool positive = yk.probability[0].positive;
            for (int j = left; j <= right; j+= yk.ykstep)
            {
                //概率图
                double tempX = (int)j;
                double tempY = StrategyWindow.ComputeZT((int)j,lastprice,yk.ykmax);
                
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

                //盈亏图
                tempX = (int) j;
                XY point = yk.points[(int)((j - left)/yk.ykstep)];
                if (point.Y == 0)
                {
                    data3.Add(point);
                    data4.Add(point);
                }
                else if (point.Y > 0)
                    data3.Add(point);
                else
                    data4.Add(point);
            }

            System.Windows.Data.Binding coorBinding = new System.Windows.Data.Binding();    //X坐标轴绑定

            System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding();   //数据绑定
            System.Windows.Data.Binding dataBinding2 = new System.Windows.Data.Binding();   //数据绑定
            System.Windows.Data.Binding dataBinding3 = new System.Windows.Data.Binding();   //数据绑定
            System.Windows.Data.Binding dataBinding4 = new System.Windows.Data.Binding();   //数据绑定

            coorBinding.Source = coor;

            dataBinding.Source = data;
            dataBinding2.Source = data2;
            dataBinding3.Source = data3;
            dataBinding4.Source = data4;

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


        }

        /// <summary>
        /// 绘制盈亏对比图
        /// </summary>
        /// <param name="which"></param>
        /// <param name="count"></param>
        public void BindingForCharts(int which, int count)
        {
            LegendMask.Visibility = Visibility.Hidden;
            ObservableCollection<XY> data = new ObservableCollection<XY>();
            ObservableCollection<XY> data2 = new ObservableCollection<XY>();

            ObservableCollection<XY> coor = new ObservableCollection<XY>();
            coor = ykm.yk[which, 0].points;
            System.Windows.Data.Binding coorBinding = new System.Windows.Data.Binding();    //X坐标轴绑定
            coorBinding.Source = coor;
            VolatilityChart3.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
            VolatilityChart3.IDMemberPath = "X";
            VolatilityChart3.Graphs.Clear();

            for (int i = 0; i < count; i++)
            {
                YK yk = ykm.yk[which, i];

                data = yk.points;

                System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding();   //数据绑定

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


        private void groupListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            int no =  groupListView.Items.IndexOf(e.AddedItems[0]);
            BindingForChart(0, no);
        }

        private void strategyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            int no=strategyListView.Items.IndexOf(e.AddedItems[0]);
            BindingForChart(1, no);
        }

        /// <summary>
        /// 得到用户输入的关于最大幅度的百分数
        /// </summary>
        /// <param name="tb">该TextBox的指针</param>
        /// <returns></returns>
        private double GetRateText(TextBox tb)
        {
            double _max;
            string x = tb.Text;
            if (x == null || x.Equals("")) _max = 0.3;
            else _max = Double.Parse(x.Trim()) / 100;
            return _max;
        }


        /// <summary>
        /// 根据yk.ExpectEarn将其插入ans中，取前Tot位
        /// </summary>
        /// <param name="ans"></param>
        /// <param name="yk"></param>
        /// <param name="Tot"></param>
        private void SortYKAnswer(YK[] ans, YK yk, int Tot)
        {
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



    }
}
