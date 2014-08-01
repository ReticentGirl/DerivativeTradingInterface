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

namespace qiquanui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {
        DataManager dm;
        private double originalHeight, originalWidth, list1h, list1w, grid3w, grid3h, list1hper, list1wper, grid3hper, grid3wper, top1w, top1wper, canvas1h, canvas1hper, multipleTabControlw, multipleTabControlwper, tradingListVieww, tradingListViewwper, optionsHoldDetailListVieww, optionsHoldDetailListViewwper, historyListVieww, historyListViewwper, userManageListVieww, userManageListViewwper, statusBar1w, statusBar1wper, canvas2h, canvas2hper, Grid1w, Grid1h, Grid1wper, Grid1hper, optionsMarketListVieww, optionsMarketListViewwper, optionsMarketListViewh, optionsMarketListViewhper, TopCanvas1h, TopCanvas1w, TopCanvas1wper, TopCanvas1hper, TopCanvasButtomGridw, TopCanvasButtomGridwper, optionsMarketTitleGridw, optionsMarketTitleGridwper, titileBorder4w, titileBorder4wper, profitListVieww, profitListViewwper;

        TradingManager otm;   //维护交易区的对象



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

        public MainWindow()
        {

            //new Thread(new ThreadStart(dataStart)).Start();
            dataStart();

            //datathread = new thread(new threadstart(dmupdate));
            //datathread.start();

            //tradingthread = new thread(new threadstart(tradingthreadstart));    //交易区线程启动
            //tradingthread.start();


          

            InitializeComponent();

            otm = new TradingManager(this);

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
            TopCanvas1h=TopCanvas1.Height;
            TopCanvasButtomGridw = TopCanvasButtomGrid.Width;
            optionsMarketTitleGridw = optionsMarketTitleGrid.Width;
            titileBorder4w = titileBorder4.Width;
            Grid1w = Grid1.Width;
            Grid1h = Grid1.Height;
            profitListVieww = profitListView.Width;
          

            //初始化伸缩面板动画
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

            basicInforAndPromptGridStoryboard = (Storyboard)this.FindResource("basicInforAndPromptGridAnimate");
            basicInforAndPromptGridStoryboard_Leave = (Storyboard)this.FindResource("basicInforAndPromptGridAnimate_Leave");

            typeComboBox.SelectedIndex = 0;
            typeInStrategyPanelComboBox.SelectedIndex = 1;            
                      
        }

        
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }//窗口移动
      
        
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
        } //双击标题栏最大化、还原
        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        } //最小化窗口按钮

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
            profitListViewwper=userManageListViewwper = historyListViewwper = optionsHoldDetailListViewwper = tradingListViewwper = multipleTabControlwper = grid3wper = top1wper = (this.Width - (originalWidth - grid3w)) / (originalWidth - (originalWidth - grid3w));
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
            tradingListView.Width = tradingListVieww * tradingListViewwper;
            multipleTabControl.Width = multipleTabControlw * multipleTabControlwper;
            holdDetailListView.Width = optionsHoldDetailListVieww * optionsHoldDetailListViewwper;
            profitListView.Width = profitListViewwper * profitListVieww;
            statusBar1.Width = statusBar1w * statusBar1wper;
            optionsMarketListView.Width = optionsMarketListVieww * optionsMarketListViewwper;
            optionsMarketListView.Height = optionsMarketListViewh * optionsMarketListViewhper;
            TopCanvas1.Width = TopCanvas1w * TopCanvas1wper;
            TopCanvas1.Height = TopCanvas1h * TopCanvas1hper;
            TopCanvasButtomGrid.Width = TopCanvasButtomGridw * TopCanvasButtomGridwper;
            optionsMarketTitleGrid.Width = optionsMarketTitleGridwper * optionsMarketTitleGridw;
            titileBorder4.Width = titileBorder4wper * titileBorder4w;
            return true;
        } //拉伸窗口时改变各个控件大小
        private void Window_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ResizeControl();
        } //拉伸窗口调用ResizeControl()

       
        private void Grid1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            grid1Storyboard.Begin(this);
        } //鼠标进入行情区，区域增亮

        private void Grid1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            grid1Storyboard_Leave.Begin(this);
        } //鼠标离开行情区，区域亮度还原

        private void Grid3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
         {
             grid3Storyboard.Begin(this);
         } //鼠标进入操作区，区域增亮

        private void Grid3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
         {
             grid3Storyboard_Leave.Begin(this);
         } //鼠标离开操作区，区域亮度还原
        private void optionsMarketTitleGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            optionsMarketTitleGridStoryboard.Begin(this);

        } //鼠标进入期权对应标的物行情区，区域增亮

        private void optionsMarketTitleGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            optionsMarketTitleGridStoryboard_Leave.Begin(this);
        } //鼠标离开期权对应标的物行情区，区域亮度还原

        private void basicInforAndPromptGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            basicInforAndPromptGridStoryboard.Begin(this);
        }

        private void basicInforAndPromptGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            basicInforAndPromptGridStoryboard_Leave.Begin(this);
        }

        private void futuresComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

            optionsMarketListView.Visibility = Visibility.Hidden;
            optionsMarketTitleGrid.Visibility = Visibility.Hidden;
            subjectMatterLabel.Visibility = Visibility.Hidden;
            subjectMatterComboBox.Visibility = Visibility.Hidden;
            futuresMarketListView.Visibility = Visibility.Visible;
            subjectMatterMarketGrid.Visibility = Visibility.Hidden;
            titileBorder4.Visibility = Visibility.Hidden;
        } //行情区，“衍生品种类”选择“期货”，隐藏“标的期货”、期权行情，显示期货行情

        private void optionsComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

            futuresMarketListView.Visibility = Visibility.Hidden;
            optionsMarketListView.Visibility = Visibility.Visible;
            optionsMarketTitleGrid.Visibility = Visibility.Visible;
            subjectMatterLabel.Visibility = Visibility.Visible;
            subjectMatterComboBox.Visibility = Visibility.Visible;
            subjectMatterMarketGrid.Visibility = Visibility.Visible;
            titileBorder4.Visibility = Visibility.Visible;
        } //行情区，“衍生品种类”选择“期权”，显示“标的期货”、期权行情，隐藏期货行情
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


        private void StategyCanvasButtom_Click(object sender, RoutedEventArgs e)
        {
            if (Canvas1.Width == 29.0)
             {
                 DoubleAnimation animate = new DoubleAnimation();
                 animate.To = 292;
                 animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                 animate.DecelerationRatio = 1;
                 //   animate.AccelerationRatio = 0.33;
                 Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
                 LeftImage.Source = new BitmapImage(new Uri("Resources/left.png", UriKind.Relative)); 
             }
            else if (Canvas1.Width == 292.0)
            {
                DoubleAnimation animate = new DoubleAnimation();
                animate.To = 29.0;
                animate.Duration = new Duration(TimeSpan.FromSeconds(0.4));
                //   animate.DecelerationRatio = 0.33;
                animate.AccelerationRatio = 1;
                Canvas1.BeginAnimation(Canvas.WidthProperty, animate);
                LeftImage.Source = new BitmapImage(new Uri("Resources/right.png", UriKind.Relative)); 
            }
        }
         private void Canvas1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
         {
             
                 canvas1Storyboard.Begin(this);
             
         } //当鼠标进入策略实验室面板时，展开面板动画

         private void Canvas1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
         {
             
             canvas1Storyboard_Leave.Begin(this);
         } //当鼠标离开策略实验室面板时，缩回面板动画
         private void RiskCanvasButtom_Click(object sender, RoutedEventArgs e)
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
                 RightImage.Source = new BitmapImage(new Uri("Resources/right.png", UriKind.Relative)); 
             }
             else if (Canvas2.Width == 120.0)
             {
                 DoubleAnimation animate1 = new DoubleAnimation();
             animate1.To = 29.0;
             animate1.Duration = new Duration(TimeSpan.FromSeconds(0.4));
             //   animate.DecelerationRatio = 0.33;
             animate1.AccelerationRatio = 1;
             Canvas2.BeginAnimation(Canvas.WidthProperty, animate1);
             RightImage.Source = new BitmapImage(new Uri("Resources/left.png", UriKind.Relative)); 
             }
         }
         private void Canvas2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
         {
             
                 canvas2Storyboard.Begin(this);
             
         } //当鼠标进入风险实验室面板时，展开面板动画

         private void Canvas2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
         {
             
             canvas2Storyboard_Leave.Begin(this);
         } //当鼠标离开风险实验室面板时，缩回面板动画


         private void predictComboBoxItem_Selected(object sender, RoutedEventArgs e)
         {
             maxAmountOfUpAndDownLabel.Visibility = Visibility.Visible;
             maxAmountOfUpAndDownTextBox.Visibility = Visibility.Visible;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
         } //策略实验室，“情景选择”选择“上涨”，显示“最高涨跌幅”，隐藏“波动率”

         private void predictComboBoxItem2_Selected(object sender, RoutedEventArgs e)
         {
             maxAmountOfUpAndDownLabel.Visibility = Visibility.Visible;
             maxAmountOfUpAndDownTextBox.Visibility = Visibility.Visible;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
         } //策略实验室，“情景选择”选择“下跌”，显示“最高涨跌幅”，隐藏“波动率”

         private void predictComboBoxItem3_Selected(object sender, RoutedEventArgs e)
         {
             maxAmountOfUpAndDownLabel.Visibility = Visibility.Hidden;
             maxAmountOfUpAndDownTextBox.Visibility = Visibility.Hidden;
             volatilityLabel.Visibility = Visibility.Visible;
             volatilityComboBox.Visibility = Visibility.Visible;
         } //策略实验室，“情景选择”选择“波动”，隐藏“最高涨跌幅”，显示“波动率”

         private void predictComboBoxItem4_Selected(object sender, RoutedEventArgs e)
         {
             maxAmountOfUpAndDownLabel.Visibility = Visibility.Hidden;
             maxAmountOfUpAndDownTextBox.Visibility = Visibility.Hidden;
             volatilityLabel.Visibility = Visibility.Hidden;
             volatilityComboBox.Visibility = Visibility.Hidden;
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
            StrategyWindow strategyWindow = new StrategyWindow();
            strategyWindow.Show();
           
        } //策略实验室，点击“开始分析”

        //private void marketPriceOTCBI_Selected(object sender, RoutedEventArgs e)
        //{
        //    comboBoxItem1.Visibility = Visibility.Hidden;
        //} //期权交易区，“委托方式”选择“市价”，“托单方式”中“ROD”不可选



       

        private void placeOrderTBtn_Click(object sender, RoutedEventArgs e)         //交易区下单按钮
        {
            placeOrder PlaceOrder = new placeOrder();
            PlaceOrder.Show();

            if (tradingListView.Items.Count > 0)
            {
                for (int i = 0; i < tradingListView.Items.Count; i++)
                {

                    TradingData otd = (TradingData) tradingListView.Items[i];
                    if (otd.IfChooseOTGVCH == true)
                        System.Windows.MessageBox.Show(otd.InstrumentID);
                }
            }
        }


        private void deleteTBtn_Click(object sender, RoutedEventArgs e)      //交易区删除按钮
        {
          if (tradingListView.Items.Count > 0)
            {
            for (int i = tradingListView.Items.Count-1; i >=0; i--)    //倒回来写才删的干净
                {

                    TradingData otd = (TradingData)tradingListView.Items[i];
                    if (otd.IfChooseOTGVCH == true)
                    {
                        otm.TradingOC.RemoveAt(i);
                        
                    }
                        
                }
            }
        }

        private void bidROMBtn_Click(object sender, RoutedEventArgs e)        //期权看涨 “买”按钮
        {
            int  exercisePrice = (int)((sender as System.Windows.Controls.Button).Tag);  //获取按钮Tag

            int buttonIndex = (int)dm.ep_no[(int)exercisePrice];    //根据哈希表获得行数

            option selectedOption = dm.ObservableObj[buttonIndex];

            string selectedUserID = "balabala";

            string selectedInstrumentID = selectedOption.instrumentid1;   //看涨

            string selectedCallOrPut = "看涨";

            double selectedExercisePrice = selectedOption.ExercisePrice;

            double selectedMarketPrice = selectedOption.BidPrice1;

            bool haveSame = false;    //用以判断交易区中是否有相同的

            if (otm.TradingOC.Count()>0)    //判断是否有相同的
                 for (int i = 0; i < otm.TradingOC.Count();i++)
                {
                     TradingData td = otm.TradingOC[i];
                     if (td.UserID == selectedUserID && td.InstrumentID == selectedInstrumentID && td.ExercisePrice == selectedExercisePrice)
                         haveSame = true;
                }

            if(haveSame==false)    //没有相同的就加入到交易区列表中
                otm.OnAdd(selectedUserID, selectedInstrumentID, selectedCallOrPut, selectedExercisePrice, selectedMarketPrice);
    
        }

        private void tradingListView_SelectionChanged(object sender, SelectionChangedEventArgs e)    //交易区窗口选择列改变事件
        {
            //System.Windows.MessageBox.Show("change");
           TradingData selectedItem = tradingListView.SelectedItem as TradingData;

        }

      


        #region 顶端各选单逻辑 
        /// <summary>
        /// 顶端各选单逻辑 CREATED BY IVES DING
        /// </summary>



        void DmUpdate()
        {
            dm.Update();
        }

        public static string[] NameSubject = { "金", "铜", "白糖", "豆粕", "上证50", "沪深300" };
        public static string[] NameOption = { "金期权", "铜期权", "白糖期权","豆粕期权", "上证50期权", "沪深300期权" };
        /// <summary>
        /// 标的期货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subjectMatterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;
            string _subject = (string)e.AddedItems[0];
            subjectMatterComboBox.Text = _subject;
            string _option="";
            for (int i = 0; i < NameSubject.Length; i++)
                if (NameSubject[i].Equals(_subject))
                    _option = NameOption[i];
            string duedatesql = "select duedate from staticdata where instrumentname='"+_option+"' group by duedate";
            DataTable dt= DataControl.QueryTable(duedatesql);
            dueDateComboBox.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _duedate = (string) dt.Rows[i][0];
                if (_duedate.Equals("1407")) continue;
                dueDateComboBox.Items.Add(_duedate);
            }
            dueDateComboBox.SelectedIndex=0; 

        }

        /// <summary>
        /// 交易商
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void traderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count == 0) return;
            string trader;
            trader = (string)((ComboBoxItem)e.AddedItems[0]).Content;
            trader=trader.Trim();
            type_change(typeComboBox.Text.Trim(), trader);
        }

        private void type_change(string _type,string _trader)
        {
            if (!_type.Equals("期权")) {
                string duedatesql = "SELECT duedate FROM staticdata s  where exchangename='"+_trader+"' and optionorfuture=1 group by duedate ";
                DataTable dt = DataControl.QueryTable(duedatesql);
                dueDateComboBox.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _duedate = (string)dt.Rows[i][0];
                    if (_duedate.Length==4 && _duedate[0].Equals('1'))
                    dueDateComboBox.Items.Add(_duedate);
                }
                dueDateComboBox.SelectedIndex = 0;
                return;
            }
            string trader = _trader;
            subjectMatterComboBox.Items.Clear();
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

            string trader = traderComboBox.Text.Trim();
            string duedata = (string)e.AddedItems[0];
            string subject = subjectMatterComboBox.Text.Trim();

            if (trader.Equals("") || duedata.Equals("") || subject.Equals(""))
                return;

            dataThread = new Thread(new ThreadStart(DmUpdate));
            dataThread.Start();
        }


        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count == 0) type_change("期货", "中金所");
            else
            {
                string type = ((string)((ComboBoxItem)e.AddedItems[0]).Content).Trim();
                type_change(type, traderComboBox.Text.Trim());
            }
        }



        #endregion

       

      

        

    }
}
