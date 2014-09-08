using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace qiquanui
{
    /// <summary>
    /// ArbitrageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ArbitrageWindow : Window
    {
        private double originalHeight, originalWidth, Top1_ArbitrageLabw, Top1_ArbitrageLabwper;
        private double windowShadowControlWidth;//窗口阴影控制宽度，有阴影时为0，无阴影时为7
        MainWindow pwindow;

        public ArbitrageWindow(MainWindow _pWindow)
        {
            InitializeComponent();
            pwindow = _pWindow;

            windowShadowControlWidth = 0;
            originalHeight = this.Height;
            originalWidth = this.Width;
            Top1_ArbitrageLabw = Top1_ArbitrageLab.Width;
        }

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

            Top1_ArbitrageLabwper = (this.Width - (originalWidth - Top1_ArbitrageLabw)) / (originalWidth - (originalWidth - Top1_ArbitrageLabw));

            //groupListViewwper = (this.Width - (originalWidth - groupListVieww)) / (originalWidth - (originalWidth - groupListVieww));

            //groupListViewhper = (this.Height - (originalHeight - groupListViewh)) / (originalHeight - (originalHeight - groupListViewh));

            //groupListView.Width = groupListViewwper * groupListVieww + 2 * windowShadowControlWidth;
            //groupListView.Height = groupListViewhper * groupListViewh + 2 * windowShadowControlWidth;

            Top1_ArbitrageLab.Width = Top1_ArbitrageLabwper * Top1_ArbitrageLabw + 2 * windowShadowControlWidth;

            return true;
        }

        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Top1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaxButton_Click_1(null, null);
        } //双击标题栏最大化、还原

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
            pwindow.WindowState = WindowState.Normal;
            pwindow.strategyAndProfitTabItem.Visibility = Visibility.Hidden;
            this.Close();
        }

        private DoubleAnimation closeAnimation1;
        private bool closeStoryBoardCompleted = false;
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

        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isWindowMax == false)
            {
                this.DragMove();
            }
            else if (isWindowMax == true) ;

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CrossDueOC = new ObservableCollection<CrossArbitrageLine>();
            calendarSpreadListView.DataContext = CrossDueOC;

            stockChart.Charts[0].Collapse();
            stockChart2.Charts[0].Collapse();
            stockChart3.Charts[0].Collapse();

            DateTime present = DataManager.now;
            present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
            stockChart.StartDate = present;
            stockChart2.StartDate = present;
            stockChart3.StartDate = present;

        }//窗口移动

        ObservableCollection<CrossArbitrageLine> CrossDueOC;
        ObservableCollection<CrossArbitrageLine> CrossTypeOC;

        DataTable listOfCrossDue;
        private void ListCrossDue()
        {
            string sql = "select * from staticdata where type='SPD'";
            listOfCrossDue = DataControl.QueryTable(sql);
            for (int i = 0; i < listOfCrossDue.Rows.Count; i++)
            {
                CrossDueOC.Add(new CrossArbitrageLine());
            }
            RefreshCrossDue(null, null);
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(RefreshCrossDue);
            timer.Start();
        }

        private void RefreshCrossDue(object sender,ElapsedEventArgs e)
        {
            for (int i = 0; i < listOfCrossDue.Rows.Count; i++)
            { 
                string id=(string)listOfCrossDue.Rows[i]["InstrumentID"];
                DataRow row = (DataRow) DataManager.All[id];
                CrossArbitrageLine cal = CrossDueOC[i];
                cal.InstrumentID = (string) row["InstrumentID"];
                string id1, id2;
                id1 = cal.InstrumentID.Substring(4, 5);
                id2 = cal.InstrumentID.Substring(10, 5);
                cal.FutureID1 = id1;
                cal.FutureID2 = id2;
                string ch1, ch2;
                DataRow dr1, dr2;
                dr1 = (DataRow)DataManager.All[id1];
                dr2 = (DataRow)DataManager.All[id2];
                ch1 = (string)dr1["InstrumentName"];
                ch2 = (string)dr2["InstrumentName"];
                cal.Chinese = ch1 +"1"+id1.Substring(2,3)+ "/" + ch2+"1"+id2.Substring(2,3);
                cal.Price = ""+(SomeCalculate.caculateMargin(id1,1,true,(double)dr1["LastPrice"])+SomeCalculate.caculateMargin(id2,1,true,(double)dr2["LastPrice"]));
                cal.Difference = "" + ((double)dr1["LastPrice"] - (double)dr2["LastPrice"]);
                cal.DayToDue = DayToDue((string)dr1["LastDate"]) + "天/" + DayToDue((string)dr2["LastDate"]) + "天";
            }
        }


        private int DayToDue(string dueDate)
        {
            DateTime timeNow = DataManager.now;
            DateTime timeDue = new DateTime(Convert.ToInt32(dueDate.Substring(0, 4)), Convert.ToInt32(dueDate.Substring(4, 2)), Convert.ToInt32(dueDate.Substring(6, 2)));
            TimeSpan ts = timeDue - timeNow;
            return ts.Days;
        }

        string FutureID1, FutureID2;
        private void calendarSpreadListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            int no = calendarSpreadListView.Items.IndexOf(e.AddedItems[0]);
            FutureID1 = CrossDueOC[no].FutureID1;
            FutureID2 = CrossDueOC[no].FutureID2;
            Chart1.Title = FutureID1;
            Chart2.Title = FutureID2;
            new Thread(new ThreadStart(TrendInitial)).Start();
        }

        private void TrendInitial()
        {
            trm1 = new Trend();
            trm1.initial(FutureID1);
            TrendInitialCallBack(trm1);
            trm2 = new Trend();
            trm2.initial(FutureID2);
            TrendInitialCallBack2(trm2);
            trm3 = new Trend();
            trm3.initial(trm1.Data, trm2.Data);
            TrendInitialCallBack3(trm3);
        }//初始化走势图（载入历史数据）

        Trend trm1,trm2,trm3;
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
                stockSet1.ItemsSource = tr.Data;
                //stockSet2.ItemsSource = tr.Data;

            }
        }//走势图数据绑定

        private void TrendInitialCallBack2(Trend tr)
        {
            trendInitialCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new trendInitialCallBack(TrendInitialCallBack2);
                this.Dispatcher.Invoke(d, new object[] { tr });
            }
            else
            {
                stockSet2.ItemsSource = tr.Data;
                //stockSet2.ItemsSource = tr.Data;

            }
        }//走势图数据绑定

        private void TrendInitialCallBack3(Trend tr)
        {
            trendInitialCallBack d;
            if (System.Threading.Thread.CurrentThread != this.Dispatcher.Thread)
            {
                d = new trendInitialCallBack(TrendInitialCallBack3);
                this.Dispatcher.Invoke(d, new object[] { tr });
            }
            else
            {
                stockSet3.ItemsSource = tr.Data;
                //stockSet2.ItemsSource = tr.Data;

            }
        }//走势图数据绑定

        private void calendarSpreadBtn_Click(object sender, RoutedEventArgs e)
        {
            ListCrossDue();

        }

        private void placeOrderCSButton_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            for (int i = 0; i < CrossDueOC.Count; i++)
                if (CrossDueOC[i].IfChoose)
                {
                    changed = true;
                    int index = i;
                    string id1=CrossDueOC[i].FutureID1;
                    MainWindow.otm.AddTrading(id1, true, 1, 1000);    //需要改一下

                    string id2 = CrossDueOC[i].FutureID2;
                    MainWindow.otm.AddTrading(id2, true, 1, 1000);    //需要改一下

                }
            //this.WindowState = WindowState.Minimized;
            if (changed)
                pwindow.WindowState = WindowState.Normal;

        }

    }




    public class CrossArbitrageLine : INotifyPropertyChanged
    { 
        public string InstrumentID{get;set;}
        public string Chinese { get; set; }
        public string Price { get; set; }
        public string Difference { get; set; }
        public string DayToDue { get; set; }
        public string FutureID1 { get; set; }
        public string FutureID2 { get; set; }
        public bool IfChoose { get; set; }

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

}
