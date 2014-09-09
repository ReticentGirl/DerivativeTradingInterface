using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using RiskControl;

namespace qiquanui
{

    public class RiskLabManager
    {

        public ObservableCollection<RiskLabData> RiskOC1 = new ObservableCollection<RiskLabData>(); //单支期权的希腊值
        public  static ObservableCollection<RiskLabData> RiskOC2 = new ObservableCollection<RiskLabData>(); //推荐组合

        RiskWindow pRiskWindow;    //风险实验室窗体指针

        TradingManager otm;//交易区指针

        RiskLabData rd1;
        //RiskLabData rd1 = new RiskLabData();
        //pRiskWindow.compoGrid.DataContext = rd1;

        string datanow;//系统当前时间

        public RiskLabManager() 
        { 
        }

        public RiskLabManager(TradingManager _otm, RiskWindow _pRiskWindow,string _datanow)
        {
            otm = _otm;
            pRiskWindow = _pRiskWindow;
            pRiskWindow.optionsRiskLV.ItemsSource = RiskOC1;
            pRiskWindow.recomLV.ItemsSource = RiskOC2;
            datanow = _datanow;
            rd1 = new RiskLabData();
           pRiskWindow.compoGrid.DataContext = rd1;
        }

        public void GetData(double cov,string _datanow)
        {
            for (int i = 0; i < RiskOC1.Count(); i++) {
                RiskLabData rd = new RiskLabData(RiskOC1[i].InstrumentID,RiskOC1[i].SCallOrPut,RiskOC1[i].SBuyOrSell,RiskOC1[i].TradingNum, cov,_datanow);
                double temp = Math.Round(Convert.ToDouble(rd.SingleVar), 3);
                RiskOC1[i].SingleVar = temp.ToString();
            }
        }

        public void GetData(string instrumentID, string callOrPut, int tradingType, int tradingNum,string _datanow)
        {

            string sCallOrPut;////看涨（0/false）看跌（1/true）
            if (callOrPut.Equals("看涨"))
            {
                sCallOrPut = "call";
            }
            else
            {
                sCallOrPut = "put";
            }

            string sSellOrBuy;//交易类型   0 买开  1 卖开  2买平 3卖平
            if (tradingType == 1 || tradingType == 3)
            {
                sSellOrBuy = "sell";
            }
            else
            {
                sSellOrBuy = "buy";
            }

            RiskLabData rd2 = new RiskLabData(instrumentID, sCallOrPut, sSellOrBuy, tradingNum, _datanow);
            RiskOC1.Add(rd2);
        }

        public void Com(double cov)
        {
            MWCellArray InputNum = new MWCellArray(1, RiskOC1.Count());
            MWCellArray InputID = new MWCellArray(1, RiskOC1.Count());
            for (int i = 0; i < RiskOC1.Count(); i++)
            {
                InputNum[i + 1] = RiskOC1[i].TradingNum;
                InputID[i + 1] = "'" + RiskOC1[i].InstrumentID + "'";
            }
            RiskControl.Class1 output1 = new Class1();
            MWNumericArray x0 = (MWNumericArray)output1.VaR(InputID, InputNum, RiskOC1.Count(),cov);
            rd1.ComVar = Math.Round((double)x0, 3).ToString();
        }

        public void Com()
        {
            double tempDelta = 0;
            double tempGama = 0;
            double tempTheta = 0;
            double tempVega = 0;
            double tempRho = 0;

            double dMultiDelta = 0;
            double dMultiGamma = 0;
            double dMultiTheta = 0;
            double dMultiVega = 0;
            double dMultidRho = 0;

            for (int i = 0; i < RiskOC1.Count(); i++)
            {
                //if (RiskOC1[i].SBuyOrSell == "buy")
                //{

                    tempDelta = RiskOC1[i].SingleDelta * RiskOC1[i].TradingNum;
                    tempGama = Convert.ToDouble(RiskOC1[i].SingleGamma) * RiskOC1[i].TradingNum;
                    tempTheta = Convert.ToDouble(RiskOC1[i].SingleTheta) * RiskOC1[i].TradingNum;
                    tempVega = Convert.ToDouble(RiskOC1[i].SingleVega) * RiskOC1[i].TradingNum;
                    tempRho = Convert.ToDouble(RiskOC1[i].SingleRho) * RiskOC1[i].TradingNum;
                //}
                //else if (RiskOC1[i].SBuyOrSell == "call")
                //{
                //    tempDelta = RiskOC1[i].SingleDelta * RiskOC1[i].TradingNum * (-1);
                //    tempGama = Convert.ToDouble(RiskOC1[i].SingleGamma) * RiskOC1[i].TradingNum * (-1);
                //    tempTheta = Convert.ToDouble(RiskOC1[i].SingleTheta) * RiskOC1[i].TradingNum * (-1);
                //    tempVega = Convert.ToDouble(RiskOC1[i].SingleVega) * RiskOC1[i].TradingNum * (-1);
                //    tempRho = Convert.ToDouble(RiskOC1[i].SingleRho) * RiskOC1[i].TradingNum * (-1);
                //}
                dMultiDelta += tempDelta;
                dMultiGamma += tempGama;
                dMultiTheta += tempTheta;
                dMultiVega += tempVega;
                dMultidRho += tempRho;

                tempDelta = 0;
                tempGama = 0;
                tempTheta = 0;
                tempVega = 0;
                tempRho = 0;
            }
          

            rd1.ComDelta = Math.Round(dMultiDelta, 3); 
            rd1.ComGamma = Math.Round(dMultiGamma, 3);
            rd1.ComTheta =Math.Round(dMultiTheta, 3);
            rd1.ComRho = Math.Round(dMultidRho, 3);
            rd1.ComVega = Math.Round(dMultiVega, 3);
            rd1.ComVar = "---";

            pRiskWindow.compoGrid.DataContext = rd1;
        }

