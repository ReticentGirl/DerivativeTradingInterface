using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace qiquanui
{
    public class UserData : INotifyPropertyChanged
    {

        private string userID;
        private string userPassWord;
        private string userName;
        private string userMail;
        private string openCompany;   //开户公司

        private int userState;

        private double openingBalance;   //4、	期初结存
        private double finalBalance;    //5、	期末结存
        private double closedProfitAndLoss;  //6、	平仓盈亏
        private double floatingProfitAndLoss; // 浮动盈亏
        private double userEquity;  //   8、	客户权益（动态改变的）
        private double usedMargin;  //   9、	保证金占用
        private double availableCapital;  //  10、	可用资金
        private double royaltyOutcome;  //    11、	权利金支出
        private double royaltyIncome;  //   12、	权利金收入
        private double procedureFees;  //   13、	手续费
        private double riskLevel;  //  14、	风险度
        private double additionalCapital;  //  15、	应追加资金


        public string UserID
        {
            get { return userID; }
            set
            {

                userID = value;
                OnPropertyChanged("userID");
            }
        }

        public string UserPassWord
        {
            get { return userPassWord; }
            set
            {

                userPassWord = value;
                OnPropertyChanged("UserPassWord");
            }
        }


        public string UserName
        {
            get { return userName; }
            set
            {

                userName = value;
                OnPropertyChanged("UserName");
            }
        }


        public string UserMail
        {
            get { return userMail; }
            set
            {

                userMail = value;
                OnPropertyChanged("UserMail");
            }
        }


        public string OpenCompany
        {
            get { return openCompany; }
            set
            {

                openCompany = value;
                OnPropertyChanged("OpenCompany");
            }
        }


        public int UserState
        {
            get { return userState; }
            set
            {

                userState = value;
                OnPropertyChanged("UserState");
            }
        }



        public double OpeningBalance
        {
            get { return openingBalance; }
            set
            {

                openingBalance = value;
                OnPropertyChanged("OpeningBalance");
            }
        }




        public double FinalBalance
        {
            get { return finalBalance; }
            set
            {

                finalBalance = value;
                OnPropertyChanged("FinalBalance");
            }
        }




        public double ClosedProfitAndLoss
        {
            get { return closedProfitAndLoss; }
            set
            {

                closedProfitAndLoss = value;
                OnPropertyChanged("ClosedProfitAndLoss");
            }
        }




        public double FloatingProfitAndLoss
        {
            get { return floatingProfitAndLoss; }
            set
            {

                floatingProfitAndLoss = value;
                OnPropertyChanged("FloatingProfitAndLoss");
            }
        }




        public double UserEquity
        {
            get { return userEquity; }
            set
            {

                userEquity = value;
                OnPropertyChanged("UserEquity");
            }
        }




        public double UsedMargin
        {
            get { return usedMargin; }
            set
            {

                usedMargin = value;
                OnPropertyChanged("UsedMargin");
            }
        }




        public double AvailableCapital
        {
            get { return availableCapital; }
            set
            {

                availableCapital = value;
                OnPropertyChanged("AvailableCapital");
            }
        }



        public double RoyaltyOutcome
        {
            get { return royaltyOutcome; }
            set
            {

                royaltyOutcome = value;
                OnPropertyChanged("RoyaltyOutcome");
            }
        }




        public double RoyaltyIncome
        {
            get { return royaltyIncome; }
            set
            {

                royaltyIncome = value;
                OnPropertyChanged("RoyaltyIncome");
            }
        }




        public double ProcedureFees
        {
            get { return procedureFees; }
            set
            {

                procedureFees = value;
                OnPropertyChanged("ProcedureFees");
            }
        }




        public double RiskLevel
        {
            get { return riskLevel; }
            set
            {

                riskLevel = value;
                OnPropertyChanged("RiskLevel");
            }
        }




        public double AdditionalCapital
        {
            get { return additionalCapital; }
            set
            {

                additionalCapital = value;
                OnPropertyChanged("AdditionalCapital");
            }
        }


        public UserData()
        {

        }







        public UserData(
              string _userID,
         string _userPassWord,
         string _userName,
         string _userMail,
         string _openCompany,   //开户公司
         double _openingBalance,   //4、	期初结存
         double _finalBalance,  //5、	期末结存
         double _closedProfitAndLoss,  //6、	平仓盈亏
         double _floatingProfitAndLoss, // 浮动盈亏
         double _userEquity,  //   8、	客户权益（动态改变的）
         double _usedMargin,  //   9、	保证金占用
         double _availableCapital,  //  10、	可用资金
         double _royaltyOutcome,  //    11、	权利金支出
         double _royaltyIncome,  //   12、	权利金收入
         double _procedureFees,  //   13、	手续费
         double _riskLevel,  //  14、	风险度
         double _additionalCapital  //  15、	应追加资金
            )
        {
            userID = _userID;
            userPassWord = _userPassWord;
            userName = _userName;
            userMail = _userMail;
            openCompany = _openCompany;   //开户公司


            openingBalance = _openingBalance;   //4、	期初结存
            finalBalance = _finalBalance;    //5、	期末结存
            closedProfitAndLoss = _closedProfitAndLoss;  //6、	平仓盈亏
            floatingProfitAndLoss = _floatingProfitAndLoss; // 浮动盈亏
            userEquity = _userEquity;  //   8、	客户权益（动态改变的）
            usedMargin = _usedMargin;  //   9、	保证金占用
            availableCapital = _availableCapital;  //  10、	可用资金
            royaltyOutcome = _royaltyOutcome;  //    11、	权利金支出
            royaltyIncome = _royaltyIncome;  //   12、	权利金收入
            procedureFees = _procedureFees;  //   13、	手续费
            riskLevel = _riskLevel;  //  14、	风险度
            additionalCapital = _additionalCapital;  //  15、	应追加资金

            userState = 1;   //默认状态

        }






        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }




        #endregion
    }

    public class UserManager
    {
        public ObservableCollection<UserData> UserOC = new ObservableCollection<UserData>();

        MainWindow pWindow;    //主窗体指针

        public static Hashtable userHash = new Hashtable(100);

        public UserManager(MainWindow _pwindow)
        {
            pWindow = _pwindow;

            pWindow.userManageListView.ItemsSource = UserOC;

            //OnAdd("1", "2", "3", "4", "5", 0, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18);

            GetInfoFromDBToHash();    //从数据库获得数据

            GetInfoFromHashToOC();   //从Hash到OC

            initUserCombobox(pWindow);   //初始化程序上方的 投资账户ComBoBox
        }

        public void OnAddForToOC(
              string _userID,
         string _userPassWord,
         string _userName,
         string _userMail,
         string _openCompany,   //开户公司
         double _openingBalance,   //4、	期初结存
         double _finalBalance,  //5、	期末结存
         double _closedProfitAndLoss,  //6、	平仓盈亏
         double _floatingProfitAndLoss, // 浮动盈亏
         double _userEquity,  //   8、	客户权益（动态改变的）
         double _usedMargin,  //   9、	保证金占用
         double _availableCapital,  //  10、	可用资金
         double _royaltyOutcome,  //    11、	权利金支出
         double _royaltyIncome,  //   12、	权利金收入
         double _procedureFees,  //   13、	手续费
         double _riskLevel,  //  14、	风险度
         double _additionalCapital  //  15、	应追加资金
            )
        {
            UserData add_data = new UserData(_userID,
          _userPassWord,
          _userName,
          _userMail,
          _openCompany,
          _openingBalance,
          _finalBalance,
          _closedProfitAndLoss,
          _floatingProfitAndLoss,
            _userEquity,
          _usedMargin,
          _availableCapital,
          _royaltyOutcome,
          _royaltyIncome,
          _procedureFees,
          _riskLevel,
          _additionalCapital);


            UserOC.Add(add_data);
        }



        public void GetInfoFromDBToHash()
        {
            string sql = String.Format("select * from User");
            DataTable dt = DataControl.QueryTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = (string)dt.Rows[i]["UserID"];
                userHash[id] = dt.Rows[i];
            }


        }



        public void GetInfoFromHashToOC()
        {
            foreach (DictionaryEntry eachUser in userHash)
            {
                string e_userID = eachUser.Key.ToString();

                DataRow userDr = (DataRow)userHash[e_userID];

                string e_userName = userDr["UserName"].ToString();

                string e_openCompany = userDr["OpenCompany"].ToString();

                string e_userPassWord = userDr["UserPassWord"].ToString();

                string e_userMail = userDr["UserMail"].ToString();

                double e_openingBalance = Convert.ToDouble(userDr["OpeningBalance"]);

                double e_finalBalance = Convert.ToDouble(userDr["FinalBalance"]);

                double e_closedProfitAndLoss = Convert.ToDouble(userDr["ClosedProfitAndLoss"]);

                double e_floatingProfitAndLoss = Convert.ToDouble(userDr["FloatingProfitAndLoss"]);

                double e_userEquity = Convert.ToDouble(userDr["UserEquity"]);

                double e_usedMargin = Convert.ToDouble(userDr["UsedMargin"]);

                double e_availableCapital = Convert.ToDouble(userDr["AvailableCapital"]);

                double e_royaltyOutcome = Convert.ToDouble(userDr["RoyaltyOutcome"]);

                double e_royaltyIncome = Convert.ToDouble(userDr["RoyaltyIncome"]);

                double e_procedureFees = Convert.ToDouble(userDr["ProcedureFees"]);

                double e_riskLevel = Convert.ToDouble(userDr["RiskLevel"]);

                double e_additionalCapital = Convert.ToDouble(userDr["AdditionalCapital"]);

                OnAddForToOC(   e_userID,
                                e_userPassWord,
                                e_userName,
                                e_userMail,
                                e_openCompany,
                                e_openingBalance,
                                e_finalBalance,
                                e_closedProfitAndLoss,
                                e_floatingProfitAndLoss,
                                e_userEquity,
                                e_usedMargin,
                                e_availableCapital,
                                e_royaltyOutcome,
                                e_royaltyIncome,
                                e_procedureFees,
                                e_riskLevel,
                                e_additionalCapital);

            }
        }


        public void initUserCombobox(MainWindow _pw)
        {
            for(int i =0;i<UserOC.Count();i++)
            {
                string comShow = UserOC[i].UserID;

                _pw.userComboBox.Items.Add(comShow);

            }

            _pw.userComboBox.SelectedIndex = 0;
            
        }

    }
}
