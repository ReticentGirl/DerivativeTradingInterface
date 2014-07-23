using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Collections;
using System.Collections.ObjectModel;

namespace qiquanui
{
    public struct option : INotifyPropertyChanged
    {
        private double bidPrice1;
        private double askPrice1;
        private double lastPrice1;
        private int volume1;
        private int openInterest1;
        private int exercisePrice;
        private int openInterest2;
        private int volume2;
        private double bidPrice2;
        private double askPrice2;
        private double lastPrice2;

        public double BidPrice1
        {
            get { return bidPrice1; }
            set
            {
                bidPrice1 = value;
                //OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        public double AskPrice1
        {
            get { return askPrice1; }
            set
            {
                askPrice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ImageUrl"));
            }
        }
        public double LastPrice1
        {
            get { return lastPrice1; }
            set
            {
                lastPrice1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("lastPrice1"));
            }
        }
        public int Volume1
        {
            get { return volume1; }
            set { volume1 = value; OnPropertyChanged(new PropertyChangedEventArgs("Age")); }
        }
        public int OpenInterest1
        {
            get { return openInterest1; }
            set { openInterest1 = value; OnPropertyChanged(new PropertyChangedEventArgs("Age")); }
        }
        public int ExercisePrice
        {
            get { return exercisePrice; }
            set
            {
                exercisePrice = value;
                //OnPropertyChanged(new PropertyChangedEventArgs("Age")); 
            }
        }

        public double BidPrice2
        {
            get { return bidPrice2; }
            set
            {
                bidPrice2 = value;
                //OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        public double AskPrice2
        {
            get { return askPrice2; }
            set { askPrice2 = value; OnPropertyChanged(new PropertyChangedEventArgs("ImageUrl")); }
        }
        public double LastPrice2
        {
            get { return lastPrice2; }
            set
            {
                lastPrice2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LastPrice2"));
            }
        }
        public int Volume2
        {
            get { return volume2; }
            set
            {
                volume2 = value;
                //OnPropertyChanged(new PropertyChangedEventArgs("Age"));
            }
        }
        public int OpenInterest2
        {
            get { return openInterest2; }
            set
            {
                openInterest2 = value;
                //OnPropertyChanged(new PropertyChangedEventArgs("Age")); 
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

    class DataManager
    {
        Timer timer;
        MainWindow pwindow;
        DateTime now;
        public ObservableCollection<option> ObservableObj = new ObservableCollection<option>();


        public DataManager(MainWindow window)
        {
            pwindow = window;
            timer = new System.Timers.Timer(100);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            now = new DateTime(2014, 7, 22, 14, 0, 25);
        }

        public int TimeToInt(string time,int mill)
        {
            int hour =Convert.ToInt32( time.Substring(0,2));
            int min = Convert.ToInt32(time.Substring(3,2));
            int sec = Convert.ToInt32(time.Substring(6,2));
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

        public void Update()
        {
            ClearOb();

            GetChoice();
            DataTable list = DataControl.QueryTable(exercisesql);
            Hashtable ep = new Hashtable(50);
            Hashtable position= new Hashtable(50);
        
            for (int j = 0; j < list.Rows.Count; j++)
            {
                double x=(double)list.Rows[j][0];
                ep[(int)x] = new option();

            }


            DataTable dt= DataControl.QueryTable(updatesql);
            int i=0;
            string uptime;
            int uptimemill;
            int datatime;
            int nowtime=TimeToInt(DateTime.Now);
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
                    {
                        position["" + _ep + "C"] = i;

                    }
                    else
                        position["" + _ep + "P"] = i;
                }
                else break;
                i++;
            } 
          
            for (int j = 0; j < list.Rows.Count; j++)
            {

                option _op = (option)ep[(int)((double)list.Rows[j][0])];
                int rowid =(int) position["" + list.Rows[j][0] + "C"];
                if (rowid < 0)
                {
                    Console.WriteLine("ROWID < 0!");
                }
                else
                {
                    _op.AskPrice1 = (double)dt.Rows[rowid]["AskPrice1"];
                    _op.BidPrice1 = (double)dt.Rows[rowid]["BidPrice1"];
                    _op.ExercisePrice = (int)(double)dt.Rows[rowid]["ExercisePrice"];
                    _op.OpenInterest1 = (int)(double)dt.Rows[rowid]["OpenInterest"];
                    _op.LastPrice1 = (double)dt.Rows[rowid]["LastPrice"];
                    _op.Volume1 = (int)(Int64)dt.Rows[rowid]["Volume"];
                }

                rowid = (int)position["" + list.Rows[j][0] + "P"];
                if (rowid < 0)
                {
                    Console.WriteLine("ROWID < 0!");
                }
                else
                {
                    _op.AskPrice2 = (double)dt.Rows[rowid]["AskPrice1"];
                    _op.BidPrice2 = (double)dt.Rows[rowid]["BidPrice1"];
                    _op.OpenInterest2 = (int)(double)dt.Rows[rowid]["OpenInterest"];
                    _op.LastPrice2 = (double)dt.Rows[rowid]["LastPrice"];
                    _op.Volume2 = (int)(Int64)dt.Rows[rowid]["Volume"];
                }

                Adding(_op);
            }
         
            Binding();


        }
        delegate void AddingCallBack(option _op);
        public void Adding(option _op)
        {

            AddingCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new AddingCallBack(Adding);
                pwindow.Dispatcher.Invoke(d, new object[] { _op });
            }
            else
            {
                ObservableObj.Add(_op);
            }

        }

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
                pwindow.optionsMarketListView.DataContext = ObservableObj;
            }

        }



        static string updatesql,exercisesql;
        delegate void SetTextCallback();
        public void GetChoice()
        {
            
            SetTextCallback d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                 d = new SetTextCallback(GetChoice);
                 pwindow.Dispatcher.Invoke(d,new object[]{});
            }
            else
            {
                string box_type=pwindow.typeComboBox.Text ;
                string box_exchange=pwindow.futuresTraderComboBox.Text;
                string box_future = pwindow.nameComboBox.Text;
                string instrumentname=box_future;
                string box_time = "1409";
                updatesql = String.Format("SELECT CallOrPut,UpdateTime,UpdateMillisec,LastPrice,AskPrice1,BidPrice1,ExercisePrice,OpenInterest,Volume FROM alldata0722 a,staticdata s where s.instrumentid=a.instrumentid and s.instrumentname='{0}' and s.duedate='{1}'  and a.updatetime<'09:31:00' order by updatetime", instrumentname, box_time);
                exercisesql = String.Format("select exerciseprice from staticdata where instrumentname='{0}' and duedate='{1}' and callorput=0 ",instrumentname, box_time);
               
            }
         
            
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            
        }
        
    }
}
