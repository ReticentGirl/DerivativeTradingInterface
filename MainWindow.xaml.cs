﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace qiquanui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {

        private double oldx, oldy, originalHeight, originalWidth, list1h, list1w, grid3w, grid3h, list1hper, list1wper, grid3hper, grid3wper, top1w, top1wper, canvas1h, canvas1hper, multipleTabControlw, multipleTabControlwper, optionsTradingListVieww, optionsTradingListViewwper, optionsHoldDetailListVieww, optionsHoldDetailListViewwper, historyListVieww, historyListViewwper, userManageListVieww, userManageListViewwper, statusBar1w, statusBar1wper, canvas2h, canvas2hper, Grid1w, Grid1h, Grid1wper, Grid1hper, optionsMarketListVieww, optionsMarketListViewwper, optionsMarketListViewh, optionsMarketListViewhper, TopCanvas1h, TopCanvas1w, TopCanvas1wper, TopCanvas1hper, TopCanvasButtomGridw, TopCanvasButtomGridwper, futuresTradingListVieww, futuresTradingListViewwper, optionsMarketTitleGridw, optionsMarketTitleGridwper, titileBorder4w, titileBorder4wper;

        private Storyboard grid1Storyboard, grid1Storyboard_Leave, canvas1Storyboard, canvas1Storyboard_Leave, grid3Storyboard, grid3Storyboard_Leave, canvas2Storyboard_Leave, canvas2Storyboard, optionsMarketTitleGridStoryboard, optionsMarketTitleGridStoryboard_Leave, TopCanvasButtomGridStoryboard_Leave, TopCanvasButtomGridStoryboard, strategyOfOptionsCanvasStoryboard_Leave, strategyOfOptionsCanvasStoryboard, strategyOfFuturesCanvasStoryboard_Leave, strategyOfFuturesCanvasStoryboard;
        public struct option  : INotifyPropertyChanged
        {
            private double bidPrice1;
            private double askPrice1;
            private double lastPrice1;
            private int volume1;
            private int openInterest1;
            private int exercisePrice;
            private int openInterest2;
            private int volume2;
            private double bidPrice2;
            private double askPrice2;
            private double lastPrice2;

            public double BidPrice1
            {
                get { return bidPrice1; }
                set
                {
                    bidPrice1 = value; 
                    //OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
            public double AskPrice1
            {
                get { return askPrice1; }
                set { 
                    askPrice1 = value; 
                    OnPropertyChanged(new PropertyChangedEventArgs("ImageUrl")); 
                }
            }
            public double LastPrice1
            {
                get { return lastPrice1; }
                set
                {
                    lastPrice1 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("lastPrice1"));
                }
            }
            public int Volume1
            {
                get { return volume1; }
                set { volume1 = value; OnPropertyChanged(new PropertyChangedEventArgs("Age")); }
            }
            public int OpenInterest1
            {
                get { return openInterest1; }
                set { openInterest1 = value; OnPropertyChanged(new PropertyChangedEventArgs("Age")); }
            }
            public int ExercisePrice
            {
                get { return exercisePrice; }
                set { exercisePrice = value;
                    //OnPropertyChanged(new PropertyChangedEventArgs("Age")); 
                }
            }

            public double BidPrice2
            {
                get { return bidPrice2; }
                set
                {
                    bidPrice2 = value;
                    //OnPropertyChanged(new PropertyChangedEventArgs("Name"));
                }
            }
            public double AskPrice2
            {
                get { return askPrice2; }
                set { askPrice2 = value; OnPropertyChanged(new PropertyChangedEventArgs("ImageUrl")); }
            }
            public double LastPrice2
            {
                get { return lastPrice2; }
                set
                {
                    lastPrice2 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("LastPrice2"));
                }
            }
            public int Volume2
            {
                get { return volume2; }
                set { volume2 = value; 
                    //OnPropertyChanged(new PropertyChangedEventArgs("Age"));
                }
            }
            public int OpenInterest2
            {
                get { return openInterest2; }
                set { openInterest2 = value; 
                    //OnPropertyChanged(new PropertyChangedEventArgs("Age")); 
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


        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            oldx = e.GetPosition(this).X;
            oldy = e.GetPosition(this).Y;
        }

        private void Top1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

                if (e.LeftButton == MouseButtonState.Pressed)
                {

                    double x = e.GetPosition(this).X;
                    double y = e.GetPosition(this).Y;
                    this.Left += (x - oldx);
                    this.Top += (y - oldy);
                }
           
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
        public ObservableCollection<option> ObservableObj = new ObservableCollection<option>();
        //static MktData mkt = null;

        public MainWindow()
        {

            //mkt = new MktData();
            //mkt.Run();

            InitializeComponent();
            ObservableObj.Clear();
            ObservableObj.Add(new option()
            {
                BidPrice1 = 3243,
                AskPrice1 = 324,
                LastPrice1 = 324,
                Volume1 = 234,
                OpenInterest1 = 324,
                ExercisePrice = 2250,
                BidPrice2 = 3243,
                AskPrice2 = 324,
                LastPrice2 = 324,
                Volume2 = 234,
                OpenInterest2 = 324,
            });
            ObservableObj.Add(new option()
            {
                BidPrice1 = 3243,
                AskPrice1 = 324,
                LastPrice1 = 324,
                Volume1 = 234,
                OpenInterest1 = 324,
                ExercisePrice = 2250,
                BidPrice2 = 3243,
                AskPrice2 = 324,
                LastPrice2 = 324,
                Volume2 = 234,
                OpenInterest2 = 324,
            });
            ObservableObj.Add(new option()
            {
                BidPrice1 = 3243,
                AskPrice1 = 324,
                LastPrice1 = 324,
                Volume1 = 234,
                OpenInterest1 = 324,
                ExercisePrice = 2250,
                BidPrice2 = 3243,
                AskPrice2 = 324,
                LastPrice2 = 324,
                Volume2 = 234,
                OpenInterest2 = 324,
            });
            ObservableObj.Add(new option()
            {
                BidPrice1 = 3243,
                AskPrice1 = 324,
                LastPrice1 = 324,
                Volume1 = 234,
                OpenInterest1 = 324,
                ExercisePrice = 2250,
                BidPrice2 = 3243,
                AskPrice2 = 324,
                LastPrice2 = 324,
                Volume2 = 234,
                OpenInterest2 = 324,
            });
            ObservableObj.Add(new option()
            {
                BidPrice1 = 3243,
                AskPrice1 = 324,
                LastPrice1 = 324,
                Volume1 = 234,
                OpenInterest1 = 324,
                ExercisePrice = 2250,
                BidPrice2 = 3243,
                AskPrice2 = 324,
                LastPrice2 = 324,
                Volume2 = 234,
                OpenInterest2 = 324,
            });
            optionsMarketListView.DataContext = ObservableObj;



            this.Left = 50;
            this.Top = 20;
            originalHeight = this.Height;
            originalWidth = this.Width;
            list1h = futuresMarketListView.Height;
            list1w = futuresMarketListView.Width;

            grid3h = Grid3.Height;
            grid3w = Grid3.Width;
            top1w = Top1.Width;
            canvas1h = Canvas1.Height;
            canvas2h = Canvas2.Height;
            optionsTradingListVieww = optionsTradingListView.Width;
            multipleTabControlw = multipleTabControl.Width;
            optionsHoldDetailListVieww = holdDetailListView.Width;
            historyListVieww = historyListView.Width;
            userManageListVieww = userManageListView.Width;
            statusBar1w = statusBar1.Width;
            optionsMarketListVieww = optionsMarketListView.Width;
            optionsMarketListViewh = optionsMarketListView.Height;
            TopCanvas1w = TopCanvas1.Width;
            TopCanvas1h=TopCanvas1.Height;
            TopCanvasButtomGridw = TopCanvasButtomGrid.Width;
            futuresTradingListVieww=futuresTradingListView.Width;
            optionsMarketTitleGridw = optionsMarketTitleGrid.Width;
            titileBorder4w = titileBorder4.Width;


            Grid1w = Grid1.Width;
            Grid1h = Grid1.Height;

            grid1Storyboard = (Storyboard)this.FindResource("grid1Animate");
            grid1Storyboard_Leave = (Storyboard)this.FindResource("grid1Animate_Leave");
            canvas1Storyboard = (Storyboard)this.FindResource("canvas1Animate");
            canvas1Storyboard_Leave = (Storyboard)this.FindResource("canvas1Animate_Leave");
  
            grid3Storyboard = (Storyboard)this.FindResource("Grid3Animate");
            grid3Storyboard_Leave = (Storyboard)this.FindResource("Grid3Animate_Leave");

            canvas2Storyboard = (Storyboard)this.FindResource("canvas2Animate");
            canvas2Storyboard_Leave = (Storyboard)this.FindResource("canvas2Animate_Leave");

            optionsMarketTitleGridStoryboard=(Storyboard)this.FindResource("optionsMarketTitleGridAnimate");
            optionsMarketTitleGridStoryboard_Leave = (Storyboard)this.FindResource("optionsMarketTitleGridAnimate_Leave");

            TopCanvasButtomGridStoryboard = (Storyboard)this.FindResource("TopCanvasButtomGridAnimate");
            TopCanvasButtomGridStoryboard_Leave = (Storyboard)this.FindResource("TopCanvasButtomGridAnimate_Leave");

            strategyOfOptionsCanvasStoryboard = (Storyboard)this.FindResource("strategyOfOptionsCanvasAnimate");
            strategyOfOptionsCanvasStoryboard_Leave = (Storyboard)this.FindResource("strategyOfOptionsCanvasAnimate_Leave");

            strategyOfFuturesCanvasStoryboard = (Storyboard)this.FindResource("strategyOfFuturesCanvasAnimate");
            strategyOfFuturesCanvasStoryboard_Leave = (Storyboard)this.FindResource("strategyOfFuturesCanvasAnimate_Leave");

            typeComboBox.SelectedIndex = 0;
            typeInStrategyPanelComboBox.SelectedIndex = 1;

            
           
            

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
                Border1.Visibility = Visibility.Collapsed;
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

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool ResizeControl()
        {
            optionsMarketTitleGridwper = TopCanvasButtomGridwper = TopCanvas1wper = Grid1wper = list1wper = optionsMarketListViewwper = (this.Width - (originalWidth - list1w)) / (originalWidth - (originalWidth - list1w));
            futuresTradingListViewwper = userManageListViewwper = historyListViewwper = optionsHoldDetailListViewwper = optionsTradingListViewwper = multipleTabControlwper = grid3wper = top1wper = (this.Width - (originalWidth - grid3w)) / (originalWidth - (originalWidth - grid3w));
            list1hper = (this.Height - (originalHeight - list1h)) / (originalHeight - (originalHeight - list1h));
            optionsMarketListViewhper = (this.Height - (originalHeight - optionsMarketListViewh)) / (originalHeight - (originalHeight - optionsMarketListViewh));
            TopCanvas1hper=Grid1hper = grid3hper =  (this.Height - (originalHeight - Grid1h)) / (originalHeight - (originalHeight - Grid1h));
            titileBorder4wper = (this.Width - (originalWidth - titileBorder4w)) / (originalWidth - (originalWidth - titileBorder4w));
            
            statusBar1wper = this.Width / originalWidth;
            canvas2hper=canvas1hper = (this.Height-(originalHeight-canvas1h)) / (originalHeight-(originalHeight-canvas1h));
            Border1.Height = this.Height - 14.0;
            Border1.Width = this.Width - 14.0;
            Grid1.Width=futuresMarketListView.Width = list1w * list1wper;
            futuresMarketListView.Height = list1h * list1hper;
            Grid1.Height = Grid1h * Grid1hper;
            historyListView.Width = historyListVieww * historyListViewwper;
            userManageListView.Width = userManageListVieww * userManageListViewwper;


            Grid3.Width = grid3w * grid3wper;
            Top1.Width = top1w * top1wper;
            Canvas1.Height = canvas1h * canvas1hper;
            Canvas2.Height = canvas2h * canvas2hper;
            optionsTradingListView.Width = optionsTradingListVieww * optionsTradingListViewwper;
            futuresTradingListView.Width=futuresTradingListVieww*futuresTradingListViewwper;
            multipleTabControl.Width = multipleTabControlw * multipleTabControlwper;
            holdDetailListView.Width = optionsHoldDetailListVieww * optionsHoldDetailListViewwper;
            statusBar1.Width = statusBar1w * statusBar1wper;
            optionsMarketListView.Width = optionsMarketListVieww * optionsMarketListViewwper;
            optionsMarketListView.Height = optionsMarketListViewh * optionsMarketListViewhper;
            TopCanvas1.Width = TopCanvas1w * TopCanvas1wper;
            TopCanvas1.Height = TopCanvas1h * TopCanvas1hper;
            TopCanvasButtomGrid.Width = TopCanvasButtomGridw * TopCanvasButtomGridwper;
            optionsMarketTitleGrid.Width = optionsMarketTitleGridwper * optionsMarketTitleGridw;
            titileBorder4.Width = titileBorder4wper * titileBorder4w;
            return true;
        }
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ResizeControl();
        }

        private void Canvas1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Canvas1.Width == 29.0)
            {
                DoubleAnimation animate = new DoubleAnimation();
                animate.To = 292;
                animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate.DecelerationRatio = 1;
                //   animate.AccelerationRatio = 0.33;
                Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
                canvas1Storyboard.Begin(this);
            }
        }

        private void Canvas1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation animate = new DoubleAnimation();
            animate.To = 29.0;
            animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            //   animate.DecelerationRatio = 0.33;
            animate.AccelerationRatio = 1;
            Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
            canvas1Storyboard_Leave.Begin(this);
        }
        private void Canvas2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Canvas2.Width == 29.0)
            {
                DoubleAnimation animate1 = new DoubleAnimation();
                animate1.To = 120;
                animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                animate1.DecelerationRatio = 1;
                //   animate.AccelerationRatio = 0.33;
                Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
                canvas2Storyboard.Begin(this);
            }
        }

        private void Canvas2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            DoubleAnimation animate1 = new DoubleAnimation();
            animate1.To = 29.0;
            animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            //   animate.DecelerationRatio = 0.33;
            animate1.AccelerationRatio = 1;
            Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
            canvas2Storyboard_Leave.Begin(this);
        }

        private void Grid1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            grid1Storyboard.Begin(this);
        }

        private void Grid1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            grid1Storyboard_Leave.Begin(this);
        }

        private void Grid3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
         {
             grid3Storyboard.Begin(this);
         }

        private void Grid3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
         {
             grid3Storyboard_Leave.Begin(this);
         }
        private void optionsMarketTitleGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            optionsMarketTitleGridStoryboard.Begin(this);

        }

        private void optionsMarketTitleGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            optionsMarketTitleGridStoryboard_Leave.Begin(this);
        }
        private void TopCanvasButtomGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TopCanvasButtomGridStoryboard.Begin(this);
        }

        private void TopCanvasButtomGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TopCanvasButtomGridStoryboard_Leave.Begin(this);

        }
        private void strategyOfOptionsCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            strategyOfOptionsCanvasStoryboard.Begin(this);
        }

        private void strategyOfOptionsCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            strategyOfOptionsCanvasStoryboard_Leave.Begin(this);
        }

        private void strategyOfFuturesCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            strategyOfFuturesCanvasStoryboard.Begin(this);
        }

        private void strategyOfFuturesCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            strategyOfFuturesCanvasStoryboard_Leave.Begin(this);
        }

         private void optionsTradingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {

         }
         private bool closeStoryBoardCompleted = false;
         private DoubleAnimation closeAnimation1;
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

         private void futuresComboBoxItem_Selected(object sender, RoutedEventArgs e)
         {
            
                 optionsMarketListView.Visibility = Visibility.Hidden;
                 optionsTraderComboBox.Visibility = Visibility.Hidden;
                 optionsMarketTitleGrid.Visibility = Visibility.Hidden;
                 optionsTradingListView.Visibility = Visibility.Hidden;
                 nameLabel.Visibility = Visibility.Hidden;
                 nameComboBox.Visibility = Visibility.Hidden;
                 futuresTradingListView.Visibility = Visibility.Visible;
                 futuresMarketListView.Visibility = Visibility.Visible;
                 futuresTraderComboBox.Visibility = Visibility.Visible;
                 
                 futuresOfOptionGrid.Visibility = Visibility.Hidden;
                 titileBorder4.Visibility = Visibility.Hidden;
         }

         private void optionsComboBoxItem_Selected(object sender, RoutedEventArgs e)
         {
             
             futuresMarketListView.Visibility = Visibility.Hidden;
             futuresTraderComboBox.Visibility = Visibility.Hidden;
             futuresTradingListView.Visibility = Visibility.Hidden;
             optionsMarketListView.Visibility = Visibility.Visible;
             optionsTraderComboBox.Visibility = Visibility.Visible;
             optionsMarketTitleGrid.Visibility = Visibility.Visible;
             optionsTradingListView.Visibility = Visibility.Visible;
             nameLabel.Visibility = Visibility.Visible;
             nameComboBox.Visibility = Visibility.Visible;
             futuresOfOptionGrid.Visibility = Visibility.Visible;
             titileBorder4.Visibility = Visibility.Visible;
         }

         private void predictComboBoxItem_Selected(object sender, RoutedEventArgs e)
         {
             amountOfUpAndDownLabel.Visibility = Visibility.Visible;
             MaxAmountOfUpAndDownTextBox.Visibility = Visibility.Visible;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
         }

         private void predictComboBoxItem2_Selected(object sender, RoutedEventArgs e)
         {
             amountOfUpAndDownLabel.Visibility = Visibility.Visible;
             MaxAmountOfUpAndDownTextBox.Visibility = Visibility.Visible;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
         }

         private void predictComboBoxItem3_Selected(object sender, RoutedEventArgs e)
         {
             amountOfUpAndDownLabel.Visibility = Visibility.Hidden;
             amountOfUpAndDownTextBox.Visibility = Visibility.Hidden;
             volatilityLabel.Visibility = Visibility.Visible;
             volatilityComboBox.Visibility = Visibility.Visible;
         }

         private void predictComboBoxItem4_Selected(object sender, RoutedEventArgs e)
         {
             amountOfUpAndDownLabel.Visibility = Visibility.Hidden;
             amountOfUpAndDownTextBox.Visibility = Visibility.Hidden;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
         }

        private void optionsMarketListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
         {

         }

        private void futuresInStrategyPanelComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            strategyOfOptionsCanvas.Visibility = Visibility.Hidden;
            strategyOfFuturesCanvas.Visibility = Visibility.Visible;
        }

        private void optionsInStrategyPanelComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            strategyOfOptionsCanvas.Visibility = Visibility.Visible;
            strategyOfFuturesCanvas.Visibility = Visibility.Hidden;
        }

        private void startStrategyBtn_Click(object sender, RoutedEventArgs e)
        {
            StrategyWindow strategyWindow = new StrategyWindow();
            strategyWindow.Show();
           
        }

       

       
      

 
       


         
    }
}
