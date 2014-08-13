using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;


namespace qiquanui
{
    public static class SomeCalculate
    {
        static int rateArryLength = 5;
        static string[] rate_instrumentName = { "上证50期货", "中证500期货", "国债", "沪深300期货", "铜" };
        static double[] rate_eachfee = { 0.25 / 10000, 0.25 / 10000, 0.25 / 10000, 0.25 / 10000, 0.00005 };

        static int numArryLength = 4;
        static string[] num_instrumentName = { "金", "白糖", "硅铁", "锰硅" };
        static int[] num_eachefee = { 10, 4, 3, 3 };


        static public double calculateFees(string _instrumentID, int _tradingNum, double _finalPrice)
        {
            DataRow nDr = (DataRow)DataManager.All[_instrumentID];

            string instrumentType = nDr["InstrumentType"].ToString();

            if (instrumentType.Equals("期权"))
            {
                double option_fees = Math.Abs(_tradingNum) * 5;
                return option_fees;
            }
            else if (instrumentType.Equals("期货"))
            {
                string instrumentName = nDr["InstrumentName"].ToString();

                double future_fees = 0;
                bool isRate = false;  //判断是否在Rate的数组里
                int tag = -1;

                for (int i = 0; i < rateArryLength; i++)
                {
                    if (rate_instrumentName[i].Equals(instrumentName))
                    {
                        isRate = true;
                        tag = i;
                    }

                }

                if (isRate == false)
                {
                    for (int i = 0; i < numArryLength; i++)
                    {
                        if (num_instrumentName[i].Equals(instrumentName))
                        {
                            tag = i;
                        }
                    }
                }


                if (isRate == true)
                {
                    double rate_fees = 0;

                    int instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                    rate_fees = _finalPrice * instrumentMultiplier * Math.Abs(_tradingNum) * rate_eachfee[tag];

                    future_fees = Math.Round(rate_fees, 1);
                }
                else
                {
                    double num_fees = 0;

                    num_fees = Math.Abs(_tradingNum) * num_eachefee[tag];

                    future_fees = Math.Round(num_fees, 1);
                }



                return future_fees;
            }
            else
            {
                return 0;
            }


        }


        static public double caculateRoyalty(string _instrumentID, int _tradingNum, bool _isBuy, double _finalPrice)
        {
            DataRow nDr = (DataRow)DataManager.All[_instrumentID];

            string instrumentType = nDr["InstrumentType"].ToString();



            if (instrumentType.Equals("期权"))  //表示是期权   
            {
                int instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                double cost = Math.Abs(_tradingNum) * instrumentMultiplier * _finalPrice;

                if (_isBuy == true)   //买返回正，卖返回负   正表示支出  负表示获得
                {
                    return cost;
                }
                else
                {
                    return -cost;
                }

            }
            else    //期货不用权利金
            {
                return 0;
            }
        }


        static public double caculateMargin(string _instrumentID, int _tradingNum, bool _isBuy, double _finalPrice)
        {

            DataRow nDr = (DataRow)DataManager.All[_instrumentID];

            string instrumentType = nDr["InstrumentType"].ToString();

            if (instrumentType.Equals("期权"))   //对于卖家期货保证金计算
            {
                if (_isBuy == false)   //卖期权  要交保证金
                {
                    int t_callOrPut = Convert.ToInt32(nDr["CallOrPut"]);

                    //////////
                    //////////////////////////////以下数据从ALL获取//////////////////////


                    double t_PreClosePrice = Math.Round((double)nDr["PreClosePrice"], 1);  //昨收盘价

                    double t_PreSettlementPrice = Math.Round((double)nDr["PreSettlementPrice"], 1);  //前结算价

                    double t_ExercisePrice = Math.Round((double)nDr["ExercisePrice"], 1);


                    double MarginAdjust = 0.1;//股指期权保证金调整系数  
                    double MiniGuarantee = 0.5;//最低保障系数  

                    //int VolumeMultiple = 100;
                    int instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);   //合约乘数  


                    double dummy = Math.Max(t_ExercisePrice - t_PreClosePrice, 0.0);//虚值额  



                    if (t_callOrPut == 0)  //看涨
                    {



                        double c_margin = (t_PreSettlementPrice + t_PreClosePrice * Math.Max(MarginAdjust - dummy / t_PreClosePrice, MarginAdjust * MiniGuarantee)) * instrumentMultiplier * Math.Abs(_tradingNum);//保证金  

                        return c_margin;

                    }
                    else if (t_callOrPut == 1)   //看跌
                    {
                        double p_margin = (t_PreSettlementPrice + Math.Max(t_PreClosePrice * MarginAdjust - dummy, t_ExercisePrice * MarginAdjust * MiniGuarantee)) * instrumentMultiplier * Math.Abs(_tradingNum);

                        return p_margin;

                    }
                    else
                    {
                        return 0;
                    }
                }
                else    //买期权 不用保证金
                {
                    return 0;
                }

            }
            else     //期货保证金计算
            {
                double marginRate = Convert.ToDouble(nDr["GuaranteeMoney"]);   //保证金率

                int instrumentMultiplier = Convert.ToInt32(nDr["InstrumentMultiplier"]);

                double t_margin = _finalPrice * instrumentMultiplier * Math.Abs(_tradingNum) * marginRate; //交易价格*交易单位*交易手数*保证金收取比例

                return t_margin;
                //return 0;
            }
        }



