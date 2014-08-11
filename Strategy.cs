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

    struct YKCell
    {
        public double BidPrice;
        public double AskPrice;
        public int ExercisePrice;
    }

    struct YKFuture
    {
        public double LastPrice;
    }

    class YKProba
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
    class YK
    {
        public YKCell[,] ykoption;
        public YKFuture[] ykfuture;
        //不含端点
        public ObservableCollection<XY> points;
        //含端点，用于单图
        public ObservableCollection<XY> points2;
        //含端点，用于对比图
        public ObservableCollection<XY> points3;
        //存概率点
        public ObservableCollection<YKProba> probability;
        public double ykmax;
        public double multiplier = 1;
        public YKType yktype;
        public string ykname;
        //有 TotLine+1 行，最后一行 0/1 分别为期货的涨/跌
        public int[,] number;
        public int TotLine;
        public int LeftEdge, RightEdge, LeftCompute, RightCompute;
        public double EarnRate, LoseRate, MaxEarn, MaxLose, ExpectEarn;
        public double EDGE = 0.2;

        /// <summary>
        ///     构造完之后使用 .ykoption 和 .ykfuture 填充数据，然后再调用相应计算方法
        /// </summary>
        /// <param name="totline">相当于行权价的行数</param>
        /// <param name="max">最大涨跌幅/波幅</param>
        /// <param name="type">是四种中的哪种盈亏图</param>
        public YK(int totline, int[,] num)
        {
            TotLine = totline;
            number = num;
            ykoption = YKManager.ykOption;
            ykfuture = YKManager.ykFuture;
            ykmax = 0.2;//!
            LeftCompute = (int)(ykfuture[0].LastPrice * (1 - ykmax));
            RightCompute = (int)(ykfuture[1].LastPrice * (1 + ykmax));
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
                        ans += ComputeY(x, (OptionType)j, ykoption[i, j]);
                    }
            if (number[TotLine, 0] > 0)
                ans += (x - ykfuture[0].LastPrice);
            if (number[TotLine, 1] > 0)
                ans += -(x - ykfuture[1].LastPrice);
            ans *= multiplier;
            return ans;
        }

        /// <summary>
        /// 给指定ObservableCollection添加左右两个端点
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
            points2 = new ObservableCollection<XY>();
            points3 = new ObservableCollection<XY>();
            probability = new ObservableCollection<YKProba>();

            ///计算盈亏图的折点
            for (int i = 0; i <= ykoption.GetUpperBound(0); i++)
                for (int j = 0; j < 4; j++)
                    if (number[i, j] > 0)
                    {
                        int x = ykoption[i, j].ExercisePrice;
                        double y = ComputePoint(x);
                        points.Add(new XY(x, y));
                        points2.Add(new XY(x, y));
                        points3.Add(new XY(x, y));
                        break;
                    }

            AddEdge((int)(LeftCompute * (1 - EDGE)), (int)(RightCompute * (1 + EDGE)), points2);

            ///计算概率相关
            MaxEarn = 0;
            EarnRate = 0;
            MaxLose = 0;
            LoseRate = 0;
            ExpectEarn = 0;
            int EarnCount = 0, LoseCount = 0, ProbaCount = 0, TotCount = 0;
            double k;
            for (int i = LeftCompute; i <= RightCompute; i++)
            {
                k = ComputePoint(i);

                if (YKManager.History[i] != null)
                {

                    int count = (int)YKManager.History[i];
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

                }

                if (k == 0)
                {
                    YKProba p = new YKProba();
                    if (ComputePoint(i - 1) > 0) p.positive = true;
                    else if (ComputePoint(i - 1) < 0) p.positive = false;
                    else continue;
                    p.x = i;
                    p.percent = ProbaCount;
                    probability.Add(p);
                    ProbaCount = 0;
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
            _p.x = -1;
            _p.percent = ProbaCount;
            probability.Add(_p);
            for (int i = 0; i < probability.Count; i++)
            {
                probability[i].percent = Math.Round(probability[i].percent / TotCount * 100, 1);
            }
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

    }


    class Test
    {
        public void Run()
        {
            YKManager ykm = new YKManager();
            int tot = ykm.Initial("中金所", "沪深300", "1409", 2);
            for (int i = 0; i < 4; i++)
            {
                int[,] x = new int[tot + 1, 4];
                x[0, 0] = i;
                x[1, 1] = 4 - i;
                YK temp = new YK(tot, x);
                temp.ComputeYK();
                ykm.yk[0, i] = temp;
            }
            ykm.ComputeYKs(0);
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
        public int Initial(string trader, string subject, string duedate, int count)
        {
            yk = new YK[2, 4];

            //得到行权价列表
            Count = count;
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
                for (int i = 0; i < count; i++)
                {
                    YKCell cell = new YKCell();
                    cell.AskPrice = (double)Math.Round((double)dr["AskPrice1"], 1);
                    cell.BidPrice = (double)Math.Round((double)dr["BidPrice1"], 1);
                    cell.ExercisePrice = (int)x;
                    ykOption[j, 0] = cell;
                    ykOption[j, 1] = cell;
                }
                id = (string)list.Rows[j * 2 + 1]["InstrumentID"];
                dr = (DataRow)DataManager.All[id];
                for (int i = 0; i < count; i++)
                {
                    YKCell cell = new YKCell();
                    cell.AskPrice = Math.Round((double)dr["AskPrice1"], 1);
                    cell.BidPrice = Math.Round((double)dr["BidPrice1"], 1);
                    cell.ExercisePrice = (int)x;
                    ykOption[j, 2] = cell;
                    ykOption[j, 3] = cell;
                }

            }

            //填充期货数据
            string futurename = Subject2Future(subject);
            string idsql = "select instrumentid from staticdata where instrumentname='" + futurename + "' and duedate='" + duedate + "' ";
            list = DataControl.QueryTable(idsql);
            string id2 = (string)list.Rows[0]["InstrumentID"];
            DataRow dr2 = (DataRow)DataManager.All[id2];
            double lastprice = Math.Round((double)dr2["LastPrice"], 1);
            for (int i = 0; i < count; i++)
            {
                ykFuture[0].LastPrice = lastprice;
                ykFuture[1].LastPrice = lastprice;
            }


            //填充历史概率
            History = new Hashtable(2000);
            string sql = "SELECT LastPrice from minutedata where instrumentid='" + id2 + "'  order by tradingday desc,updatetime desc limit 1000";
            list = DataControl.QueryTable(sql);
            TotHistory = list.Rows.Count;
            for (int i = 0; i < list.Rows.Count; i++)
            {
                int x = (int)(double)list.Rows[i][0];
                int _count;
                if (History[x] == null)
                {
                    _count = 1;
                }
                else
                    _count = (int)History[x] + 1;
                History[x] = _count;
            }


            return TotLine;
        }


        ///<summary>
        ///计算盈亏对比图
        ///</summary>
        ///<returns></returns>
        public void ComputeYKs(int no)
        {
            //计算端点用
            double leftx = 1e10, rightx = 0;

            for (int i = 0; i < 4; i++)
            {
                //计算端点用
                if (yk[no, i].points[0].X < leftx)
                    leftx = yk[no, i].points[0].X;
                if (yk[no, i].points.Last().X > rightx)
                    rightx = yk[no, i].points.Last().X;
            }

            //计算端点用
            leftx = (int)leftx * (1 - EDGE);
            rightx = (int)rightx * (1 + EDGE);
            for (int i = 0; i < 4; i++)
                yk[no, i].AddEdge((int)leftx, (int)rightx, yk[no, i].points3);

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

    }
}
