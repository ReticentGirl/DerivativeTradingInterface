﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace qiquanui
{
    class TradingData : INotifyPropertyChanged
    {
        
        private bool ifChooseOTGVCH;  //是否选择了
        private string userID;   //投资账户
        private string instrumentID;   //合约代码
        private string callOrPut;    //看涨（0/false）看跌（1/true）
        private double exercisePrice;  //行权价

        private int tradingType;  //交易类型   0 买开  1 卖开  2买平 3卖平


        private int tradingNum;   //交易手数
        private int clientageType;   //委托方式    0 市价  限价
        private double clientagePrice; //委托价格
        private double marketPrice;   //市场价格
        private int clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消

        private bool isSetGradient;     //是否设置梯度
        private double accuracy;     //精确度
        private int arithmeticProgression;  //  手数等差


        



      


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

        public double ExercisePrice
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


       
         public TradingData()
        { }

         public TradingData(
                                  string _userID,
                                  string _instrumentID,   
                                  string _callOrPut,    
                                  double _exercisePrice,   
                                  double _marketPrice   
                               )
         {
              userID = _userID;
              instrumentID = _instrumentID;
              callOrPut = _callOrPut;  
              exercisePrice = _exercisePrice;
              marketPrice = _marketPrice;

              clientagePrice = _marketPrice;   //将委托价默认为市场价

             ///不用到的在这里初始化

              
              ifChooseOTGVCH = false;

              tradingNum = 1;
              tradingType = 0;  //初始化为买开
             


         }

         public TradingData(
                                 string _userID,
                                 string _instrumentID,
                                 string _callOrPut,
                                 double _exercisePrice,
                                 int _tradingType,
                                 int _tradingNum,
                                 int _clientageType,
                                 double _clientagePrice,
                                 double _marketPrice,
                                 int _clientageCondition,
                                 bool _isSetGradient,
                                 double _accuracy,
                                 int _arithmeticProgression 
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

             //////不用到的在这里初始化
            
             ifChooseOTGVCH = false;
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
