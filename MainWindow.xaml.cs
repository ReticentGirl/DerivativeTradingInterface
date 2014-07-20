using System;
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

namespace qiquanui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {

        private double oldx, oldy, originalHeight, originalWidth, list1h, list1w, grid3w, grid3h, list1hper, list1wper, grid3hper, grid3wper, top1w, top1wper, canvas1h, canvas1hper, multipleTabControlw, multipleTabControlwper, optionsTradingListVieww, optionsTradingListViewwper, optionsHoldDetailListVieww, optionsHoldDetailListViewwper, historyListVieww, historyListViewwper, userManageListVieww, userManageListViewwper, statusBar1w, statusBar1wper, canvas2h, canvas2hper, Grid1w, Grid1h, Grid1wper, Grid1hper, optionsMarketListVieww, optionsMarketListViewwper, optionsMarketListViewh, optionsMarketListViewhper, TopCanvas1h, TopCanvas1w, TopCanvas1wper, TopCanvas1hper, TopCanvasButtomGridw, TopCanvasButtomGridwper;

        private Storyboard grid1Storyboard, grid1Storyboard_Leave, canvas1Storyboard, canvas1Storyboard_Leave, grid3Storyboard, grid3Storyboard_Leave, canvas2Storyboard_Leave, canvas2Storyboard;
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

        static MktData mkt = null;
        public MainWindow()
        {

            //mkt = new MktData();
            //mkt.Run();

            InitializeComponent();

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
            typeComboBox.SelectedIndex = 0;
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
            TopCanvasButtomGridwper=TopCanvas1wper = Grid1wper = list1wper = optionsMarketListViewwper = (this.Width - (originalWidth - list1w)) / (originalWidth - (originalWidth - list1w));
            userManageListViewwper=historyListViewwper = optionsHoldDetailListViewwper = optionsTradingListViewwper = multipleTabControlwper = grid3wper = top1wper = (this.Width - (originalWidth - grid3w)) / (originalWidth - (originalWidth - grid3w));
            list1hper = (this.Height - (originalHeight - list1h)) / (originalHeight - (originalHeight - list1h));
            optionsMarketListViewhper = (this.Height - (originalHeight - optionsMarketListViewh)) / (originalHeight - (originalHeight - optionsMarketListViewh));
            TopCanvas1hper=Grid1hper = grid3hper =  (this.Height - (originalHeight - Grid1h)) / (originalHeight - (originalHeight - Grid1h));
            
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
            multipleTabControl.Width = multipleTabControlw * multipleTabControlwper;
            holdDetailListView.Width = optionsHoldDetailListVieww * optionsHoldDetailListViewwper;
            statusBar1.Width = statusBar1w * statusBar1wper;
            optionsMarketListView.Width = optionsMarketListVieww * optionsMarketListViewwper;
            optionsMarketListView.Height = optionsMarketListViewh * optionsMarketListViewhper;
            TopCanvas1.Width = TopCanvas1w * TopCanvas1wper;
            TopCanvas1.Height = TopCanvas1h * TopCanvas1hper;
            TopCanvasButtomGrid.Width = TopCanvasButtomGridw * TopCanvasButtomGridwper;
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
                 futuresTradingListView.Visibility = Visibility.Visible;
                 futuresMarketListView.Visibility = Visibility.Visible;
                 futuresTraderComboBox.Visibility = Visibility.Visible;
           
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
            
         }

         private void predictComboBoxItem_Selected(object sender, RoutedEventArgs e)
         {
             amountOfUpAndDownLabel.Visibility = Visibility.Visible;
             amountOfUpAndDownTextBox.Visibility = Visibility.Visible;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
         }

         private void predictComboBoxItem2_Selected(object sender, RoutedEventArgs e)
         {
             amountOfUpAndDownLabel.Visibility = Visibility.Visible;
             amountOfUpAndDownTextBox.Visibility = Visibility.Visible;
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

        private void optionsMarketListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
         {

         }


         
    }
}
