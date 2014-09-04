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

        public void initial(ObservableCollection<StockInfo> oc1,ObservableCollection<StockInfo> oc2)
        {
            Data = new ObservableCollection<StockInfo>();
            int i1=0, i2=0;
            while (i1<oc1.Count && i2<oc2.Count)  {
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
