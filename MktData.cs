using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTP;
using System.Diagnostics;
using System.Reflection;

namespace qiquanui
{
    class MktData
    {
        CTPMDAdapter api = null;
        /*
        //期权（只有开盘时能接收）
        string FRONT_ADDR = "tcp://27.17.62.149:42213";  // 前置地址
        string BrokerID = "8888";                       // 经纪公司代码
        string UserID = "001109";                       // 投资者代码
        string Password = "001109";                     // 用户密码
        // 大连,上海代码为小写
        // 郑州,中金所代码为大写
        // 郑州品种年份为一位数
        string[] ppInstrumentID = {"HO1407-C-1500"};	// 行情订阅列表
        int iRequestID = 0;
        */
        //期货（任何时候都能接收）
        string FRONT_ADDR = "tcp://27.17.62.149:40213";  // 前置地址
        string BrokerID = "1035";                       // 经纪公司代码
        string UserID = "00000023";                       // 投资者代码
        string Password = "123456";                     // 用户密码
        // 大连,上海代码为小写
        // 郑州,中金所代码为大写
        // 郑州品种年份为一位数
        string[] ppInstrumentID = { "IF1407" };	// 行情订阅列表
        int iRequestID = 0;


        public void Run()
        {
            api = new CTPMDAdapter();
            api.OnFrontConnected += new FrontConnected(OnFrontConnected);
            api.OnFrontDisconnected += new FrontDisconnected(OnFrontDisconnected);
            api.OnHeartBeatWarning += new HeartBeatWarning(OnHeartBeatWarning);
            api.OnRspError += new RspError(OnRspError);
            api.OnRspSubMarketData += new RspSubMarketData(OnRspSubMarketData);
            api.OnRspUnSubMarketData += new RspUnSubMarketData(OnRspUnSubMarketData);
            api.OnRspUserLogin += new RspUserLogin(OnRspUserLogin);
            api.OnRspUserLogout += new RspUserLogout(OnRspUserLogout);
            api.OnRtnDepthMarketData += new RtnDepthMarketData(OnRtnDepthMarketData);

            try
            {
                api.RegisterFront(FRONT_ADDR);
                api.Init();
                //api.SubscribeMarketData(ppInstrumentID);
                api.Join(); // 阻塞直到关闭或者CTRL+C
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                api.Release();
            }
        }

        void OnRspUserLogout(ThostFtdcUserLogoutField pUserLogout, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
        }

        void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            if (bIsLast && !IsErrorRspInfo(pRspInfo))
            {
                ///获取当前交易日
                Console.WriteLine("--->>> 获取当前交易日 = " + api.GetTradingDay());
                // 请求订阅行情
                SubscribeMarketData();
            }
        }

        void OnRspUnSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
        }

        void OnRspSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
        }

        void OnRspError(ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            IsErrorRspInfo(pRspInfo);
        }

        void OnHeartBeatWarning(int nTimeLapse)
        {
            __DEBUGPF__();
            Console.WriteLine("--->>> nTimerLapse = " + nTimeLapse);
        }

        void OnFrontDisconnected(int nReason)
        {
            __DEBUGPF__();
            Console.WriteLine("--->>> Reason = {0}", nReason);
        }

        void OnFrontConnected()
        {
            __DEBUGPF__();
            ReqUserLogin();
        }

        bool IsErrorRspInfo(ThostFtdcRspInfoField pRspInfo)
        {
            // 如果ErrorID != 0, 说明收到了错误的响应
            bool bResult = ((pRspInfo != null) && (pRspInfo.ErrorID != 0));
            if (bResult)
                Console.WriteLine("--->>> ErrorID={0}, ErrorMsg={1}", pRspInfo.ErrorID, pRspInfo.ErrorMsg);
            return bResult;
        }

        void ReqUserLogin()
        {
            ThostFtdcReqUserLoginField req = new ThostFtdcReqUserLoginField();
            req.BrokerID = BrokerID;
            req.UserID = UserID;
            req.Password = Password;
            int iResult = api.ReqUserLogin(req, ++iRequestID);

            Console.WriteLine("--->>> 发送用户登录请求: " + ((iResult == 0) ? "成功" : "失败"));
        }

        void SubscribeMarketData()
        {
            int iResult = api.SubscribeMarketData(ppInstrumentID);
            Console.WriteLine("--->>> 发送行情订阅请求: " + ((iResult == 0) ? "成功" : "失败"));
        }

        void OnRtnDepthMarketData(ThostFtdcDepthMarketDataField pDepthMarketData)
        {
            //DateTime now = DateTime.Parse(pDepthMarketData.UpdateTime);
            //now.AddMilliseconds(pDepthMarketData.UpdateMillisec);
            string s = string.Format("{0,-6} : UpdateTime = {1}.{2:D3},  LasPrice = {3}", pDepthMarketData.InstrumentID, pDepthMarketData.UpdateTime, pDepthMarketData.UpdateMillisec, pDepthMarketData.LastPrice);
            Debug.WriteLine(s);
            Console.WriteLine(s);
        }

        void __DEBUGPF__()
        {
            StackTrace ss = new StackTrace(false);
            MethodBase mb = ss.GetFrame(1).GetMethod();
            string str = "--->>> " + mb.DeclaringType.Name + "." + mb.Name + "()";
            Debug.WriteLine(str);
            Console.WriteLine(str);
        }

    }
}
