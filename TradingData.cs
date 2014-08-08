using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;

namespace qiquanui
{
    public class TradingData : INotifyPropertyChanged
    {
        //public static string ccUsed ="Visible";
        //public static string ccNotUsed = "Collapsed";


        private bool ifChooseOTGVCH;  //是否选择了
        private string userID;   //投资账户
        private string instrumentID;   //合约代码
        private string callOrPut;    //看涨（0/false）看跌（1/true）
        private string exercisePrice;  //行权价

        private int tradingType;  //交易类型   0 买开  1 卖开  2买平 3卖平


        private int tradingNum;   //交易手数
        private int clientageType;   //委托方式    0 市价  限价
        private string clientagePrice; //委托价格
        private double marketPrice;   //市场价格
        private int clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消

        private bool isSetGradient;     //是否设置梯度
        private double accuracy;     //精确度
        private int arithmeticProgression;  //  手数等差

        private bool isBuy;  //用来确定是买还是卖 

        private bool optionOrFuture;   //期权(false)或者期货(true)

        private int typeChangeCount;   //是按下的那一次导致的 买开卖开买平卖平 的变换

        private bool isEnableOfClientagePrice;    //委托价格是否可用

        Visibility aboutROD ;

        Visibility aboutFOK;

        Visibility aboutIOC;


        public bool IsEnableOfClientagePrice
        {
            get { return isEnableOfClientagePrice; }
            set
            {

                isEnableOfClientagePrice = value;
                OnPropertyChanged("IsEnableOfClientagePrice");
            }
        }


        public Visibility AboutROD
        {
            get { return aboutROD; }
            set
            {

                aboutROD = value;
                OnPropertyChanged("AboutROD");
            }
        }

        public Visibility AboutFOK
        {
            get { return aboutFOK; }
            set
            {

                aboutFOK = value;
                OnPropertyChanged("AboutFOK");
            }
        }

        public Visibility AboutIOC
        {
            get { return aboutIOC; }
            set
            {

                aboutIOC = value;
                OnPropertyChanged("AboutIOC");
            }
        }

     
        public int TypeChangeCount
        {
            get { return typeChangeCount; }
            set
            {

                typeChangeCount = value;
                OnPropertyChanged("ClickButton");
            }
        }


        public bool IfChooseOTGVCH
        {
            get { return ifChooseOTGVCH; }
            set
            {
               
                ifChooseOTGVCH = value;
                OnPropertyChanged("IfChooseOTGVCH"); 
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

        public string CallOrPut
        {
            get { return callOrPut; }
            set { callOrPut = value;
                  OnPropertyChanged("CallOrPut");        
                }
        }

        public string ExercisePrice
        {
            get { return exercisePrice; }
            set { exercisePrice = value;
                  OnPropertyChanged("ExercisePrice"); 
                }
        }

        public int TradingType
        {
            get { return tradingType; }
            set { tradingType = value;
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



        public int ClientageType
        {
            get { return clientageType; }
            set { clientageType = value;
            OnPropertyChanged("ClientageType");
            }
        }


        public string ClientagePrice
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

        public int ClientageCondition
        {
            get { return clientageCondition; }
            set { clientageCondition = value;
            OnPropertyChanged("ClientageCondition"); 
            }
        }

        public bool IsSetGradient
        {
            get { return isSetGradient; }
            set { isSetGradient = value;
            OnPropertyChanged("IsSetGradient"); 
            }
        }

        public double Accuracy
        {
            get { return accuracy; }
            set { accuracy = value;
            OnPropertyChanged("Accuracy"); 
            }
        }


        public int ArithmeticProgression
        {
            get { return arithmeticProgression; }
            set { arithmeticProgression = value;
            OnPropertyChanged("ArithmeticProgression"); 
            }
        }

        public bool IsBuy
        {
            get { return isBuy; }
            set
            {
                isBuy = value;
                OnPropertyChanged("IsBuy");
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


       
         public TradingData()
        { }

         public TradingData(
                                  string _userID,
                                  string _instrumentID,   
                                  string _callOrPut,
                                  string _exercisePrice,   
                                  double _marketPrice,   
                                  bool _isBuy, 
                                  bool _optionOrFuture                             
                               )
         {
              userID = _userID;
              instrumentID = _instrumentID;
              callOrPut = _callOrPut;  
              exercisePrice = _exercisePrice;
              marketPrice = _marketPrice;

              clientagePrice = "-";   //将委托价默认"-"

              isBuy = _isBuy;

              optionOrFuture = _optionOrFuture;

             ///不用到的在这里初始化
             ///


              isEnableOfClientagePrice = false;
              
              ifChooseOTGVCH = true;

              if (_isBuy == true)
              {
                  tradingType = 0;  //初始化为买开
                  tradingNum = 1;

              }
              else
              {
                  tradingNum = -1;
                  tradingType = 1;
              }


              if (optionOrFuture == false)    //如果是期权
              {
                  clientageCondition = 2;  //默认为 IOC

                  aboutROD = Visibility.Collapsed;   //又因为是市价 所有 没有ROD

              }
              else    //期货默认为ROD
              {
                  clientageCondition = 0;

                  aboutFOK = Visibility.Collapsed;    //期货没有这两个
                  aboutIOC = Visibility.Collapsed;
              }

              

              typeChangeCount = 0;

         }

         public TradingData(
                                 string _userID,
                                 string _instrumentID,
                                 string _callOrPut,
                                 string _exercisePrice,
                                 int _tradingType,
                                 int _tradingNum,
                                 int _clientageType,
                                 string _clientagePrice,
                                 double _marketPrice,
                                 int _clientageCondition,
                                 bool _isSetGradient,
                                 double _accuracy,
                                 int _arithmeticProgression, 
                                 bool _isBuy,
                                 bool _optionOrFuture                                 
                              )
         {
             userID = _userID;
             instrumentID = _instrumentID;
             callOrPut = _callOrPut;
             exercisePrice = _exercisePrice;
             tradingType = _tradingType;
             tradingNum = _tradingNum;
             clientageType = _clientageType;
             clientagePrice = _clientagePrice;
             marketPrice = _marketPrice;
             clientageCondition = _clientageCondition;
             isSetGradient = _isSetGradient;
             accuracy = _accuracy;
             arithmeticProgression = _arithmeticProgression;

             isBuy = _isBuy;

             optionOrFuture = _optionOrFuture;


             //////不用到的在这里初始化
            
             ifChooseOTGVCH = true;

             isEnableOfClientagePrice = false;
            
             typeChangeCount = 0;  
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
}
