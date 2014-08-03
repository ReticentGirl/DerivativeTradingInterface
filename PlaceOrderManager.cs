using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Data;

using System.Windows.Data;

namespace qiquanui
{
    class PlaceOrderData : INotifyPropertyChanged
    {

        private string userID;   //投资账户
        private string instrumentID;   //合约代码
        //private string callOrPut;    //看涨（0/false）看跌（1/true）
        //private string exercisePrice;  //行权价

        private string tradingType;  //交易类型   0 买开  1 卖开  2买平 3卖平

        private int tradingNum;   //交易手数

        private string clientageType;   //委托方式    0 市价  限价 1

        private double clientagePrice; //委托价格

        private double marketPrice;   //市场价格

        private string clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消

        private double finalPrice;   //根据市价或者限价 来显示价格

        private bool optionOrFuture;


        public bool OptionOrFuture
        {
            get { return optionOrFuture; }
            set
            {
                optionOrFuture = value;
                OnPropertyChanged("OptionOrFuture");
            }
        }



        public string UserID
        {
            get { return userID; }
            set { userID = value;
            OnPropertyChanged("UserID"); 
            }
        }


        public string InstrumentID
        {
            get { return instrumentID; }
            set { instrumentID = value;
                  OnPropertyChanged("InstrumentID"); 
                 }
        }

        //public string CallOrPut
        //{
        //    get { return callOrPut; }
        //    set { callOrPut = value;
        //          OnPropertyChanged("CallOrPut");        
        //        }
        //}

        //public double ExercisePrice
        //{
        //    get { return exercisePrice; }
        //    set { exercisePrice = value;
        //          OnPropertyChanged("ExercisePrice"); 
        //        }
        //}

        public string TradingType
        {
            get { return tradingType; }
            set
            {
                tradingType = value;
                OnPropertyChanged("TradingType");
            }
        }

        public int TradingNum
        {
            get { return tradingNum; }
            set { tradingNum = value;
            OnPropertyChanged("TradingNum");
            }
        }



        public string ClientageType
        {
            get { return clientageType; }
            set { clientageType = value;
            OnPropertyChanged("ClientageType");
            }
        }


        public double ClientagePrice
        {
            get { return clientagePrice; }
            set { clientagePrice = value;
            OnPropertyChanged("ClientagePrice");
            }
        }



        public double MarketPrice
        {
            get { return marketPrice; }
            set { marketPrice = value;
                  OnPropertyChanged("MarketPrice"); 
                }
        }

        public string ClientageCondition
        {
            get { return clientageCondition; }
            set { clientageCondition = value;
            OnPropertyChanged("ClientageCondition"); 
            }
        }

        public double FinalPrice
        {
            get { return finalPrice; }
            set
            {
                finalPrice = value;
                OnPropertyChanged("FinalPrice");
            }
        }

     

       
         public PlaceOrderData()
        { }

        public PlaceOrderData(
                                 string _userID,
                                 string _instrumentID,
                                 string _tradingType,
                                 int _tradingNum,
                                 string _clientageType,
                                 double _clientagePrice,
                                 double _marketPrice,
                                 string _clientageCondition,
                                 double _finalPrice,
                                 bool _optionOrFuture
                              )
         {
             userID = _userID;
             instrumentID = _instrumentID;
             tradingType = _tradingType;
             tradingNum = _tradingNum;
             clientageType = _clientageType;
             clientagePrice = _clientagePrice;
             marketPrice = _marketPrice;
             clientageCondition = _clientageCondition;
             finalPrice = _finalPrice;
             optionOrFuture = _optionOrFuture;
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



    class OrderCapitalData : INotifyPropertyChanged
    {
        private double occupyCapital;    //占用资金

        public double OccupyCapital
        {
            get { return occupyCapital; }
            set
            {
                occupyCapital = value;
                OnPropertyChanged("OccupyCapital");
            }
        }

        public OrderCapitalData()
        {
           
        }



            public OrderCapitalData(double _occupyCapital)
            {
                occupyCapital=_occupyCapital;
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


        

 

    class PlaceOrderManager 
    {
        public ObservableCollection<PlaceOrderData> OrderOC = new ObservableCollection<PlaceOrderData>();

       // public ObservableCollection<OccupyCapitalData> OccupyOC = new ObservableCollection<OccupyCapitalData>(); 

            PlaceOrder pOrderWindow;    //下单盒子指针

            System.Timers.Timer placeOrderTimer; //刷新下单盒子的计时器

            OrderCapitalData orderOccupy = new OrderCapitalData(1110);
            



            public PlaceOrderManager(PlaceOrder _pOrderWindow)
        {
            pOrderWindow = _pOrderWindow;

            pOrderWindow.tradingListView.ItemsSource = OrderOC;

            placeOrderTimer = new System.Timers.Timer(500);

            placeOrderTimer.Start();

            placeOrderTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            
           // OrderOC.Add(new PlaceOrderData("dffsd","ewfwefw","看涨",123,"dsfsdf",123,"cdscs",1234,2134,"dsfsdf"));

            

        }


        public void OnAdd(string _userID,string _instrumentID,string _tradingType,int _tradingNum,string _clientageType,double _clientagePrice,double _marketPrice,string _clientageCondition,double _finalPrice,bool _optionOrFuture)
        {

            OrderOC.Add(new PlaceOrderData(_userID, _instrumentID, _tradingType, _tradingNum, _clientageType, _clientagePrice, _marketPrice, _clientageCondition, _finalPrice, _optionOrFuture));
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            if (OrderOC.Count() > 0)
            {
                for (int i = 0; i < OrderOC.Count(); i++)
                {
                    string reInstrumentID = OrderOC[i].InstrumentID;

                    DataRow nDr = (DataRow)DataManager.All[reInstrumentID];

                    double nMarketPrice = 0;

                    //string tt = OrderOC[i].CallOrPut;

                    if (OrderOC[i].TradingType.Equals("买开") || OrderOC[i].TradingType.Equals("买平"))     //选择买价刷新
                        nMarketPrice = Math.Round((double)nDr["BidPrice1"], 1);
                    else if (OrderOC[i].TradingType.Equals("卖开") || OrderOC[i].TradingType.Equals("卖平"))
                        nMarketPrice = Math.Round((double)nDr["AskPrice1"], 1);

                    OrderOC[i].MarketPrice = nMarketPrice;
                    if (OrderOC[i].ClientageType.Equals("市价"))
                        OrderOC[i].FinalPrice = nMarketPrice;

                    orderOccupy.OccupyCapital += 1;
                }
            }
        }

        

    }
}
