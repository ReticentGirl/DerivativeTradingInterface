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
    public class PlaceOrderData : INotifyPropertyChanged
    {

        private string userID;   //投资账户
        private string instrumentID;   //合约代码
        //private string callOrPut;    //看涨（0/false）看跌（1/true）
        //private string exercisePrice;  //行权价

        private string tradingType;  //交易类型   0 买  1 卖

        private int tradingNum;   //交易手数

        private string clientageType;   //委托方式    0 市价  限价 1

        private double clientagePrice; //委托价格

        private double marketPrice;   //市场价格

        private string clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消

        private double finalPrice;   //根据市价或者限价 来显示价格

        private bool optionOrFuture;

        private bool isBuy;

        private bool isFromGradient;    //是否是由于 使用了等差梯度 加入到 交易盒子中的


        public bool IsFromGradient
        {
            get { return isFromGradient; }
            set
            {
                isFromGradient = value;
                OnPropertyChanged("IsFromGradient");
            }
        }


        public bool IsBuy
        {
            get { return isBuy; }
            set
            {
                isBuy = value;
                OnPropertyChanged("OptionOrFuture");
            }
        }

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
                                 bool _optionOrFuture,
                                 bool _isBuy,
                                 bool _isFromGradient
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
             isBuy = _isBuy;
             isFromGradient = _isFromGradient;
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



    public class OrderCapitalData : INotifyPropertyChanged
    {
        private double royalty;    //占用资金
        private double fee;              //手续费
        private double margin;     //保证金

        public double Royalty
        {
            get { return royalty; }
            set
            {
                royalty = value;
                OnPropertyChanged("Royalty");
            }
        }


        public double Fee
        {
            get { return fee; }
            set
            {
                fee = value;
                OnPropertyChanged("Fee");
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

        public OrderCapitalData()
        {
           
        }



            public OrderCapitalData(double _royalty, double _fee, double _margin)
            {
                royalty = _royalty;
                fee = _fee;
                margin = _margin;
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


        

 

    public class PlaceOrderManager 
    {
        public ObservableCollection<PlaceOrderData> OrderOC = new ObservableCollection<PlaceOrderData>();

       // public ObservableCollection<OccupyCapitalData> OccupyOC = new ObservableCollection<OccupyCapitalData>(); 

            PlaceOrder pOrderWindow;    //下单盒子指针

            System.Timers.Timer placeOrderTimer; //刷新下单盒子的计时器

            OrderCapitalData orderOccupy = new OrderCapitalData(0,0,0);

            //static double eachFee = 5;   //初定每笔手续费5元


            public PlaceOrderManager(PlaceOrder _pOrderWindow)
        {
            pOrderWindow = _pOrderWindow;

            pOrderWindow.tradingListView.ItemsSource=OrderOC;

            pOrderWindow.costGrid.DataContext = orderOccupy;

            placeOrderTimer = new System.Timers.Timer(500);

            placeOrderTimer.Start();

            placeOrderTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            
           // OrderOC.Add(new PlaceOrderData("dffsd","ewfwefw","看涨",123,"dsfsdf",123,"cdscs",1234,2134,"dsfsdf"));

            

        }


            public void OnAdd(string _userID, string _instrumentID, string _tradingType, int _tradingNum, string _clientageType, double _clientagePrice, double _marketPrice, string _clientageCondition, double _finalPrice, bool _optionOrFuture, bool _isBuy, bool _isFromGradient)
        {

            OrderOC.Add(new PlaceOrderData(_userID, _instrumentID, _tradingType, _tradingNum, _clientageType, _clientagePrice, _marketPrice, _clientageCondition, _finalPrice, _optionOrFuture, _isBuy, _isFromGradient));
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            if (OrderOC.Count() > 0)
            {
                double[] royaltyArry = new double[OrderOC.Count()];
                double[] feeArry = new double[OrderOC.Count()];
                double[] marginArry = new double[OrderOC.Count()];

                for (int i = 0; i < OrderOC.Count(); i++)
                {
                    string reInstrumentID = OrderOC[i].InstrumentID;

                   
                    DataRow nDr = (DataRow)DataManager.All[reInstrumentID];

                    double nMarketPrice = 0;

                    //string tt = OrderOC[i].CallOrPut;

                    if (OrderOC[i].TradingType.Equals("买"))     //选择买价刷新
                        nMarketPrice = Math.Round((double)nDr["AskPrice1"], 1);
                    else if (OrderOC[i].TradingType.Equals("卖"))
                        nMarketPrice = Math.Round((double)nDr["BidPrice1"], 1);

                    OrderOC[i].MarketPrice = nMarketPrice;
                    if (OrderOC[i].ClientageType.Equals("市价"))
                        OrderOC[i].FinalPrice = nMarketPrice;



                    ///////////////////////////////
                    //double royalty = caculateRoyalty(nDr, reInstrumentID, OrderOC[i].OptionOrFuture, OrderOC[i].IsBuy, OrderOC[i].TradingNum, OrderOC[i].FinalPrice);
                    double royalty = SomeCalculate.caculateRoyalty(reInstrumentID, OrderOC[i].TradingNum, OrderOC[i].IsBuy, OrderOC[i].FinalPrice);

                    royaltyArry[i] = royalty;

                    feeArry[i]=SomeCalculate.calculateFees(reInstrumentID,OrderOC[i].TradingNum,OrderOC[i].FinalPrice);

                    //feeArry[i] = Math.Abs(OrderOC[i].TradingNum) * eachFee;


                    marginArry[i] = SomeCalculate.caculateMargin(reInstrumentID, OrderOC[i].TradingNum, OrderOC[i].IsBuy, OrderOC[i].FinalPrice);
                    //marginArry[i] = caculateMargin(nDr, reInstrumentID, OrderOC[i].OptionOrFuture, OrderOC[i].IsBuy, OrderOC[i].TradingNum, OrderOC[i].FinalPrice);
                    //orderOccupy.OccupyCapital += cost;

                    
                }
                orderOccupy.Royalty = 0;  //每次清理为零

                orderOccupy.Fee = 0;   //每次清理为零

                orderOccupy.Margin = 0;  //每次清理为零


                for (int j = 0; j < OrderOC.Count(); j++)
                {
                    orderOccupy.Royalty += royaltyArry[j];   //将所有权利金加起来

                    orderOccupy.Fee += feeArry[j];     //将所有手续费加起来

                    orderOccupy.Margin += marginArry[j];   //将所有的保证金加起来

                }

            }
        }

        /*
        double caculateRoyalty(DataRow _nDr, string _InstrumentID, bool _optionOrFuture, bool _isBuy, int _tradingNum, double _finalPrice)    //计算花费
        {
            if (_optionOrFuture == false)  //表示是期权   
            {
                int instrumentMultiplier = Convert.ToInt32(_nDr["InstrumentMultiplier"]);

                double cost = Math.Abs(_tradingNum) * instrumentMultiplier * _finalPrice;

                if (_isBuy==true)   //买返回正，卖返回负   正表示支出  负表示获得
                {
                    return cost;
                }
                else
                {
                    return -cost;
                }

            }
            else    //期货不用权利金
            {
                return 0;
            }
         
        }
        */

        /*
        double caculateMargin(DataRow _nDr, string _InstrumentID, bool _optionOrFuture, bool _isBuy, int _tradingNum, double _finalPrice)
        {
            if (_optionOrFuture == false)   //对于卖家期货保证金计算
            {
                if (_isBuy == false)   //卖期权  要交保证金
                {
                    int t_callOrPut = Convert.ToInt32(_nDr["CallOrPut"]);

                    //////////
                    //////////////////////////////以下数据从ALL获取//////////////////////


                    double t_PreClosePrice = Math.Round((double)_nDr["PreClosePrice"], 1);  //昨收盘价

                    double t_PreSettlementPrice = Math.Round((double)_nDr["PreSettlementPrice"], 1);  //前结算价

                    double t_ExercisePrice = Math.Round((double)_nDr["ExercisePrice"], 1);


                    double MarginAdjust = 0.1;//股指期权保证金调整系数  
                    double MiniGuarantee = 0.5;//最低保障系数  

                    //int VolumeMultiple = 100;
                    int instrumentMultiplier = Convert.ToInt32(_nDr["InstrumentMultiplier"]);   //合约乘数  


                    double dummy = Math.Max(t_ExercisePrice - t_PreClosePrice, 0.0);//虚值额  



                    if (t_callOrPut == 0)  //看涨
                    {

                        

                        double c_margin = (t_PreSettlementPrice + t_PreClosePrice * Math.Max(MarginAdjust - dummy / t_PreClosePrice, MarginAdjust * MiniGuarantee)) * instrumentMultiplier;//保证金  

                        return c_margin;

                    }
                    else if (t_callOrPut == 1)   //看跌
                    {
                        double p_margin = (t_PreSettlementPrice + Math.Max(t_PreClosePrice * MarginAdjust - dummy, t_ExercisePrice * MarginAdjust * MiniGuarantee)) * instrumentMultiplier;

                        return p_margin;

                    }
                    else
                    {
                        return 0;
                    }
                }
                else    //买期权 不用保证金
                {
                    return 0;
                }
                
            }
            else     //期货保证金计算
            {
                double marginRate = Convert.ToDouble(_nDr["GuaranteeMoney"]) ;   //保证金率

                double instrumentMultiplier = Convert.ToInt32(_nDr["InstrumentMultiplier"]);

                double t_margin = _finalPrice * instrumentMultiplier * Math.Abs(_tradingNum) * marginRate; //交易价格*交易单位*交易手数*保证金收取比例

                return t_margin;
                //return 0;
            }
        }
        */
        

    }
}
