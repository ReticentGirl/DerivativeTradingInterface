using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qiquanui
{
    class DynamicData
    {
        int id;
        string instrumentID;
        string tradingDay;
        double lastPrice;
        double highestPrice;
        double lowestPrice;
        int volume;
        double openInterest;
        double upperLimitPrice;
        double lowerLimitPrice;
        double preClosePrice;
        double preOpenInterest;
        double openPrice;
        double closePrice;
        double bidPrice1;
        int bidVolume1;
        double askPrice1;
        int askVolume1;
        double averagePrice;
        string updateTime;
        int updateMillisec;
        double riseAndFall;
        double riseAndFallRate;
        double settlementPrice;
        double preSettlementPrice;
        double turnover;




        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string InstrumentID
        {
            get { return instrumentID; }
            set { instrumentID = value; }
        }

        public string TradingDay
        {
            get { return tradingDay; }
            set { tradingDay = value; }
        }

        public double LastPrice
        {
            get { return lastPrice; }
            set { lastPrice = value; }
        }

        public double HighestPrice
        {
            get { return highestPrice; }
            set { highestPrice = value; }
        }


        public double LowestPrice
        {
            get { return lowestPrice; }
            set { lowestPrice = value; }
        }


        public int Volume
        {
            get { return volume; }
            set { volume = value; }
        }


        public double OpenInterest
        {
            get { return openInterest; }
            set { openInterest = value; }
        }


        public double UpperLimitPrice
        {
            get { return upperLimitPrice; }
            set { upperLimitPrice = value; }
        }


        public double LowerLimitPrice
        {
            get { return lowerLimitPrice; }
            set { lowerLimitPrice = value; }
        }


        public double PreClosePrice
        {
            get { return preClosePrice; }
            set { preClosePrice = value; }
        }


        public double PreOpenInterest
        {
            get { return preOpenInterest; }
            set { preOpenInterest = value; }
        }


        public double OpenPrice
        {
            get { return openPrice; }
            set { openPrice = value; }
        }


        public double ClosePrice
        {
            get { return closePrice; }
            set { closePrice = value; }
        }


        public double BidPrice1
        {
            get { return bidPrice1; }
            set { bidPrice1 = value; }
        }


        public int BidVolume1
        {
            get { return bidVolume1; }
            set { bidVolume1 = value; }
        }


        public double AskPrice1
        {
            get { return askPrice1; }
            set { askPrice1 = value; }
        }

        public int AskVolume1
        {
            get { return askVolume1; }
            set { askVolume1 = value; }
        }


        public double AveragePrice
        {
            get { return averagePrice; }
            set { averagePrice = value; }
        }


        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }


        public int UpdateMillisec
        {
            get { return updateMillisec; }
            set { updateMillisec = value; }
        }


        public double RiseAndFall
        {
            get { return riseAndFall; }
            set { riseAndFall = value; }
        }



        public double RiseAndFallRate
        {
            get { return riseAndFallRate; }
            set { riseAndFallRate = value; }
        }



        public double SettlementPrice
        {
            get { return settlementPrice; }
            set { settlementPrice = value; }
        }



        public double PreSettlementPrice
        {
            get { return preSettlementPrice; }
            set { preSettlementPrice = value; }
        }



        public double Turnover
        {
            get { return turnover; }
            set { turnover = value; }
        }




        public DynamicData()
        { }

        public DynamicData(
                            string _instrumentID,
                            string _tradingDay,
                            double _lastPrice,
                            double _highestPrice,
                            double _lowestPrice,
                            int _volume,
                            double _openInterest,
                            double _upperLimitPrice,
                            double _lowerLimitPrice,
                            double _preClosePrice,
                            double _preOpenInterest,
                            double _openPrice,
                            double _closePrice,
                            double _bidPrice1,
                            int _bidVolume1,
                            double _askPrice1,
                            int _askVolume1,
                            double _averagePrice,
                            string _updateTime,
                            int _updateMillisec,
                            double _riseAndFall,
                            double _riseAndFallRate,
                            double _settlementPrice,
                            double _preSettlementPrice,
                            double _turnover
                            )
        {

            instrumentID = _instrumentID;
            tradingDay = _tradingDay;
            lastPrice = _lastPrice;
            highestPrice = _highestPrice;
            lowestPrice = _lowestPrice;
            volume = _volume;
            openInterest = _openInterest;
            upperLimitPrice = _upperLimitPrice;
            lowerLimitPrice = _lowerLimitPrice;
            preClosePrice = _preClosePrice;
            preOpenInterest = _preOpenInterest;
            openPrice = _openPrice;
            closePrice = _closePrice;
            bidPrice1 = _bidPrice1;
            bidVolume1 = _bidVolume1;
            askPrice1 = _askPrice1;
            askVolume1 = _askVolume1;
            averagePrice = _averagePrice;
            updateTime = _updateTime;
            updateMillisec = _updateMillisec;
            riseAndFall = _riseAndFall;
            riseAndFallRate = _riseAndFallRate;
            settlementPrice = _settlementPrice;
            preSettlementPrice = _preSettlementPrice;
            turnover = _turnover;
        }



    }
}
