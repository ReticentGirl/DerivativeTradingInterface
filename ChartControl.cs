using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace qiquanui
{

    public class XY : INotifyPropertyChanged
    {
        private double x;
        private double y;

        // public double x { get; set; }
        // public double y { get; set; }

        public double X
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


    class ChartControl
    {

        static public void DrawStaticChart(StrategyWindow _stWindow, ObservableCollection<XY>[] _datas)   //画静态图
        {

            

        }

    }
}
