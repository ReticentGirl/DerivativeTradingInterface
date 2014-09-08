using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
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

        public XY()
        {

        }

        public XY(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

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

    /// <summary>
    /// 下单列表的单元
    /// </summary>
    public class OrderUnit
    {
        public string InstrumentID;
        public int Number;
        public double BidPrice;
        public double AskPrice;

    }


    public struct YKCell
    {
        public double BidPrice;
        public double AskPrice;
        public int ExercisePrice;
        public string ID;
    }

    public struct YKFuture
    {
        public double BidPrice;
        public double AskPrice;
        public double LastPrice;
        public string ID;
    }

    public class YKProba
    {
        public bool positive;
        public double percent;
        public int x;
    }

    public enum OptionType : int { CallSell = 0, CallBuy = 1, PutBuy = 2, PutSell = 3 };
    public enum YKType : int { Call = 0, Put, HighWave, LowWave };


    /// <summary>
    /// 盈亏图计算相关类。构造完之后使用 .ykoption 和 .ykfuture 填充数据，然后再调用相应计算方法
    /// </summary>
    public class YK
    {
        public YKCell[,] ykoption;
        public YKFuture[] ykfuture;
        public ObservableCollection<XY> points;
        public ObservableCollection<YKProba> probability;        //存概率点
        public double ykmax;
        public double multiplier = 100;
        public YKType yktype;
        public string ykname;
        public string ykgroupname;
        public int ykstep;
        public string title;
        public int[,] number;        //有 TotLine+1 行，最后一行 0/1 分别为期货的买/卖
        public int TotLine;
        public int LeftEdge, RightEdge, LeftCompute, RightCompute;
        public double EarnRate, LoseRate, MaxEarn, MaxLose, ExpectEarn, Price;
        public double EDGE = 0.2;
        public bool szorhs = false;

        /// <summary>
        ///     构造完之后使用 .ykoption 和 .ykfuture 填充数据，然后再调用相应计算方法
        /// </summary>
        /// <param name="totline">相当于行权价的行数</param>
        /// <param name="max">最大涨跌幅/波幅</param>
        /// <param name="type">是四种中的哪种盈亏图</param>
        public YK(int totline, int[,] num, string _name, string _group, double _max)
        {
            TotLine = totline;
            number = num;
            ykname = _name;
            ykgroupname = _group;
            ykmax = _max;
            ykoption = YKManager.ykOption;
            ykfuture = YKManager.ykFuture;
            LeftCompute = (int)(ykfuture[0].LastPrice * (1 - ykmax));
            RightCompute = (int)(ykfuture[0].LastPrice * (1 + ykmax));
            //LeftEdge = (int)((ykoption[0,0].ExercisePrice)*0.9);
            //RightEdge = (int)((ykoption[ykoption.GetUpperBound(0)-1,0].ExercisePrice)*1.1);
            double t = 0.02;
            LeftEdge = (int)(LeftCompute * (1 - t));
            RightEdge = (int)(RightCompute * (1 + t));
            ykstep = (RightEdge - LeftEdge) / 500;
            if (ykstep == 0) ykstep = 1;
        }

        /// <summary>
        /// 计算Y值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="i"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public double ComputeY(double x, OptionType i, YKCell c)
        {
            switch (i)
            {
                case OptionType.CallBuy:
                    if (x <= c.ExercisePrice)
                        return -c.AskPrice;
                    else
                        return -c.AskPrice + (x - c.ExercisePrice);
                case OptionType.CallSell:
                    if (x <= c.ExercisePrice)
                        return c.BidPrice;
                    else
                        return c.BidPrice - (x - c.ExercisePrice);
                case OptionType.PutBuy:
                    if (x <= c.ExercisePrice)
                        return -c.AskPrice - (x - c.ExercisePrice);
                    else
                        return -c.AskPrice;
                case OptionType.PutSell:
                    if (x <= c.ExercisePrice)
                        return c.BidPrice + (x - c.ExercisePrice);
                    else
                        return c.BidPrice;
            }
            return 0;
        }

        /// <summary>
        /// 计算某个点
        /// </summary>
        /// <param name="x">< /param>
        /// <returns></returns>
        public double ComputePoint(double x)
        {
            double ans = 0;
            for (int i = 0; i <= ykoption.GetUpperBound(0); i++)
                for (int j = 0; j < 4; j++)
                    if (number[i, j] > 0)
                    {
                        if ((ykname.Equals("组合标的") || ykname.Equals("转换套利") )&& (szorhs))
                            ans += ComputeY(x, (OptionType)j, ykoption[i, j]) * number[i, j]/3;
                        else 
                        ans += ComputeY(x, (OptionType)j, ykoption[i, j])*number[i,j];

                    }
            if (number[TotLine, 0] > 0)
                ans += (x - ykfuture[0].LastPrice)*number[TotLine,0];
            if (number[TotLine, 1] > 0)
                ans += -(x - ykfuture[1].LastPrice)*number[TotLine,1];
            ans *= multiplier;
            ans = Math.Round(ans, 1);
            return ans;
        }

        /// <summary>
        /// 给指定ObservableCollection添加左右两个端点，【已废弃】
        /// </summary>
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// <param name="oc"></param>
        public void AddEdge(int Left, int Right, ObservableCollection<XY> oc)
        {
            XY temp = new XY(Left, ComputePoint(Left));
            oc.Insert(0, temp);
            temp = new XY(Right, ComputePoint(Right));
            oc.Insert(oc.Count, temp);
        }

        /// <summary>
        /// 计算盈亏图、概率图
        /// </summary>
        public void ComputeYK()
        {
            if (ykoption == null || ykoption.Length <= 0)
            {
                Console.WriteLine("YKOption error ! Maybe it's empty. [ComputeYK]");
                return;
            }
            points = new ObservableCollection<XY>();
            //points2 = new ObservableCollection<XY>();
            //points3 = new ObservableCollection<XY>();
            probability = new ObservableCollection<YKProba>();



            MaxEarn = 0;
            EarnRate = 0;
            MaxLose = 0;
            LoseRate = 0;
            ExpectEarn = 0;
            double EarnCount = 0, LoseCount = 0, ProbaCount = 0, TotCount = 0;
            double k;
            XY last = null, now;

            for (int i = LeftEdge; i <= RightEdge; i += ykstep)
            {
                k = ComputePoint(i);
                now = new XY((int)i, k);
                points.Add(now);

                if (i >= LeftCompute && i <= RightCompute)
                {
                    ///如果在计算范围内才进行概率与收益相关计算

                    double count = StrategyWindow.ComputeZT((int)i, ykfuture[0].LastPrice, ykmax);
                    TotCount += count;
                    if (k > 0)
                    {
                        EarnCount += count;
                        if (k > MaxEarn)
                            MaxEarn = k;
                    }
                    if (k < 0)
                    {
                        LoseCount += count;
                        if (-k > MaxLose)
                            MaxLose = -k;
                    }

                    ExpectEarn += k * (double)count;

                    ProbaCount += count;


                    if (last != null && (k == 0 || last.Y * now.Y < 0))
                    {
                        YKProba p = new YKProba();
                        if (last.Y > 0) p.positive = true;
                        else if (last.Y < 0) p.positive = false;
                        if (last.Y != 0)
                        {
                            p.x = (int)i;
                            p.percent = ProbaCount;
                            probability.Add(p);
                            ProbaCount = 0;
                        }
                    }
                    last = now;
                }
            }


            EarnRate = Math.Round((double)EarnCount / TotCount * 100, 1);
            LoseRate = Math.Round((double)LoseCount / TotCount * 100, 1);
            ExpectEarn = Math.Round(ExpectEarn / TotCount, 1);
            MaxEarn = Math.Round(MaxEarn, 1);
            MaxLose = Math.Round(MaxLose, 1);

            YKProba _p = new YKProba();
            if (ComputePoint(RightCompute) > 0) _p.positive = true;
            else if (ComputePoint(RightCompute) < 0) _p.positive = false;
            //最后一段概率右端点是无穷大
            _p.x = -1;
            _p.percent = ProbaCount;
            probability.Add(_p);
            for (int i = 0; i < probability.Count; i++)
                if (i == 0 || i == probability.Count - 1)
                {
                    if (probability.Count==1)
                        probability[i].percent = Math.Round(probability[i].percent / TotCount * 95 + 5, 1);
                    else
                    probability[i].percent = Math.Round(probability[i].percent / TotCount * 95 + 2.5, 1);
                } 
                else
                    probability[i].percent = Math.Round(probability[i].percent / TotCount * 95, 1);

            //price
            Price = 0;
            for (int i = 0; i <= ykoption.GetUpperBound(0); i++)
                for (int j = 0; j < 4; j++)
                    if (number[i, j] > 0)
                    {
                        if (j == (int)OptionType.CallBuy || j == (int)OptionType.PutBuy)
                            Price += number[i, j] * ykoption[i, j].AskPrice;
                        else
                            Price -= number[i, j] * ykoption[i, j].BidPrice;
                    }
            Price += number[TotLine, 0] * ykfuture[0].LastPrice;
            Price -= number[TotLine, 0] * ykfuture[1].LastPrice;
            Price *= multiplier;


            //为方便下单加此函数
            ComputeOrderList();
        }

        //double leftx = ykfuture[0].LastPrice * (1 - ykmax);
        //XY leftP= new XY(leftx,ComputePoint(leftx));
        //double rightx = ykfuture[0].LastPrice * (1 + ykmax);
        //XY rightP = new XY(rightx,ComputePoint(rightx));
        //switch (yktype)
        //{
        //    case YKType.Call:
        //        points.Insert(points.Count, rightP);
        //        break;
        //    case YKType.Put:
        //        points.Insert(0, leftP);
        //        break;
        //    case YKType.HighWave:
        //    case YKType.LowWave:
        //        points.Insert(0, leftP);
        //        points.Insert(points.Count, rightP);
        //        break;
        //}


        /// <summary>
        ///     填充数量数据
        /// </summary>
        /// <param name="num">期权</param>
        public void FillNumber(int[,] num)
        {
            number = num;
        }


        /// <summary>
        /// 每张图存储一个下单列表
        /// </summary>
        public List<OrderUnit> OrderList;

        public void ComputeOrderList()
        {
            OrderList = new List<OrderUnit>();
            for (int i = 0; i < TotLine; i++)
            {
                if (-number[i, 0] + number[i, 1] != 0)
                {
                    OrderUnit ou = new OrderUnit();
                    ou.InstrumentID = ykoption[i, 0].ID;
                    ou.Number = -number[i, 0] + number[i, 1];
                    ou.AskPrice = ykoption[i, 0].AskPrice;
                    ou.BidPrice = ykoption[i, 0].BidPrice;
                    OrderList.Add(ou);
                }

                if (number[i, 2] - number[i, 3] != 0)
                {
                    OrderUnit ou = new OrderUnit();
                    ou.InstrumentID = ykoption[i, 2].ID;
                    ou.Number = number[i, 2] - number[i, 3];
                    ou.AskPrice = ykoption[i, 0].AskPrice;
                    ou.BidPrice = ykoption[i, 0].BidPrice;
                    OrderList.Add(ou);
                }
            }

            if (number[TotLine, 0] - number[TotLine, 1] != 0)
            {
                OrderUnit ou2 = new OrderUnit();
                ou2.InstrumentID = ykfuture[0].ID;
                ou2.Number = number[TotLine, 0] - number[TotLine, 1];
                ou2.AskPrice = ykfuture[0].AskPrice;
                ou2.BidPrice = ykfuture[0].BidPrice;

                OrderList.Add(ou2);

            }


        }




    }


    class Test
    {
        public void Run()
        {
            //YKManager ykm = new YKManager();
            //int tot = ykm.Initial("中金所", "沪深300", "1409");
            //for (int i = 0; i < 4; i++)
            //{
            //    int[,] x = new int[tot + 1, 4];
            //    x[0, 0] = i;
            //    x[1, 1] = 4 - i;
                //YK temp = new YK(tot, x,"单买");
                //temp.ComputeYK();
                //ykm.yk[0, i] = temp;
            //}
        }
    }


    class YKManager
    {
        public YK[,] yk;
        public static int TotLine;
        public static int[] EPs;
        public static int Count;
        public static YKCell[,] ykOption;
        public static YKFuture[] ykFuture;
        public const double EDGE = 0.25;
        public static Hashtable History;
        public static int TotHistory;
        public static string SubjectID;
        /// <summary>
        /// 返回的是共有多少行
        /// </summary>
        /// <param name="trader"></param>
        /// <param name="subject"></param>
        /// <param name="duedate"></param>
        /// <param name="max"></param>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int Initial( string subject, string duedate)
        {
            yk = new YK[2, 4];

            //得到行权价列表
            string optionname = Subject2Option(subject);
            string exercisesql = "select instrumentid,exerciseprice from staticdata where instrumentname='" + optionname + "' and duedate='" + duedate + "' order by exerciseprice,callorput ";
            DataTable list = DataControl.QueryTable(exercisesql);
            TotLine = list.Rows.Count / 2;
            EPs = new int[TotLine];

            ykOption = new YKCell[TotLine, 4];
            ykFuture = new YKFuture[2];
            //填充行情数据
            for (int j = 0; j < TotLine; j++)
            {
                double x = (double)list.Rows[j * 2]["ExercisePrice"];
                EPs[j] = (int)x;

                string id = (string)list.Rows[j * 2]["InstrumentID"];
                DataRow dr = (DataRow)DataManager.All[id];

                YKCell cell = new YKCell();
                cell.AskPrice = (double)Math.Round((double)dr["AskPrice1"], 1);
                cell.BidPrice = (double)Math.Round((double)dr["BidPrice1"], 1);
                cell.ExercisePrice = (int)x;
                cell.ID = id;
                ykOption[j, 0] = cell;
                ykOption[j, 1] = cell;


                id = (string)list.Rows[j * 2 + 1]["InstrumentID"];
                dr = (DataRow)DataManager.All[id];

                cell = new YKCell();
                cell.AskPrice = Math.Round((double)dr["AskPrice1"], 1);
                cell.BidPrice = Math.Round((double)dr["BidPrice1"], 1);
                cell.ExercisePrice = (int)x;
                cell.ID = id;
                ykOption[j, 2] = cell;
                ykOption[j, 3] = cell;

            }

            //填充期货数据
            string futurename = Subject2Future(subject);
            string idsql = "select instrumentid from staticdata where instrumentname='" + futurename + "' and duedate='" + duedate + "' ";
            list = DataControl.QueryTable(idsql);
            string id2 = (string)list.Rows[0]["InstrumentID"];
            //if (futurename.Equals("上证50期货"))
            //    id2 = "上证50指数";
            //if (futurename.Equals("沪深300期货"))
            //    id2 = "沪深300指数";
            SubjectID = id2;

            DataRow dr2 = (DataRow)DataManager.All[id2];
            double lastprice = Math.Round((double)dr2["LastPrice"], 1);
            double bidprice = Math.Round((double)dr2["BidPrice1"], 1);
            double askprice = Math.Round((double)dr2["AskPrice1"], 1);
            


            ykFuture[0].LastPrice = lastprice;
            ykFuture[0].ID = id2;
            ykFuture[0].AskPrice = askprice;
            ykFuture[0].BidPrice = bidprice;
            ykFuture[1].LastPrice = lastprice;
            ykFuture[1].ID = id2;
            ykFuture[1].AskPrice = askprice;
            ykFuture[1].BidPrice = bidprice;

            //填充历史概率
            //History = new Hashtable(2000);
            //string sql = "SELECT LastPrice from minutedata where instrumentid='" + id2 + "'  order by tradingday desc,updatetime desc limit 1000";
            //list = DataControl.QueryTable(sql);
            //TotHistory = list.Rows.Count;
            //for (int i = 0; i < list.Rows.Count; i++)
            //{
            //    int x = (int)(double)list.Rows[i][0];
            //    int _count;
            //    if (History[x] == null)
            //    {
            //        _count = 1;
            //    }
            //    else
            //        _count = (int)History[x] + 1;
            //    History[x] = _count;
            //}


            return TotLine;
        }






        public static string Subject2Option(string subject)
        {
            for (int i = 0; i < MainWindow.NameSubject.Length; i++)
                if (MainWindow.NameSubject[i].Equals(subject))
                    return MainWindow.NameOption[i];
            Console.WriteLine("Can't find ! [Subject2Option]");
            return null;
        }

        public static string Subject2Future(string subject)
        {
            for (int i = 0; i < MainWindow.NameSubject.Length; i++)
                if (MainWindow.NameSubject[i].Equals(subject))
                    return MainWindow.NameFuture[i];
            Console.WriteLine("Can't find ! [Subject2Future]");
            return null;
        }

        public static string Future2Subject(string future)
        {
            for (int i = 0; i < MainWindow.NameFuture.Length; i++)
                if (MainWindow.NameFuture[i].Equals(future))
                    return MainWindow.NameSubject[i];
            Console.WriteLine("Can't find ! [Future2Subject]");
            return null;
        }

        public static string Future2Option(string future)
        {
            for (int i = 0; i < MainWindow.NameFuture.Length; i++)
                if (MainWindow.NameFuture[i].Equals(future))
                    return MainWindow.NameOption[i];
            Console.WriteLine("Can't find ! [Future2Option]");
            return null;
        }

        public static string Option2Future(string option)
        {
            for (int i = 0; i < MainWindow.NameOption.Length; i++)
                if (MainWindow.NameOption[i].Equals(option))
                    return MainWindow.NameFuture[i];
            Console.WriteLine("Can't find ! [Option2Future]");
            return null;
        }




    }
}
