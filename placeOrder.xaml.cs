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

        public PlaceOrder()
        {
            InitializeComponent();
            Border1.Height = this.Height-14;
            Border1.Width = this.Width-14;

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
            tradingCanvasStoryboard.Begin(this);
        }

        private void tradingCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            tradingCanvasStoryboard_Leave.Begin(this);
        }

     
		private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }


        //public void OrderToHistory(HistoryManager _hm,PlaceOrderManager _pom)      //处理从下单盒子到历史区的数据
        //{
        //    porder_hm = _hm;
        //    porder_pom = _pom;

        //    for (int i = 0; i < _pom.OrderOC.Count(); i++)
        //    {
                 
        //        PlaceOrderData pOrder = _pom.OrderOC[i];

        //    }


        //}
      
    }
}
