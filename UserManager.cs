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

        private int userState;     //0 登出  1登入

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
        private string riskLevel;  //  14、	风险度
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




        public string RiskLevel
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
         string _riskLevel,  //  14、	风险度
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

        PickUpUserManager u_puum;

        System.Timers.Timer userTimer;

        public static Hashtable userHash = new Hashtable(100);

        public int userCount = 0;

        public UserManager(MainWindow _pwindow, PickUpUserManager _u_puum)
        {
            pWindow = _pwindow;

            u_puum = _u_puum;

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

            CalculateDynamicFloatingProfitAndLossAndUserEquityAndRiskLevel();
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
         string _riskLevel,  //  14、	风险度
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

            string sql = String.Format("select * from User WHERE IsLogin=1");
            DataTable dt = DataControl.QueryTable(sql);

            userCount = dt.Rows.Count;

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

                double e_closedProfitAndLoss = Math.Round(Convert.ToDouble(userDr["ClosedProfitAndLoss"]), 2);

                double e_floatingProfitAndLoss = Convert.ToDouble(userDr["FloatingProfitAndLoss"]);

                double e_userEquity = Convert.ToDouble(userDr["UserEquity"]);

                double e_usedMargin = Convert.ToDouble(userDr["UsedMargin"]);

                double e_availableCapital = Convert.ToDouble(userDr["AvailableCapital"]);

                double e_royaltyOutcome = Convert.ToDouble(userDr["RoyaltyOutcome"]);

                double e_royaltyIncome = Convert.ToDouble(userDr["RoyaltyIncome"]);

                double e_procedureFees = Convert.ToDouble(userDr["ProcedureFees"]);

                string e_riskLevel = (userDr["RiskLevel"]).ToString();

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
                initPickUpUser(u_puum);
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

        public void initPickUpUser(PickUpUserManager _puud)     //初始化历史记录账户筛选
        {
            ObservableCollection<PickUpUserData> tempPickUpUserOC = new ObservableCollection<PickUpUserData>();

            for (int i = 0; i < PickUpUserManager.PickUpUserOC.Count; i++)
            {
                bool t_isChoose = PickUpUserManager.PickUpUserOC[i].IsChoose;
                string t_userID = PickUpUserManager.PickUpUserOC[i].UserID;

                tempPickUpUserOC.Add(new PickUpUserData(t_isChoose, t_userID));
            }
            
            PickUpUserManager.PickUpUserOC.Clear();

            for (int i = 0; i < UserOC.Count(); i++)
            {
                string eachUserID = UserOC[i].UserID;

                _puud.OnAddPickUpUserDara(true, eachUserID);

            }

            for (int i = 0; i < PickUpUserManager.PickUpUserOC.Count; i++)
            {
                for (int j = 0; j < tempPickUpUserOC.Count; j++)
                {
                    if (PickUpUserManager.PickUpUserOC[i].UserID.Equals(tempPickUpUserOC[j].UserID))
                        PickUpUserManager.PickUpUserOC[i].IsChoose = tempPickUpUserOC[j].IsChoose;
                }
            }

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

                            double d_positionAveragePrice = Math.Round(Convert.ToDouble(dt.Rows[ii]["PositionAveragePrice"]), 1);

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



                            d_latestPrice = Convert.ToDouble(nDr["LastPrice"]);

                            string d_dueDate = nDr["LastDate"].ToString();

                            int d_instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                            double d_floatingProfitAndLoss = 0;

                            if (d_isBuy == 1)   //买
                            {
                                d_floatingProfitAndLoss = (d_latestPrice - d_averagePrice) * Math.Abs(d_tradingNum) * d_instrumentMultiplier;
                            }
                            else if (d_isBuy == 0)  //卖
                            {
                                d_floatingProfitAndLoss = (d_averagePrice - d_latestPrice) * Math.Abs(d_tradingNum) * d_instrumentMultiplier;
                            }



                            d_floatingProfitAndLoss = (d_latestPrice - d_averagePrice) * Math.Abs(d_tradingNum) * d_instrumentMultiplier;



                            double d_floatingProfitAndLossRate = d_floatingProfitAndLoss / d_averagePrice;

                            double d_margin = SomeCalculate.caculateMargin(d_instrumentID, d_tradingNum, Convert.ToBoolean(d_isBuy), d_averagePrice);

                            PositionsData d_pd = new PositionsData(d_userID, d_exchangeName, d_instrumentID, d_latestPrice, d_averagePrice, d_positionAveragePrice, d_tradingNum, d_buyOrSell, d_dueDate, d_floatingProfitAndLoss, d_floatingProfitAndLossRate, d_margin);

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

                    r_latestPrice = Convert.ToDouble(nDr["LastPrice"]);

                    bool r_isBuy = true;


                    int r_instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                    double r_floatingProfitAndLoss = 0;

                    if (r_upo.BuyOrSell.Equals("买"))   //买
                    {
                        r_floatingProfitAndLoss = (r_latestPrice - r_upo.AveragePrice) * Math.Abs(r_upo.TradingNum) * r_instrumentMultiplier;

                        r_isBuy = true;
                    }
                    else if (r_upo.BuyOrSell.Equals("卖"))
                    {

                        r_floatingProfitAndLoss = (r_upo.AveragePrice - r_latestPrice) * Math.Abs(r_upo.TradingNum) * r_instrumentMultiplier;

                        r_isBuy = false;
                    }


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

            //检测平仓
            TestForCloseOut(_userID, _insrtumentID);

            GetInfoFromDBToHash();
            GetInfoFromHashToOC();
            GetPositionsFromDBToUserPositions();
        }


        public void CalculateDynamicFloatingProfitAndLossAndUserEquityAndRiskLevel()
        {

            for (int i = 0; i < UserOC.Count(); i++)
            {
                double u_floatingProfitAndLoss = 0;


                for (int j = 0; j < UserOC[i].UserPositionsOC.Count(); j++)
                {
                    u_floatingProfitAndLoss += UserOC[i].UserPositionsOC[j].FloatingProfitAndLoss;
                }


                UserOC[i].FloatingProfitAndLoss = u_floatingProfitAndLoss;

                UserOC[i].UserEquity = UserOC[i].AvailableCapital + UserOC[i].UsedMargin + u_floatingProfitAndLoss;

                double d_riskLevelRate = Math.Round(UserOC[i].UsedMargin / UserOC[i].UserEquity * 100, 2);


                UserOC[i].RiskLevel = Convert.ToString(d_riskLevelRate) + "%";
            }
        }


        public void TestForCloseOut(string _userID, string _insrtumentID)    //测试是否有 平仓
        {
            //bool testIsBuy = !Convert.ToBoolean(_isBuy);

            string testQuerySql = String.Format("SELECT * from Positions WHERE UserID='{0}' AND InstrumentID='{1}' ORDER BY ID", _userID, _insrtumentID);

            DataTable testForCloseOutTable = DataControl.QueryTable(testQuerySql);

            if (testForCloseOutTable.Rows.Count == 2)
            {

                string getUserSql = String.Format("SELECT * FROM User WHERE UserID='{0}'", _userID);

                DataTable userDataTable = DataControl.QueryTable(getUserSql);

                DataRow userDr = userDataTable.Rows[0];

                double new_usedMargin = Convert.ToDouble(userDr["UsedMargin"]);

                double new_availableCapital = Convert.ToDouble(userDr["AvailableCapital"]);



                DataRow positionBuy = null;
                DataRow positionSell = null;

                DataRow positionTemp = testForCloseOutTable.Rows[0];


                DataRow nDrAll = (DataRow)DataManager.All[_insrtumentID];

                int instrumentMultiplier = Convert.ToInt32(nDrAll["InstrumentMultiplier"]);

                //string First = "";    //先买 还是先卖


                if (Convert.ToInt32(positionTemp["IsBuy"]) == 1)
                {
                    positionBuy = testForCloseOutTable.Rows[0];
                    positionSell = testForCloseOutTable.Rows[1];
                    //  First = "买";
                }
                else if (Convert.ToInt32(positionTemp["IsBuy"]) == 0)
                {
                    positionBuy = testForCloseOutTable.Rows[1];
                    positionSell = testForCloseOutTable.Rows[0];
                    //First = "卖";
                }

                if (Math.Abs(Convert.ToInt32(positionBuy["TradingNum"])) == Math.Abs(Convert.ToInt32(positionSell["TradingNum"])))
                {
                    double cut_marginBuy = SomeCalculate.caculateMargin(_insrtumentID, Convert.ToInt32(positionBuy["TradingNum"]), true, Convert.ToDouble(positionBuy["AveragePrice"]));

                    double cut_marginSell = SomeCalculate.caculateMargin(_insrtumentID, Convert.ToInt32(positionSell["TradingNum"]), false, Convert.ToDouble(positionSell["AveragePrice"]));

                    new_usedMargin -= (cut_marginBuy + cut_marginSell);



                    //////平仓盈亏
                    double old_closedProfitAndLoss = Convert.ToDouble(userDr["ClosedProfitAndLoss"]);

                    DataRow firstOne = testForCloseOutTable.Rows[0];



                    DataRow secondOne = testForCloseOutTable.Rows[1];


                    double new_closedProfitAndLoss = old_closedProfitAndLoss + (Convert.ToDouble(firstOne["AveragePrice"]) - Convert.ToDouble(secondOne["AveragePrice"])) * Convert.ToInt32(positionBuy["TradingNum"]) * instrumentMultiplier;

                    string updateUserSql = String.Format("UPDATE User SET ClosedProfitAndLoss='{0}' WHERE UserID='{1}'", new_closedProfitAndLoss, _userID);
                    ///////////////////



                    new_availableCapital += (cut_marginBuy + cut_marginSell);


                    string deleteBuySqlAndSell = String.Format("DELETE FROM Positions WHERE UserID='{0}' AND InstrumentID='{1}'", _userID, _insrtumentID);

                    DataControl.InsertOrUpdate(deleteBuySqlAndSell);

                    DataControl.InsertOrUpdate(updateUserSql);

                    //string deleteSellSql = String.Format("DELETE FROM Positions WHERE UserID='{0}' AND AND InstrumentID='{1}' AND IsBuy=1", _userID, _insrtumentID);

                }
                else if (Math.Abs(Convert.ToInt32(positionBuy["TradingNum"])) > Math.Abs(Convert.ToInt32(positionSell["TradingNum"])))
                {
                    int buyNum = Convert.ToInt32(positionBuy["TradingNum"]);

                    int sellNum = Convert.ToInt32(positionSell["TradingNum"]);

                    int d_num = Math.Abs(buyNum) - Math.Abs(sellNum);

                    double cut_marginBuy = SomeCalculate.caculateMargin(_insrtumentID, Math.Abs(sellNum), true, Convert.ToDouble(positionBuy["AveragePrice"]));

                    double cut_marginSell = SomeCalculate.caculateMargin(_insrtumentID, sellNum, false, Convert.ToDouble(positionSell["AveragePrice"]));

                    new_usedMargin -= (cut_marginBuy + cut_marginSell);




                    double old_positionAveragePrice = Convert.ToDouble(positionBuy["PositionAveragePrice"]);

                    double sellAveragePrice = Convert.ToDouble(positionSell["AveragePrice"]);

                    double new_positionAveragePrice = (old_positionAveragePrice * buyNum - Math.Abs(sellNum) * sellAveragePrice) / (buyNum + (Math.Abs(sellNum)));

                    //////平仓盈亏
                    double old_closedProfitAndLoss = Convert.ToDouble(userDr["ClosedProfitAndLoss"]);

                    DataRow firstOne = testForCloseOutTable.Rows[0];

                    DataRow secondOne = testForCloseOutTable.Rows[1];

                    double new_closedProfitAndLoss = old_closedProfitAndLoss + (Convert.ToDouble(firstOne["AveragePrice"]) - Convert.ToDouble(secondOne["AveragePrice"])) * Math.Abs(sellNum) * instrumentMultiplier;

                    string updateUserSql = String.Format("UPDATE User SET ClosedProfitAndLoss='{0}' WHERE UserID='{1}'", new_closedProfitAndLoss, _userID);
                    ///////////////////


                    new_availableCapital += (cut_marginBuy + cut_marginSell);

                    string updateBuySql = string.Format("UPDATE Positions SET TradingNum='{0}',PositionAveragePrice='{1}' WHERE UserID='{2}' AND InstrumentID='{3}' AND IsBuy=1", d_num, new_positionAveragePrice, _userID, _insrtumentID);

                    string deleteSellSql = String.Format("DELETE FROM Positions WHERE UserID='{0}' AND InstrumentID='{1}' And IsBuy=0", _userID, _insrtumentID);

                    DataControl.InsertOrUpdate(updateBuySql);

                    DataControl.InsertOrUpdate(deleteSellSql);

                    DataControl.InsertOrUpdate(updateUserSql);

                }
                else if (Math.Abs(Convert.ToInt32(positionBuy["TradingNum"])) < Math.Abs(Convert.ToInt32(positionSell["TradingNum"])))
                {
                    int buyNum = Convert.ToInt32(positionBuy["TradingNum"]);

                    int sellNum = Convert.ToInt32(positionSell["TradingNum"]);

                    int d_num = Math.Abs(sellNum) - Math.Abs(buyNum);

                    double cut_marginBuy = SomeCalculate.caculateMargin(_insrtumentID, buyNum, true, Convert.ToDouble(positionBuy["AveragePrice"]));

                    double cut_marginSell = SomeCalculate.caculateMargin(_insrtumentID, -buyNum, false, Convert.ToDouble(positionSell["AveragePrice"]));

                    new_usedMargin -= (cut_marginBuy + cut_marginSell);




                    double old_positionAveragePrice = Convert.ToDouble(positionSell["PositionAveragePrice"]);

                    double buyAveragePrice = Convert.ToDouble(positionBuy["AveragePrice"]);

                    double new_positionAveragePrice = (old_positionAveragePrice * Math.Abs(sellNum) - buyAveragePrice * buyNum) / (Math.Abs(sellNum) - buyNum);


                    //////平仓盈亏
                    double old_closedProfitAndLoss = Convert.ToDouble(userDr["ClosedProfitAndLoss"]);

                    DataRow firstOne = testForCloseOutTable.Rows[0];

                    DataRow secondOne = testForCloseOutTable.Rows[1];

                    double new_closedProfitAndLoss = old_closedProfitAndLoss + (Convert.ToDouble(firstOne["AveragePrice"]) - Convert.ToDouble(secondOne["AveragePrice"])) * Math.Abs(buyNum) * instrumentMultiplier;

                    string updateUserSql = String.Format("UPDATE User SET ClosedProfitAndLoss='{0}' WHERE UserID='{1}'", new_closedProfitAndLoss, _userID);
                    ///////////////////


                    new_availableCapital += (cut_marginBuy + cut_marginSell);

                    string updateSellSql = string.Format("UPDATE Positions SET TradingNum='{0}',PositionAveragePrice='{1}' WHERE UserID='{2}' AND InstrumentID='{3}' AND IsBuy=0", -d_num, new_positionAveragePrice, _userID, _insrtumentID);

                    string deleteBuySql = String.Format("DELETE FROM Positions WHERE UserID='{0}' AND InstrumentID='{1}' And IsBuy=1", _userID, _insrtumentID);

                    DataControl.InsertOrUpdate(updateSellSql);

                    DataControl.InsertOrUpdate(deleteBuySql);

                    DataControl.InsertOrUpdate(updateUserSql);

                }

                string upUserSql = string.Format("UPDATE User SET  UsedMargin='{0}', AvailableCapital='{1}'  WHERE UserID='{2}' ", new_usedMargin, new_availableCapital, _userID);

                DataControl.InsertOrUpdate(upUserSql);


            }

        }

        public void CalculateStaticFloatingProfitAndLossAndFinalBalance()
        {
            for (int i = 0; i < UserOC.Count(); i++)
            {
                double new_finalBalance = UserOC[i].AvailableCapital + UserOC[i].UsedMargin;

                double staticFloatingProfitAndLoss = 0;

                for (int j = 0; j < UserOC[i].UserPositionsOC.Count(); j++)
                {
                    string s_instrumentID = UserOC[i].UserPositionsOC[j].InstrumentID;

                    DataRow nDr = (DataRow)DataManager.All[s_instrumentID];

                    double closePrice = Convert.ToDouble(nDr["ClosePrice"]);

                    double eachStaticFloating = UserOC[i].UserPositionsOC[j].AveragePrice - closePrice;

                    staticFloatingProfitAndLoss += eachStaticFloating;
                }

                string updateSql = String.Format("UPDATE User SET FinalBalance='{0}',FloatingProfitAndLoss='{1}' WHERE UserID='{2}'", new_finalBalance, staticFloatingProfitAndLoss, UserOC[i].UserID);

                DataControl.InsertOrUpdate(updateSql);
            }
        }



        public void initOpeningBalance()
        {
            for (int i = 0; i < UserOC.Count(); i++)
            {
                double old_finalBalance = UserOC[i].FinalBalance;

                UserOC[i].OpeningBalance = old_finalBalance;

                string updateSql = String.Format("UPDATE User SET OpeningBalance='{0}' WHERE UserID='{1}'", old_finalBalance, UserOC[i].UserID);

                DataControl.InsertOrUpdate(updateSql);
            }
        }


        public void UserLogOut()
        {

            for (int i = 0; i < UserOC.Count(); i++)
            {
                if (UserOC[i].UserState == 0)    //登出
                {
                    double new_finalBalance = UserOC[i].AvailableCapital + UserOC[i].UsedMargin;

                    double staticFloatingProfitAndLoss = 0;

                    for (int j = 0; j < UserOC[i].UserPositionsOC.Count(); j++)
                    {
                        string s_instrumentID = UserOC[i].UserPositionsOC[j].InstrumentID;

                        DataRow nDr = (DataRow)DataManager.All[s_instrumentID];

                        double closePrice = Convert.ToDouble(nDr["ClosePrice"]);

                        double eachStaticFloating = UserOC[i].UserPositionsOC[j].AveragePrice - closePrice;

                        staticFloatingProfitAndLoss += eachStaticFloating;
                    }

                    string updateSql = String.Format("UPDATE User SET FinalBalance='{0}',FloatingProfitAndLoss='{1}',IsLogin=0 WHERE UserID='{2}'", new_finalBalance, staticFloatingProfitAndLoss, UserOC[i].UserID);

                    DataControl.InsertOrUpdate(updateSql);
                }



            }


            GetInfoFromDBToHash();
            GetInfoFromHashToOC();
            GetPositionsFromDBToUserPositions();

        }


        public bool UserLogOut(MainWindow _pWindow)
        {
            string add_userID = _pWindow.userIDAUTB.Text;

            string add_passWord = _pWindow.passwordAUTB.Password;

            bool isHave=false;    //看是否已经登录

            for (int i = 0; i < UserOC.Count; i++)
            {
                if (UserOC[i].UserID.Equals(add_userID))
                    isHave = true;
            }


            if (isHave == false)
            {
                string querySql = String.Format("SELECT * FROM User WHERE UserID='{0}' AND UserPassWord={1}", add_userID, add_passWord);

                DataTable usersDT = DataControl.QueryTable(querySql);

                if (usersDT.Rows.Count > 0)
                {

                    string updateSql = String.Format("UPDATE User SET IsLogin=1 WHERE UserID='{0}'", add_userID);

                    DataControl.InsertOrUpdate(updateSql);

                    this.initOpeningBalance();   //一定要放在um pm 初始化之后

                    MainWindow.pm.initPositionAveragePrice();


                    GetInfoFromDBToHash();
                    GetInfoFromHashToOC();
                    GetPositionsFromDBToUserPositions();



                    _pWindow.addUserErrorWarn.Visibility = System.Windows.Visibility.Hidden;

                    _pWindow.userIDAUTB.Text = null;

                    _pWindow.passwordAUTB.Password = null;

                    return true;
                }
                else
                {
                    _pWindow.addUserErrorWarn.Text = "账号不存在或者密码不正确！";

                    _pWindow.addUserErrorWarn.Visibility = System.Windows.Visibility.Visible;

                    _pWindow.userIDAUTB.Text = null;

                    _pWindow.passwordAUTB.Password = null;

                    return false;
                }
            }
            else
            {
                _pWindow.addUserErrorWarn.Text = "账号已登录，无法重复登录！";

                _pWindow.addUserErrorWarn.Visibility = System.Windows.Visibility.Visible;

                _pWindow.userIDAUTB.Text = null;

                _pWindow.passwordAUTB.Password = null;

                return false;
            }

            
        }
        





    }
}
