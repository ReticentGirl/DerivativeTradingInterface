﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using qiquanui.Properties;
 
namespace qiquanui
{
    #region class option
    public class option : INotifyPropertyChanged
    {
        private string bidPrice1;
        private string askPrice1;
        private string lastPrice1;
        private string volume1;
        private string openInterest1;
        private string exercisePrice;
        private string openInterest2;
        private string volume2;
        private string bidPrice2;
        private string askPrice2;
        private string lastPrice2;
        public string instrumentid1;
        public string instrumentid2;

        public string BidPrice1
        {
            get { return bidPrice1; }
            set
            {
                bidPrice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BidPrice1"));
            }
        }
        public string AskPrice1
        {
            get { return askPrice1; }
            set
            {
                askPrice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AskPrice1"));
            }
        }
        public string LastPrice1
        {
            get { return lastPrice1; }
            set
            {
                lastPrice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("lastPrice1"));
            }
        }
        public string Volume1
        {
            get { return volume1; }
            set { volume1 = value; OnPropertyChanged(new PropertyChangedEventArgs("Volume1")); }
        }
        public string OpenInterest1
        {
            get { return openInterest1; }
            set { openInterest1 = value; OnPropertyChanged(new PropertyChangedEventArgs("OpenInterest1")); }
        }
        public string ExercisePrice
        {
            get { return exercisePrice; }
            set
            {
                exercisePrice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ExercisePrice"));
            }
        }

