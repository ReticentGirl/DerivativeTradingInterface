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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace qiquanui
{
    /// <summary>
    /// CloseOutPlaceOrderWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CloseOutPlaceOrderWindow : Window
    {
        public static CloseOutPlaceOrderWindowManager copowm;

        public MainWindow pWindow;

        public CloseOutPlaceOrderWindow(MainWindow _pWindow)
        {
            pWindow = _pWindow;
            InitializeComponent();
            copowm = new CloseOutPlaceOrderWindowManager(this,pWindow);
        }
        
        bool isWindowMax = false;
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isWindowMax == false)
            {
                this.DragMove();
            }
            else if (isWindowMax == true) ;

        }//窗口移动

        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        } //最小化窗口按钮

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {

            this.Close();

        }//关闭窗口按钮

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
        } //窗口关闭效果

       

        private void formOfClientageTComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)   //交易区 市价 限价 ComboBOX 变换调用的函数
        {


            for (int i = 0; i < copowm.CloseOutPlaceOrderWindowOC.Count; i++)
            {
                TradingData i_td = copowm.CloseOutPlaceOrderWindowOC[i];
                if (i_td.ClientageType == 0)
                {
                    i_td.ClientagePrice = "-";
                    i_td.IsEnableOfClientagePrice = false;
                }
                else
                {
                    i_td.ClientagePrice = i_td.MarketPrice.ToString();
                    i_td.IsEnableOfClientagePrice = true;
                }
            }
           
        }

        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)    //确定按钮
        {
            copowm.CloseOutOCToHistory();

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

            this.Close();
        }
    }
}
