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
        public CloseOutPlaceOrderWindow()
        {
            InitializeComponent();

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

        private void typeOfTradingTComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)    //对买 卖 ComboBox 写动态 0 买  1 卖
        {

            //for (int i = 0; i < tradingListView.Items.Count; i++)
            //{
            //    TradingData otd = (TradingData)tradingListView.Items[i];

            //    bool becomeSame = false;   //改变了之后有相同的

            //    int old_tradingNum = otd.TradingNum;

            //    switch (otd.TradingType)
            //    {
            //        case 0:
            //            otd.IsBuy = true;

            //            otd.TradingNum = Math.Abs(old_tradingNum);
            //            break;
            //        case 1:
            //            otd.IsBuy = false;
            //            otd.TradingNum = -Math.Abs(old_tradingNum);
            //            break;

            //    }

            //    otd.TypeChangeCount += 1;

            //    if (otd.TypeChangeCount > 1)   //如果已经不是第一次改变，那就要判断是否有重复
            //    {
            //        if (otd.OptionOrFuture == false)  //如果是期权
            //        {
            //            if (otm.TradingOC.Count() > 0)    //判断是否有相同的
            //                for (int j = 0; j < otm.TradingOC.Count(); j++)
            //                {
            //                    TradingData td = otm.TradingOC[j];
            //                    if (td.UserID == otd.UserID && td.InstrumentID == otd.InstrumentID && Convert.ToDouble(td.ExercisePrice) == Convert.ToDouble(otd.ExercisePrice) && td.IsBuy == otd.IsBuy &&
            //                        td.CallOrPut.Equals(otd.CallOrPut) && td.OptionOrFuture == otd.OptionOrFuture && i != j)
            //                        becomeSame = true;
            //                }
            //        }
            //        else if (otd.OptionOrFuture == true)  //如果是期货
            //        {
            //            if (otm.TradingOC.Count() > 0)    //判断是否有相同的
            //                for (int j = 0; j < otm.TradingOC.Count(); j++)
            //                {
            //                    TradingData td = otm.TradingOC[j];

            //                    if (td.InstrumentID.Equals(otd.InstrumentID) && td.IsBuy == otd.IsBuy && td.OptionOrFuture == otd.OptionOrFuture && i != j)
            //                    {
            //                        becomeSame = true;
            //                    }

            //                }
            //        }
                }



                if (becomeSame == true)
                {
                    otm.TradingOC.RemoveAt(i);
                }



            }

        }

        private void formOfClientageTComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)   //交易区 市价 限价 ComboBOX 变换调用的函数
        {
            //System.Windows.MessageBox.Show("123");
            for (int i = 0; i < otm.TradingOC.Count(); i++)
            {
                TradingData cTd = otm.TradingOC[i];
                if (cTd.OptionOrFuture == true)  //如果是期货
                {
                    cTd.AboutFOK = Visibility.Collapsed;
                    cTd.AboutIOC = Visibility.Collapsed;
                    cTd.AboutROD = Visibility.Visible;
                    if (cTd.ClientageType == 0) //市价
                    {
                        cTd.ClientagePrice = "-";
                        cTd.IsEnableOfClientagePrice = false;
                    }
                    else if (cTd.ClientageType == 1)    //限价
                    {
                        cTd.ClientagePrice = cTd.MarketPrice.ToString();
                        cTd.IsEnableOfClientagePrice = true;
                    }
                }
                else     //如果是期权
                {
                    if (cTd.ClientageType == 0) //市价
                    {
                        cTd.AboutFOK = Visibility.Visible;
                        cTd.AboutIOC = Visibility.Visible;
                        cTd.AboutROD = Visibility.Collapsed;

                        cTd.ClientagePrice = "-";
                        cTd.IsEnableOfClientagePrice = false;
                    }
                    else if (cTd.ClientageType == 1)    //限价
                    {
                        cTd.AboutFOK = Visibility.Visible;
                        cTd.AboutIOC = Visibility.Visible;
                        cTd.AboutROD = Visibility.Visible;

                        cTd.ClientagePrice = cTd.MarketPrice.ToString();
                        cTd.IsEnableOfClientagePrice = true;
                    }

                }
            }
        }
    }
}
