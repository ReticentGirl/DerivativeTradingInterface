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
            timer = new System.Timers.Timer(500);
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

        public string TimeToString(DateTime dt)
        { 
            string str="";
            if (dt.Hour<10) str+="0"+dt.Hour;
            else str+=dt.Hour;
            str += ":";
            if (dt.Minute<10) str+="0"+dt.Minute;
            else str+=dt.Minute;
            str += ":";
            if (dt.Second<10) str+="0"+dt.Second;
            else str+=dt.Second;
            return str;
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
        Hashtable ep_no = new Hashtable(50);//行权价对应的行数
        int tot_line=0;
        public void Update()
        {
            timer.Stop();
            ClearOb();

            GetChoice();
            DataTable list = DataControl.QueryTable(exercisesql);
            Hashtable ep_op = new Hashtable(50);
            Hashtable epcp_row = new Hashtable(50);
            tot_line = list.Rows.Count;

            for (int j = 0; j < tot_line; j++)
            {
                double x=(double)list.Rows[j][0];
                ep_op[(int)x] = new option();
                ep_no[(int)x] = j;
            }


            DataTable dt= DataControl.QueryTable(updatesql);
            int i=0;
            string uptime;
            int uptimemill;
            int datatime;
            int nowtime = TimeToInt(now);
            while (i <dt.Rows.Count)
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
                        epcp_row["" + _ep + "C"] = i;

                    }
                    else
                        epcp_row["" + _ep + "P"] = i;
                }
                else break;
                i++;
            } 
          
            for (int j = 0; j < list.Rows.Count; j++)
            {

                option _op = (option)ep_op[(int)((double)list.Rows[j][0])];
                int rowid = (int)epcp_row["" + list.Rows[j][0] + "C"];
                if (rowid < 0)
                {
                    Console.WriteLine("ROWID < 0!");
                }
                else
                {
                    _op.AskPrice1 = Math.Round((double)dt.Rows[rowid]["AskPrice1"],1);
                    _op.BidPrice1 = Math.Round((double)dt.Rows[rowid]["BidPrice1"],1);
                    _op.ExercisePrice = (int)(double)dt.Rows[rowid]["ExercisePrice"];
                    _op.OpenInterest1 = (int)(double)dt.Rows[rowid]["OpenInterest"];
                    _op.LastPrice1 = Math.Round((double)dt.Rows[rowid]["LastPrice"],1);
                    _op.Volume1 = (int)(Int64)dt.Rows[rowid]["Volume"];
                }

                rowid = (int)epcp_row["" + list.Rows[j][0] + "P"];
                if (rowid < 0)
                {
                    Console.WriteLine("ROWID < 0!");
                }
                else
                {
                    _op.AskPrice2 = Math.Round( (double)dt.Rows[rowid]["AskPrice1"],1);
                    _op.BidPrice2 =Math.Round( (double)dt.Rows[rowid]["BidPrice1"],1);
                    _op.OpenInterest2 = (int)(double)dt.Rows[rowid]["OpenInterest"];
                    _op.LastPrice2 = Math.Round((double)dt.Rows[rowid]["LastPrice"],1);
                    _op.Volume2 = (int)(Int64)dt.Rows[rowid]["Volume"];
                }

                Adding(_op);
            }
         
            Binding();

            ////prepare for the dynamic change
            prepare();
            timer.Start();
            
        }

        int pt = 0;
        DataTable dtminute;
        public void prepare()
        {
            dynamicsql = String.Format("SELECT CallOrPut,UpdateTime,UpdateMillisec,LastPrice,AskPrice1,BidPrice1,ExercisePrice,OpenInterest,Volume FROM alldata0722 a,staticdata s where s.instrumentid=a.instrumentid and s.instrumentname='{0}' and s.duedate='{1}'  and a.updatetime >= '{2}' and a.updatetime <= '{3}' order by updatetime,updatemillisec", instrumentname, box_time, TimeToString(now), TimeToString(now.AddMinutes(1)));
            dtminute = DataControl.QueryTable(dynamicsql);
            pt = 0;
        }



        bool[] ob_no = new bool[30];
        option[] ob_op = new option[30];

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (pt >= dtminute.Rows.Count) return;
            timer.Stop();
            string uptime = (string)dtminute.Rows[pt]["UpdateTime"];
            int uptimemill = (int)(Int64)dtminute.Rows[pt]["UpdateMillisec"];
            now = now.AddMilliseconds(500);
            bool changed = false;
            for (int i = 0; i < tot_line; i++)
                ob_no[i] = false;
            while (pt<dtminute.Rows.Count && TimeToInt(now) >= TimeToInt(uptime, uptimemill))
            {
                int _line=(int)ep_no[(int)((double)dtminute.Rows[pt]["ExercisePrice"])];
                option _op = ObservableObj.ElementAt<option>(_line);
                bool _callOrPut = (bool)dtminute.Rows[pt]["CallOrPut"];
                if ( _callOrPut)
                {
                    _op.AskPrice1 = Math.Round((double)dtminute.Rows[pt]["AskPrice1"], 1);
                    _op.BidPrice1 = Math.Round((double)dtminute.Rows[pt]["BidPrice1"], 1);
                    _op.ExercisePrice = (int)(double)dtminute.Rows[pt]["ExercisePrice"];
                    _op.OpenInterest1 = (int)(double)dtminute.Rows[pt]["OpenInterest"];
                    _op.LastPrice1 = Math.Round((double)dtminute.Rows[pt]["LastPrice"], 1);
                    _op.Volume1 = (int)(Int64)dtminute.Rows[pt]["Volume"];
                }
                else
                {
                    _op.AskPrice2 = Math.Round((double)dtminute.Rows[pt]["AskPrice1"], 1);
                    _op.BidPrice2 = Math.Round((double)dtminute.Rows[pt]["BidPrice1"], 1);
                    _op.OpenInterest2 = (int)(double)dtminute.Rows[pt]["OpenInterest"];
                    _op.LastPrice2 = Math.Round((double)dtminute.Rows[pt]["LastPrice"], 1);
                    _op.Volume2 = (int)(Int64)dtminute.Rows[pt]["Volume"];
                }
                
                uptime = (string)dtminute.Rows[pt]["UpdateTime"];
                uptimemill = (int)(Int64)dtminute.Rows[pt]["UpdateMillisec"];
                pt++;
                changed = true;
                ob_no[_line]=true;
                ob_op[_line]=_op;
            }

            if (changed)
                Refresh();
            timer.Start();
        }

        delegate void RefreshCallBack();
        public void Refresh()
        {

            RefreshCallBack d;
            if (System.Threading.Thread.CurrentThread != pwindow.Dispatcher.Thread)
            {
                d = new RefreshCallBack(Refresh);
                pwindow.Dispatcher.Invoke(d, new object[] {  });
            }
            else
            {
                for (int i = 0; i < tot_line; i++)
                if (ob_no[i]){
                    ObservableObj.RemoveAt(i);
                    ObservableObj.Insert(i, ob_op[i]);
                }
                //pwindow.optionsMarketListView.Items.Refresh();
            }

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



        static string updatesql,exercisesql,dynamicsql,box_type,box_exchange,box_future,box_time,instrumentname;
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
                box_type=pwindow.typeComboBox.Text ;
                box_exchange=pwindow.traderComboBox.Text;
                box_future = pwindow.subjectMatterComboBox.Text;
                instrumentname = box_future;
                box_time = "1409";
                updatesql = String.Format("SELECT CallOrPut,UpdateTime,UpdateMillisec,LastPrice,AskPrice1,BidPrice1,ExercisePrice,OpenInterest,Volume FROM minutedata a,staticdata s where s.instrumentid=a.instrumentid and s.instrumentname='{0}' and s.duedate='{1}'  and a.updatetime<'09:31:00' order by updatetime", instrumentname, box_time);
                exercisesql = String.Format("select exerciseprice from staticdata where instrumentname='{0}' and duedate='{1}' and callorput=0 order by exerciseprice ", instrumentname, box_time);
            }
         
            
        }





        
    }
}
