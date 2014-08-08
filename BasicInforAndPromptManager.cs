using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


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

        BasicInforAndPromptData bInfoAndPro = new BasicInforAndPromptData("-","-","-","-","-", "-","-","-","-");

        public BasicInforAndPromptManager(MainWindow _pWindow)
        {
            pWindow = _pWindow;

            pWindow.basicInforAndPromptGrid.DataContext = bInfoAndPro;
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

    }

 }

        


   










