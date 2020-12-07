using System;
using System.Collections.Generic;
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

        public bool DownloadTraTimeTable(out string Result)
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

            //欲呼叫之API網址(此範例為台鐵車站資料)
            var APIUrl = "https://ptx.transportdata.tw/MOTC/v2/Rail/TRA/Station?$top=10&$format=JSON";
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
                return false;
            }
            return true;

        }

            
    }
}