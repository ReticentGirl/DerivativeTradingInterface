using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Timers;

namespace qiquanui
{
    public class TradingManager
    {

        public ObservableCollection<TradingData> TradingOC = new ObservableCollection<TradingData>();

        MainWindow pwindow;    //主窗体指针

        System.Timers.Timer tradingTimer; //刷新交易区的计时器


        public TradingManager(MainWindow _pwindow)
        {
            pwindow = _pwindow;

            pwindow.tradingListView.ItemsSource = TradingOC;

            tradingTimer = new System.Timers.Timer(500);

            tradingTimer.Start();

            tradingTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);


            //TradingOC.Add(new TradingData("huaqibsssssssssssssssssssei", "au1408", "看涨", 230, 240));

            //TradingOC.Add(new TradingData("xuebashishabi", "au1407", "看涨", 220, 220));


            //TradingOC.Add(new TradingData("dsfsfsdf", "au1409", "看涨", 220, 220));

        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }




        public void OnAdd(string _userID, string instrumentID, string _callOrPut, string _exercisePrice, double _marketPrice, bool _isBuy, bool _optionOrFuture)
        {
            TradingData add_td = new TradingData(_userID, instrumentID, _callOrPut, _exercisePrice, _marketPrice, _isBuy, _optionOrFuture);
            //TurnOver();
            TradingOC.Add(add_td);
            //TurnOver();
            //add_td.ClickButton = false;    //点击按钮之后 这个属性改为false

        }


        public void AddTrading(string _instrumentID, bool _isBuy, int _tradingNum)
        {
            string a_userID = pwindow.userComboBox.Text;

            string a_instrumentID = _instrumentID;

            DataRow aDr = (DataRow)DataManager.All[a_instrumentID];

            if (Convert.ToInt32(aDr["OptionOrFuture"]) == 0)  //期权
            {
                string a_callOrPut = "";

                if (Convert.ToInt32(aDr["CallOrPut"]) == 0)
                {
                    a_callOrPut = "看涨";
                }
                else
                {
                    a_callOrPut = "看跌";
                }

                double a_exercisePrice = Convert.ToDouble(aDr["ExercisePrice"]);

                double a_marketPrice = 0;

                if (_isBuy == true)
                {
                    a_marketPrice = Convert.ToDouble(aDr["AskPrice1"]);
                }
                else
                {
                    a_marketPrice = Convert.ToDouble(aDr["BidPrice1"]);
                }

                bool a_optionOrFuture = false;   //期权

                TradingData add_td = new TradingData(a_userID, a_instrumentID, a_callOrPut, a_exercisePrice.ToString(), a_marketPrice, _isBuy, a_optionOrFuture, _tradingNum);

                TradingOC.Add(add_td);

            }
            else
            {
                string a_callOrPut = "-";

                string a_exercisePrice = "-";

                double a_marketPrice = 0;
                
                if (_isBuy == true)
                {
                    a_marketPrice = Convert.ToDouble(aDr["AskPrice1"]);
                }
                else
                {
                    a_marketPrice = Convert.ToDouble(aDr["BidPrice1"]);
                }

                bool a_optionOrFuture = true;

                TradingData add_td = new TradingData(a_userID, a_instrumentID, a_callOrPut, a_exercisePrice, a_marketPrice, _isBuy, a_optionOrFuture);

                TradingOC.Add(add_td);

            }

          

            
        }


        public void AddTradingForCloseOut(string _userID,string _instrumentID, bool _isBuy, int _tradingNum)
        {
            string a_userID = _userID;

            string a_instrumentID = _instrumentID;

            DataRow aDr = (DataRow)DataManager.All[a_instrumentID];

            if (Convert.ToInt32(aDr["OptionOrFuture"]) == 0)  //期权
            {
                string a_callOrPut = "";

                if (Convert.ToInt32(aDr["CallOrPut"]) == 0)
                {
                    a_callOrPut = "看涨";
                }
                else
                {
                    a_callOrPut = "看跌";
                }

                double a_exercisePrice = Convert.ToDouble(aDr["ExercisePrice"]);

                double a_marketPrice = 0;

                if (_isBuy == true)
                {
                    a_marketPrice = Convert.ToDouble(aDr["AskPrice1"]);
                }
                else
                {
                    a_marketPrice = Convert.ToDouble(aDr["BidPrice1"]);
                }

                bool a_optionOrFuture = false;   //期权

                TradingData add_td = new TradingData(a_userID, a_instrumentID, a_callOrPut, a_exercisePrice.ToString(), a_marketPrice, _isBuy,_tradingNum,a_optionOrFuture);

                TradingOC.Add(add_td);

            }
            else
            {
                string a_callOrPut = "-";

                string a_exercisePrice = "-";

                double a_marketPrice = 0;

                if (_isBuy == true)
                {
                    a_marketPrice = Convert.ToDouble(aDr["AskPrice1"]);
                }
                else
                {
                    a_marketPrice = Convert.ToDouble(aDr["BidPrice1"]);
                }

                bool a_optionOrFuture = true;

                TradingData add_td = new TradingData(a_userID, a_instrumentID, a_callOrPut, a_exercisePrice, a_marketPrice, _isBuy, _tradingNum, a_optionOrFuture);

                TradingOC.Add(add_td);

            }

        }


