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
    /// placeOrder.xaml 的交互逻辑
    /// </summary>
    public partial class PlaceOrder : Window
    {
        private Storyboard tradingCanvasStoryboard, tradingCanvasStoryboard_Leave;

     

        HistoryManager porder_hm;  //下单盒子中维护历史记录区的指针
        PlaceOrderManager porder_pom; //下单盒子中维护盒子数据的指针
        TradingManager porder_tm;  ///交易区指针
        DataManager porder_dm;        //dm指针
        MainWindow pWindow;           //主窗体指针



        public PlaceOrder()
        {
            InitializeComponent();
            Border1.Height = this.Height - 14;
            Border1.Width = this.Width - 14;
            tradingCanvasStoryboard = (Storyboard)this.FindResource("tradingCanvasAnimate");
            tradingCanvasStoryboard_Leave = (Storyboard)this.FindResource("tradingCanvasAnimate_Leave");

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        } //最小化窗口按钮



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

        private void tradingCanvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // tradingCanvasStoryboard.Begin(this);
        }

        private void tradingCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // tradingCanvasStoryboard_Leave.Begin(this);
        }


        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)    //下单盒子中“确定”按钮
        {
            //System.Windows.MessageBox.Show("123");
            OrderToHistory();
            cleanTrading();

            if (pWindow.optionsCheckBox.IsChecked == true && pWindow.futuresCheckBox.IsChecked == true)
            {
                pWindow.hm.OnShowAll();
            }
            else if (pWindow.optionsCheckBox.IsChecked == true && pWindow.futuresCheckBox.IsChecked == false)
            {
                pWindow.hm.OnShowOption();
            }
            else if (pWindow.optionsCheckBox.IsChecked == false && pWindow.futuresCheckBox.IsChecked == true)
            {
                pWindow.hm.OnShowFuture();
            }
            else if (pWindow.optionsCheckBox.IsChecked == false && pWindow.futuresCheckBox.IsChecked == false)
            {
                pWindow.hm.OnShowNull();
            }

            
            pWindow.historyTabItem.IsSelected = true;


            //pWindow.historyListView.Visibility = Visibility.Visible;


            //porder_hm.HistoryOC.OrderBy(x>x.y);

            this.Close();
        }


        public void getPoint(HistoryManager _hm, PlaceOrderManager _pom, DataManager _dm,TradingManager _tm,MainWindow _pWindow)     //获得指针用的函数
        {
            
            porder_hm = _hm;
            porder_pom = _pom;
            porder_dm = _dm;
            porder_tm = _tm;
            pWindow = _pWindow;
        }

        public void OrderToHistory()      //处理从下单盒子到历史区的数据
        {
            //porder_hm = _hm;
            //porder_pom = _pom;

            for (int i = porder_pom.OrderOC.Count()-1; i >=0 ; i--)
            {

                PlaceOrderData pOrder = porder_pom.OrderOC[i];

                DataRow nDr = (DataRow)DataManager.All[pOrder.InstrumentID];

                string oInstrumentID = pOrder.InstrumentID;

                string oTradingTime = DataManager.now.ToString();

                string oTradingType = pOrder.TradingType;

                int oPostNum = pOrder.TradingNum;

                int oDoneNum = 0;

                string oTradingState = HistoryManager.POST;   //初始化为挂单状态

                double oPostPrice = pOrder.FinalPrice;

                double oDonePrice = 0;

                string oTimeLimit = nDr["LastDate"].ToString();

                string oUserID = pOrder.UserID;

                string oClientageCondition = pOrder.ClientageCondition;

                bool oOptionOrFuture = pOrder.OptionOrFuture;

                string oClientageType = pOrder.ClientageType;

                porder_hm.OnAdd(oInstrumentID, oTradingTime, oTradingType, oPostNum, oDoneNum, oTradingState, oPostPrice, oDonePrice, oTimeLimit, oUserID, oClientageCondition, oOptionOrFuture, oClientageType);

            }


        }

        public void cleanTrading()     //当下单盒子“确定”点击后，清理掉交易区相同的单子
        {
            for (int i = porder_tm.TradingOC.Count()-1; i >= 0; i--)
            {
                TradingData cTd = porder_tm.TradingOC[i];
                for (int j = porder_pom.OrderOC.Count()-1; j >=0 ; j--)
                {
                    PlaceOrderData cPo = porder_pom.OrderOC[j];
                    if (cTd.InstrumentID == cPo.InstrumentID && cTd.IsBuy == cPo.IsBuy && cPo.IsFromGradient==false)
                        porder_tm.TradingOC.RemoveAt(i);
                }
            }
        }

    }
}
