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
using AmCharts.Windows;
using AmCharts.Windows.Core;
using RiskControl;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using AmCharts.Windows.Core;
using AmCharts.Windows.Line;


namespace qiquanui
{
    /// <summary>
    /// RiskWindow.xaml 的交互逻辑
    /// </summary>
    /// 

    //public class XY : INotifyPropertyChanged
    //{
    //    private double x;
    //    private double y;

    //    // public double x { get; set; }
    //    // public double y { get; set; }

    //    public double X
    //    {
    //        get { return x; }
    //        set
    //        {
    //            x = value;
    //            OnPropertyChanged("X");
    //        }
    //    }

    //    public double Y
    //    {
    //        get { return y; }
    //        set
    //        {
    //            y = value;
    //            OnPropertyChanged("Y");
    //        }
    //    }



    //    #region INotifyPropertyChanged 成员

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    void OnPropertyChanged(string name)
    //    {
    //        if (PropertyChanged != null)
    //            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
    //    }




    //    #endregion
    //}


    public partial class RiskWindow : Window
    {
        private double originalHeight, originalWidth, Grid1w, Grid1h, Grid1wper, Grid1hper, Grid2w, Grid2h, Grid2wper, Grid2hper, Grid1Label1h, Grid1Label1hper, Grid2Label1h, Grid2Label1hper, DeltaTabGridh, DeltaTabGridhper, GammaTabGridh, GammaTabGridhper, optionsRiskLVw, optionsRiskLVwper, optionsRiskLVh, optionsRiskLVhper, recomLVw, recomLVwper, recomLVh, recomLVhper, DeltaAndGammaTabControlh, DeltaAndGammaTabControlhper;
        private Storyboard Grid1Storyboard, Grid1Storyboard_Leave, Grid2Storyboard, Grid2Storyboard_Leave, Grid1Label1Storyboard, Grid1Label1Storyboard_Leave, Grid2Label1Storyboard, Grid2Label1Storyboard_Leave, compoGridStoryboard, compoGridStoryboard_Leave, DeltaTabGridStoryboard, DeltaTabGridStoryboard_Leave, GammaTabGridStoryboard, GammaTabGridStoryboard_Leave;
        private double windowShadowControlWidth;//窗口阴影控制宽度，有阴影时为0，无阴影时为7
        MainWindow pWindow;
        RiskLabManager pRm;
        string teat, teat1;
        public RiskWindow(MainWindow _pWindow)
        {
            InitializeComponent();

            pWindow = _pWindow;

            originalHeight = this.Height;
            originalWidth = this.Width;


            Grid1w = Grid1.Width;
            Grid1h = Grid1.Height;
            Grid2w = Grid2.Width;
            Grid2h = Grid2.Height;
            Grid1Label1h = Grid1Label1.Height;
            Grid2Label1h = Grid2Label1.Height;
            DeltaTabGridh = DeltaTabGrid.Height;
            GammaTabGridh = GammaTabGrid.Height;
            optionsRiskLVw = optionsRiskLV.Width;
            optionsRiskLVh = optionsRiskLV.Height;
            recomLVw = recomLV.Width;
            recomLVh = recomLV.Height;
            DeltaAndGammaTabControlh = DeltaAndGammaTabControl.Height;

            windowShadowControlWidth = 0;

            Grid1Storyboard = (Storyboard)this.FindResource("Grid1Animate");
            Grid1Storyboard_Leave = (Storyboard)this.FindResource("Grid1Animate_Leave");
            Grid2Storyboard = (Storyboard)this.FindResource("Grid2Animate");
            Grid2Storyboard_Leave = (Storyboard)this.FindResource("Grid2Animate_Leave");
            Grid1Label1Storyboard = (Storyboard)this.FindResource("Grid1Label1Animate");
            Grid1Label1Storyboard_Leave = (Storyboard)this.FindResource("Grid1Label1Animate_Leave");
            Grid2Label1Storyboard = (Storyboard)this.FindResource("Grid2Label1Animate");
            Grid2Label1Storyboard_Leave = (Storyboard)this.FindResource("Grid2Label1Animate_Leave");
            compoGridStoryboard = (Storyboard)this.FindResource("compoGridAnimate");
            compoGridStoryboard_Leave = (Storyboard)this.FindResource("compoGridAnimate_Leave");
            DeltaTabGridStoryboard = (Storyboard)this.FindResource("DeltaTabGridAnimate");
            DeltaTabGridStoryboard_Leave = (Storyboard)this.FindResource("DeltaTabGridAnimate_Leave");
            GammaTabGridStoryboard = (Storyboard)this.FindResource("GammaTabGridAnimate");
            GammaTabGridStoryboard_Leave = (Storyboard)this.FindResource("GammaTabGridAnimate_Leave");


            Border1.Width = this.Width - 14;
            Border1.Height = this.Height - 14;


            testForChart();


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
            pWindow.WindowState = WindowState.Normal;
            pWindow.CloseLeftCanvas();
            pWindow.CloseRightCanvas();
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
        private void ResizeControl()
        {

            Top1_StrategyLab.Width = this.Width;
            Border1.Height = this.Height - 14.0;
            Border1.Width = this.Width - 14.0;

            Grid1wper = (this.Width - (originalWidth - Grid1w)) / (originalWidth - (originalWidth - Grid1w));
            Grid2wper = (this.Width - (originalWidth - Grid2w)) / (originalWidth - (originalWidth - Grid2w));
            Grid2Label1hper = Grid1Label1hper = Grid2hper = Grid1hper = (this.Height - (originalHeight - Grid1h - Grid2h)) / (originalHeight - (originalHeight - Grid1h - Grid2h));
            GammaTabGridhper = DeltaTabGridhper = (this.Height - (originalHeight - DeltaTabGridh)) / (originalHeight - (originalHeight - DeltaTabGridh));
            optionsRiskLVwper = (this.Width - (originalWidth - optionsRiskLVw)) / (originalWidth - (originalWidth - optionsRiskLVw));
            recomLVwper = (this.Width - (originalWidth - recomLVw)) / (originalWidth - (originalWidth - recomLVw));
            optionsRiskLVhper = (this.Height - (originalHeight - optionsRiskLVh - Grid2h)) / (originalHeight - (originalHeight - optionsRiskLVh - Grid2h));
            recomLVhper = (this.Height - (originalHeight - recomLVh - Grid1h)) / (originalHeight - (originalHeight - recomLVh - Grid1h));
            DeltaAndGammaTabControlhper = (this.Height - (originalHeight - DeltaAndGammaTabControlh)) / (originalHeight - (originalHeight - DeltaAndGammaTabControlh));


            Grid1.Width = Grid1w * Grid1wper + 2 * windowShadowControlWidth;
            Grid1.Height = Grid1h * Grid1hper + 2 * windowShadowControlWidth;
            Grid2.Width = Grid2w * Grid2wper + 2 * windowShadowControlWidth;
            Grid2.Height = Grid2h * Grid2hper + 2 * windowShadowControlWidth;
            Grid1Label1.Height = Grid1Label1h * Grid1Label1hper + 2 * windowShadowControlWidth;
            Grid2Label1.Height = Grid2Label1h * Grid2Label1hper + 2 * windowShadowControlWidth;
            DeltaTabGrid.Height = DeltaTabGridh * DeltaTabGridhper + 2 * windowShadowControlWidth;
            GammaTabGrid.Height = GammaTabGridh * GammaTabGridhper + 2 * windowShadowControlWidth;
            optionsRiskLV.Width = optionsRiskLVw * optionsRiskLVwper + 2 * windowShadowControlWidth;
            optionsRiskLV.Height = optionsRiskLVh * optionsRiskLVhper + 2 * windowShadowControlWidth;
            recomLV.Width = recomLVw * recomLVwper + 2 * windowShadowControlWidth;
            recomLV.Height = recomLVh * recomLVhper + 2 * windowShadowControlWidth;
            DeltaAndGammaTabControl.Height = DeltaAndGammaTabControlh * DeltaAndGammaTabControlhper + 2 * windowShadowControlWidth;

        }//拉伸窗口调用ResizeControl()
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
           // Grid1Storyboard.Begin(this);
        }

