using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Timers;

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

        public ObservableCollection<PositionsData> UserPositionsOC;


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

            UserPositionsOC = new ObservableCollection<PositionsData>();
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

        PositionsManager u_pm;

        System.Timers.Timer userTimer;

        public static Hashtable userHash = new Hashtable(100);

        public UserManager(MainWindow _pwindow)
        {
            pWindow = _pwindow;

            pWindow.userManageListView.ItemsSource = UserOC;

            //OnAdd("1", "2", "3", "4", "5", 0, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18);

            GetInfoFromDBToHash();    //从数据库获得数据

            GetInfoFromHashToOC();   //从Hash到OC

            GetPositionsFromDBToUserPositions();

            userTimer = new System.Timers.Timer(500);

            userTimer.Start();

            userTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);


        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }


        public void Refresh()
        {
            RefreshUserPositionOC();
        }

        public void GetPositonManagerPoint(PositionsManager _pm)
        {
            u_pm = _pm;
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
            userHash.Clear();

            string sql = String.Format("select * from User");
            DataTable dt = DataControl.QueryTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string id = (string)dt.Rows[i]["UserID"];
                userHash[id] = dt.Rows[i];
            }


        }


        delegate void GetInfoFromHashToOCCallBack();
        public void GetInfoFromHashToOC()
        {
            GetInfoFromHashToOCCallBack d;
            if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
            {
                d = new GetInfoFromHashToOCCallBack(GetInfoFromHashToOC);
                pWindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                UserOC.Clear();//先清理了
            }


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


                if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
                {
                    d = new GetInfoFromHashToOCCallBack(GetInfoFromHashToOC);
                    pWindow.Dispatcher.Invoke(d, new object[] { });
                }
                else
                {
                    OnAddForToOC(e_userID,
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

            if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
            {
                d = new GetInfoFromHashToOCCallBack(GetInfoFromHashToOC);
                pWindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                initUserCombobox(pWindow);   //初始化程序上方的 投资账户ComBoBox
            }



        }


        public void initUserCombobox(MainWindow _pw)
        {
            int old_selectedIndex = _pw.userComboBox.SelectedIndex;
            if (old_selectedIndex == -1)
                old_selectedIndex = 0;
            _pw.userComboBox.Items.Clear();
            for (int i = 0; i < UserOC.Count(); i++)
            {
                string comShow = UserOC[i].UserID;

                _pw.userComboBox.Items.Add(comShow);

            }

            _pw.userComboBox.SelectedIndex = old_selectedIndex;

        }


        public void GetPositionsFromDBToUserPositions()
        {

            for (int i = 0; i < UserOC.Count(); i++)
            {
                UserOC[i].UserPositionsOC.Clear();
            }


            if (UserOC.Count() > 0)
            {
                string sql = String.Format("select * from Positions");
                DataTable dt = DataControl.QueryTable(sql);

                for (int ii = 0; ii < dt.Rows.Count; ii++)
                {
                    string d_userID = (string)dt.Rows[ii]["UserID"];

                    for (int jj = 0; jj < UserOC.Count(); jj++)
                    {

                        if (d_userID.Equals(UserOC[jj].UserID))
                        {
                            string d_instrumentID = (string)dt.Rows[ii]["InstrumentID"];

                            double d_averagePrice = Math.Round(Convert.ToDouble(dt.Rows[ii]["AveragePrice"]), 1);

                            int d_tradingNum = Convert.ToInt32(dt.Rows[ii]["TradingNum"]);

                            int d_isBuy = Convert.ToInt32(dt.Rows[ii]["IsBuy"]);

                            string d_buyOrSell = "";

                            if (d_isBuy == 1)
                            {
                                d_buyOrSell = "买";
                            }
                            else if (d_isBuy == 0)
                            {
                                d_buyOrSell = "卖";
                            }

                            ///////////////从All获取数据

                            DataRow nDr = (DataRow)DataManager.All[d_instrumentID];

                            string d_exchangeName = nDr["ExchangeName"].ToString();

                            double d_latestPrice = 0;

                            if (d_isBuy == 1)   //买
                            {
                                d_latestPrice = Convert.ToDouble(nDr["AskPrice1"]);
                            }
                            else if (d_isBuy == 0)
                            {
                                d_latestPrice = Convert.ToDouble(nDr["BidPrice1"]);
                            }

                            string d_dueDate = nDr["LastDate"].ToString();

                            int d_instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                            double d_floatingProfitAndLoss = (d_latestPrice - d_averagePrice) * Math.Abs(d_tradingNum) * d_instrumentMultiplier;

                            //double d_floatingProfitAndLossRate = (d_latestPrice - d_averagePrice) / d_averagePrice;

                            double d_floatingProfitAndLossRate = d_floatingProfitAndLoss / d_averagePrice;

                            double d_margin = SomeCalculate.caculateMargin(d_instrumentID, d_tradingNum, Convert.ToBoolean(d_isBuy), d_averagePrice);

                            PositionsData d_pd = new PositionsData(d_userID, d_exchangeName, d_instrumentID, d_latestPrice, d_averagePrice, d_tradingNum, d_buyOrSell, d_dueDate, d_floatingProfitAndLoss, d_floatingProfitAndLossRate, d_margin);

                            UserOC[jj].UserPositionsOC.Add(d_pd);
                        }
                    }



                }


            }

            if (u_pm != null)
            {
                u_pm.GetInfoFromUserToOC();
            }


        }



        public void RefreshUserPositionOC()
        {
            for (int i = 0; i < UserOC.Count(); i++)
            {
                for (int j = 0; j < UserOC[i].UserPositionsOC.Count(); j++)
                {
                    PositionsData r_upo = UserOC[i].UserPositionsOC[j];

                    string r_instrumentID = r_upo.InstrumentID;

                    DataRow nDr = (DataRow)DataManager.All[r_upo.InstrumentID];

                    double r_latestPrice = 0;

                    bool r_isBuy = true;

                    if (r_upo.BuyOrSell.Equals("买"))   //买
                    {
                        r_latestPrice = Convert.ToDouble(nDr["AskPrice1"]);
                        r_isBuy = true;
                    }
                    else if (r_upo.BuyOrSell.Equals("卖"))
                    {
                        r_latestPrice = Convert.ToDouble(nDr["BidPrice1"]);
                        r_isBuy = false;
                    }


                    int r_instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                    double r_floatingProfitAndLoss = (r_latestPrice - r_upo.AveragePrice) * Math.Abs(r_upo.TradingNum) * r_instrumentMultiplier;

                    //double r_floatingProfitAndLossRate = (r_latestPrice - r_pd.AveragePrice) / r_pd.AveragePrice;

                    double r_floatingProfitAndLossRate = r_floatingProfitAndLoss / r_upo.AveragePrice;

                    double r_margin = SomeCalculate.caculateMargin(r_upo.InstrumentID, r_upo.TradingNum, r_isBuy, r_upo.AveragePrice);


                    UserOC[i].UserPositionsOC[j].LatestPrice = r_latestPrice;
                    UserOC[i].UserPositionsOC[j].FloatingProfitAndLoss = Math.Round(r_floatingProfitAndLoss, 2);
                    UserOC[i].UserPositionsOC[j].FloatingProfitAndLossRate = Math.Round(r_floatingProfitAndLossRate, 2);
                    UserOC[i].UserPositionsOC[j].Margin = r_margin;

                }
            }
        }



        //为下单写的
        public void changeMarginForPlace(string _userID, string _insrtumentID, double _finalPrice, int _tradingNum, int _isBuy)   //增加保证金占用，减少可用资金
        {
            double c_margin = SomeCalculate.caculateMargin(_insrtumentID, _tradingNum, Convert.ToBoolean(_isBuy), _finalPrice);

            if (c_margin != 0)
            {
                string querySql = String.Format("SELECT * from User WHERE UserID='{0}'", _userID);

                DataTable old_userTable = DataControl.QueryTable(querySql);

                DataRow old_userRow = old_userTable.Rows[0];

                double old_margin = Convert.ToDouble(old_userRow["UsedMargin"]);

                double old_availableCapital = Convert.ToDouble(old_userRow["AvailableCapital"]);

                double new_margin = old_margin + c_margin;

                double new_availableCapital = old_availableCapital - c_margin;

                string updateSql = String.Format("UPDATE User SET UsedMargin='{0}',AvailableCapital='{1}' WHERE UserID='{2}' ", new_margin, new_availableCapital, _userID);

                DataControl.InsertOrUpdate(updateSql);

            }

        }


        //为下单写的
        public void changeRoyaltyForPlace(string _userID, string _insrtumentID, double _finalPrice, int _tradingNum, int _isBuy)    //修改保证金的收入和支出，以及可用资金的变动
        {
            double c_royalty = SomeCalculate.caculateRoyalty(_insrtumentID, _tradingNum, Convert.ToBoolean(_isBuy), _finalPrice);

            if (c_royalty != 0)
            {
                string querySql = String.Format("SELECT * from User WHERE UserID='{0}'", _userID);

                DataTable old_userTable = DataControl.QueryTable(querySql);

                DataRow old_userRow = old_userTable.Rows[0];

                double old_royaltyOutcome = Convert.ToDouble(old_userRow["RoyaltyOutcome"]);

                double old_royaltyIncome = Convert.ToDouble(old_userRow["RoyaltyIncome"]);

                double old_availableCapital = Convert.ToDouble(old_userRow["AvailableCapital"]);

                double new_royaltyOutcome = old_royaltyOutcome;

                double new_royaltyIncome = old_royaltyIncome;

                if (c_royalty > 0)       //权利金支出
                {
                    new_royaltyOutcome += c_royalty;
                }
                else if (c_royalty < 0)    //权利金获得
                {
                    new_royaltyIncome += -c_royalty;
                }

                double new_availableCapital = old_availableCapital - c_royalty;


                string updateSql = String.Format("UPDATE User SET RoyaltyOutcome='{0}',RoyaltyIncome='{1}',AvailableCapital='{2}' WHERE UserID='{3}' ", new_royaltyOutcome, new_royaltyIncome, new_availableCapital, _userID);

                DataControl.InsertOrUpdate(updateSql);

            }
        }

        //为下单写的
        public void changeFeesForPlace(string _userID, string _insrtumentID, double _finalPrice, int _tradingNum, int _isBuy)
        {
            double c_fees = SomeCalculate.calculateFees(_insrtumentID, _tradingNum, _finalPrice);

            if (c_fees != 0)
            {
                string querySql = String.Format("SELECT * from User WHERE UserID='{0}'", _userID);

                DataTable old_userTable = DataControl.QueryTable(querySql);

                DataRow old_userRow = old_userTable.Rows[0];

                double old_fees = Convert.ToDouble(old_userRow["ProcedureFees"]);

                double old_availableCapital = Convert.ToDouble(old_userRow["AvailableCapital"]);

                double new_fees = old_fees + c_fees;

                double new_availableCapital = old_availableCapital - c_fees;

                string updateSql = String.Format("UPDATE User SET ProcedureFees='{0}',AvailableCapital='{1}' WHERE UserID='{2}' ", new_fees, new_availableCapital, _userID);

                DataControl.InsertOrUpdate(updateSql);
            }

        }



        public void UserManagerHandleInHistory(string _userID, string _insrtumentID, double _finalPrice, int _tradingNum, int _isBuy)
        {
            // 资金扣除

            changeMarginForPlace(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);
            changeRoyaltyForPlace(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);
            changeFeesForPlace(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);
            //每次执行之后要刷新持仓区
            //h_pm.GetInfoFromDBToOC();

            GetInfoFromDBToHash();
            GetInfoFromHashToOC();
            GetPositionsFromDBToUserPositions();
        }




    }
}
