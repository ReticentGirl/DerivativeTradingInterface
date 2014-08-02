using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace qiquanui
{

    class HistoryData : INotifyPropertyChanged
    {

        bool hIfChoose;     //前面的勾有木有选
        
        private string instrumentID;   //合约代码
        
        private string tradingTime;   //交易时间

        private string tradingType;  //交易类型   0 买开  1 卖开  2买平 3卖平

        private int postNum;  //挂单手数

        private int doneNum;   //成交手数

        private string tradingState;  //成交状态

        private double postPrice;   //挂单价格

        private double donePrice;   //成交价格

        private string timeLimit;  //有效期

        private string userID;   //投资账户



        private string clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消




        public bool HIfChoose
        {
            get { return hIfChoose; }
            set
            {
                hIfChoose = value;
                OnPropertyChanged("HIfChoose");
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


        public string TradingTime
        {
            get { return tradingTime; }
            set
            {
                tradingTime = value;
                OnPropertyChanged("TradingTime");
            }
        }



        public string TradingType
        {
            get { return tradingType; }
            set
            {
                tradingType = value;
                OnPropertyChanged("TradingType");
            }
        }


        public int PostNum
        {
            get { return postNum; }
            set
            {
                postNum = value;
                OnPropertyChanged("PostNum");
            }
        }


        public int DoneNum
        {
            get { return doneNum; }
            set
            {
                doneNum = value;
                OnPropertyChanged("DoneNum");
            }
        }


        public string TradingState
        {
            get { return tradingState; }
            set
            {
                tradingState = value;
                OnPropertyChanged("TradingState");
            }
        }


        public double PostPrice
        {
            get { return postPrice; }
            set
            {
                postPrice = value;
                OnPropertyChanged("PostPrice");
            }
        }

        public double DonePrice
        {
            get { return donePrice; }
            set
            {
                donePrice = value;
                OnPropertyChanged("DonePrice");
            }
        }

        public string TimeLimit
        {
            get { return timeLimit; }
            set
            {
                timeLimit = value;
                OnPropertyChanged("TimeLimit");
            }
        }



        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }


        public string ClientageCondition
        {
            get { return clientageCondition; }
            set
            {
                clientageCondition = value;
                OnPropertyChanged("ClientageCondition");
            }
        }
      

        


        public HistoryData()
        { }

        public HistoryData(       
            string _instrumentID,
            string _tradingTime,
            string _tradingType,
            int _postNum,
            int _doneNum,
            string _tradingState,
            double _postPrice,
            double _donePrice,
            string _timeLimit,
            string _userID,
            string _clientageCondition 
                           )
        {
      
            instrumentID=_instrumentID;
            tradingTime = _tradingTime;
            tradingType = _tradingType;
            postNum = _postNum;
            doneNum = _doneNum;
            tradingState = _tradingState;
            postPrice = _postPrice;
            donePrice = _donePrice;
            timeLimit = _timeLimit;
            userID = _userID;
            clientageCondition = _clientageCondition;



            hIfChoose = false;   //初始化为false
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


    class HistoryManager
    {
        public ObservableCollection<HistoryData> HistoryOC = new ObservableCollection<HistoryData>();

        MainWindow pwindow;    //主窗体指针


        public HistoryManager(MainWindow _pwindow)
        {
            pwindow = _pwindow;

            pwindow.historyListView.ItemsSource = HistoryOC;

            OnAdd("1", "2", "3", 4, 5, "6", 7.0, 8.0, "9", "10","11");

        }



        public void OnAdd(
            string _instrumentID,
            string _tradingTime,
            string _tradingType,
            int _postNum,
            int _doneNum,
            string _tradingState,
            double _postPrice,
            double _donePrice,
            string _timeLimit,
            string _userID,
            string _clientageCondition)
        {
            HistoryOC.Add(new HistoryData(_instrumentID, _tradingTime, _tradingType, _postNum, _doneNum, _tradingState, _postPrice, _donePrice, _timeLimit, _userID, _clientageCondition));
        }


    }
}
