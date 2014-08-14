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
using System.Data;

namespace qiquanui
{
    /// <summary>
    /// SignInWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SignInWindow : Window
    {
        public UserManager s_um;
        public SignInWindow(UserManager _um)
        {
            s_um = _um;
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

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {

            if (testForNull() == true)   //如果有空的
            {
                wrongTypeTB.Visibility = Visibility.Visible;
                wrongTypeTB.Text = "请完善基本信息！";

            }
            else
            {
                wrongTypeTB.Visibility = Visibility.Hidden;
            }

            if (wrongTypeTB.Visibility == Visibility.Hidden)
            {
                string i_userID = setUserIDTBox.Text;

                string i_passWord = setPasswordPBox.Password;

                string i_userName = setUserNameTBox.Text;

                string i_userMail = setEmailTBox.Text;

                string i_openCompany = setCompanyTBox.Text;

                SignToDB(i_userID, i_passWord, i_userName, i_userMail, i_openCompany);

                reInitUser();

                this.Close();
            }

        }

        public void SignToDB(string _userID, string _userPassWord, string _userName, string _userMail, string _openCompany)
        {
            double openMoney = 3000000;

            double zero = 0;

            int isLogin = 1;

            string insertSql = String.Format(" INSERT INTO User VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')"
                , _userID, _userPassWord, _userName, _userMail, _openCompany, openMoney, zero, zero, zero, openMoney, zero, openMoney, zero, zero, zero, zero, zero, isLogin);

            DataControl.InsertOrUpdate(insertSql);

            //INSERT INTO User VALUES("001109","001109","吴永柱","123456@qq.com","a",3000000,0,0,0,30000000,0,3000000,0,0,0,0,0);
        }


        public bool testIfpassWordSame(string _passWord, String _confPassWord)
        {
            if (_passWord.Equals(_confPassWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool testForSameID(string _userID)
        {
            string testSql = String.Format("SELECT * FROM User WHERE UserID='{0}'", _userID);

            DataTable testTable = DataControl.QueryTable(testSql);

            if (testTable.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void checkPasswordPBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            string passWord = setPasswordPBox.Password;

            string confPassWord = checkPasswordPBox.Password;

            if (testIfpassWordSame(passWord, confPassWord) == false)
            {
                wrongTypeTB.Visibility = Visibility.Visible;
                wrongTypeTB.Text = "两次输入不一致，请重新输入密码！";
            }
            else
            {
                wrongTypeTB.Visibility = Visibility.Hidden;
            }
        }

        public bool testForNull()
        {
            string i_userID = setUserIDTBox.Text;

            string i_passWord = setPasswordPBox.Password;

            string i_confPassWord = checkPasswordPBox.Password;

            string i_userName = setUserNameTBox.Text;

            string i_userMail = setEmailTBox.Text;

            string i_openCompany = setCompanyTBox.Text;

            if (i_userID.Equals("") || i_passWord.Equals("") || i_confPassWord.Equals("") || i_userName.Equals("") || i_userMail.Equals("") || i_openCompany.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void reInitUser()
        {
            s_um.GetInfoFromDBToHash();
            s_um.GetInfoFromHashToOC();
        }


        private void setUserIDTBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            string t_userID = setUserIDTBox.Text;
            if (testForSameID(t_userID) == true)
            {
                wrongTypeTB.Visibility = Visibility.Visible;
                wrongTypeTB.Text = "该账号已被注册，请您换一个试试！";
            }
            else
            {
                wrongTypeTB.Visibility = Visibility.Hidden;
            }
        }



    }
}
