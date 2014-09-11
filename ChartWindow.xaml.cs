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
using AmCharts.Windows;

namespace qiquanui
{
    /// <summary>
    /// ChartWindow.xaml 的交互逻辑
    /// </summary>

    public partial class ChartWindow : Window
    {
        private double originalHeight, originalWidth, Top1_StrategyLabw, Top1_StrategyLabwper;

        private double windowShadowControlWidth;//窗口阴影控制宽度，有阴影时为0，无阴影时为7

        public static int CHARTTYPE = 0;
        MainWindow pwindow;
        public ChartWindow(MainWindow _pWindow)
        {
            InitializeComponent();

            pwindow = _pWindow;
            originalHeight = this.Height;
            originalWidth = this.Width;
            this.Left += 200;
            this.Top += 100;

            Top1_StrategyLabw = Top1_StrategyLab.Width;


            windowShadowControlWidth = 0;


        }
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
                this.DragMove();
           

        }//窗口移动


        
       
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

        
        private bool closeStoryBoardCompleted = false;
        private DoubleAnimation closeAnimation1;
        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            //pwindow.WindowState = WindowState.Normal;
            //pwindow.strategyAndProfitTabItem.Visibility = Visibility.Hidden;
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            stockChart.Charts[0].Collapse();



            switch (CHARTTYPE)
            {
                case 0:
                                labels = new Label[10];
            for (int i = 0; i < 10; i++)
            {
                labels[i] = new Label();
                labels[i].Foreground = Brushes.White;
                GLCanvas.Children.Add(labels[i]);
                //this.AddVisualChild(labels[i]);
                labels[i].Visibility = Visibility.Hidden;
                //labels[i]
            }

                    ChartGL();
                    break;
                case 1:
                    ChartYK();
                    break;
                case 2:
                    ChartYKs();
                    break;
                case 3:
                    ChartZS();
                                System.Timers.Timer timer2 = new System.Timers.Timer(3000);
            timer2.Elapsed += new ElapsedEventHandler(RefreshChart);
            timer2.Start();

                    break;
                case 4:
                    ChartZS2();
                    break;
            }

                        //刷新走势图

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


        
        public Label[] labels;

        public static Binding[] Bindings;
        public static int BindingCount;
        public static string[] BindingTitles;
        public static ObservableCollection<StockInfo> BindingStockData { get; set; }
        public static  ObservableCollection<YKProba> Probability;
        public static int LeftEdge,RightEdge;

        private void ChartGL()
        {
            if (Bindings[0] == null || Bindings[1] == null || Bindings[2] == null)
                return;
            //设置概率标签
            for (int i = 0; i < Probability.Count; i++)
            {
                Label temp = labels[i];
                //[style]
                temp.Content = Probability[i].percent + "%";
                temp.FontSize = 18;

                if (Probability[i].positive)
                    temp.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFCB2525"));
                else
                    temp.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF25CB5A"));
                int l = 0, r = 0;
                if (i == 0)
                    l = LeftEdge;
                else
                    l = Probability[i - 1].x;
                if (i == Probability.Count - 1)
                    r = RightEdge;
                else
                    r = Probability[i].x;
                temp.Margin = new Thickness(10 - 50 + 1.0 * ((l + r) / 2 - LeftEdge) / (RightEdge - LeftEdge) * Width, 15, 0, 0);
                temp.Height = 35;
                temp.VerticalAlignment = VerticalAlignment.Top;
                temp.Visibility = Visibility.Visible;

            }
            for (int i = Probability.Count; i < 10; i++)
                labels[i].Visibility = Visibility.Hidden;

            VolatilityChart2.Visibility = Visibility.Hidden;
            VolatilityChart3.Visibility = Visibility.Hidden;
            VolatilityChart4.Visibility = Visibility.Hidden;
            LegendMask.Visibility = Visibility.Hidden;
            ZS2Canvas.Visibility = Visibility.Hidden;

            this.VolatilityChart.SetBinding(SerialChart.SeriesSourceProperty, Bindings[0]);
            this.VolatilityChart.IDMemberPath = "X";

            this.VolatilityGraph.SetBinding(SerialGraph.DataItemsSourceProperty, Bindings[1]);
            this.VolatilityGraph.SeriesIDMemberPath = "X";
            this.VolatilityGraph.ValueMemberPath = "Y";

            this.VolatilityGraph2.SetBinding(SerialGraph.DataItemsSourceProperty, Bindings[2]);
            this.VolatilityGraph2.SeriesIDMemberPath = "X";
            this.VolatilityGraph2.ValueMemberPath = "Y";

        }

