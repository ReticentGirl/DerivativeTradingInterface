using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class option : INotifyPropertyChanged
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
        public string instrumentid1;
        public string instrumentid2;

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
        System.Timers.Timer timer;
        MainWindow pwindow;
        DateTime now;
        public ObservableCollection<option> ObservableObj = new ObservableCollection<option>();
        private ObservableCollection<option> ObservableObj2 = new ObservableCollection<option>();
        public static Hashtable All = new Hashtable(2000);
        static string updatesql, exercisesql, dynamicsql, box_type, box_exchange, box_future, box_time, duedate = "", instrumentname = "";

        Hashtable ep_no = new Hashtable(50);//行权价对应的行数
        int tot_line = 0;

        const int Max_line = 30;
        bool[,] ob_no2 = new bool[Max_line, 20];//表示行index1的第index2列是否被修改（OnTimedEvent更新面板数据时）
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
            mytimer+=TIMER_UNIT;
            now=now.AddMilliseconds(TIMER_UNIT);
            
            if (mytimer % timer_milli[0]==0)
            {
                OnTimedEvent();
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
            DateTime preday=now.AddDays(-1);
            if (preday.DayOfWeek == DayOfWeek.Sunday) preday.AddDays(-2);
            if (preday.DayOfWeek == DayOfWeek.Saturday) preday.AddDays(-1);
            string _month,_day;
            if (preday.Month<10) _month="0"+preday.Month;
            else _month=""+preday.Month;
            if (preday.Day<10) _day="0"+preday.Day;
            else _day=Convert.ToString(preday.Day);
            string _date="2014"+_month+_day;
            string initialsql = String.Format("select * from daydata d,staticdata s where tradingday='{0}' and d.instrumentid=s.instrumentid", _date);
            DataTable _dt = DataControl.QueryTable(initialsql);
            for (int i = 0; i < _dt.Rows.Count; i++)
            { 
                string _id=(string) _dt.Rows[i]["InstrumentID"];
                All[_id] = _dt.Rows[i];
            }
        }

        public DataManager(MainWindow window)
        {
            pwindow = window;
            timer = new System.Timers.Timer(TIMER_UNIT);
            timer.Elapsed += new ElapsedEventHandler(TimeManage);
            timer.Start();

            now = new DateTime(2014, 7, 23, 14, 0, 25);

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
            ObservableObj2 = new ObservableCollection<option>();

            ///将界面中所选的东西保存在全局变量中
            GetChoice();

            ///根据行权价的列表list，将所有行权价对应的行数保存在ep_no中
            DataTable list = DataControl.QueryTable(exercisesql);
            Hashtable epcp_row = new Hashtable(50);
            tot_line = list.Rows.Count;
           
            for (int j = 0; j < tot_line; j++)
            {
                double x = (double)list.Rows[j][0];              
                ep_no[(int)x] = j;
            }


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


            Binding();



        }





        /// <summary>
        /// 刷新面板中的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnTimedEvent()
        {
            //Console.WriteLine("Timer elapsed! now=" + now + "  " + now.Millisecond + "  ");
            if (ObservableObj.Count < tot_line)
            {
                return;
            }

            for (int i = 0; i < tot_line; i++)
                for (int j = 0; j < 11; j++)
                    ob_no2[i, j] = false;

            for (int i = 0; i < tot_line; i++)
            {

                option _op=ObservableObj.ElementAt<option>(i);

                DataRow _dr = (DataRow)All[_op.instrumentid1];
                double _new_double;
                int _new_int;

                _new_double = Math.Round((double)_dr["BidPrice1"], 1);
                if (!_new_double.Equals(_op.BidPrice1)) 
                    ob_no2[i, 0] = true;
                _op.BidPrice1 = _new_double;

                _new_double=Math.Round((double)_dr["AskPrice1"], 1);
                if (!_new_double.Equals(_op.AskPrice1)) 
                    ob_no2[i, 1] = true;
                _op.AskPrice1 = _new_double;

                _new_double = Math.Round((double)_dr["LastPrice"], 1);
                if (!_new_double.Equals(_op.LastPrice1)) 
                    ob_no2[i, 2] = true;
                _op.LastPrice1 = _new_double;

                _new_int = (int)(Int64)_dr["Volume"];
                if (!_new_int.Equals(_op.Volume1)) 
                    ob_no2[i, 3] = true;
                _op.Volume1 = _new_int;

                _new_int = (int)(double)_dr["OpenInterest"];
                if (!_new_int.Equals(_op.OpenInterest1)) 
                    ob_no2[i, 4] = true;
                 _op.OpenInterest1 = _new_int;

                _new_int=(int)(double)_dr["ExercisePrice"];
                if (!_new_int.Equals(_op.ExercisePrice)) 
                    ob_no2[i, 5] = true;
                _op.ExercisePrice = _new_int;




                _dr = (DataRow)All[_op.instrumentid2];

                _new_int = (int)(double)_dr["OpenInterest"];
                if (!_new_int.Equals(_op.OpenInterest2)) ob_no2[i, 6] = true;
                _op.OpenInterest2 = _new_int;

                _new_int = (int)(Int64)_dr["Volume"];
                if (!_new_int.Equals(_op.Volume2)) ob_no2[i, 7] = true;
                _op.Volume2 = _new_int;

                _new_double = Math.Round((double)_dr["LastPrice"], 1);
                if (!_new_double.Equals(_op.LastPrice2)) ob_no2[i, 8] = true;
                _op.LastPrice2 = _new_double;

                _new_double = Math.Round((double)_dr["AskPrice1"], 1);
                if (!_new_double.Equals(_op.AskPrice2)) ob_no2[i, 9] = true;
                _op.AskPrice2 = _new_double;

                _new_double = Math.Round((double)_dr["BidPrice1"], 1);
                if (!_new_double.Equals(_op.BidPrice2)) ob_no2[i, 10] = true;
                _op.BidPrice2 = _new_double;


                //ob_op[i] = _op;
            }
            

            //Refresh();
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
                        if (ob_no2[i, j] && j<2 )
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

                            System.Windows.Controls.Button b = x as System.Windows.Controls.Button;
                            b.Style = pwindow.Resources["normalSty"] as Style;

                            //System.Windows.Controls.ListViewItem lvi = pwindow.optionsMarketListView.Items[i] as System.Windows.Controls.ListViewItem;
                            b.Background = System.Windows.Media.Brushes.Red;
                            b.FontStyle = FontStyles.Italic;
                            b.Foreground = System.Windows.Media.Brushes.Green; 
                            //UIElement u = pwindow.optionsMarketListView
                            //((ListViewItem)pwindow.optionsMarketListView.Items[i]).SubItems[j].BackColor = Color.Red;


                            //switch (j)
                            //{
                            //    case 0: _op.BidPrice1 = ob_op[i].BidPrice1; break;
                            //    case 1: _op.AskPrice1 = ob_op[i].AskPrice1; break;
                            //    case 2: _op.LastPrice1 = ob_op[i].LastPrice1; break;
                            //    case 3: _op.Volume1 = ob_op[i].Volume1; break;
                            //    case 4: _op.OpenInterest1 = ob_op[i].OpenInterest1; break;
                            //    case 6: _op.OpenInterest2 = ob_op[i].OpenInterest2; break;
                            //    case 7: _op.Volume2 = ob_op[i].Volume2; break;
                            //    case 8: _op.LastPrice2 = ob_op[i].LastPrice2; break;
                            //    case 9: _op.AskPrice2 = ob_op[i].AskPrice2; break;
                            //    case 10: _op.BidPrice2 = ob_op[i].BidPrice2; break;
                            //}
                        }
                    

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
                ObservableObj = ObservableObj2;
                pwindow.optionsMarketListView.DataContext = ObservableObj;
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
                box_type = pwindow.typeComboBox.Text;
                box_exchange = pwindow.traderComboBox.Text;
                box_future = pwindow.subjectMatterComboBox.Text;
                for (int i = 0; i < MainWindow.NameSubject.Length; i++)
                    if (MainWindow.NameSubject[i].Equals(box_future))
                        instrumentname = MainWindow.NameOption[i];
                //instrumentname = box_future;
                box_time = pwindow.dueDateComboBox.Text;
                duedate = box_time;
                updatesql = String.Format("SELECT * FROM minutedata a,staticdata s where s.instrumentid=a.instrumentid and s.instrumentname='{0}' and s.duedate='{1}'  and a.updatetime<'{2}' order by updatetime", instrumentname, duedate,TimeToString(now));
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
