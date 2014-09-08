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
using System.Windows.Threading;
using System.Collections;
using AmCharts.Windows.Core;

namespace qiquanui
{
    /// <summary>
    /// MessagesWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessagesWindow : Window
    {
        Rect rc = SystemParameters.WorkArea;//获取工作区大小
        public MessagesWindow()
        {

            this.ShowInTaskbar = false; 
            InitializeComponent();
            MessageText.Content = MessagesControl.MessageText;
            this.Topmost = true;






            if (MessagesControl.messagesWindowNum < 3 && MessagesControl.messagesWindowNum >= 0)
            {
                this.Top = 30 + (MessagesControl.messagesWindowNum) * (this.Height + 20);

            }
            else
            {
                MessagesControl.messagesWindowNum = 0;



                this.Top = 30 + (MessagesControl.messagesWindowNum) * (this.Height + 20);
            }
            MessagesControl.messagesWindowNum += 1;


            // this.Left = rc.Width - this.Width;//窗口置顶


        }



        private System.Windows.Threading.DispatcherTimer dTimer = new DispatcherTimer();//提示框消失计时器
        private System.Windows.Threading.DispatcherTimer dTimer_c = new DispatcherTimer();//提示框消失关闭计时器
        private void dTimer_c_Tick(object sender, EventArgs e)
        {
            dTimer_c.Stop();
            if (MessagesControl.messagesWindowNum > 0)
                MessagesControl.messagesWindowNum -= 1;



            this.Close();
        }
        private void dTimer_Tick(object sender, EventArgs e)
        {
            dTimer_c.Tick += new EventHandler(dTimer_c_Tick);
            dTimer_c.Interval = new TimeSpan(0, 0, 5);
            dTimer_c.Start();


            Storyboard s = new Storyboard();
            this.Resources.Add(Guid.NewGuid().ToString(), s);
            DoubleAnimation da = new DoubleAnimation();
            Storyboard.SetTarget(da, this);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity", new object[] { }));
            da.From = 1;
            da.To = 0;
            s.Duration = new Duration(TimeSpan.FromSeconds(5));
            s.Children.Add(da);
            s.Begin();

            dTimer.Stop();

        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation Animation1 = new DoubleAnimation();
            Animation1.From = rc.Width;     //初始值   1为  整个窗体     0.1表示窗体的十分之一
            Animation1.To = rc.Width - this.Width;     //结束值    0为完全关闭
            Animation1.Duration = new Duration(TimeSpan.Parse("0:0:0.8"));    //所用时间的间隔
            Animation1.AccelerationRatio = 0.5;
            Animation1.DecelerationRatio = 0.5;
            this.BeginAnimation(Window.LeftProperty, Animation1);

            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0, 0, 10);
            dTimer.Start();

        }
        private bool closeStoryBoardCompleted = false;
        private DoubleAnimation closeAnimation1;

        private System.Windows.Threading.DispatcherTimer dTimer_closing = new DispatcherTimer();//点击关闭按钮的计时器
        private void dTimer_closing_Tick(object sender, EventArgs e)
        {
            dTimer_closing.Stop();
            if (MessagesControl.messagesWindowNum > 0)
                MessagesControl.messagesWindowNum -= 1;




            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dTimer_closing.Tick += new EventHandler(dTimer_closing_Tick);
            dTimer_closing.Interval = new TimeSpan(0, 0, 1);
            dTimer_closing.Start();
            if (!closeStoryBoardCompleted)
            {
                closeAnimation1 = new DoubleAnimation();
                closeAnimation1.From = rc.Width - this.Width;     //初始值   1为  整个窗体     0.1表示窗体的十分之一
                closeAnimation1.To = rc.Width;     //结束值    0为完全关闭
                closeAnimation1.Duration = new Duration(TimeSpan.Parse("0:0:0.8"));    //所用时间的间隔
                closeAnimation1.AccelerationRatio = 0.5;
                closeAnimation1.DecelerationRatio = 0.5;
                closeAnimation1.Completed += new EventHandler(closeWindow_Completed);

                this.BeginAnimation(Window.LeftProperty, closeAnimation1);

            }

        }


        private void closeWindow_Completed(object sender, EventArgs e)
        {

            closeStoryBoardCompleted = true;
            this.Close();
        }



    }
}