        public string BidPrice2
        {
            get { return bidPrice2; }
            set
            {
                bidPrice2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BidPrice2"));
            }
        }
        public string AskPrice2
        {
            get { return askPrice2; }
            set { askPrice2 = value; OnPropertyChanged(new PropertyChangedEventArgs("AskPrice2")); }
        }
        public string LastPrice2
        {
            get { return lastPrice2; }
            set
            {
                lastPrice2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastPrice2"));
            }
        }
        public string Volume2
        {
            get { return volume2; }
            set
            {
                volume2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Volume2"));
            }
        }
        public string OpenInterest2
        {
            get { return openInterest2; }
            set
            {
                openInterest2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OpenInterest2"));
            }
        }


        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        #endregion
    }
    #endregion

    #region class future
    public class future : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        #endregion
        private string instrumentname;
        private string lastprice;
        private string askprice1;
        private string bidprice1;
        private string bidvolume1;
        private string askvolume1;
        private string openinterest;
        private string openprice;
        private string riseandfall;
        private string riseandfallrate;
        private string highestprice;
        private string lowestprice;
        private string upperlimitprice;
        private string lowerlimitprice;
        private string presettlementprice;
        private string precloseprice;
        private string preopeninterest;
        public string instrumentid;
        public string InstrumentName
        {
            get { return instrumentname; }
            set
            {
                instrumentname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InstrumentName"));
            }
        }
        public string LastPrice
        {
            get { return lastprice; }
            set
            {
                lastprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastPrice"));
            }
        }
        public string AskPrice1
        {
            get { return askprice1; }
            set
            {
                askprice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AskPrice1"));
            }
        }
        public string BidPrice1
        {
            get { return bidprice1; }
            set
            {
                bidprice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BidPrice1"));
            }
        }
        public string BidVolume1
        {
            get { return bidvolume1; }
            set
            {
                bidvolume1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BidVolume1"));
            }
        }
        public string AskVolume1
        {
            get { return askvolume1; }
            set
            {
                askvolume1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AskVolume1"));
            }
        }
        public string OpenInterest
        {
            get { return openinterest; }
            set
            {
                openinterest = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OpenInterest"));
            }
        }
        public string OpenPrice
        {
            get { return openprice; }
            set
            {
                openprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OpenPrice"));
            }
        }
        public string RiseAndFall
        {
            get { return riseandfall; }
            set
            {
                riseandfall = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RiseAndFall"));
            }
        }
        public string RiseAndFallRate
        {
            get { return riseandfallrate; }
            set
            {
                riseandfallrate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RiseAndFallRate"));
            }
        }
        public string HighestPrice
        {
            get { return highestprice; }
            set
            {
                highestprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HighestPrice"));
            }
        }
        public string LowestPrice
        {
            get { return lowestprice; }
            set
            {
                lowestprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LowestPrice"));
            }
        }
        public string UpperLimitPrice
        {
            get { return upperlimitprice; }
            set
            {
                upperlimitprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UpperLimitPrice"));
            }
        }
        public string LowerLimitPrice
        {
            get { return lowerlimitprice; }
            set
            {
                lowerlimitprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LowerLimitPrice"));
            }
        }
        public string PreSettlementPrice
        {
            get { return presettlementprice; }
            set
            {
                presettlementprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PreSettlementPrice"));
            }
        }
        public string PreClosePrice
        {
            get { return precloseprice; }
            set
            {
                precloseprice = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PreClosePrice"));
            }
        }
        public string PreOpenInterest
        {
            get { return preopeninterest; }
            set
            {
                preopeninterest = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PreOpenInterest"));
            }
        }
        public string InstrumentID
        {
            get { return instrumentid; }
            set
            {
                instrumentid = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InstrumentID"));
            }
        }

    }
    #endregion

    class DataManager
    {
        System.Timers.Timer timer;
        MainWindow pwindow;
        DateTime now;
        public ObservableCollection<option> ObservableObj = new ObservableCollection<option>();
        private ObservableCollection<option> ObservableObj2 = new ObservableCollection<option>();
        public ObservableCollection<future> ObservableOb = new ObservableCollection<future>();
        private ObservableCollection<future> ObservableOb2 = new ObservableCollection<future>();
        public static Hashtable All = new Hashtable(2000);
        static string updatesql, exercisesql, dynamicsql, box_type, box_exchange, box_future, box_time, duedate = "", instrumentname = "";

        public Hashtable ep_no = new Hashtable(50);//行权价对应的行数(期权）
        public Hashtable name_no = new Hashtable(100);//名字对应的行数（期货）
        int tot_line = 0;

        const int Max_line = 30;
        int[,] ob_no2 = new int[Max_line, 20];//表示行index1的第index2列是否被修改（OnTimedEvent更新面板数据时）
        int[,] ob_timer = new int[Max_line, 20];
        bool[] ob_no = new bool[Max_line];//表示行index是否被修改（OnTimedEvent 更新面板数据时）
        
        option[] ob_op = new option[Max_line];

        const int TIMER_UNIT = 100;
        int mytimer = 0;
        bool locked = false;
        int[] timer_milli = { 300, 300 };
        public void TimeManage(object sender, ElapsedEventArgs e)
        {
            if (locked) return;
            locked = true;
            mytimer += TIMER_UNIT;
            now = now.AddMilliseconds(TIMER_UNIT);

            if (mytimer % timer_milli[0] == 0)
            {
                if (box_type != null)
                {
                    if (box_type.Equals("期权"))
                        OnTimedEvent(false);
                    else if (box_type.Equals("期货"))
                        OnTimedEvent3(false);
                }
            }
            if (mytimer % timer_milli[1] == 0)
            {
                OnTimedEvent2();
            }
            if (mytimer >= 1000000)
                mytimer -= 1000000;
            locked = false;
        }

        public void initial()
        {
            DateTime preday = now.AddDays(-1);
            if (preday.DayOfWeek == DayOfWeek.Sunday) preday.AddDays(-2);
            if (preday.DayOfWeek == DayOfWeek.Saturday) preday.AddDays(-1);
            string _month, _day;
            if (preday.Month < 10) _month = "0" + preday.Month;
            else _month = "" + preday.Month;
            if (preday.Day < 10) _day = "0" + preday.Day;
            else _day = Convert.ToString(preday.Day);
            string _date = "2014" + _month + _day;
            string initialsql = String.Format("select * from daydata d,staticdata s where tradingday='{0}' and d.instrumentid=s.instrumentid", _date);
            DataTable _dt = DataControl.QueryTable(initialsql);
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                string _id = (string)_dt.Rows[i]["InstrumentID"];
                All[_id] = _dt.Rows[i];
            }
        }

        public DataManager(MainWindow window)
        {
            pwindow = window;
            timer = new System.Timers.Timer(TIMER_UNIT);
            timer.Elapsed += new ElapsedEventHandler(TimeManage);
            timer.Start();

            now = new DateTime(2014, 7, 25, 14, 0, 25);

            initial();
            prepare();
        }

        int pt = 0;
        DataTable dtall;
        DateTime lastupdate;
        /// <summary>
        /// 更新dtall表中的数据（每分钟调用一次）
        /// </summary>
        public void prepare()
        {
            dynamicsql = String.Format("SELECT * FROM alldata0722 a,staticdata s where s.instrumentid=a.instrumentid  and a.updatetime >= '{0}' and a.updatetime <= '{1}' order by updatetime,updatemillisec", TimeToString(now), TimeToString(now.AddMinutes(1)));
            dtall = DataControl.QueryTable(dynamicsql);
            lastupdate = now;
            pt = 0;
        }



        /// <summary>
        /// 根据dtall表,pt指针,以及时间，更新All中数据至最新（每0.5秒一次）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTimedEvent2()
        {
            //Console.WriteLine("Timer elapsed! now=" + now+"  "+now.Millisecond+"  z");

            //timer_z.Stop();
            //timer.Stop();

            if (now >= lastupdate.AddMinutes(1)) prepare();
            if (pt >= dtall.Rows.Count)
            {
                return;
            }

            ///更新All中数据至最新
            string uptime = (string)dtall.Rows[pt]["UpdateTime"];
            int uptimemill = (int)(Int64)dtall.Rows[pt]["UpdateMillisec"];
            //for (int i = 0; i < 30; i++)
            //{
            //    ob_no2[i, 0] = false;
            //    ob_no2[i, 1] = false;
            //}

            while (pt < dtall.Rows.Count && TimeToInt(now) >= TimeToInt(uptime, uptimemill))
            {
                string _instrumentid = (string)dtall.Rows[pt]["InstrumentID"];
                All[_instrumentid] = dtall.Rows[pt];

                ///倘若是
                //string _instrumentname = (string)dtall.Rows[pt]["InstrumentName"];
                //string _duedate = (string)dtall.Rows[pt]["DueDate"];
                //if (instrumentname == _instrumentname && duedate == _duedate)
                //{
                //    int _line = (int)ep_no[(int)((double)dtall.Rows[pt]["ExercisePrice"])];
                //    bool _corp = (bool)dtall.Rows[pt]["CallOrPut"];
                //    if (!_corp)
                //        ob_no2[_line, 0] = true;
                //    else ob_no2[_line, 1] = true;
                //}

                uptime = (string)dtall.Rows[pt]["UpdateTime"];
                uptimemill = (int)(Int64)dtall.Rows[pt]["UpdateMillisec"];
                pt++;
            }

            //timer_z.Start();
            //timer.Start();

        }






        delegate void ClearObCallBack();
        public void ClearOb()
        {

            ClearObCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new ClearObCallBack(ClearOb);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                ObservableObj.Clear();
            }

        }






        /// <summary>
        /// 从MinuteData中获取当前期权列表中每只期权的最接近当前的数据用作初始数据
        /// </summary>
        public void Update()
        {
            //ClearOb();

            ///将界面中所选的东西保存在全局变量中
            GetChoice();

            if (box_type.Equals("期权"))
            {

                ///根据行权价的列表list，将所有行权价对应的行数保存在ep_no中
                DataTable list = DataControl.QueryTable(exercisesql);
                Hashtable epcp_row = new Hashtable(50);
                tot_line = list.Rows.Count;

                for (int j = 0; j < tot_line; j++)
                {
                    double x = (double)list.Rows[j][0];
                    ep_no[(int)x] = j;
                }

                /*
                /////主体
                ///扫描dt，计算出epcp_row
                DataTable dt = DataControl.QueryTable(updatesql);
                int i = 0;
                string uptime;
                int uptimemill;
                int datatime;
                int nowtime = TimeToInt(now);
                while (i < dt.Rows.Count)
                {
                    uptime = (string)dt.Rows[i]["UpdateTime"];
                    uptimemill = (int)(Int64)dt.Rows[i]["UpdateMillisec"];
                    datatime = TimeToInt(uptime, 0);
                    if (datatime < nowtime)
                    {
                        int _ep = (int)(double)dt.Rows[i]["ExercisePrice"];
                        bool _callOrPut = (bool)dt.Rows[i]["CallOrPut"];

                        if (!_callOrPut)

                            epcp_row["" + _ep + "C"] = i;


                        else
                            epcp_row["" + _ep + "P"] = i;
                    }
                    else break;
                    i++;
                }

                ///通过list穷举界面上每支期权，根据epcp_row，加入相应数据至集合
                for (int j = 0; j < list.Rows.Count; j++)
                {

                    option _op = new option();
                    if (epcp_row["" + list.Rows[j][0] + "C"] == null)
                    {
                        Console.WriteLine("can't find epcp_row in " + list.Rows[j][0] + "C");
                        continue;
                    }
                    int rowid = (int)epcp_row["" + list.Rows[j][0] + "C"];
                    if (rowid < 0)
                    {
                        Console.WriteLine("ROWID < 0!");
                    }
                    else
                    {

                        _op.AskPrice1 = Math.Round((double)dt.Rows[rowid]["AskPrice1"], 1);
                        _op.BidPrice1 = Math.Round((double)dt.Rows[rowid]["BidPrice1"], 1);
                        _op.ExercisePrice = (int)(double)dt.Rows[rowid]["ExercisePrice"];
                        _op.OpenInterest1 = (int)(double)dt.Rows[rowid]["OpenInterest"];
                        _op.LastPrice1 = Math.Round((double)dt.Rows[rowid]["LastPrice"], 1);
                        _op.Volume1 = (int)(Int64)dt.Rows[rowid]["Volume"];
                        _op.instrumentid1 = (string)dt.Rows[rowid]["InstrumentID"];

                        All[_op.instrumentid1] = dt.Rows[rowid];

                    }

                    if (epcp_row["" + list.Rows[j][0] + "P"] == null)
                    {
                        Console.WriteLine("can't find epcp_row in " + list.Rows[j][0] + "P");
                        continue;
                    }
                    rowid = (int)epcp_row["" + list.Rows[j][0] + "P"];

                    if (rowid < 0)
                    {
                        Console.WriteLine("ROWID < 0!");
                    }
                    else
                    {
                        _op.AskPrice2 = Math.Round((double)dt.Rows[rowid]["AskPrice1"], 1);
                        _op.BidPrice2 = Math.Round((double)dt.Rows[rowid]["BidPrice1"], 1);
                        _op.OpenInterest2 = (int)(double)dt.Rows[rowid]["OpenInterest"];
                        _op.LastPrice2 = Math.Round((double)dt.Rows[rowid]["LastPrice"], 1);
                        _op.Volume2 = (int)(Int64)dt.Rows[rowid]["Volume"];
                        _op.instrumentid2 = (string)dt.Rows[rowid]["InstrumentID"];


                        All[_op.instrumentid2] = dt.Rows[rowid];


                    }

                    ObservableObj2.Add(_op);
                    //Adding(_op);

                }
                */

                while (locked) { }
                locked = true;
                ObservableObj2 = new ObservableCollection<option>();
                updatesql = "SELECT instrumentID FROM staticdata where instrumentname='" + instrumentname + "' and duedate='" + duedate + "' order by exerciseprice,callorput";
                DataTable dt = DataControl.QueryTable(updatesql);
                for (int i = 0; i < tot_line; i++)
                {
                    string _id = (string)dt.Rows[i * 2]["InstrumentID"];
                    option _op = new option();
                    _op.instrumentid1 = _id;
                    _id = (string)dt.Rows[i * 2 + 1]["InstrumentID"];
                    _op.instrumentid2 = _id;
                    ObservableObj2.Add(_op);
                    for (int j = 0; j < 11; j++)
                    {
                        ob_timer[i, j] = 0;
                        ob_no2[i, j] = 0;
                    }
                }

                Binding();
                OnTimedEvent(true);
                //Bind与Show分开是为了防止这期间显示黑色字体
                Show();
                locked = false;
            }
            else
            {
                while (locked) { }
                locked = true;
                ObservableOb2 = new ObservableCollection<future>();
                updatesql = "SELECT * from staticdata where optionorfuture=1 and duedate='"+box_time+"' and exchangename='"+box_exchange+"'";
                DataTable dt = DataControl.QueryTable(updatesql);
                tot_line = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _name = (string)dt.Rows[i]["InstrumentName"];
                    name_no[_name] = i;


                    string _id = (string)dt.Rows[i]["InstrumentID"];
                    future _fu = new future();
                    _fu.instrumentid = _id;
                    ObservableOb2.Add(_fu);
                    for (int j = 0; j < 17; j++)
                    {
                        ob_timer[i, j] = 0;
                        ob_no2[i, j] = 0;
                    }
                  
                }
                Binding2();
                OnTimedEvent3(true);
                Show2();

                locked = false;
            }
        }


        string SetTextAndColor(int i, int j, string news, string olds, double preClose)
        {
            if (news.Length > MaxLength || news.Equals("正无穷大"))
                return "-";


            if (!(olds == null)&& !news.Equals(olds)  )
            {
                if (Convert.ToDouble(news) > preClose)
                    ob_no2[i, j] = 2;
                else if (Convert.ToDouble(news) < preClose)
                    ob_no2[i, j] = 3;
                else ob_no2[i, j] = 1;
                ob_timer[i, j] = TIMES;
            }

            else
            {
                if (ob_timer[i, j] > 0) ob_timer[i, j]--;
                else
                {
                    int _temp = ob_no2[i, j];
                    if (Convert.ToDouble(news) > preClose)
                        ob_no2[i, j] = 4;
                    else if (Convert.ToDouble(news) < preClose)
                        ob_no2[i, j] = 5;
                    else ob_no2[i, j] = 6;
                    if (_temp == ob_no2[i, j])
                        ob_no2[i, j] = 0;
                }
            }
            return news;
        }

        /// <summary>
        /// 刷新期货面板中的数据
        /// </summary>
        /// <param name="first"></param>
        const int TIMES = 5, MaxLength = 10;
        public void OnTimedEvent3(bool first)
        {
            if (ObservableOb.Count < tot_line && !first)
            {
                return;
            }


            for (int i = 0; i < tot_line; i++)
            {
                future _fu;
                if (first)
                    _fu = ObservableOb2.ElementAt<future>(i);
                else
                    _fu = ObservableOb.ElementAt<future>(i);

                DataRow _dr = (DataRow)All[_fu.instrumentid];

                string _new_string;

                double _preClose = (double)_dr["PreClosePrice"];

                //0
                _new_string = (string)_dr["InstrumentName"];
                _fu.InstrumentName = _new_string;

                //1
                _new_string = Math.Round((double)_dr["LastPrice"], 1).ToString();
                _fu.LastPrice = SetTextAndColor(i, 1, _new_string, _fu.LastPrice, _preClose);

                //2
                _new_string = Math.Round((double)_dr["AskPrice1"], 1).ToString();
                _fu.AskPrice1 = SetTextAndColor(i, 2, _new_string, _fu.AskPrice1, _preClose);

                //3
                _new_string = Math.Round((double)_dr["BidPrice1"], 1).ToString();
                _fu.BidPrice1 = SetTextAndColor(i, 3, _new_string, _fu.BidPrice1, _preClose);

                //4
                _new_string = ((Int64)_dr["BidVolume1"]).ToString();
                _fu.BidVolume1 = _new_string;
                if (first) 
                    ob_no2[i, 4] = 7;

                //5
                _new_string = ((Int64)_dr["AskVolume1"]).ToString();
                _fu.AskVolume1 = _new_string;
                if (first) ob_no2[i, 5] = 7;

                //6
                _new_string = Math.Round(((double)_dr["OpenInterest"]), 1).ToString();
                _fu.OpenInterest = _new_string;
                if (first) ob_no2[i,  6] = 7;

                //7
                _new_string = Math.Round(((double)_dr["OpenPrice"]), 1).ToString();
                _fu.OpenPrice = SetTextAndColor(i, 7, _new_string, _fu.OpenPrice, _preClose);

                //8
                double x=Math.Round(((double)_dr["RiseAndFall"]), 1);
                _new_string=x.ToString();
                bool negative = false;
                if (!(_new_string.Length > MaxLength || _new_string.Equals("正无穷大")))
                {
                    if (x < 0)
                    {
                        x = -x;
                        negative = true;
                    }
                }
                if (_fu.RiseAndFall!=null)
                _new_string = SetTextAndColor(i, 8, _new_string, _fu.RiseAndFall.Substring(1), 0);
                else
                    _new_string = SetTextAndColor(i, 8, _new_string, _fu.RiseAndFall, 0);
                //▲▼
                if (!_new_string.Equals("-"))
                {
                    if (!x.Equals(0))
                    {
                        if (negative)
                            _new_string = "▼" + _new_string;
                        else
                            _new_string = "▲" + _new_string;
                    }
                }
                _fu.RiseAndFall = _new_string;

                //9
                _new_string = Math.Round(((double)_dr["RiseAndFallRate"]) * 100.0, 2).ToString();
                _fu.RiseAndFallRate = SetTextAndColor(i, 9, _new_string, _fu.RiseAndFallRate, 0);

                //10
                _new_string = Math.Round((double)_dr["HighestPrice"], 1).ToString();
                _fu.HighestPrice = SetTextAndColor(i, 10, _new_string, _fu.HighestPrice, _preClose);

                //11
                _new_string = Math.Round((double)_dr["LowestPrice"], 1).ToString();
                _fu.LowestPrice = SetTextAndColor(i, 11, _new_string, _fu.LowestPrice, _preClose);

                //12
                _new_string = Math.Round((double)_dr["UpperLimitPrice"], 1).ToString();
                _fu.UpperLimitPrice = SetTextAndColor(i, 12, _new_string, _fu.UpperLimitPrice, _preClose);

                //13
                _new_string = Math.Round((double)_dr["LowerLimitPrice"], 1).ToString();
                _fu.LowerLimitPrice = SetTextAndColor(i, 13, _new_string, _fu.LowerLimitPrice, _preClose);

                //14
                _new_string = Math.Round((double)_dr["PreSettlementPrice"], 1).ToString();
                _fu.PreSettlementPrice = SetTextAndColor(i, 14, _new_string, _fu.PreSettlementPrice, _preClose);

                //15
                _new_string = Math.Round((double)_dr["PreClosePrice"], 1).ToString();
                _fu.PreClosePrice = SetTextAndColor(i, 15, _new_string, _fu.PreClosePrice, _preClose);

                //16
                _new_string = Math.Round((double)_dr["PreOpenInterest"], 1).ToString();
                _fu.PreOpenInterest = _new_string;
                if (first) ob_no2[i, 16] = 7;


            }


            Refresh2();
            //timer.Start();
            //timer_z.Start();
         
        }




        /// <summary>
        /// 刷新期权面板中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTimedEvent(bool first)
        {
            //Console.WriteLine("Timer elapsed! now=" + now + "  " + now.Millisecond + "  ");
            if (ObservableObj.Count < tot_line && !first)
            {
                return;
            }

            for (int i = 0; i < tot_line; i++)
            {
                option _op;
                if (first)
                    _op = ObservableObj2.ElementAt<option>(i);
                else
                    _op = ObservableObj.ElementAt<option>(i);

                DataRow _dr = (DataRow)All[_op.instrumentid1];

                string _new_string;

                double _preClose = (double)_dr["PreClosePrice"];
                _new_string = Math.Round((double)_dr["BidPrice1"], 1).ToString();
                _op.BidPrice1 = SetTextAndColor(i,0,_new_string,_op.BidPrice1,_preClose);

                _new_string = Math.Round((double)_dr["AskPrice1"], 1).ToString();
                _op.AskPrice1 = SetTextAndColor(i, 1, _new_string, _op.AskPrice1, _preClose);

                _new_string = Math.Round((double)_dr["LastPrice"], 1).ToString();
                _op.LastPrice1 = SetTextAndColor(i, 2, _new_string, _op.LastPrice1, _preClose);

                _new_string = Convert.ToString((int)(Int64)_dr["Volume"]);
                if (first)
                    ob_no2[i, 3] = 7;
                
                _op.Volume1 = _new_string;

                _new_string = Convert.ToString((int)(double)_dr["OpenInterest"]);
                if (first)
                    ob_no2[i, 4] = 7;
                _op.OpenInterest1 = _new_string;

                _new_string = Convert.ToString((int)(double)_dr["ExercisePrice"]);
                _op.ExercisePrice = _new_string;




                _dr = (DataRow)All[_op.instrumentid2];

                _new_string = Convert.ToString((int)(double)_dr["OpenInterest"]);
                if (first)
                    ob_no2[i, 6] = 7;
                _op.OpenInterest2 = _new_string;

                _new_string = Convert.ToString((int)(Int64)_dr["Volume"]);
                if (first)
                    ob_no2[i, 7] = 7;
                _op.Volume2 = _new_string;

                _new_string = Math.Round((double)_dr["LastPrice"], 1).ToString();
                _op.LastPrice2 = SetTextAndColor(i, 8, _new_string, _op.LastPrice2, _preClose);

                _new_string = Math.Round((double)_dr["AskPrice1"], 1).ToString();
                _op.AskPrice2 = SetTextAndColor(i, 9, _new_string, _op.AskPrice2, _preClose);

                _new_string = Math.Round((double)_dr["BidPrice1"], 1).ToString();
                _op.BidPrice2 = SetTextAndColor(i, 10, _new_string, _op.BidPrice2, _preClose);


                //ob_op[i] = _op;
            }


            Refresh();
            //timer.Start();
            //timer_z.Start();

        }




        delegate void RefreshCallBack();
        public void Refresh()
        {

            RefreshCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new RefreshCallBack(Refresh);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                for (int i = 0; i < tot_line; i++)
                {
                    ////option _op = ObservableObj[i] as option;
                    for (int j = 0; j < 11; j++)
                        if (ob_no2[i, j] > 0 && (j <= 4 || j >= 6))
                        {
                            UIElement u = pwindow.optionsMarketListView.ItemContainerGenerator.ContainerFromIndex(i) as UIElement;
                            if (u == null) return;

                            UIElement x = null;
                            while ((u = (VisualTreeHelper.GetChild(u, 0) as UIElement)) != null)
                            {
                                if (u is GridViewRowPresenter)
                                {
                                    x = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(u, j), 0) as UIElement;
                                    break;
                                }
                            }
                            //option dr = (pwindow.optionsMarketListView.Items[0]) as option;

                            if (j <= 1 || j >= 9)
                            {

                                System.Windows.Controls.Button b = x as System.Windows.Controls.Button;
                                //b.Style = pwindow.Resources["normalSty"] as Style;

                                //System.Windows.Controls.ListViewItem lvi = pwindow.optionsMarketListView.Items[i] as System.Windows.Controls.ListViewItem;
                                //红底
                                switch (ob_no2[i, j])
                                {
                                    case 1:
                                    case 6:
                                        ///此处为平价时，无背景
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style;
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 2:
                                        ///此处为价格变动，且为涨价时，有背景
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        b.Style = pwindow.Resources["marketListViewButtom_red"] as Style;
                                        break;
                                    case 3:
                                        ///此处为价格变动，且为跌价时，有背景
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        b.Style = pwindow.Resources["marketListViewButtom_green"] as Style;
                                        break;
                                    case 4:
                                        ///此处为价格不变，且为涨价时，无背景
                                        b.Foreground = System.Windows.Media.Brushes.Tomato;
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style;
                                        break;
                                    case 5:
                                        ///此处为价格不变，且为跌价时，无背景
                                        b.Foreground = System.Windows.Media.Brushes.LightGreen;
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style;                                       
                                        break;
                                    case 7:
                                        ///此处为中性数值，无背景
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style;
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                }

                            }
                            else
                            {
                                System.Windows.Controls.Label t = x as System.Windows.Controls.Label;
                                switch (ob_no2[i, j])
                                {
                                    case 1:
                                    case 6:
                                        ///此处为平价时，无背景
                                        t.Background = System.Windows.Media.Brushes.Transparent;
                                        t.Foreground = System.Windows.Media.Brushes.AliceBlue;
                                        break;
                                    case 2:
                                        ///此处为价格变动，且为涨价时，有背景
                                        t.Background = System.Windows.Media.Brushes.Red;
                                        t.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 3:
                                        ///此处为价格变动，且为跌价时，有背景
                                        t.Background = System.Windows.Media.Brushes.Green;
                                        t.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 4:
                                        ///此处为价格不变，且为涨价时，无背景
                                        t.Background = System.Windows.Media.Brushes.Transparent;
                                        t.Foreground = System.Windows.Media.Brushes.Tomato;
                                        break;
                                    case 5:
                                        ///此处为价格不变，且为跌价时，无背景
                                        t.Background = System.Windows.Media.Brushes.Transparent;
                                        t.Foreground = System.Windows.Media.Brushes.LightGreen;
                                        break;
                                    case 7:
                                        ///此处为中性数值，无背景
                                        t.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                }
                            }
                            //UIElement u = pwindow.optionsMarketListView
                            //((ListViewItem)pwindow.optionsMarketListView.Items[i]).SubItems[j].BackColor = Color.Red;

                        }


                }
                //pwindow.optionsMarketListView.Items.Refresh();
            }
        }

        /// <summary>
        /// 期货
        /// </summary>
        public void Refresh2()
        {

            RefreshCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new RefreshCallBack(Refresh2);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                for (int i = 0; i < tot_line; i++)
                {
                    ////option _op = ObservableObj[i] as option;
                    for (int j = 0; j < 17; j++)
                        if (ob_no2[i, j] > 0 && !(j == 0))
                        {
                            UIElement u = pwindow.futuresMarketListView.ItemContainerGenerator.ContainerFromIndex(i) as UIElement;
                            if (u == null) continue;

                            UIElement x = null;
                            while ((u = (VisualTreeHelper.GetChild(u, 0) as UIElement)) != null)
                            {
                                if (u is GridViewRowPresenter)
                                {
                                    x = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(u, j), 0) as UIElement;
                                    break;
                                }
                            }
                            //option dr = (pwindow.optionsMarketListView.Items[0]) as option;

                            if (j == 2 || j == 3)
                            {

                                System.Windows.Controls.Button b = x as System.Windows.Controls.Button;
                                //b.Style = pwindow.Resources["normalSty"] as Style;

                                //System.Windows.Controls.ListViewItem lvi = pwindow.optionsMarketListView.Items[i] as System.Windows.Controls.ListViewItem;
                                switch (ob_no2[i, j])
                                {
                                    case 1:
                                    case 6:
                                        ///此处为平价时，无背景
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style; 
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 2:
                                        ///此处为价格变动，且为涨价时，有背景
                                        b.Style = pwindow.Resources["marketListViewButtom_red"] as Style; 
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 3:
                                        ///此处为价格变动，且为跌价时，有背景
                                        b.Style = pwindow.Resources["marketListViewButtom_green"] as Style; 
                                        b.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 4:
                                        ///此处为价格不变，且为涨价时，无背景
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style; 
                                        b.Foreground = System.Windows.Media.Brushes.Tomato;
                                        break;
                                    case 5:
                                        ///此处为价格不变，且为跌价时，无背景
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style; 
                                        b.Foreground = System.Windows.Media.Brushes.LightGreen;
                                        break;
                                    case 7:
                                        ///此处为中性数值，无背景
                                        b.Style = pwindow.Resources["marketListViewButtom"] as Style; 
                                        b.Foreground = System.Windows.Media.Brushes.Yellow;
                                        break;
                                }

                            }
                            else
                            {
                                System.Windows.Controls.Label t = x as System.Windows.Controls.Label;
                                switch (ob_no2[i, j])
                                {
                                    case 1:
                                    case 6:
                                        ///此处为平价时，无背景
                                        t.Background = System.Windows.Media.Brushes.Transparent;
                                        t.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 2:
                                        ///此处为价格变动，且为涨价时，有背景
                                        t.Background = System.Windows.Media.Brushes.Red;
                                        t.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 3:
                                        ///此处为价格变动，且为跌价时，有背景
                                        t.Background = System.Windows.Media.Brushes.Green;
                                        t.Foreground = System.Windows.Media.Brushes.White;
                                        break;
                                    case 4:
                                        ///此处为价格不变，且为涨价时，无背景
                                        t.Background = System.Windows.Media.Brushes.Transparent;
                                        t.Foreground = System.Windows.Media.Brushes.Tomato;
                                        break;
                                    case 5:
                                        ///此处为价格不变，且为跌价时，无背景
                                        t.Background = System.Windows.Media.Brushes.Transparent;
                                        t.Foreground = System.Windows.Media.Brushes.LightGreen;
                                        break;
                                    case 7:
                                        ///此处为中性数值，无背景
                                        t.Foreground = System.Windows.Media.Brushes.AliceBlue;
                                        break;
                                }
                            }
                            //UIElement u = pwindow.optionsMarketListView
                            //((ListViewItem)pwindow.optionsMarketListView.Items[i]).SubItems[j].BackColor = Color.Red;

                        }


                }
                //pwindow.optionsMarketListView.Items.Refresh();
            }
        }



        /// <summary>
        /// 绑定期权并使其不可视
        /// </summary>
        delegate void BindingCallBack();
        public void Binding()
        {

            BindingCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new BindingCallBack(Binding);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                ObservableObj = ObservableObj2;
                pwindow.optionsMarketListView.Visibility = Visibility.Hidden;
                pwindow.optionsMarketListView.DataContext = ObservableObj;
            }

        }
        /// <summary>
        /// 绑定期货并使其不可视
        /// </summary>
        public void Binding2()
        {

            BindingCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new BindingCallBack(Binding2);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                ObservableOb = ObservableOb2;
                pwindow.futuresMarketListView.Visibility = Visibility.Hidden;
                pwindow.futuresMarketListView.DataContext = ObservableOb;
            }

        }

        /// <summary>
        /// 期权数据可视
        /// </summary>
        delegate void ShowCallBack();
        public void Show()
        {

            ShowCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new ShowCallBack(Show);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                pwindow.optionsMarketListView.Visibility = Visibility.Visible;
            }

        }
        /// <summary>
        /// 期货数据可视
        /// </summary>
        public void Show2()
        {

            ShowCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new ShowCallBack(Show2);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                pwindow.futuresMarketListView.Visibility = Visibility.Visible;
            }

        }


        delegate void SetTextCallback();
        public void GetChoice()
        {

            SetTextCallback d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new SetTextCallback(GetChoice);
                pwindow.Dispatcher.Invoke(d, new object[] { });
            }
            else
            {
                box_type = pwindow.typeComboBox.Text.Trim();
                box_exchange = pwindow.traderComboBox.Text.Trim();
                box_future = pwindow.subjectMatterComboBox.Text.Trim();
                for (int i = 0; i < MainWindow.NameSubject.Length; i++)
                    if (MainWindow.NameSubject[i].Equals(box_future))
                        instrumentname = MainWindow.NameOption[i];
                //instrumentname = box_future;
                box_time = pwindow.dueDateComboBox.Text;
                duedate = box_time;
                //updatesql = String.Format("SELECT * FROM minutedata a,staticdata s where s.instrumentid=a.instrumentid and s.instrumentname='{0}' and s.duedate='{1}'  and a.updatetime<'{2}' order by updatetime", instrumentname, duedate,TimeToString(now));
                exercisesql = String.Format("select exerciseprice from staticdata where instrumentname='{0}' and duedate='{1}' and callorput=0 order by exerciseprice ", instrumentname, duedate);
            }


        }


        public int TimeToInt(string time, int mill)
        {
            int hour = Convert.ToInt32(time.Substring(0, 2));
            int min = Convert.ToInt32(time.Substring(3, 2));
            int sec = Convert.ToInt32(time.Substring(6, 2));
            return (hour * 3600 + min * 60 + sec) * 1000 + mill;
        }

        public int TimeToInt(DateTime dt)
        {
            int hour = dt.Hour;
            int min = dt.Minute;
            int sec = dt.Second;
            int millisec = dt.Millisecond;
            return (hour * 3600 + min * 60 + sec) * 1000 + millisec;
        }

        public string TimeToString(DateTime dt)
        {
            string str = "";
            if (dt.Hour < 10) str += "0" + dt.Hour;
            else str += dt.Hour;
            str += ":";
            if (dt.Minute < 10) str += "0" + dt.Minute;
            else str += dt.Minute;
            str += ":";
            if (dt.Second < 10) str += "0" + dt.Second;
            else str += dt.Second;
            return str;
        }




    }
}