        private void Grid1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // Grid1Storyboard_Leave.Begin(this);
        }

        private void Grid2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  Grid2Storyboard.Begin(this);
        }

        private void Grid2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {//
          //  Grid2Storyboard_Leave.Begin(this);
        }

        private void Grid1Label1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
         //   Grid1Label1Storyboard.Begin(this);
        }

        private void Grid1Label1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
          //  Grid1Label1Storyboard_Leave.Begin(this);
        }

        private void Grid2Label1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // Grid2Label1Storyboard.Begin(this);
        }

        private void Grid2Label1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // Grid2Label1Storyboard_Leave.Begin(this);
        }

        private void compoGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // compoGridStoryboard.Begin(this);
        }

        private void compoGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           /// compoGridStoryboard_Leave.Begin(this);
        }

        private void GammaTabGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // GammaTabGridStoryboard.Begin(this);
        }

        private void GammaTabGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // GammaTabGridStoryboard_Leave.Begin(this);
        }

        private void DeltaTabGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // DeltaTabGridStoryboard.Begin(this);
        }

        private void DeltaTabGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //DeltaTabGridStoryboard_Leave.Begin(this);
        }


        public void getRM(RiskLabManager r)
        {
            pRm = r;
        }


        private void okCompoBtn_Click(object sender, RoutedEventArgs e)//已选择组合的置信水平
        {
            teat = setConfidenceLevelCompoTBox.Text;
            double Cov = Convert.ToDouble(teat);
            pRm.GetData(Cov);
            pRm.Com(Cov);
        }


        private void okRecomBtn_Click(object sender, RoutedEventArgs e)
        {
            teat1 = setConfidenceLevelRecomTBox.Text;
            double Cov = Convert.ToDouble(teat);
            pRm.showData(Cov);

        }

        public void testForChart()
        {
            //System.Windows.Data.Binding coorBinding = new System.Windows.Data.Binding();    //X坐标轴绑定

            //System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding();   //数据绑定


            //coorBinding.Source = coor;

            //dataBinding.Source = data;

            //this.VolatilityChart.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
            //this.VolatilityChart.IDMemberPath = "X";

            //this.VolatilityGraph.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding);
            //this.VolatilityGraph.SeriesIDMemberPath = "X";
            //this.VolatilityGraph.ValueMemberPath = "Y";
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private void optionsRiskLV_SelectionChanged(object sender, SelectionChangedEventArgs e)//画图
        {
            RiskLabData selectedItem = optionsRiskLV.SelectedItem as RiskLabData;
            RiskControl.Class1 output1 = new Class1();
            MWArray[] best=output1.PicVolatility(4,"'"+selectedItem.InstrumentID+"'");




            MWCellArray x11 = (MWCellArray)best[0];//横坐标
            ObservableCollection<XY> coor = new ObservableCollection<XY>();

            for (int j = 0; j < x11.NumberOfElements; j++) {
                double[,] tempX1 = (double[,])x11[1, j + 1].ToArray();
                double tempX = Math.Round(tempX1[0, 0], 2);
                coor.Add(new XY() { X = tempX });
            }
            

            MWCellArray x22 = (MWCellArray)best[1]; //时间
           

            MWArray x33 = (MWArray)best[2];
            MWArray x44 = (MWArray)best[3];

            Random rnd = new Random();

            System.Windows.Data.Binding coorBinding = new System.Windows.Data.Binding();    //X坐标轴绑定

            VolatilityChart.Graphs.Clear();


            gougou.Visibility = Visibility.Hidden;

            for (int i = 0; i < x22.NumberOfElements; i++)
            {
                string datetime=x22[1+i].ToString();
                ObservableCollection<XY> data = new ObservableCollection<XY>();
                for (int j = 0; j <(x33.NumberOfElements/x22.NumberOfElements); j++)
                {
                    double[,] tempX1 = (double[,])x33[i+1, j+1].ToArray();
                    double[,] tempY1 = (double[,])x44[i+1, j+1].ToArray();
                    bool b1 = double.IsNaN(tempX1[0,0]);
                    bool b2=double.IsNaN(tempY1[0,0]);
                    if (!b1&&!b2&&(tempX1[0,0]!=0)&&(tempY1[0,0]!=0))
                    {
                        double tempX = Math.Round(tempX1[0, 0], 2);
                        double tempY = Math.Round(tempY1[0, 0], 2);
                        data.Add(new XY() { X = tempX, Y = tempY });
                    }
                    
                }

                System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding();   //数据绑定

                LineChartGraph test = new LineChartGraph();

                dataBinding.Source = data;

                test.SetBinding(SerialGraph.DataItemsSourceProperty, dataBinding);
                test.SeriesIDMemberPath = "X";
                test.ValueMemberPath = "Y";
                test.Title = datetime;


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
                    //case 4:
                       
                    //    test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString());//蓝色
                    //    break;
                    //case 5:
                    //    test.Brush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF1CC963"));//青色
                    //    break;

                }


                VolatilityChart.Graphs.Add(test);
            }


            coorBinding.Source = coor;
            this.VolatilityChart.SetBinding(SerialChart.SeriesSourceProperty, coorBinding);
            this.VolatilityChart.IDMemberPath = "X";
     

        }

    }
}
