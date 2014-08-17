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

        private void openUpPoly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void upBtn_Click(object sender, RoutedEventArgs e)
        {

        }

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

        private void binding(int which, int num)
        {
            ObservableCollection<TopStrategy> oc;
            if (which == 0)
                oc = qjoc;
            else
                oc = cloc;
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


        private YK[] ComputeBuyAndUp(int Tot)
        {
            YK[] ans = new YK[Tot];

            double _max;
            string x = setMaxRateOfGrothTBox.Text;
            if (x == null) _max = 0.3;
            _max = Double.Parse(x.Trim()) / 100;

            for (int i = 0; i < TotLine; i++)
            {
                int[,] _num = new int[TotLine + 1, 4];
                _num[i, (int)OptionType.CallBuy] = 1;
                YK yk = new YK(TotLine, _num, "单买看涨", "上涨", _max);
                yk.ComputeYK();


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

            return ans;
        }


        private void buyAndUpBtn_Click(object sender, RoutedEventArgs e)
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

            binding(1, no);
            BindingForCharts(1, no);

            ResultTab.SelectedIndex = 1;

            strategyListView.SelectedIndex = 0;
            Lock = false;
        }

        public static double ComputeZT(int x, double lastprice,double ykmax)
        {
            double u = lastprice, _o = ykmax * 100,o=_o;
            if (u >= 1000) o = 3 * _o;
            if (u >= 5000) o = 5 * _o;
            if (u >= 10000) o = 7 * _o;
            if (u >= 30000) o = 10 * _o;
            return 1.0 / (Math.Sqrt(2 * Math.PI) * o) * Math.Exp(-Math.Pow((x - u), 2) / (2 * (Math.Pow(o, 2))));
        }

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
           
        }

        private void strategyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            int no=strategyListView.Items.IndexOf(e.AddedItems[0]);
            BindingForChart(1, no);
        }




    }
}
