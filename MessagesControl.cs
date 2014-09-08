using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qiquanui
{
    class MessagesControl
    {
        //计算打开消息数
        public static double messagesWindowNum = 0;
        //public static double messagesWindowNum_first = 0;
       // public static double messagesWindowNum_second = 0;
        //public static double messagesWindowNum_third = 0;
       
        
        //传递消息文字
        public static String MessageText;
        public static void showMessage(String t)
        {
            MessageText = t;
            MessagesWindow messagesWindow = new MessagesWindow();
            messagesWindow.Show();
        }

    }
}
