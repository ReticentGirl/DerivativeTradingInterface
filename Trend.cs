using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;

namespace qiquanui
{
    class Trend
    {

        public DateTime StringToDate(string trading, string update)
        {
            return new DateTime(Convert.ToInt32(trading.Substring(0, 4)), Convert.ToInt32(trading.Substring(4, 2)), Convert.ToInt32(trading.Substring(6, 2)),
                Convert.ToInt32(update.Substring(0, 2)), Convert.ToInt32(update.Substring(3, 2)), 0);
        }

        public ObservableCollection<StockInfo> Data { get; set; }
        private Timer timer;
        public string id;

        public void initial(string _id)
        {
            this.id = _id;
            initial();
        }

        public void initial()
        {
            if (id == null) return;
            if (id.Equals("上证50指数")) id = "IH1409";
            if (id.Equals("沪深300指数")) id = "IF1409";
            Data = new ObservableCollection<StockInfo>();
            string sql = "select * from minutedata where instrumentid='" + id + "' order by TradingDay,updatetime";
            DataTable dt = DataControl.QueryTable(sql);
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Initialing Trend Error! " + id + " Count==0");
                return;
            }
            DateTime start = new DateTime(2014, 7, 22);
            DateTime end = DataManager.now.AddMinutes(1);
            end = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 0);
            DateTime present = new DateTime(start.Year, start.Month, start.Day, 9, 15, 0);
            int i = 0;
            StockInfo last = null;
            DateTime nextLine = StringToDate((string)dt.Rows[i]["TradingDay"], (string)dt.Rows[i]["UpdateTime"]);
            do
            {
                while (nextLine < present && i < dt.Rows.Count)
                {
                    i++;
                    if (i == dt.Rows.Count)
                        nextLine = end;
                    else
                        nextLine = StringToDate((string)dt.Rows[i]["TradingDay"], (string)dt.Rows[i]["UpdateTime"]);
                }
                if (present.Equals(nextLine))
                {
                    double _open = Math.Round((double)dt.Rows[i]["OpenPrice"], 1);
                    double _close = Math.Round((double)dt.Rows[i]["LastPrice"], 1);
                    double _high = Math.Round((double)dt.Rows[i]["HighestPrice"], 1);
                    double _low = Math.Round((double)dt.Rows[i]["LowestPrice"], 1);
                    double _volume = Math.Round((double)dt.Rows[i]["OpenInterest"], 1);
                    last = new StockInfo
                    {
                        date = present,
                        open = _open,
                        high = _high,
                        low = _low,
                        close = _close,
                        volume = _volume
                    };
                }
                else if (last != null)
                {
                    last = new StockInfo
                    {
                        date = present,
                        open = last.open,
                        high = last.high,
                        low = last.low,
                        close = last.close,
                        volume = last.volume
                    };
                }
                if (last != null)
                    Data.Add(last);

                present = present.AddMinutes(1);
                if (present.Hour == 11 && present.Minute == 31)
                    present = new DateTime(present.Year, present.Month, present.Day, 13, 0, 0);
                if (present.Hour == 15 && present.Minute == 16)
                {
                    present = present.AddDays(1);
                    present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
                }
                //Console.WriteLine(present);
            }
            while (!present.Equals(end));


        }

        public void initial(ObservableCollection<StockInfo> oc1, ObservableCollection<StockInfo> oc2)
        {
            Data = new ObservableCollection<StockInfo>();
            int i1 = 0, i2 = 0;
            while (i1 < oc1.Count && i2 < oc2.Count)
            {
                if (oc1[i1].date < oc2[i2].date)
                    i1++;
                else
                    if (oc1[i1].date > oc2[i2].date)
                        i2++;
                    else
                        break;
            }
            while (i1 < oc1.Count && i2 < oc2.Count)
            {
                StockInfo si1 = oc1[i1];
                StockInfo si2 = oc2[i2];
                StockInfo si3 = new StockInfo();
                si3.date = si1.date;
                si3.close = si1.close - si2.close;
                Data.Add(si3);
                i1++;
                i2++;
            }
        }

    }


    public class TrendPoint : INotifyPropertyChanged
    {
        private string x;
        private double y;

        // public double x { get; set; }
        // public double y { get; set; }

        public TrendPoint()
        {

        }

        public TrendPoint(string _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public string X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
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



    class Trend2
    {
        public DateTime StringToDate(string trading, string update)
        {
            return new DateTime(Convert.ToInt32(trading.Substring(0, 4)), Convert.ToInt32(trading.Substring(4, 2)), Convert.ToInt32(trading.Substring(6, 2)),
                Convert.ToInt32(update.Substring(0, 2)), Convert.ToInt32(update.Substring(3, 2)), 0);
        }

        public string DateToMinute(DateTime dt)
        {
            string ans = "";
            ans += dt.Hour;
            if (dt.Minute < 10)
                ans += "0" + dt.Minute;
            else
                ans += dt.Minute;
            return ans;
        }

        public string id,id2;
        public void initial(string _id, Window _pwindow)
        {
            this.id = _id;
            this.id2 = null;
            this.pwindow = _pwindow;
            initial();
        }

        public void initial2(string _id1, string _id2, Window _pwindow)
        {
            this.id = _id1;
            this.id2 = _id2;
            this.pwindow = _pwindow;
            initial();
            initial2();
            initialDiff();
        }

        public Window pwindow;
        public ObservableCollection<TrendPoint> Data, Data2, DataDiff1, DataDiff2;
        public System.Windows.Data.Binding CoorBinding;
        public System.Windows.Data.Binding DataBinding, DataBinding2, DataBindingDiff1, DataBindingDiff2;
        public ObservableCollection<TrendPoint> Coor;
        Timer timer;
        public string DateToTradingDay(DateTime dt)
        {
            string ans;
            ans = dt.Year.ToString();
            if (dt.Month < 10)
                ans += "0" + dt.Month;
            else
                ans += dt.Month;
            if (dt.Day < 10)
                ans += "0" + dt.Day;
            else
                ans += dt.Day;
            return ans;
        }



        public int middleNumber = 0;
        public DateTime middle = new DateTime();
        public DateTime end = new DateTime();
        private void initial()
        {
            if (id == null) return;
            if (id.Equals("上证50指数")) id = "IH1409";
            if (id.Equals("沪深300指数")) id = "IF1409";
            string _tradingday = DateToTradingDay(DataManager.now);
            string sql = "select * from minutedata where instrumentid='" + id + "' and tradingday='" + _tradingday + "'  order by TradingDay,updatetime";
            DataTable dt = DataControl.QueryTable(sql);
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Initialing Trend Error! " + id + " Count==0");
                return;
            }

            Data = new ObservableCollection<TrendPoint>();
            Coor = new ObservableCollection<TrendPoint>();

            DateTime now = DataManager.now;

            DateTime start = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            middle = now.AddMinutes(0);
            middle = new DateTime(middle.Year, middle.Month, middle.Day, middle.Hour, middle.Minute, 0);
            end = new DateTime(now.Year, now.Month, now.Day, 15, 16, 0);
            middleNumber = -1;

            DateTime present = start;
            int i = 0;
            DateTime nextLine = StringToDate((string)dt.Rows[i]["TradingDay"], (string)dt.Rows[i]["UpdateTime"]);
            TrendPoint last = new TrendPoint(DateToMinute(start.AddMinutes(-1)), (double)dt.Rows[0]["PreClosePrice"]);
            do
            {
                while (nextLine < present && i < dt.Rows.Count)
                {
                    i++;
                    if (i == dt.Rows.Count)
                        nextLine = end;
                    else
                        nextLine = StringToDate((string)dt.Rows[i]["TradingDay"], (string)dt.Rows[i]["UpdateTime"]);
                }
                if (present.Equals(nextLine))
                {
                    string time = DateToMinute(present);
                    double price = (double)dt.Rows[i]["LastPrice"];
                    last = new TrendPoint(time, price);
                }
                else
                {
                    string time = DateToMinute(present);
                    last = new TrendPoint(time, last.Y);

                }


                if (present <= middle)
                {
                    Data.Add(last);
                    Coor.Add(last);
                }
                else
                {
                    Coor.Add(last);
                }


                if (present <= middle)
                    middleNumber++;

                present = present.AddMinutes(1);
                if (present.Hour == 11 && present.Minute == 31)
                    present = new DateTime(present.Year, present.Month, present.Day, 13, 0, 0);
                //if (present.Hour == 15 && present.Minute == 16)
                //{
                //    present = present.AddDays(1);
                //    present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
                //}
                //Console.WriteLine(present);
            }
            while (!present.Equals(end));


            CoorBinding = new System.Windows.Data.Binding();
            CoorBinding.Source = Coor;
            DataBinding = new System.Windows.Data.Binding();
            DataBinding.Source = Data;



            timer = new Timer(500);
            timer.Elapsed += new ElapsedEventHandler(RefreshTrend);
            timer.Start();
        }

        private void initial2()
        {
            timer.Stop();
            if (id2 == null) return;
            if (id2.Equals("上证50指数")) id2 = "IH1409";
            if (id2.Equals("沪深300指数")) id2 = "IF1409";
            string _tradingday = DateToTradingDay(DataManager.now);
            string sql = "select * from minutedata where instrumentid='" + id2 + "' and tradingday='" + _tradingday + "'  order by TradingDay,updatetime";
            DataTable dt = DataControl.QueryTable(sql);
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Initialing Trend Error! " + id2 + " Count==0");
                return;
            }

            Data2 = new ObservableCollection<TrendPoint>();

            DateTime now = DataManager.now;

            DateTime start = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            middle = now.AddMinutes(0);
            middle = new DateTime(middle.Year, middle.Month, middle.Day, middle.Hour, middle.Minute, 0);
            end = new DateTime(now.Year, now.Month, now.Day, 15, 16, 0);
            middleNumber = -1;

            DateTime present = start;
            int i = 0;
            DateTime nextLine = StringToDate((string)dt.Rows[i]["TradingDay"], (string)dt.Rows[i]["UpdateTime"]);
            TrendPoint last = new TrendPoint(DateToMinute(start.AddMinutes(-1)), (double)dt.Rows[0]["PreClosePrice"]);
            do
            {
                while (nextLine < present && i < dt.Rows.Count)
                {
                    i++;
                    if (i == dt.Rows.Count)
                        nextLine = end;
                    else
                        nextLine = StringToDate((string)dt.Rows[i]["TradingDay"], (string)dt.Rows[i]["UpdateTime"]);
                }
                if (present.Equals(nextLine))
                {
                    string time = DateToMinute(present);
                    double price = (double)dt.Rows[i]["LastPrice"];
                    last = new TrendPoint(time, price);
                }
                else
                {
                    string time = DateToMinute(present);
                    last = new TrendPoint(time, last.Y);

                }


                if (present <= middle)
                {
                    Data2.Add(last);
                }



                if (present <= middle)
                    middleNumber++;

                present = present.AddMinutes(1);
                if (present.Hour == 11 && present.Minute == 31)
                    present = new DateTime(present.Year, present.Month, present.Day, 13, 0, 0);
                //if (present.Hour == 15 && present.Minute == 16)
                //{
                //    present = present.AddDays(1);
                //    present = new DateTime(present.Year, present.Month, present.Day, 9, 15, 0);
                //}
                //Console.WriteLine(present);
            }
            while (!present.Equals(end));


            DataBinding2 = new System.Windows.Data.Binding();
            DataBinding2.Source = Data2;
            timer.Start();
        }

        private void initialDiff()
        {
            timer.Stop();

            DataDiff1 = new ObservableCollection<TrendPoint>();
            DataDiff2 = new ObservableCollection<TrendPoint>();

            for (int i = 0; i < Data.Count; i++)
            {

                TrendPoint tp = new TrendPoint(Data[i].X,Data[i].Y-Data2[i].Y);
                if (tp.Y>0)
                    DataDiff1.Add(tp);
                else if (tp.Y < 0)
                    DataDiff2.Add(tp);
                else if (tp.Y == 0)
                {
                    DataDiff1.Add(tp);
                    DataDiff2.Add(tp);
       
                }
            }


            DataBindingDiff1 = new System.Windows.Data.Binding();
            DataBindingDiff1.Source = DataDiff1;
            DataBindingDiff2 = new System.Windows.Data.Binding();
            DataBindingDiff2.Source = DataDiff2;


            timer.Start();
        }


        delegate void RateTextCallBack(object sender, ElapsedEventArgs e);
        private void RefreshTrend(object sender, ElapsedEventArgs e)
        {
            RateTextCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new RateTextCallBack(RefreshTrend);
                pwindow.Dispatcher.Invoke(d, new object[] { sender, e });
            }
            else
            {
                timer.Stop();
                DateTime now = DataManager.now;
                if (now >= end)
                    return;
                if (now > middle.AddMinutes(1))
                {
                    middle.AddMinutes(1);
                    middleNumber++;
                    DateTime newtime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                    Data.Add(new TrendPoint(DateToMinute(newtime), 0));
                    if (id2!=null)
                        Data2.Add(new TrendPoint(DateToMinute(newtime), 0));
                }

                DataRow row = (DataRow)DataManager.All[id];
                double lastprice = (double)row["LastPrice"];
                string oldx=Data[middleNumber].X;
                Data.RemoveAt(middleNumber);
                Data.Add(new TrendPoint(oldx, lastprice));
               // Data[middleNumber].Y = lastprice;
                if (id2 != null)
                {
                    DataRow row2 = (DataRow)DataManager.All[id2];
                    double lastprice2 = (double)row2["LastPrice"];
                    Data2[middleNumber].Y = lastprice2;
                   // DataDiff1[middleNumber].Y = Data[middleNumber].Y - Data2[middleNumber].Y;
                }
                timer.Start();
            }


        }




    }

    public class StockInfo : INotifyPropertyChanged
    {
        private DateTime _date;
        private double _open;
        private double _close;
        private double _high;
        private double _low;
        private double _volume;

        public DateTime date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(new PropertyChangedEventArgs("date"));
            }
        }
        public double open
        {
            get { return _open; }
            set
            {
                _open = value;
                OnPropertyChanged(new PropertyChangedEventArgs("open"));
            }
        }
        public double close
        {
            get { return _close; }
            set
            {
                _close = value;
                OnPropertyChanged(new PropertyChangedEventArgs("close"));
            }
        }
        public double high
        {
            get { return _high; }
            set
            {
                _high = value;
                OnPropertyChanged(new PropertyChangedEventArgs("high"));
            }
        }
        public double low
        {
            get { return _low; }
            set
            {
                _low = value;
                OnPropertyChanged(new PropertyChangedEventArgs("low"));
            }
        }
        public double volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged(new PropertyChangedEventArgs("volume"));
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



        /// <summary>
        /// 时间
        /// </summary>

        /// <summary>
        /// 开盘价
        /// </summary>
        /// <summary>
        /// 最高价
        /// </summary>
        /// <summary>
        /// 最低价
        /// </summary>
        /// <summary>
        /// 收盘价
        /// </summary>

        /// <summary>
        /// 成交量
        /// </summary>
    }

}