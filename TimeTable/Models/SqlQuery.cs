using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    public class SqlQuery
    {
        public void ParseFromJson(JObject json)
        {
            var trainInfos =
                from info in json["TrainTimetables"] select new {
                    TrainNo = info["TrainInfo"]["TrainNo"],
                    TrainTypeName = info["TrainInfo"]["TrainTypeName"]["Zh_tw"],
                    StartingStationName = info["TrainInfo"]["StartingStationName"]["Zh_tw"],
                    StartingStationId = info["TrainInfo"]["StartingStationID"],
                    EndingStationName = info["TrainInfo"]["EndingStationName"]["Zh_tw"],
                    EndingStationId = info["TrainInfo"]["EndingStationID"],
                    StopTimes = info["StopTimes"]
                };

            SimpleTrainTableContainer trainTableDb = new SimpleTrainTableContainer();

            foreach (var t in trainInfos)
            {
                TrainInfo train = new TrainInfo();
                train.TrainNo = (short)t.TrainNo;
                train.TrainTypeName = (string)t.TrainTypeName;
                train.StartingStationName = (string)t.StartingStationName;
                train.StartingStationId = (short)t.StartingStationId;
                train.EndingStationName = (string)t.EndingStationName;
                train.EndingStationId = (short)t.EndingStationId;

                foreach (JToken stopTimesToken in t.StopTimes)
                {
                    StopTimes stop = new StopTimes();
                    stop.StationID = (short)stopTimesToken["StationID"];
                    stop.StationName = (string)stopTimesToken["StationName"]["Zh_tw"];
                    stop.DepartureTime = (string)stopTimesToken["DepartureTime"];
                    stop.ArrivalTime = (string)stopTimesToken["ArrivalTime"];                    

                    train.StopTimes.Add(stop);
                }

                trainTableDb.TrainInfoSet.Add(train);
                trainTableDb.SaveChanges();
            }
            
        }
    }
}