        static public int calculateCanBuyNum(TradingData _seTd, ObservableCollection<TradingData> _tradingOC)
        {

            double otherCostOfMargin = 0;
            double otherCostOfRoyalty = 0;

            DataRow uDr = (DataRow)UserManager.userHash[_seTd.UserID];

            double seAvailable = Convert.ToDouble(uDr["AvailableCapital"]);

            for (int i = 0; i < _tradingOC.Count(); i++)
            {
                if (_tradingOC[i].IsBuy == _seTd.IsBuy && _tradingOC[i].UserID.Equals(_seTd.UserID) && _tradingOC[i].InstrumentID.Equals(_seTd.InstrumentID))
                {

                }
                else
                {
                    if (_tradingOC[i].OptionOrFuture == true)   //期货
                    {
                        if (_tradingOC[i].ClientageType==0)
                        {
                            otherCostOfMargin += caculateMargin(_tradingOC[i].InstrumentID, _tradingOC[i].TradingNum, _tradingOC[i].IsBuy, _tradingOC[i].MarketPrice);
                        }
                        else if (_tradingOC[i].ClientageType==1)
                        {
                            otherCostOfMargin += caculateMargin(_tradingOC[i].InstrumentID, _tradingOC[i].TradingNum, _tradingOC[i].IsBuy, Convert.ToDouble(_tradingOC[i].ClientagePrice));
                        }

                    }
                    else if (_tradingOC[i].OptionOrFuture == false)   //期权
                    {
                           //扣权利金  或者获得 权利金
                        {
                            if (_tradingOC[i].ClientageType==0)
                            {
                                otherCostOfRoyalty += caculateRoyalty(_tradingOC[i].InstrumentID, _tradingOC[i].TradingNum, _tradingOC[i].IsBuy, _tradingOC[i].MarketPrice);

                            }
                            else if (_tradingOC[i].ClientageType==1)
                            {
                                otherCostOfRoyalty += caculateRoyalty(_tradingOC[i].InstrumentID, _tradingOC[i].TradingNum, _tradingOC[i].IsBuy, Convert.ToDouble(_tradingOC[i].ClientagePrice));

                            }

                        }
                       if (_tradingOC[i].IsBuy == false)  //保证金
                        {
                            if (_tradingOC[i].ClientageType==0)
                            {
                                otherCostOfMargin += caculateMargin(_tradingOC[i].InstrumentID, _tradingOC[i].TradingNum, _tradingOC[i].IsBuy, _tradingOC[i].MarketPrice);
                            }
                            else if (_tradingOC[i].ClientageType==1)
                            {
                                otherCostOfMargin += caculateMargin(_tradingOC[i].InstrumentID, _tradingOC[i].TradingNum, _tradingOC[i].IsBuy, Convert.ToDouble(_tradingOC[i].ClientagePrice));
                            }
                        }
                    }
                }
            }

            int canBuyNum = 0;

            if (_seTd.OptionOrFuture == true)
            {
                double eachMargin = 0;
                if (_seTd.ClientageType==0)
                {
                    eachMargin = SomeCalculate.caculateMargin(_seTd.InstrumentID, 1, _seTd.IsBuy, _seTd.MarketPrice);
                }
                else if (_seTd.ClientageType==1)
                {
                    eachMargin = SomeCalculate.caculateMargin(_seTd.InstrumentID, 1, _seTd.IsBuy, Convert.ToDouble(_seTd.ClientagePrice));
                }
                canBuyNum = (int)((seAvailable - otherCostOfMargin - otherCostOfRoyalty) / eachMargin);

            }
            else if (_seTd.OptionOrFuture == false)
            {
                if (_seTd.IsBuy==true)
                {
                    double eachRoyalty=0;
                    if (_seTd.ClientageType==0)
                    {
                        eachRoyalty = SomeCalculate.caculateRoyalty(_seTd.InstrumentID, 1, _seTd.IsBuy, _seTd.MarketPrice);
                    }
                    else if (_seTd.ClientageType==1)
                    {
                        eachRoyalty = SomeCalculate.caculateRoyalty(_seTd.InstrumentID, 1, _seTd.IsBuy,Convert.ToDouble(_seTd.ClientagePrice));
                    }

                    canBuyNum = (int)((seAvailable - otherCostOfMargin - otherCostOfRoyalty)/eachRoyalty);
                }
                else if (_seTd.IsBuy == false)
                {
                    double eachMargin=0;
                    if (_seTd.ClientageType==0)
                    {
                        eachMargin = SomeCalculate.caculateMargin(_seTd.InstrumentID, 1, _seTd.IsBuy, _seTd.MarketPrice);
                    }
                    else if (_seTd.ClientageType==1)
                    {
                        eachMargin = SomeCalculate.caculateMargin(_seTd.InstrumentID, 1, _seTd.IsBuy, Convert.ToDouble(_seTd.ClientagePrice));
                    }
                    canBuyNum =(int) ((seAvailable - otherCostOfMargin - otherCostOfRoyalty) / eachMargin);
                }
            }

            return canBuyNum;
        }

    }
}
