using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class TraTimeController : ApiController
    {
        DownloadHelper downloadHelper = null;

        public TraTimeController()
        {
            downloadHelper = new DownloadHelper();
        }

        /// <summary> 無指定字串時，下載前 10 筆車站資料 </summary>
        /// <returns> 車站資料或下載訊息 </returns>
        public string Get()
        {
            string url = "https://ptx.transportdata.tw/MOTC/v2/Rail/TRA/Station?$top=10&$format=JSON";
            string result = "";
            if(downloadHelper.DownloadPtxData(url, out result) == false)
            {
                result = "下載失敗";
            }
            return result;
        }

        /// <summary> 下載指定車次時刻資料 </summary>
        /// <param name="code"> 列車車次 </param>
        /// <returns> 列車時刻或錯誤訊息 </returns>
        public string Get(int code)
        {
            string url = "https://ptx.transportdata.tw/MOTC/v3/Rail/TRA/DailyTrainTimetable/Today?$format=JSON";
            string result = "";
            if (downloadHelper.DownloadPtxData(url, out result) == false)
            {
                result = "下載失敗";
            }
            JObject json = JObject.Parse(result);
            SqlQuery sqlQuery = new SqlQuery();
            sqlQuery.ParseFromJson(json);
            JProperty timeTable = json.Property("TrainTimetables");
            if(timeTable == null)
            {
                return "下載資料不包含時刻資訊";
            }

            string trainTimeTable = "列車時刻資訊";
            foreach(JToken token in timeTable.Children())
            {
                foreach(JToken trainToken in token.Children())
                {
                    JToken infoToken = trainToken.SelectToken("TrainInfo");
                    if (code.ToString().Equals(infoToken["TrainNo"].ToString()) == false)
                    {
                        continue;
                    }

                    JToken timeToken = trainToken.SelectToken("StopTimes");
                    trainTimeTable = timeToken.ToString();
                }

                
            }
            return trainTimeTable;
        }
    }
}
