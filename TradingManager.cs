using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace qiquanui
{
    class TradingManager 
    {

        public ObservableCollection<TradingData> OptionsTradingOC = new ObservableCollection<TradingData>();

        MainWindow pwindow;    //主窗体指针


        public TradingManager(MainWindow _pwindow)
        {
            pwindow = _pwindow;

            OptionsTradingOC.Add(new TradingData( "huaqibsssssssssssssssssssei", "au1408","看涨", 230, 240));
            
            OptionsTradingOC.Add(new TradingData( "xuebashishabi", "au1407","看涨", 220, 220));


            pwindow.tradingListView.ItemsSource = OptionsTradingOC;
            
        }


        public void OnAdd()
       {
          
       
       }        
     }

     

    

}