        private void ChartYK()
        {
            VolatilityChart.Visibility = Visibility.Hidden;
            LegendMask.Visibility = Visibility.Hidden;
            VolatilityChart3.Visibility = Visibility.Hidden;
            VolatilityChart4.Visibility = Visibility.Hidden;
            ZS2Canvas.Visibility = Visibility.Hidden;

            if (Bindings[0] == null || Bindings[3] == null || Bindings[4] == null)
                return;

            this.VolatilityChart2.SetBinding(SerialChart.SeriesSourceProperty, Bindings[0]);
            this.VolatilityChart2.IDMemberPath = "X";

            this.VolatilityGraph3.SetBinding(SerialGraph.DataItemsSourceProperty, Bindings[3]);
            this.VolatilityGraph3.SeriesIDMemberPath = "X";
            this.VolatilityGraph3.ValueMemberPath = "Y";

            this.VolatilityGraph4.SetBinding(SerialGraph.DataItemsSourceProperty, Bindings[4]);
            this.VolatilityGraph4.SeriesIDMemberPath = "X";
            this.VolatilityGraph4.ValueMemberPath = "Y";

        }

        private void ChartYKs()
        {

            VolatilityChart2.Visibility = Visibility.Hidden;
            VolatilityChart.Visibility = Visibility.Hidden;
            VolatilityChart4.Visibility = Visibility.Hidden;
            ZS2Canvas.Visibility = Visibility.Hidden;
            if (Bindings[5] == null )
                return;
            for (int i=6;i<6+BindingCount;i++)
                if (Bindings[i] == null)
                    return;


                VolatilityChart3.Visibility = Visibility.Visible;

                VolatilityChart3.SetBinding(SerialChart.SeriesSourceProperty, Bindings[5]);
                VolatilityChart3.IDMemberPath = "X";
                VolatilityChart3.Graphs.Clear();
                LegendMask.Visibility = Visibility.Hidden;


            for (int i=0;i<BindingCount;i++)
            {
                    LineChartGraph test = new LineChartGraph();

                    test.SetBinding(SerialGraph.DataItemsSourceProperty, Bindings[6+i]);
                    test.SeriesIDMemberPath = "X";
                    test.ValueMemberPath = "Y";
                    test.Title = BindingTitles[i];
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


        private void ChartZS()
        {
            VolatilityChart2.Visibility = Visibility.Hidden;
            VolatilityChart3.Visibility = Visibility.Hidden;
            VolatilityChart.Visibility = Visibility.Hidden;
            stockChart.Visibility = Visibility.Hidden;
            if (Bindings[10] == null || Bindings[11] == null )
                return;

            System.Windows.Data.Binding coorBinding = Bindings[10];    //X坐标轴绑定
            System.Windows.Data.Binding dataBinding = Bindings[11];    //X坐标轴绑定

            VolatilityChart4.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
            VolatilityChart4.IDMemberPath = "X";
            VolatilityChart4.Graphs.Clear();
            LineChartGraph test = new LineChartGraph();

            test.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding);
            test.SeriesIDMemberPath = "X";
            test.ValueMemberPath = "Y";
            //test.Title = yk.title;
            test.LineThickness = 2;
            ///[Style]

            test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFC160EE"));//紫色


            VolatilityChart4.Graphs.Add(test);
            //stockChart.Charts[0].Collapse();
            //DateTime present = DataManager.now;
            //present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
            //stockChart.StartDate = present;

            //stockSet1.ItemsSource = BindingStockData;

        }


        private void ChartZS2()
        {
            VolatilityChart2.Visibility = Visibility.Hidden;
            VolatilityChart3.Visibility = Visibility.Hidden;
            VolatilityChart.Visibility = Visibility.Hidden;
            VolatilityChart4.Visibility = Visibility.Hidden;

            if (BindingStockData==null)
                return;

            DateTime present = DataManager.now;
            present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
            stockChart.StartDate = present;

            stockSet1.ItemsSource = BindingStockData;

        }


    }
}
