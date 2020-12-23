using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    public class SqlQuery
    {
        public class TrainTravelTime
        {
            public short TrainNo { get; set; }
            public string TrainTypeName { get; set; }
            public string StartingStationName { get; set; }
            public string EndingStationName { get; set; }
            public string StartStation { get; set; }
            public string DepartureTime { get; set; }
            public string EndStation { get; set; }
            public string ArrivalTime { get; set; }
        }

        public void ClearDatabase()
        {
            SimpleTrainTableContainer trainTableDb = new SimpleTrainTableContainer();
            trainTableDb.Database.ExecuteSqlCommand("DELETE from [StopTimesSet]");
            trainTableDb.Database.ExecuteSqlCommand("DELETE from [TrainInfoSet]");
        }

        public void ParseFromJson(JObject json)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
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
            List<TrainInfo> trainList = new List<TrainInfo>();

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
                    stop.StopSeq = (short)stopTimesToken["StopSequence"];

                    train.StopTimes.Add(stop);
                }

                trainList.Add(train);
            }
            trainTableDb.TrainInfoSet.AddRange(trainList);
            trainTableDb.SaveChanges();
            trainTableDb.Dispose();
            watch.Stop();
            Debug.WriteLine("ParseJsonToData: "+ (watch.ElapsedMilliseconds / 1000));
        }

        public IEnumerable<TrainTravelTime> TrainTravelTimeQuery(string startStation, string endStation)
        {
            SimpleTrainTableContainer trainTableDb = new SimpleTrainTableContainer();
            var result = trainTableDb.Database.SqlQuery<TrainTravelTime>("SELECT Info.TrainNo, Info.TrainTypeName, Info.StartingStationName, Info.EndingStationName, StartTimes.StationName As StartStation, StartTimes.DepartureTime, EndTimes.StationName As EndStation, EndTimes.ArrivalTime FROM[dbo].[TrainInfoSet] AS Info, [dbo].[StopTimesSet] AS EndTimes, [dbo].[StopTimesSet] AS StartTimes WHERE EndTimes.TrainInfoId = Info.Id AND StartTimes.TrainInfoId = Info.Id AND(EndTimes.StationName = N'" + endStation + "') AND(StartTimes.StationName = N'" + startStation + "') AND EndTimes.StopSeq > StartTimes.StopSeq").ToList();

            return result;
        }
    }
}