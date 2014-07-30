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
    class TradingManager 
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


        public void OnAdd(string _userID,string instrumentID,string _callOrPut,double _exercisePrice,double _marketPrice)
       {
           TradingOC.Add(new TradingData(_userID, instrumentID, _callOrPut, _exercisePrice, _marketPrice));
       }


        public void Refresh()
        {
            if (TradingOC.Count() > 0)
            {
                for (int i = 0; i < TradingOC.Count(); i++)
                {
                    string reInstrumentID = TradingOC[i].InstrumentID;

                    DataRow nDr = (DataRow)DataManager.All[reInstrumentID];

                    double nMarketPrice = Math.Round((double)nDr["BidPrice1"], 1);

                    TradingOC[i].MarketPrice = nMarketPrice;
                   
                }
            }
        }
     
    
    }

     

    

}
