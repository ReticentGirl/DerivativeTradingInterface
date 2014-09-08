using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using System.Collections.ObjectModel;
using RiskControl;

namespace qiquanui
{
    public class RiskLabData : INotifyPropertyChanged
    {
        //合约的代码 行情数据
        private string instrumentID;
        private string sCallOrPut;    //看涨（0/false）看跌（1/true）
        private string sBuyOrSell;    //买（0） 卖（1）
        private int tradingNum;   //交易手数
        private string datanow;//系统当前时间

        //需求数据
        private double singleDelta;
        private string singleGamma;
        private string singleTheta;
        private string singleVega;
        private string singleRho;
        private string singleVol;
        private string singleVar;

        //组合值
        private double comDelta;
        private double comGamma;
        private double comTheta;
        private double comVega;
        private double comRho;
        private string comVar;

        public string Datanow
        {
            get { return datanow; }
            set
            {
                instrumentID = value;
                OnPropertyChanged("Datanow");
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

        public string SCallOrPut
        {
            get { return sCallOrPut; }
            set
            {
                sCallOrPut = value;
                OnPropertyChanged("SCallOrPut");
            }
        }

        public string SBuyOrSell
        {
            get { return sBuyOrSell; }
            set
            {
                sBuyOrSell = value;
                OnPropertyChanged("SBuyOrSell");
            }
        }

        public int TradingNum
        {
            get { return tradingNum; }
            set
            {
                tradingNum = value;
                OnPropertyChanged("TradingNum");
            }
        }

        public double SingleDelta
        {
            get { return singleDelta; }
            set
            {
                singleDelta = value;
                OnPropertyChanged("SingleDelta");
            }
        }

        public string SingleGamma
        {
            get { return singleGamma; }
            set
            {
                singleGamma = value;
                OnPropertyChanged("SingleGamma");
            }
        }

        public string SingleTheta
        {
            get { return singleTheta; }
            set
            {
                singleTheta = value;
                OnPropertyChanged("SingleTheta");
            }
        }

        public string SingleVega
        {
            get { return singleVega; }
            set
            {
                singleVega = value;
                OnPropertyChanged("SingleVega");
            }
        }

        public string SingleRho
        {
            get { return singleRho; }
            set
            {
                singleRho = value;
                OnPropertyChanged("SingleRho");
            }
        }

        public string SingleVol
        {
            get { return singleVol; }
            set
            {
                singleVol = value;
                OnPropertyChanged("SingleVol");
            }
        }

        public string SingleVar
        {
            get { return singleVar; }
            set
            {
                singleVar = value;
                OnPropertyChanged("SingleVar");
            }
        }

        public double ComDelta
        {
            get { return comDelta; }
            set
            {
                comDelta = value;
                OnPropertyChanged("ComDelta");
            }
        }

        public double ComGamma
        {
            get { return comGamma; }
            set
            {
                comGamma = value;
                OnPropertyChanged("ComGamma");
            }
        }

        public double ComTheta
        {
            get { return comTheta; }
            set
            {
                comTheta = value;
                OnPropertyChanged("ComTheta");
            }
        }

        public double ComVega
        {
            get { return comVega; }
            set
            {
                comVega = value;
                OnPropertyChanged("ComVega");
            }
        }

        public double ComRho
        {
            get { return comRho; }
            set
            {
                comRho = value;
                OnPropertyChanged("ComRho");
            }
        }

        public string ComVar
        {
            get { return comVar; }
            set
            {
                comVar = value;
                OnPropertyChanged("ComVar");
            }
        }

        public RiskLabData()
        {
            //comVar = "---";
        }

        public RiskLabData(string _instrumentID, string _sCallOrPut, string _sBuyOrSell, int _tradingNum, double cov, string _datanow)
        {

            instrumentID = _instrumentID;
            sCallOrPut = _sCallOrPut;

            string instrumentID1 = "'" + _instrumentID + "'";
          
            sBuyOrSell = _sBuyOrSell;
            tradingNum = _tradingNum;

            RiskControl.Class1 output1 = new Class1();
            MWArray[] x = output1.GreekLetter(7, instrumentID1, _sBuyOrSell, tradingNum, cov, _datanow);
            //delta,gamma,theta,vega,rho,vol,var 
            MWNumericArray x0 = (MWNumericArray)x[0];
            MWNumericArray x1 = (MWNumericArray)x[1];
            MWNumericArray x2 = (MWNumericArray)x[2];
            MWNumericArray x3 = (MWNumericArray)x[3];
            MWNumericArray x4 = (MWNumericArray)x[4];
            MWNumericArray x5 = (MWNumericArray)x[5];
            MWNumericArray x6 = (MWNumericArray)x[6];

            double[,] ddVolatility = (double[,])x5.ToArray(MWArrayComponent.Real);
            double[,] ddDelta = (double[,])x0.ToArray(MWArrayComponent.Real);
            double[,] ddGamma = (double[,])x1.ToArray(MWArrayComponent.Real);
            double[,] ddTheta = (double[,])x2.ToArray(MWArrayComponent.Real);
            double[,] ddVega = (double[,])x3.ToArray(MWArrayComponent.Real);
            double[,] ddRho = (double[,])x4.ToArray(MWArrayComponent.Real);
            double[,] ddVar = (double[,])x6.ToArray(MWArrayComponent.Real);

            singleVol = Math.Round(ddVolatility[0, 0], 3).ToString();
            singleDelta = Math.Round(ddDelta[0, 0], 3);
            singleGamma = Math.Round(ddGamma[0, 0], 3).ToString();
            singleTheta = Math.Round(ddTheta[0, 0], 3).ToString();
            singleVega = Math.Round(ddVega[0, 0], 3).ToString();
            singleRho = Math.Round(ddRho[0, 0], 3).ToString();
            double temp = Math.Round(ddVar[0, 0], 3);
            singleVar = temp.ToString();
        }

        public RiskLabData(string _instrumentID, string _sCallOrPut, string _sBuyOrSell, int _tradingNum, string _datanow)
        {

            instrumentID = _instrumentID;
            sCallOrPut = _sCallOrPut;

            string instrumentID1 = "'" + _instrumentID + "'";
           
            sBuyOrSell = _sBuyOrSell;
            tradingNum = _tradingNum;

            //datanow = _datanow;
            

            RiskControl.Class1 output1 = new Class1();
            MWArray[] x = output1.GreekLetter(7, instrumentID1, _sCallOrPut, tradingNum, 0.95, _datanow);
            //delta,gamma,theta,vega,rho,vol,var 
            MWNumericArray x0 = (MWNumericArray)x[0];
            MWNumericArray x1 = (MWNumericArray)x[1];
            MWNumericArray x2 = (MWNumericArray)x[2];
            MWNumericArray x3 = (MWNumericArray)x[3];
            MWNumericArray x4 = (MWNumericArray)x[4];
            MWNumericArray x5 = (MWNumericArray)x[5];
           // MWNumericArray x6 = (MWNumericArray)x[6];

            double[,] ddVolatility = (double[,])x5.ToArray(MWArrayComponent.Real);
            double[,] ddDelta = (double[,])x0.ToArray(MWArrayComponent.Real);
            double[,] ddGamma = (double[,])x1.ToArray(MWArrayComponent.Real);
            double[,] ddTheta = (double[,])x2.ToArray(MWArrayComponent.Real);
            double[,] ddVega = (double[,])x3.ToArray(MWArrayComponent.Real);
            double[,] ddRho = (double[,])x4.ToArray(MWArrayComponent.Real);
            //double[,] ddVar = (double[,])x6.ToArray(MWArrayComponent.Real);

            singleVol = Math.Round(ddVolatility[0, 0], 3).ToString();
            singleDelta = Math.Round(ddDelta[0, 0], 3);
            singleGamma = Math.Round(ddGamma[0, 0], 3).ToString();
            singleTheta = Math.Round(ddTheta[0, 0], 3).ToString();
            singleVega = Math.Round(ddVega[0, 0], 3).ToString();
            singleRho = Math.Round(ddRho[0, 0], 3).ToString();
            singleVar = "---";
        }

        public RiskLabData(double delta,double gamma,double vega,double theta,double rho,double var)
        {
            comDelta = delta;
            comGamma = gamma;
            comVega = vega;
            comTheta = theta;
            comRho = rho;
            comVar = var.ToString();
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


}