        public void Refresh()
        {
            if (TradingOC.Count() > 0)
            {
                for (int i = 0; i < TradingOC.Count(); i++)
                {

                    string reInstrumentID = TradingOC[i].InstrumentID;

                    DataRow nDr = (DataRow)DataManager.All[reInstrumentID];

                    double nMarketPrice = 0;


                    if (TradingOC[i].IsBuy == true)     //选择买价刷新
                    {

                        nMarketPrice = Math.Round((double)nDr["AskPrice1"], 1);

                    }
                    else if (TradingOC[i].IsBuy == false)
                    {
                        nMarketPrice = Math.Round((double)nDr["BidPrice1"], 1);

                    }


                    TradingOC[i].MarketPrice = nMarketPrice;

                    //double jj = nMarketPrice;



                    // int iiii = 1;

                }
            }
        }




        public void LimitTradingNum()
        {
            for (int i = 0; i < TradingOC.Count(); i++)
            {
                TradingData l_td = TradingOC[i];
                int canBuy = SomeCalculate.calculateCanBuyNum(l_td, TradingOC);
                if (l_td.IsBuy == true)
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
            for (int i = 0; i < TradingOC.Count(); i++)
            {
                TradingData l_td = TradingOC[i];

                if (l_td.TradingNum > 0 && l_td.IsBuy != true)
                {
                    l_td.TradingType = 0;
                }

                if (l_td.TradingNum < 0 && l_td.IsBuy != false)
                {
                    l_td.TradingType = 1;
                }
            }

        }

        
        /*
        public void TurnOver()
        {
            for (int i = 0; i < TradingOC.Count() / 2; i++)
            {
                TradingData head_td = TradingOC[i];

                TradingData tail_td = TradingOC[TradingOC.Count() - 1 - i];

                bool temp_ifChooseOTGVCH = head_td.IfChooseOTGVCH;
                string temp_userID = head_td.UserID;
                string temp_instrumentID = head_td.InstrumentID;
                string temp_callOrPut = head_td.CallOrPut;
                string temp_exercisePrice = head_td.ExercisePrice;
                int temp_tradingType = head_td.TradingType;
                int temp_tradingNum = head_td.TradingNum;
                int temp_clientageType = head_td.ClientageType;
                double temp_clientagePrice = head_td.ClientagePrice;
                double temp_marketPrice = head_td.MarketPrice;
                int temp_clientageCondition = head_td.ClientageCondition;
                bool temp_isSetGradient = head_td.IsSetGradient;
                double temp_accuracy = head_td.Accuracy;
                int temp_arithmeticProgression = head_td.ArithmeticProgression;
                bool temp_isBuy = head_td.IsBuy;
                bool temp_optionOrFuture = head_td.OptionOrFuture;
                int temp_typeChangeCount = head_td.TypeChangeCount;
                ///////////////////////////////////////////////////////////////////////////////////////////
                head_td.IfChooseOTGVCH = tail_td.IfChooseOTGVCH;
                head_td.UserID = tail_td.UserID;
                head_td.InstrumentID = tail_td.InstrumentID;
                head_td.CallOrPut = tail_td.CallOrPut;
                head_td.ExercisePrice = tail_td.ExercisePrice;
                head_td.TradingType = tail_td.TradingType;
                head_td.TradingNum = tail_td.TradingNum;
                head_td.ClientageType = tail_td.ClientageType;
                head_td.ClientagePrice = tail_td.ClientagePrice;
                head_td.MarketPrice = tail_td.MarketPrice;
                head_td.ClientageCondition = tail_td.ClientageCondition;
                head_td.IsSetGradient = tail_td.IsSetGradient;
                head_td.Accuracy = tail_td.Accuracy;
                head_td.ArithmeticProgression = tail_td.ArithmeticProgression;
                head_td.IsBuy = tail_td.IsBuy;
                head_td.OptionOrFuture = tail_td.OptionOrFuture;
                head_td.TypeChangeCount = tail_td.TypeChangeCount;

                ////////////////////////////////////////////////////////////////////////////////////////

                tail_td.IfChooseOTGVCH = temp_ifChooseOTGVCH;
                tail_td.UserID = temp_userID;
                tail_td.InstrumentID = temp_instrumentID;
                tail_td.CallOrPut = temp_callOrPut;
                tail_td.ExercisePrice = temp_exercisePrice;
                tail_td.TradingType = temp_tradingType;
                tail_td.TradingNum = temp_tradingNum;
                tail_td.ClientageType = temp_clientageType;
                tail_td.ClientagePrice = temp_clientagePrice;
                tail_td.MarketPrice = temp_marketPrice;
                tail_td.ClientageCondition = temp_clientageCondition;
                tail_td.IsSetGradient = temp_isSetGradient;
                tail_td.Accuracy = temp_accuracy;
                tail_td.ArithmeticProgression = temp_arithmeticProgression;
                tail_td.IsBuy = temp_isBuy;
                tail_td.OptionOrFuture = temp_optionOrFuture;
                tail_td.TypeChangeCount = temp_typeChangeCount;

            }
        }
        */


    }





}
