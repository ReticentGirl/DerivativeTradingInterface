using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Timers;
using System.Data;

namespace qiquanui
{

    public class HistoryData : INotifyPropertyChanged
    {

        bool hIfChoose;     //前面的勾有木有选

        private string instrumentID;   //合约代码

        private string tradingTime;   //交易时间

        private string tradingType;  //交易类型   0 买  1 卖

        private int postNum;  //挂单手数

        private int doneNum;   //成交手数

        private string tradingState;  //成交状态

        private double postPrice;   //挂单价格

        private double donePrice;   //成交价格

        private string timeLimit;  //有效期

        private string userID;   //投资账户

        private bool optionOrFuture;  //判断是期货还是期权

        private string clientageCondition;   //托单条件   0  ROD 當日有效單    1 FOK  委託之數量須全部且立即成交，否則取消   2 IOC 立即成交否則取消

        private string clientageType;  //委托方式

        private int isBuy;


        public int IsBuy
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



        public string ClientageType
        {
            get { return clientageType; }
            set
            {
                clientageType = value;
                OnPropertyChanged("ClientageType");
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
            bool _optionOrFuture,
            string _clientageType
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
            clientageType = _clientageType;

            hIfChoose = false;   //初始化为false

            //交易类型   0 买  1 卖
            if (tradingType.Equals("买"))
                isBuy = 1;
            else if (tradingType.Equals("卖"))
                isBuy = 0;
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

        public ObservableCollection<HistoryData> HistoryFutureOC = new ObservableCollection<HistoryData>();

        public ObservableCollection<HistoryData> HistoryOptionOC = new ObservableCollection<HistoryData>();

        public ObservableCollection<HistoryData> HistoryNullOC = new ObservableCollection<HistoryData>();



        MainWindow pwindow;    //主窗体指针

        PositionsManager h_pm; //持仓指针

        UserManager h_um;   //账户管理指针

        public static string ALLDONE = "全成";
        public static string SOMEDONE = "部成";
        public static string NOTDONE = "挂单取消";
        public static string POST = "挂单状态";

        System.Timers.Timer historyTimer; //刷新历史记录的计时器


        public HistoryManager(MainWindow _pwindow, PositionsManager _pm, UserManager _um)
        {
            pwindow = _pwindow;

            h_pm = _pm;

            h_um = _um;

            pwindow.historyListView.ItemsSource = HistoryOC;

            //OnAdd("1", "2", "3", 4, 5, "6", 7.0, 8.0, "9", "10","11",false);

            historyTimer = new System.Timers.Timer(500);

            historyTimer.Start();

            historyTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

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
            bool _optionOrFuture,
            string _clientageType
            )
        {

            // TurnOver(HistoryOC);
            // TurnOver(HistoryOptionOC);
            // TurnOver(HistoryFutureOC);


            HistoryData a_hd = new HistoryData(_instrumentID, _tradingTime, _tradingType, _postNum, _doneNum, _tradingState, _postPrice, _donePrice, _timeLimit, _userID, _clientageCondition, _optionOrFuture, _clientageType);


            HistoryOC.Add(a_hd);

            if (a_hd.OptionOrFuture == false)
            {

                HistoryOptionOC.Add(a_hd);

            }
            else if (a_hd.OptionOrFuture == true)
            {

                HistoryFutureOC.Add(a_hd);

            }

            //TurnOver(HistoryOC);
            // TurnOver(HistoryOptionOC);
            // TurnOver(HistoryFutureOC);
        }


        public void TurnOver(ObservableCollection<HistoryData> t_OC)
        {
            for (int i = 0; i < t_OC.Count() / 2; i++)
            {
                HistoryData head_hd = t_OC[i];

                HistoryData tail_hd = t_OC[t_OC.Count() - 1 - i];

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

                string temp_clientageType = head_hd.ClientageType;

                int temp_isBuy = head_hd.IsBuy;
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

                head_hd.ClientageType = tail_hd.ClientageType;

                head_hd.IsBuy = tail_hd.IsBuy;
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

                tail_hd.ClientageType = temp_clientageType;

                tail_hd.IsBuy = temp_isBuy;

            }

        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            /////////////
            //ROD (Rest of Day order)：當日有效單。
            //FOK (Fill or kill order)：委託之數量須全部且立即成交，否則取消
            //IOC (Immediate or cancel)：立即成交否則取消
            ////////////

            for (int i = 0; i < HistoryOC.Count(); i++)
            {
                HistoryData hd = HistoryOC[i];
                if (hd.OptionOrFuture == false) //期权
                    RefreshForOption(hd);
                else
                    RefreshForFuture(hd);
            }

        }


        void RefreshForOption(HistoryData _hd)
        {
            Random rd = new Random();

            if (_hd.ClientageType.Equals("市价") && _hd.TradingState.Equals(POST))
            {
                if (_hd.ClientageCondition.Equals("FOK"))
                {
                    _hd.DoneNum = _hd.PostNum;
                    _hd.TradingState = ALLDONE;
                    _hd.DonePrice = _hd.PostPrice;

                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);
                }
                else if (_hd.ClientageCondition.Equals("IOC"))
                {
                    double rate = rd.NextDouble();   //返回一个 0.0到1.0之间的数
                    if (rate > 0.2)            //当RATE 大与0.2 时  成交成功
                    {
                        _hd.DoneNum = _hd.PostNum;
                        _hd.TradingState = ALLDONE;
                        _hd.DonePrice = _hd.PostPrice;

                        HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                    }
                    else
                    {


                        if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                        {
                            _hd.DoneNum = _hd.PostNum;
                            _hd.DonePrice = _hd.PostPrice;
                            _hd.TradingState = ALLDONE;

                            HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                        }
                        else
                        {
                            double rate2 = 1 - (Math.Abs(rd.NextDouble() - 0.5));
                            _hd.DoneNum = (int)(_hd.PostNum * rate2);
                            _hd.TradingState = SOMEDONE;

                            _hd.DonePrice = _hd.PostPrice;

                            HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);    //加入持仓区
                        }



                    }
                }



            }
            else if (_hd.ClientageType.Equals("限价"))
            {

                string t_instrumentID = _hd.InstrumentID;

                DataRow nDr = (DataRow)DataManager.All[t_instrumentID];

                ///////////////////////买卖做不同处理
                if (_hd.TradingType.Equals("买"))
                {
                    double nowAskPrice = Math.Round((double)nDr["AskPrice1"], 1);

                    if (nowAskPrice <= _hd.PostPrice)
                    {
                        if (_hd.ClientageCondition.Equals("ROD") && _hd.TradingState.Equals(POST))    //ROD 且 处于POST
                        {
                            if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                            {
                                _hd.DoneNum = _hd.PostNum;
                                _hd.DonePrice = _hd.PostPrice;
                                _hd.TradingState = ALLDONE;

                                HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                            }
                            else
                            {
                                historyTimer.Stop();

                                double rpRate = Math.Abs(rd.NextDouble() - 0.5);
                                int t_num = (int)(_hd.PostNum * rpRate);    //买 Num为正
                                int old_DoneNum = _hd.DoneNum;
                                _hd.DoneNum += t_num;
                                _hd.DonePrice = nowAskPrice;
                                _hd.TradingState = POST;

                                if (Math.Abs(_hd.DoneNum) >= Math.Abs(_hd.PostNum))
                                {
                                    t_num = Math.Abs(_hd.PostNum) - old_DoneNum;
                                    _hd.DoneNum = _hd.PostNum;
                                    _hd.TradingState = ALLDONE;
                                }

                                HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, t_num, _hd.IsBuy);   //加入持仓区

                                historyTimer.Start();
                            }


                        }
                        else if (_hd.ClientageCondition.Equals("FOK") && _hd.TradingState.Equals(POST))
                        {
                            double fpRate = rd.NextDouble();

                            if (fpRate < 0.2)  //不成
                            {
                                _hd.TradingState = NOTDONE;
                            }
                            else          //全成
                            {
                                _hd.DoneNum = _hd.PostNum;
                                _hd.TradingState = ALLDONE;
                                _hd.DonePrice = nowAskPrice;

                                HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                            }

                        }
                        else if (_hd.ClientageCondition.Equals("IOC") && _hd.TradingState.Equals(POST))
                        {
                            double ipRate1 = rd.NextDouble();
                            if (ipRate1 < 0.2)  //不成
                            {
                                _hd.TradingState = NOTDONE;
                            }
                            else         //部成 或者 全成
                            {
                                double ipRate2 = rd.NextDouble();
                                if (ipRate2 > 0.5)   //部成
                                {
                                    if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                                    {
                                        _hd.DoneNum = _hd.PostNum;
                                        _hd.DonePrice = _hd.PostPrice;
                                        _hd.TradingState = ALLDONE;

                                        HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                    }
                                    else
                                    {

                                        double ipRate3 = 1 - (Math.Abs(rd.NextDouble() - 0.5));

                                        _hd.DoneNum = (int)(_hd.PostNum * ipRate3);
                                        _hd.DonePrice = nowAskPrice;
                                        _hd.TradingState = SOMEDONE;

                                        HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                    }

                                }
                                else           //全成
                                {
                                    _hd.DoneNum = _hd.PostNum;
                                    _hd.DonePrice = nowAskPrice;
                                    _hd.TradingState = ALLDONE;

                                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                }

                            }

                        }
                    }
                }
                else if (_hd.TradingType.Equals("卖"))
                {
                    double nowBidPrice = Math.Round((double)nDr["BidPrice1"], 1);

                    if (nowBidPrice >= _hd.PostPrice)   //  当买价   大于挂单价钱时才激发 
                    {
                        if (_hd.ClientageCondition.Equals("ROD") && _hd.TradingState.Equals(POST))    //ROD 且 处于POST
                        {
                            if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                            {
                                _hd.DoneNum = _hd.PostNum;
                                _hd.DonePrice = _hd.PostPrice;
                                _hd.TradingState = ALLDONE;

                                HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                            }
                            else
                            {
                                historyTimer.Stop();
                                double rpRate = Math.Abs(rd.NextDouble() - 0.5);
                                int t_num = (int)(_hd.PostNum * rpRate);       //卖 Num为负
                                int old_Num = _hd.DoneNum;
                                _hd.DoneNum += t_num;                         //加上一个负数
                                _hd.DonePrice = nowBidPrice;
                                _hd.TradingState = POST;

                                if (Math.Abs(_hd.DoneNum) >= Math.Abs(_hd.PostNum))     //两个都是负数
                                {
                                    t_num = -(Math.Abs(_hd.PostNum) - Math.Abs(old_Num));
                                    _hd.DoneNum = _hd.PostNum;
                                    _hd.TradingState = ALLDONE;
                                }

                                HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, t_num, _hd.IsBuy);   //加入持仓区

                                historyTimer.Start();
                            }


                        }
                        else if (_hd.ClientageCondition.Equals("FOK") && _hd.TradingState.Equals(POST))
                        {
                            double fpRate = rd.NextDouble();

                            if (fpRate < 0.2)  //不成
                            {
                                _hd.TradingState = NOTDONE;
                            }
                            else          //全成
                            {
                                _hd.DoneNum = _hd.PostNum;
                                _hd.TradingState = ALLDONE;
                                _hd.DonePrice = nowBidPrice;

                                HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                            }

                        }
                        else if (_hd.ClientageCondition.Equals("IOC") && _hd.TradingState.Equals(POST))
                        {
                            double ipRate1 = rd.NextDouble();
                            if (ipRate1 < 0.2)  //不成
                            {
                                _hd.TradingState = NOTDONE;
                            }
                            else         //部成 或者 全成
                            {
                                double ipRate2 = rd.NextDouble();
                                if (ipRate2 > 0.5)   //部成
                                {

                                    if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                                    {
                                        _hd.DoneNum = _hd.PostNum;
                                        _hd.DonePrice = _hd.PostPrice;
                                        _hd.TradingState = ALLDONE;

                                        HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                    }
                                    else
                                    {


                                        double ipRate3 = 1 - (Math.Abs(rd.NextDouble() - 0.5)); //让取值不会是1
                                        _hd.DoneNum = (int)(_hd.PostNum * ipRate3);
                                        _hd.DonePrice = nowBidPrice;
                                        _hd.TradingState = SOMEDONE;

                                        HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                    }

                                }
                                else           //全成
                                {
                                    _hd.DoneNum = _hd.PostNum;
                                    _hd.DonePrice = nowBidPrice;
                                    _hd.TradingState = ALLDONE;

                                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                }

                            }

                        }

                    }

                }
            }
        }



        void RefreshForFuture(HistoryData _hd)
        {
            string t_instrumentID = _hd.InstrumentID;

            DataRow nDr = (DataRow)DataManager.All[t_instrumentID];

            Random rd = new Random();

            if (_hd.TradingState.Equals(POST))
            {
                if (_hd.ClientageType.Equals("限价"))
                {
                    //////////////////////买卖不同 而且期货只有ROD
                    if (_hd.TradingType.Equals("买"))
                    {
                        double nowAskPrice = Math.Round((double)nDr["AskPrice1"], 1);

                        if (nowAskPrice <= _hd.PostPrice)
                        {
                            if (_hd.ClientageCondition.Equals("ROD") && _hd.TradingState.Equals(POST))    //ROD 且 处于POST
                            {
                                if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                                {
                                    _hd.DoneNum = _hd.PostNum;
                                    _hd.DonePrice = _hd.PostPrice;
                                    _hd.TradingState = ALLDONE;

                                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                }
                                else
                                {
                                    historyTimer.Stop();
                                    double rpRate = Math.Abs(rd.NextDouble() - 0.5);
                                    int t_num = (int)(_hd.PostNum * rpRate);    //买 Num为正
                                    int old_DoneNum = _hd.DoneNum;
                                    _hd.DoneNum += t_num;
                                    _hd.DonePrice = nowAskPrice;
                                    _hd.TradingState = POST;

                                    if (Math.Abs(_hd.DoneNum) >= Math.Abs(_hd.PostNum))
                                    {
                                        t_num = Math.Abs(_hd.PostNum) - old_DoneNum;

                                        _hd.DoneNum = _hd.PostNum;
                                        _hd.TradingState = ALLDONE;

                                    }
                                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, t_num, _hd.IsBuy);   //加入持仓区
                                    historyTimer.Start();

                                }



                            }

                        }

                    }
                    else if (_hd.TradingType.Equals("卖"))
                    {
                        double nowBidPrice = Math.Round((double)nDr["BidPrice1"], 1);

                        if (nowBidPrice >= _hd.PostPrice)   //  当买价   大与挂单价钱时才激发 
                        {
                            if (_hd.ClientageCondition.Equals("ROD") && _hd.TradingState.Equals(POST))    //ROD 且 处于POST
                            {

                                if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                                {
                                    _hd.DoneNum = _hd.PostNum;
                                    _hd.DonePrice = _hd.PostPrice;
                                    _hd.TradingState = ALLDONE;

                                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                                }
                                else
                                {
                                    historyTimer.Stop();
                                    double rpRate = Math.Abs(rd.NextDouble() - 0.5);
                                    int t_num = (int)(_hd.PostNum * rpRate);       //卖 Num为负
                                    int old_DoneNum = _hd.DoneNum;
                                    _hd.DoneNum += t_num;                         //加上一个负数
                                    _hd.DonePrice = nowBidPrice;
                                    _hd.TradingState = POST;

                                    if (Math.Abs(_hd.DoneNum) >= Math.Abs(_hd.PostNum))     //两个都是负数
                                    {
                                        t_num = -(Math.Abs(_hd.PostNum) - Math.Abs(old_DoneNum));
                                        _hd.DoneNum = _hd.PostNum;
                                        _hd.TradingState = ALLDONE;
                                    }

                                    HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, t_num, _hd.IsBuy);   //加入持仓区
                                    historyTimer.Start();
                                }


                            }
                        }
                    }
                }
                else if (_hd.ClientageType.Equals("市价"))
                {
                    if (_hd.TradingType.Equals("买"))
                    {
                        double nowAskPrice = Math.Round((double)nDr["AskPrice1"], 1);

                        if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                        {
                            _hd.DoneNum = _hd.PostNum;
                            _hd.DonePrice = _hd.PostPrice;
                            _hd.TradingState = ALLDONE;

                            HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                        }
                        else
                        {
                            historyTimer.Stop();
                            double fbRate = Math.Abs(rd.NextDouble() - 0.5);
                            int t_num = (int)(_hd.PostNum * fbRate);
                            int old_DoneNum = _hd.DoneNum;
                            _hd.DoneNum += t_num;
                            _hd.DonePrice = nowAskPrice;
                            _hd.TradingState = POST;

                            if (Math.Abs(_hd.DoneNum) >= Math.Abs(_hd.PostNum))
                            {
                                t_num = Math.Abs(_hd.PostNum) - Math.Abs(old_DoneNum);
                                _hd.DoneNum = _hd.PostNum;
                                _hd.TradingState = ALLDONE;
                            }

                            HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, t_num, _hd.IsBuy);   //加入持仓区

                            historyTimer.Start();
                        }


                    }
                    else if (_hd.TradingType.Equals("卖"))
                    {
                        double nowBidPrice = Math.Round((double)nDr["BidPrice1"], 1);

                        if (Math.Abs(_hd.PostNum) <= 5)   //小于等于5的就模拟全部成交   
                        {
                            _hd.DoneNum = _hd.PostNum;
                            _hd.DonePrice = _hd.PostPrice;
                            _hd.TradingState = ALLDONE;

                            HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, _hd.DoneNum, _hd.IsBuy);   //加入持仓区
                        }
                        else
                        {
                            historyTimer.Stop();

                            double fsRate = Math.Abs(rd.NextDouble() - 0.5);
                            int t_num = (int)(_hd.PostNum * fsRate);
                            int old_DoneNum = _hd.DoneNum;
                            _hd.DoneNum += t_num;
                            _hd.DonePrice = nowBidPrice;
                            _hd.TradingState = POST;

                            if (Math.Abs(_hd.DoneNum) >= Math.Abs(_hd.PostNum))
                            {
                                t_num = -(Math.Abs(_hd.PostNum) - Math.Abs(old_DoneNum));
                                _hd.DoneNum = _hd.PostNum;
                                _hd.TradingState = ALLDONE;
                            }



                            HistoryToHold(_hd.UserID, _hd.InstrumentID, _hd.DonePrice, t_num, _hd.IsBuy);   //加入持仓区

                            historyTimer.Start();

                        }


                    }



                }
            }



        }

        // void HistoryToHold()
        //{

        //}
        public void HistoryToHold(string _userID, string _insrtumentID, double _finalPrice, int _tradingNum, int _isBuy)   //提交到持仓区
        {

            string querySql = String.Format("SELECT * from Positions WHERE UserID='{0}' AND InstrumentID='{1}' AND IsBuy='{2}'", _userID, _insrtumentID, _isBuy);

            DataTable testForNull = null;

            testForNull = DataControl.QueryTable(querySql);

            DataRow hasRow = null;

            if (testForNull.Rows.Count > 0)
            {
                hasRow = testForNull.Rows[0];
            }




            if (hasRow != null)   //说明原来就已经买过这个东西了
            {
                double old_averagePrice = Convert.ToDouble(hasRow["AveragePrice"]);

                double old_positionAveragePrice = Convert.ToDouble(hasRow["PositionAveragePrice"]);

                int old_tradingNum = Convert.ToInt32(hasRow["TradingNum"]);

                int new_tradingNum = old_tradingNum + _tradingNum;

                double new_averagePrice = (old_averagePrice * Math.Abs(old_tradingNum) + _finalPrice * Math.Abs(_tradingNum)) / Math.Abs(new_tradingNum);

                double new_positionAveragePrice = (old_positionAveragePrice * Math.Abs(old_tradingNum) + _finalPrice * Math.Abs(_tradingNum)) / Math.Abs(new_tradingNum);

                string updateSql = String.Format("UPDATE Positions SET AveragePrice='{0}',PositionAveragePrice='{1}',TradingNum='{2}' WHERE UserID='{3}' AND InstrumentID='{4}' AND IsBuy='{5}'", new_averagePrice, new_positionAveragePrice, new_tradingNum, _userID, _insrtumentID, _isBuy);

                DataControl.InsertOrUpdate(updateSql);
            }
            else if (hasRow == null)
            {
                string insertSql = String.Format("INSERT INTO Positions (UserID,InstrumentID,AveragePrice,PositionAveragePrice,TradingNum,IsBuy) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')"
                    , _userID, _insrtumentID, _finalPrice, _finalPrice, _tradingNum, _isBuy);

                DataControl.InsertOrUpdate(insertSql);
            }

            //// 资金扣除

            //h_um.changeMarginForPlace(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);
            //h_um.changeRoyaltyForPlace(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);
            //h_um.changeFeesForPlace(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);
            ////每次执行之后要刷新持仓区
            ////h_pm.GetInfoFromDBToOC();

            //h_um.GetInfoFromDBToHash();
            //h_um.GetInfoFromHashToOC();
            //h_um.GetPositionsFromDBToUserPositions();



            h_um.UserManagerHandleInHistory(_userID, _insrtumentID, _finalPrice, _tradingNum, _isBuy);

        }


        delegate void HistoryShowCallBack();

        public void OnShowOption()
        {

            ObservableCollection<HistoryData> HistoryShowOC = new ObservableCollection<HistoryData>();

            for (int i = 0; i < HistoryOptionOC.Count; i++)
            {
                if (PickUpUserManager.isChooseUser(HistoryOptionOC[i].UserID) == true)
                    HistoryShowOC.Add(HistoryOptionOC[i]);
            }
            pwindow.historyListView.ItemsSource = HistoryShowOC;

        }

        public void OnShowFuture()
        {

            ObservableCollection<HistoryData> HistoryShowOC = new ObservableCollection<HistoryData>();

            for (int i = 0; i < HistoryFutureOC.Count; i++)
            {
                if (PickUpUserManager.isChooseUser(HistoryFutureOC[i].UserID) == true)
                    HistoryShowOC.Add(HistoryFutureOC[i]);
            }
            pwindow.historyListView.ItemsSource = HistoryShowOC;

        }

        public void OnShowAll()
        {

            ObservableCollection<HistoryData> HistoryShowOC = new ObservableCollection<HistoryData>();

            for (int i = 0; i < HistoryOC.Count; i++)
            {
                if (PickUpUserManager.isChooseUser(HistoryOC[i].UserID) == true)
                    HistoryShowOC.Add(HistoryOC[i]);
            }
            pwindow.historyListView.ItemsSource = HistoryShowOC;

        }

        public void OnShowNull()
        {

            pwindow.historyListView.ItemsSource = HistoryNullOC;
        }


        public void HistoryAllChoose(bool _isChooseAll)   //历史界面 全选按钮响应函数
        {


            if (HistoryOC.Count > 0)
            {
                for (int i = 0; i < HistoryOC.Count; i++)
                {
                    HistoryOC[i].HIfChoose = _isChooseAll;
                }
            }

            if (HistoryFutureOC.Count > 0)
            {
                for (int i = 0; i < HistoryFutureOC.Count; i++)
                {
                    HistoryFutureOC[i].HIfChoose = _isChooseAll;
                }
            }


            if (HistoryOptionOC.Count > 0)
            {
                for (int i = 0; i < HistoryOptionOC.Count; i++)
                {
                    HistoryOptionOC[i].HIfChoose = _isChooseAll;
                }
            }

        }

    }
}
