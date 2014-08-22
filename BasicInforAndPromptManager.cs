using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Timers;
using System.Data;


namespace qiquanui
{
    public class BasicInforAndPromptData : INotifyPropertyChanged
    {
        private string nowUserID;
        private string totalCapital;   //用户总资本
        private string availableCapital;//可用资产
        private string occupyCapital; //占用资产
        private string nowInstrumentID;
        private string dayToEnd;     //到期天数
        private string dueDate;   //到期日
        private string tickSize; //最小变动价位
        private string maxTradeNum; //最大可交易手数

        public string NowUserID
        {
            get { return nowUserID; }
            set
            {

                nowUserID = value;
                OnPropertyChanged("NowUserID");
            }
        }


        public string TotalCapital
        {
            get { return totalCapital; }
            set
            {

                totalCapital = value;
                OnPropertyChanged("TotalCapital");
            }
        }


        public string AvailableCapital  //availableCapital
        {
            get { return availableCapital; }
            set
            {

                availableCapital = value;
                OnPropertyChanged("AvailableCapital");
            }
        }



        public string OccupyCapital   //occupyCapital; 
        {
            get { return occupyCapital; }
            set
            {

                occupyCapital = value;
                OnPropertyChanged("OccupyCapital");
            }
        }



        public string NowInstrumentID   //nowInstrumentID;
        {
            get { return nowInstrumentID; }
            set
            {

                nowInstrumentID = value;
                OnPropertyChanged("NowInstrumentID");
            }
        }



        public string DayToEnd   //dayToEnd;     
        {
            get { return dayToEnd; }
            set
            {

                dayToEnd = value;
                OnPropertyChanged("DayToEnd");
            }
        }



        public string DueDate  //dueDate;   
        {
            get { return dueDate; }
            set
            {

                dueDate = value;
                OnPropertyChanged("DueDate");
            }
        }



        public string TickSize   //tickSize; 
        {
            get { return tickSize; }
            set
            {

                tickSize = value;
                OnPropertyChanged("TickSize");
            }
        }


        public string MaxTradeNum   //maxTradeNum; 
        {
            get { return maxTradeNum; }
            set
            {

                maxTradeNum = value;
                OnPropertyChanged("MaxTradeNum");
            }
        }



         public BasicInforAndPromptData()
        {

        }

        public BasicInforAndPromptData(
         string _nowUserID,
         string _totalCapital,
         string _availableCapital,
         string _occupyCapital,
         string _nowInstrumentID,
         string _dayToEnd,
         string _dueDate,
         string _tickSize,
         string _maxTradeNum)
        {
            nowUserID = _nowUserID;
            totalCapital = _totalCapital;
            availableCapital = _availableCapital;
            occupyCapital = _occupyCapital;
            nowInstrumentID = _nowInstrumentID;
            dayToEnd = _dayToEnd;
            dueDate = _dueDate;
            tickSize = _tickSize;
            maxTradeNum = _maxTradeNum;

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

    public class BasicInforAndPromptManager
    {

        MainWindow pWindow;    //主窗体指针

        DataManager b_dm;
        TradingManager b_otm;

        TradingData b_selectedItem;

        BasicInforAndPromptData bInfoAndPro = new BasicInforAndPromptData("-","-","-","-","-", "-","-","-","-");

        System.Timers.Timer basicInfoTimer; //刷新交易区的计时器


        public BasicInforAndPromptManager(MainWindow _pWindow, DataManager _dm, TradingManager _otm)
        {
            pWindow = _pWindow;

            b_dm = _dm;

            b_otm = _otm;

            //b_selectedItem = selectedItem;

            pWindow.basicInforAndPromptGrid.DataContext = bInfoAndPro;

            basicInfoTimer = new System.Timers.Timer(500);

            basicInfoTimer.Start();

            basicInfoTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

        }

        public void changeInfo( string _nowUserID,
         string _totalCapital,
         string _availableCapital,
         string _occupyCapital,
         string _nowInstrumentID,
         string _dayToEnd,
         string _dueDate,
         string _tickSize,
         string _maxTradeNum)
        {
            bInfoAndPro.NowUserID = _nowUserID;
            bInfoAndPro.TotalCapital = _totalCapital;
            bInfoAndPro.AvailableCapital = _availableCapital;
            bInfoAndPro.OccupyCapital = _occupyCapital;
            bInfoAndPro.NowInstrumentID = _nowInstrumentID;
            bInfoAndPro.DayToEnd = _dayToEnd;
            bInfoAndPro.DueDate = _dueDate;
            bInfoAndPro.TickSize = _tickSize;
            bInfoAndPro.MaxTradeNum = _maxTradeNum;
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Refresh();
        }

        public void Refresh()
        {
            //TradingData selectedItem = pWindow.tradingListView.SelectedItem as TradingData;

            if (b_selectedItem != null)
            {


                string se_nowUserID = b_selectedItem.UserID;

                DataRow uDr = (DataRow)UserManager.userHash[se_nowUserID];



                string se_totalCapital = (Convert.ToDouble(uDr["AvailableCapital"]) + Convert.ToDouble(uDr["UsedMargin"])).ToString();         //用户总资本
                string se_availableCapital = uDr["AvailableCapital"].ToString();           //可用资产
                string se_occupyCapital = uDr["UsedMargin"].ToString();                        //占用资产
                string se_nowInstrumentID = b_selectedItem.InstrumentID;
                string se_dayToEnd = "";                                //到期天数
                string se_dueDate = "";                                       //到期日
                string se_tickSize = "";                           //最小变动价位
                string se_maxTradeNum = "";                         //最大可交易手数


                DataRow nDr = (DataRow)DataManager.All[se_nowInstrumentID];


                se_dueDate = nDr["LastDate"].ToString();

                DateTime dt_dueDate = DateTime.ParseExact(se_dueDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                TimeSpan sp_dueDate = dt_dueDate - b_dm.now;

                double i_dueDate = (int)sp_dueDate.TotalDays;

                // System.Windows.MessageBox.Show(i_dueDate.ToString());

                se_dayToEnd = i_dueDate.ToString();

                se_tickSize = nDr["MinUnit"].ToString();

                se_maxTradeNum = SomeCalculate.calculateCanBuyNum(b_selectedItem, b_otm.TradingOC).ToString();

                this.changeInfo(se_nowUserID, se_totalCapital, se_availableCapital, se_occupyCapital, se_nowInstrumentID, se_dayToEnd, se_dueDate, se_tickSize, se_maxTradeNum);

            }
        }


        public void getSelectedItemPoint(TradingData _selectedItem)
        {
            b_selectedItem = _selectedItem;
        }

    }

 }

        


   