        public void showData(double cov) {
            //RiskLabData rd1 = new RiskLabData();
            //pRiskWindow.compoGrid.DataContext = rd1;
            for (int i = 0; i < RiskOC1.Count(); i++) {
                if (RiskOC1[i].SBuyOrSell.Equals("sell"))
                    RiskOC1[i].SBuyOrSell = "卖";
                else if (RiskOC1[i].SBuyOrSell.Equals("buy"))
                    RiskOC1[i].SBuyOrSell = "买";
            RiskOC2.Add(RiskOC1[i]);
            }

            RiskControl.Class1 output1 = new Class1();
            MWCellArray InputID = new MWCellArray(1,RiskOC1.Count());
            MWCellArray InputNum = new MWCellArray(1, RiskOC1.Count());

            for (int i = 0; i < RiskOC1.Count(); i++)
            {
                InputNum[i + 1] = RiskOC1[i].TradingNum;
                InputID[i + 1] = "'" + RiskOC1[i].InstrumentID + "'";
            }

            MWArray[] best = output1.link(2, cov, (RiskOC1.Count() + 1), rd1.ComDelta, rd1.ComGamma, InputID, InputNum, datanow);
            MWCellArray x11 = (MWCellArray)best[0];
            MWCellArray x22 = (MWCellArray)best[1];

            RiskLabData rdOption = new RiskLabData();
            RiskLabData rdFuture = new RiskLabData();

            //期货数据
            rdFuture.InstrumentID = x11[1].ToString();
            double[,] FNum = (double[,])x11[2].ToArray();
            rdFuture.TradingNum = (int)FNum[0, 0];
            rdFuture.SBuyOrSell="买";
            rdFuture.SingleDelta=1.0;
            rdFuture.SingleGamma = "---";
            rdFuture.SingleRho = "---";
            rdFuture.SingleTheta= "---";
            rdFuture.SingleVega = "---";
            rdFuture.SingleVol = "---";

            //期权数据 [outArr1,outArr2]  outArr2{1,1} outArr2{1,6}=ORho;
            //outArr2{1,7}=OVol;outArr2{1,8}=OVar;outArr2{1,9}=Onum;outArr2{1,10}=OCallOrPut;

            rdOption.InstrumentID = x22[1].ToString();
            double[,] ODelta = (double[,])x22[2].ToArray();
            rdOption.SingleDelta = Math.Round(ODelta[0, 0]);
            double[,] OGamma = (double[,])x22[3].ToArray();
            rdOption.SingleGamma = Math.Round(OGamma[0, 0]).ToString();
            double[,] OVega = (double[,])x22[4].ToArray();
            rdOption.SingleVega = Math.Round(OVega[0, 0]).ToString();
            double[,] OTheta = (double[,])x22[5].ToArray();
            rdOption.SingleTheta = Math.Round(OTheta[0, 0]).ToString();
            double[,] ORho = (double[,])x22[6].ToArray();
            rdOption.SingleRho = Math.Round(ORho[0, 0]).ToString();
            double[,] OVol = (double[,])x22[7].ToArray();
            rdOption.SingleVol = Math.Round(OVol[0, 0]).ToString();
            double[,] OVar = (double[,])x22[8].ToArray();
            double temp=Math.Round(OVar[0, 0]);
            rdOption.SingleVar = temp.ToString();
            double[,] ONum = (double[,])x22[9].ToArray();
            rdOption.TradingNum = (int)ONum[0, 0];
            rdOption.SCallOrPut=x22[10].ToString();

            //期权交易类型
            if (rd1.ComDelta > 0) // 0 1 
            {
                if (rdOption.SCallOrPut.Equals("call"))
                    rdOption.SBuyOrSell = "卖";
                else if (rdOption.SCallOrPut.Equals("put"))
                    rdOption.SBuyOrSell = "买";
             }
            else if (rd1.ComDelta <0) 
            {
                if (rdFuture.SCallOrPut.Equals("call"))
                    rdFuture.SBuyOrSell = "买";
                else if (rdFuture.SCallOrPut.Equals("put"))
                    rdFuture.SBuyOrSell = "卖";
            }

            if (rd1.ComGamma > 0)
                rdOption.SBuyOrSell = "卖";
            else
                rdOption.SBuyOrSell = "买";

          
            RiskOC2.Add(rdOption);
            RiskOC2.Add(rdFuture);
            }
    }


}
