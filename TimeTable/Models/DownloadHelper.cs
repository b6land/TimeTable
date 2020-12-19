using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using TimeTable.Security;

namespace TimeTable.Models
{
    public class DownloadHelper
    {
        public DownloadHelper()
        {
            // 指定使用 TLS 1.2 以下載 HTTPS 檔案
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        /// <summary> 採用 ptxmotc/Sample-code 的程式碼，使用訪客身分下載 PTX 資料 </summary>
        /// <param name="APIUrl"> 下載位址 </param>
        /// <param name="Result"> 下載內容 </param>
        /// <returns> 是否成功下載 </returns>
        public bool DownloadPtxData(string APIUrl, out string Result)
        {
            //申請的APPID
            //（FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF 為 Guest 帳號，以IP作為API呼叫限制，請替換為註冊的APPID & APPKey）
            string APPID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";
            //申請的APPKey
            string APPKey = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

            //取得當下UTC時間
            string xdate = DateTime.Now.ToUniversalTime().ToString("r");
            string SignDate = "x-date: " + xdate;
            //取得加密簽章
            string Signature = HMAC_SHA1.Signature(SignDate, APPKey);
            string sAuth = "hmac username=\"" + APPID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + Signature + "\"";
            
            Result = string.Empty;

            try
            {
                using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
                {
                    Client.DefaultRequestHeaders.Add("Authorization", sAuth);
                    Client.DefaultRequestHeaders.Add("x-date", xdate);
                    Result = Client.GetStringAsync(APIUrl).Result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;

        }

        /// <summary> 檢查是否已下載今日的列車時刻表 </summary>
        /// <returns> 是: 今日下載 </returns>
        public bool isDailyTimetableToday()
        {
            string downloadDay = System.Web.Configuration.WebConfigurationManager.AppSettings["DownloadDay"];
            string currentDay = DateTime.Now.ToString("yyyyMMdd");
            return downloadDay.Equals(currentDay);
        }

        /// <summary> 設定列車時刻表下載日期為今日 </summary>
        public void SetDailyTimetableToday()
        {
            System.Web.Configuration.WebConfigurationManager.AppSettings["DownloadDay"] = DateTime.Now.ToString("yyyyMMdd");
        }
    }
}