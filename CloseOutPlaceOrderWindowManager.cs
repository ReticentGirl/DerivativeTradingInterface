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
    //public class CloseOutPlaceOrderWindowData
    //{
    //    private string userID;
    //    private string instrumentID;
    //    private int tradingType;  //交易类型   0 买  1 卖
    //    private int tradingNum;   //交易手数
    //    private int clientageType;   //委托方式    0 市价  1限价
    //    private string clientagePrice; //委托价格
    //    private double marketPrice;   //市场价格

    //}


    public class CloseOutPlaceOrderWindowManager
    {
        public ObservableCollection<TradingData> CloseOutPlaceOrderWindowOC = new ObservableCollection<TradingData>();


        CloseOutPlaceOrderWindow pCloseOutWindow;

        System.Timers.Timer closeOutTradingTimer; //刷新交易区的计时器

        public MainWindow pWindow;

        public CloseOutPlaceOrderWindowManager(CloseOutPlaceOrderWindow _pCloseOutWindow,MainWindow _pWindow)
        {
            pCloseOutWindow = _pCloseOutWindow;

            pWindow = _pWindow;
            
            _pCloseOutWindow.tradingListView.ItemsSource = CloseOutPlaceOrderWindowOC;

            closeOutTradingTimer = new System.Timers.Timer(500);

            closeOutTradingTimer.Start();

            closeOutTradingTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
       
        }



        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }


        public void Refresh()
        {
            for (int i = 0; i < CloseOutPlaceOrderWindowOC.Count; i++)
            {
                TradingData nTd = CloseOutPlaceOrderWindowOC[i];

                string reInstrumentID = nTd.InstrumentID;

                DataRow nDr = (DataRow)DataManager.All[reInstrumentID];

                double nMarketPrice = 0;

                if (nTd.S_tradingType.Equals("买"))
                {
                    nMarketPrice = Math.Round((double)nDr["AskPrice1"], 1);
                }
                else
                {
                    nMarketPrice = Math.Round((double)nDr["BidPrice1"], 1);
                }


                CloseOutPlaceOrderWindowOC[i].MarketPrice = nMarketPrice;
            }
        }


        public void OnAddToCloseOutPlaceOrderWindowOC(string _userID, string _instrumentID, string _s_tradingType, int _tradingNum, int _clientageType, string _clientagePrice, double _marketPrice)
        {
            TradingData add_tradingData = new TradingData(_userID, _instrumentID, _s_tradingType, _tradingNum, _clientageType, _clientagePrice, _marketPrice);
            CloseOutPlaceOrderWindowOC.Add(add_tradingData);
        }


        public void HandleCloseOut()
        {
            for (int i = 0; i < MainWindow.pm.PositionsOC.Count(); i++)
            {
                PositionsData h_pd = MainWindow.pm.PositionsOC[i];

                if (h_pd.IsChoose == true)
                {
                    string c_userID = h_pd.UserID;

                    string c_instrumentID = h_pd.InstrumentID;

                    string c_s_tradingType = "";

                    if (h_pd.BuyOrSell.Equals("买"))
                    {
                       c_s_tradingType="卖";  //与所要平仓的相反
                    }
                    else
                    {
                        c_s_tradingType="买";
                    }

                     int c_tradingNum = -h_pd.TradingNum;

                    DataRow nDr = (DataRow)DataManager.All[c_instrumentID];

                    double c_marketPrice = 0;

                    if(c_s_tradingType.Equals("买"))
                    {
                        c_marketPrice = Convert.ToDouble(nDr["AskPrice1"]);
                    }else
                    {
                        c_marketPrice = Convert.ToDouble(nDr["BidPrice1"]);
                    }

                    OnAddToCloseOutPlaceOrderWindowOC(c_userID, c_instrumentID, c_s_tradingType, c_tradingNum, 0, "-", c_marketPrice);

                }
            }
        }


        public void LimitTradingNum()
        {
            for (int i = 0; i < CloseOutPlaceOrderWindowOC.Count(); i++)
            {
                TradingData l_td = CloseOutPlaceOrderWindowOC[i];
                int canBuy = SomeCalculate.calculateCanBuyNum(l_td, CloseOutPlaceOrderWindowOC);

                if (l_td.S_tradingType.Equals("买"))
                {
                    if (l_td.TradingNum > canBuy)
                    {
                        l_td.TradingNum = canBuy;
                    }
                }
                else
                {
                    if (Math.Abs(l_td.TradingNum) > canBuy)
                    {
                        l_td.TradingNum = -canBuy;
                    }
                }


            }
        }


        public void changeBuyOrSellByTradingNum()
        {
            for (int i = 0; i < CloseOutPlaceOrderWindowOC.Count(); i++)
            {
                TradingData l_td = CloseOutPlaceOrderWindowOC[i];

                if (l_td.TradingNum < 0 && l_td.S_tradingType.Equals("买"))
                {
                    l_td.TradingNum = 0;
                }

                if (l_td.TradingNum > 0 && l_td.S_tradingType.Equals("卖"))
                {
                    l_td.TradingNum=0;
                }
            }

        }


        public void CloseOutOCToHistory()
        {
            for (int i = CloseOutPlaceOrderWindowOC.Count - 1; i >= 0; i--)
            {
                TradingData i_Td = CloseOutPlaceOrderWindowOC[i];

                DataRow nDr = (DataRow)DataManager.All[i_Td.InstrumentID];

                string oInstrumentID = i_Td.InstrumentID;

                string oTradingTime = DataManager.now.ToString();

                string oTradingType = i_Td.S_tradingType;

                int oPostNum = i_Td.TradingNum;

                int oDoneNum = 0;

                string oTradingState = HistoryManager.POST;   //初始化为挂单状态

                double oPostPrice = 0;

                if (i_Td.ClientageType == 0)
                {
                    oPostPrice = i_Td.MarketPrice;
                }
                else
                {
                    oPostPrice =Convert.ToDouble(i_Td.ClientagePrice);
                }

                double oDonePrice = 0;

                string oTimeLimit = nDr["LastDate"].ToString();

                string oUserID = i_Td.UserID;

                

                bool oOptionOrFuture = true;

                if (Convert.ToInt32(nDr["OptionOrFuture"]) == 0)
                {
                    oOptionOrFuture = false;
                }
                else
                {
                    oOptionOrFuture = true;
                }

                string oClientageType ="";

                string oClientageCondition = "";

                if (i_Td.ClientageType == 0)
                {
                    oClientageType = "市价";
                    oClientageCondition = "IOC";
                }
                else
                {
                    oClientageType = "限价";
                    oClientageCondition = "ROD";
                }

                pWindow.hm.OnAdd(oInstrumentID, oTradingTime, oTradingType, oPostNum, oDoneNum, oTradingState, oPostPrice, oDonePrice, oTimeLimit, oUserID, oClientageCondition, oOptionOrFuture, oClientageType);

            }
        }





    }
}
