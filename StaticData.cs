using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qiquanui
{
    class StaticData
    {
        private string instrumentID;
        private string instrumentName;
        private string exchangeID;
        private string exchangeName;
        private string exchangeInstID;
        private string type;
        private double exercisePrice;
        private string dueDate;
        private bool callOrPut;    //看涨（0/false）看跌（1/true）
        private bool optionOrFuture; //期权（0/false）期货（1/true）

        public string InstrumentID
        {
            get { return instrumentID; }
            set { instrumentID = value; }
        }

        public string InstrumentName
        {
            get { return instrumentName; }
            set { instrumentName = value; }
        }

        public string ExchangeID
        {
            get { return exchangeID; }
            set { exchangeID = value; }
        }

        public string ExchangeName
        {
            get { return exchangeName; }
            set { exchangeName = value; }
        }

        public string ExchangeInstID
        {
            get { return exchangeInstID; }
            set { exchangeInstID = value; }
        }


        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        public double ExercisePrice
        {
            get { return exercisePrice; }
            set { exercisePrice = value; }
        }


        public string DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }


        public bool CallOrPut
        {
            get { return callOrPut; }
            set { callOrPut = value; }
        }


        public bool OptionOrFuture
        {
            get { return optionOrFuture; }
            set { optionOrFuture = value; }
        }

        public StaticData()
        { }

        public StaticData(string _instrumentID,
                            string _instrumentName,
                            string _exchangeID,
                            string _exchangeName,
                            string _exchangeInstID,
                            string _type,
                            double _exercisePrice,
                            string _dueDate,
                            bool _callOrPut,
                            bool _optionOrFuture)
        {
            instrumentID = _instrumentID;
            instrumentName = _instrumentName;
            exchangeID = _exchangeID;
            exchangeName = _exchangeName;
            exchangeInstID = _exchangeInstID;
            type = _type;
            exercisePrice = _exercisePrice;
            dueDate = _dueDate;
            callOrPut = _callOrPut;
            optionOrFuture = _optionOrFuture;
        }



    }
}
