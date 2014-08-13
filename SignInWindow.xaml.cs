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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace qiquanui
{
    /// <summary>
    /// SignInWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//关闭窗口按钮

        private void MinButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        } //最小化窗口按钮

        bool isWindowMax = false;
        private void Top1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isWindowMax == false)
            {
                this.DragMove();
            }

        }//窗口移动

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            setUserIDTBox.Clear();
            setPasswordPBox.Clear();
            checkPasswordPBox.Clear();
            setUserNameTBox.Clear();
            setEmailTBox.Clear();
            setCompanyTBox.Clear();

            wrongTypeTB.Visibility = Visibility.Hidden;

        }
    }
}
