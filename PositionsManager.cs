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

    public class PositionsData : INotifyPropertyChanged
    {
        private bool isChoose;
        private string userID;
        private string exchangeName;
        private string instrumentID;
        private double latestPrice;
        private double averagePrice;
        private double positionAveragePrice;
        private int tradingNum;
        private string buyOrSell;
        private string dueDate;
        private double floatingProfitAndLoss;
        private double floatingProfitAndLossRate;
        private double margin;




        public bool IsChoose
        {
            get { return isChoose; }
            set
            {

                isChoose = value;
                OnPropertyChanged("IsChoose");
            }
        }



        public string UserID
        {
            get { return userID; }
            set
            {

                userID = value;
                OnPropertyChanged("userID");
            }
        }

        public string ExchangeName
        {
            get { return exchangeName; }
            set
            {

                exchangeName = value;
                OnPropertyChanged("ExchangeName");
            }
        }

        public string InstrumentID
        {
            get { return instrumentID; }
            set
            {

                instrumentID = value;
                OnPropertyChanged("InstrumentID");
            }
        }


        public double LatestPrice
        {
            get { return latestPrice; }
            set
            {

                latestPrice = value;
                OnPropertyChanged("LatestPrice");
            }
        }


        public double AveragePrice
        {
            get { return averagePrice; }
            set
            {

                averagePrice = value;
                OnPropertyChanged("AveragePrice");
            }
        }


        public double PositionAveragePrice
        {
            get { return positionAveragePrice; }
            set
            {

                positionAveragePrice = value;
                OnPropertyChanged("PositionAveragePrice");
            }
        }



        public int TradingNum
        {
            get { return tradingNum; }
            set
            {

                tradingNum = value;
                OnPropertyChanged("TradingNum");
            }
        }


        public string BuyOrSell
        {
            get { return buyOrSell; }
            set
            {

                buyOrSell = value;
                OnPropertyChanged("BuyOrSell");
            }
        }


        public string DueDate
        {
            get { return dueDate; }
            set
            {

                dueDate = value;
                OnPropertyChanged("DueDate");
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


        public double FloatingProfitAndLossRate
        {
            get { return floatingProfitAndLossRate; }
            set
            {

                floatingProfitAndLossRate = value;
                OnPropertyChanged("FloatingProfitAndLossRate");
            }
        }


        public double Margin
        {
            get { return margin; }
            set
            {

                margin = value;
                OnPropertyChanged("Margin");
            }
        }





        public PositionsData(
               string _userID,
         string _exchangeName,
         string _instrumentID,
         double _latestPrice,
         double _averagePrice,
         double _positionAveragePrice,
         int _tradingNum,
         string _buyOrSell,
         string _dueDate,
         double _floatingProfitAndLoss,
         double _floatingProfitAndLossRate,
         double _margin
            )
        {
            userID = _userID;
            exchangeName = _exchangeName;
            instrumentID = _instrumentID;
            latestPrice = _latestPrice;
            averagePrice = _averagePrice;
            positionAveragePrice = _positionAveragePrice;
            tradingNum = _tradingNum;
            buyOrSell = _buyOrSell;
            dueDate = _dueDate;
            floatingProfitAndLoss = _floatingProfitAndLoss;
            floatingProfitAndLossRate = _floatingProfitAndLossRate;
            margin = _margin;

            isChoose = false; //默认为false;
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

    public class PositionsManager
    {
        public ObservableCollection<PositionsData> PositionsOC = new ObservableCollection<PositionsData>();

        MainWindow pWindow;    //主窗体指针

        UserManager p_um;

        TradingManager p_tm;

        //Hashtable positionsHash = new Hashtable(100);

        System.Timers.Timer positionsTimer; //刷新持仓区的计时器


        public PositionsManager(MainWindow _pwindow, UserManager _um,TradingManager _tm)
        {
            pWindow = _pwindow;

            p_um = _um;

            p_tm = _tm;

            pWindow.holdDetailListView.ItemsSource = PositionsOC;

            //OnAdd("1", "2", "3", 4, 5, 6, "7", "8", 9, 10, 11);

            //GetInfoFromDBToOC();
            GetInfoFromUserToOC();

            positionsTimer = new System.Timers.Timer(500);

            positionsTimer.Start();

            positionsTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

        }


        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
            //for (int i = 0; i < PositionsOC.Count(); i++)
            //{
            //    for (int j = 0; j < p_um.UserOC.Count(); j++)
            //    {
            //        for (int m = 0; m < p_um.UserOC[j].UserPositionsOC.Count(); m++)
            //        {
            //            if (PositionsOC[i].InstrumentID == p_um.UserOC[j].UserPositionsOC[m].InstrumentID)
            //                System.Windows.MessageBox.Show("123");
            //        }
            //    }
            //}
        }






        public void OnAdd(
         string _userID,
         string _exchangeName,
         string _instrumentID,
         double _latestPrice,
         double _averagePrice,
         double _positionAveragePrice,
         int _tradingNum,
         string _buyOrSell,
         string _dueDate,
         double _floatingProfitAndLoss,
         double _floatingProfitAndLossRate,
         double _margin
            )
        {
            PositionsData add_pd = new PositionsData(_userID,
          _exchangeName,
          _instrumentID,
          _latestPrice,
          _averagePrice,
          _positionAveragePrice,
          _tradingNum,
          _buyOrSell,
          _dueDate,
          _floatingProfitAndLoss,
          _floatingProfitAndLossRate,
          _margin);

            PositionsOC.Add(add_pd);

        }



        /*
        delegate void GetInfoFromDBToOCCallBack();
        public void GetInfoFromDBToOC()
        {
            //positionsHash.Clear();

            GetInfoFromDBToOCCallBack d;
            if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
            {
                d = new GetInfoFromDBToOCCallBack(GetInfoFromDBToOC);
                pWindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                PositionsOC.Clear(); //先清理了
            }
           

            string sql = String.Format("select * from Positions");
            DataTable dt = DataControl.QueryTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                /////////////从数据库获得数据
                string d_userID = (string)dt.Rows[i]["UserID"];

                string d_instrumentID = (string)dt.Rows[i]["InstrumentID"];

                double d_averagePrice = Math.Round(Convert.ToDouble(dt.Rows[i]["AveragePrice"]),1);

                int d_tradingNum = Convert.ToInt32(dt.Rows[i]["TradingNum"]);

                int d_isBuy = Convert.ToInt32(dt.Rows[i]["IsBuy"]);

                string d_buyOrSell="";

                if(d_isBuy==1)
                {
                    d_buyOrSell = "买";
                }else if(d_isBuy==0)
                {
                    d_buyOrSell = "卖";
                }

                ///////////////从All获取数据

                DataRow nDr = (DataRow)DataManager.All[d_instrumentID];

                string d_exchangeName = nDr["ExchangeName"].ToString();

                double d_latestPrice = 0;

                if(d_isBuy==1)   //买
                {
                    d_latestPrice = Convert.ToDouble(nDr["AskPrice1"]);
                }else if(d_isBuy==0)
                {
                    d_latestPrice = Convert.ToDouble(nDr["BidPrice1"]);
                }

                string d_dueDate = nDr["LastDate"].ToString();

                int d_instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                double d_floatingProfitAndLoss = (d_latestPrice - d_averagePrice) * Math.Abs(d_tradingNum) * d_instrumentMultiplier;

                //double d_floatingProfitAndLossRate = (d_latestPrice - d_averagePrice) / d_averagePrice;

                double d_floatingProfitAndLossRate = d_floatingProfitAndLoss / d_averagePrice;

                double d_margin = SomeCalculate.caculateMargin(d_instrumentID, d_tradingNum,Convert.ToBoolean(d_isBuy), d_averagePrice);

                if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
                {
                    d = new GetInfoFromDBToOCCallBack(GetInfoFromDBToOC);
                    pWindow.Dispatcher.Invoke(d, new object[] { });
                }
                else
                {
                    OnAdd(d_userID, d_exchangeName, d_instrumentID, d_latestPrice, d_averagePrice, d_tradingNum, d_buyOrSell, d_dueDate, Math.Round(d_floatingProfitAndLoss, 2), Math.Round(d_floatingProfitAndLossRate, 2), d_margin);
                }
           
                
                
            }

        }
        */

        delegate void GetInfoFromUserToOCCallBack();
        public void GetInfoFromUserToOC()
        {

            GetInfoFromUserToOCCallBack d;
            if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
            {
                d = new GetInfoFromUserToOCCallBack(GetInfoFromUserToOC);
                pWindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                PositionsOC.Clear(); //先清理了
            }



            for (int i = 0; i < p_um.UserOC.Count(); i++)
            {
                for (int j = 0; j < p_um.UserOC[i].UserPositionsOC.Count(); j++)
                {
                    PositionsData n_pd = p_um.UserOC[i].UserPositionsOC[j];

                    if (System.Threading.Thread.CurrentThread != pWindow.Dispatcher.Thread)
                    {
                        d = new GetInfoFromUserToOCCallBack(GetInfoFromUserToOC);
                        pWindow.Dispatcher.Invoke(d, new object[] { });
                    }
                    else
                    {
                        // bool n_isChoose = n_pd.IsChoose;
                        string n_userID = n_pd.UserID;
                        string n_exchangeName = n_pd.ExchangeName;
                        string n_instrumentID = n_pd.InstrumentID;
                        double n_latestPrice = n_pd.LatestPrice;
                        double n_averagePrice = n_pd.AveragePrice;
                        double n_positionAveragePrice = n_pd.PositionAveragePrice;
                        int n_tradingNum = n_pd.TradingNum;
                        string n_buyOrSell = n_pd.BuyOrSell;
                        string n_dueDate = n_pd.DueDate;
                        double n_floatingProfitAndLoss = n_pd.FloatingProfitAndLoss;
                        double n_floatingProfitAndLossRate = n_pd.FloatingProfitAndLossRate;
                        double n_margin = n_pd.Margin;

                        PositionsData add_pd = new PositionsData(n_userID, n_exchangeName, n_instrumentID, n_latestPrice, n_averagePrice, n_positionAveragePrice, n_tradingNum, n_buyOrSell, n_dueDate, n_floatingProfitAndLoss
                            , n_floatingProfitAndLossRate, n_margin);
                        PositionsOC.Add(add_pd);
                    }

                }

            }



        }



        public void Refresh()
        {
            if (PositionsOC.Count() > 0)
                for (int i = 0; i < PositionsOC.Count(); i++)
                {
                    PositionsData r_pd = PositionsOC[i];

                    string r_instrumentID = r_pd.InstrumentID;

                    DataRow nDr = (DataRow)DataManager.All[r_instrumentID];

                    double r_latestPrice = 0;





                    r_latestPrice = Convert.ToDouble(nDr["LastPrice"]);


                    int r_instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                    double r_floatingProfitAndLoss = 0;

                    bool r_isBuy = true;

                    if (r_pd.BuyOrSell.Equals("买"))   //买
                    {

                        r_floatingProfitAndLoss = (r_latestPrice - r_pd.AveragePrice) * Math.Abs(r_pd.TradingNum) * r_instrumentMultiplier;
                        r_isBuy = true;
                    }
                    else if (r_pd.BuyOrSell.Equals("卖"))
                    {

                        r_floatingProfitAndLoss = (r_pd.AveragePrice - r_latestPrice) * Math.Abs(r_pd.TradingNum) * r_instrumentMultiplier;
                        r_isBuy = false;
                    }

                    double r_margin = SomeCalculate.caculateMargin(r_pd.InstrumentID, r_pd.TradingNum, r_isBuy, r_pd.AveragePrice);

                    double r_royalty = SomeCalculate.caculateRoyalty(r_pd.InstrumentID, r_pd.TradingNum, r_isBuy, r_pd.AveragePrice);


                    double r_floatingProfitAndLossRate = 0;

                    if (Convert.ToInt32(nDr["OptionOrFuture"]) == 1)  //期货
                    {
                        r_floatingProfitAndLossRate = r_floatingProfitAndLoss / r_margin;
                    }
                    else    //期权
                    {
                        if (r_isBuy == true)
                        {
                            r_floatingProfitAndLossRate = r_floatingProfitAndLoss / r_royalty;
                        }
                        else
                        {
                            r_floatingProfitAndLossRate = r_floatingProfitAndLoss / (r_margin - Math.Abs(r_royalty));
                        }
                    }

                    PositionsOC[i].LatestPrice = r_latestPrice;
                    PositionsOC[i].FloatingProfitAndLoss = Math.Round(r_floatingProfitAndLoss, 2);
                    PositionsOC[i].FloatingProfitAndLossRate = Math.Round(r_floatingProfitAndLossRate, 2);
                    PositionsOC[i].Margin = r_margin;

                }
        }

        public void initPositionAveragePrice()
        {
            for (int i = 0; i < PositionsOC.Count(); i++)
            {
                DataRow nDr = (DataRow)DataManager.All[PositionsOC[i].InstrumentID];

                double new_PositionAveragePrice = Convert.ToDouble(nDr["PreClosePrice"]);

                PositionsOC[i].PositionAveragePrice = new_PositionAveragePrice;

                string updateSql = String.Format("UPDATE Positions SET PositionAveragePrice='{0}' WHERE InstrumentID='{1}'", new_PositionAveragePrice, PositionsOC[i].InstrumentID);

                DataControl.InsertOrUpdate(updateSql);
            }
        }


        public void HandleCloseOut()   //处理平仓
        {
            for (int i = 0; i < PositionsOC.Count(); i++)
            {
                PositionsData h_pd =PositionsOC[i];

                if (h_pd.IsChoose == true)
                {
                    string  c_userID= h_pd.UserID;

                    string c_instrumentID = h_pd.InstrumentID;

                    bool c_isBuy = true;

                    if (h_pd.BuyOrSell.Equals("买"))
                    {
                        c_isBuy = false;  //与所要平仓的相反
                    }
                    else
                    {
                        c_isBuy = true;
                    }

                    int c_tradingNum = h_pd.TradingNum;

                    p_tm.AddTradingForCloseOut(c_userID, c_instrumentID, c_isBuy, c_tradingNum);

                    pWindow.tradingTabItem.IsSelected = true;
                }

            }

        }




    }
}
