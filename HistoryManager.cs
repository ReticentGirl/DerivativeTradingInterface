using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace qiquanui
{

    public class HistoryData : INotifyPropertyChanged
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

        private bool optionOrFuture;  //判断是期货还是期权

        private string clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消



        public bool OptionOrFuture
        {
            get { return optionOrFuture; }
            set
            {
                optionOrFuture = value;
                OnPropertyChanged("OptionOrFuture");
            }
        }




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
            string _clientageCondition,
            bool _optionOrFuture
                           )
        {

            instrumentID = _instrumentID;
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
            optionOrFuture = _optionOrFuture;


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


    public class HistoryManager
    {
        public ObservableCollection<HistoryData> HistoryOC = new ObservableCollection<HistoryData>();

        MainWindow pwindow;    //主窗体指针

        public static string ALLDONE = "全成";
        public static string SOMEDONE = "部成";
        public static string NOTDONE = "挂单取消";
        public static string POST = "挂单状态";


        public HistoryManager(MainWindow _pwindow)
        {
            pwindow = _pwindow;

            pwindow.historyListView.ItemsSource = HistoryOC;

            //OnAdd("1", "2", "3", 4, 5, "6", 7.0, 8.0, "9", "10","11",false);

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
            string _clientageCondition,
            bool _optionOrFuture
            )
        {
            TurnOver();
            HistoryOC.Add(new HistoryData(_instrumentID, _tradingTime, _tradingType, _postNum, _doneNum, _tradingState, _postPrice, _donePrice, _timeLimit, _userID, _clientageCondition, _optionOrFuture));
            TurnOver();
        }


        public void TurnOver()
        {
            for (int i = 0; i < HistoryOC.Count() / 2; i++)
            {
                HistoryData head_hd = HistoryOC[i];

                HistoryData tail_hd = HistoryOC[HistoryOC.Count() - 1 - i];

                //HistoryData temp_hd = head_hd;

                //head_hd = tail_hd;

                //tail_hd = temp_hd;

                bool temp_hIfChoose = head_hd.HIfChoose;

                string temp_instrumentID = head_hd.InstrumentID;

                string temp_tradingTime = head_hd.TradingTime;

                string temp_tradingType = head_hd.TradingType;

                int temp_postNum = head_hd.PostNum;

                int temp_doneNum = head_hd.DoneNum;

                string temp_tradingState = head_hd.TradingState;

                double temp_postPrice = head_hd.PostPrice;

                double temp_donePrice = head_hd.DonePrice;

                string temp_timeLimit = head_hd.TimeLimit;

                string temp_userID = head_hd.UserID;

                bool temp_optionOrFuture = head_hd.OptionOrFuture;

                string temp_clientageCondition = head_hd.ClientageCondition;
                ///////////////////////////////////////////////////////////////////////////////////////////

                head_hd.HIfChoose = tail_hd.HIfChoose;

                head_hd.InstrumentID = tail_hd.InstrumentID;

                head_hd.TradingTime = tail_hd.TradingTime;

                head_hd.TradingType = tail_hd.TradingType;

                head_hd.PostNum = tail_hd.PostNum;

                head_hd.DoneNum = tail_hd.DoneNum;

                head_hd.TradingState = tail_hd.TradingState;

                head_hd.PostPrice = tail_hd.PostPrice;

                head_hd.DonePrice = tail_hd.DonePrice;

                head_hd.TimeLimit = tail_hd.TimeLimit;

                head_hd.UserID = tail_hd.UserID;

                head_hd.OptionOrFuture = tail_hd.OptionOrFuture;

                head_hd.ClientageCondition = tail_hd.ClientageCondition;
                /////////////////////////////////////////////////////////////////////////////////

                tail_hd.HIfChoose = temp_hIfChoose;

                tail_hd.InstrumentID = temp_instrumentID;

                tail_hd.TradingTime = temp_tradingTime;

                tail_hd.TradingType = temp_tradingType;

                tail_hd.PostNum = temp_postNum;

                tail_hd.DoneNum = temp_doneNum;

                tail_hd.TradingState = temp_tradingState;

                tail_hd.PostPrice = temp_postPrice;

                tail_hd.DonePrice = temp_donePrice;

                tail_hd.TimeLimit = temp_timeLimit;

                tail_hd.UserID = temp_userID;

                tail_hd.OptionOrFuture = temp_optionOrFuture;

                tail_hd.ClientageCondition = temp_clientageCondition;



            }

        }

    }
}
