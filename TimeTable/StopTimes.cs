//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeTable
{
    using System;
    using System.Collections.Generic;
    
    public partial class StopTimes
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public short StationID { get; set; }
        public string StationName { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public int TrainInfoId { get; set; }
        public short StopSeq { get; set; }
    
        public virtual TrainInfo TrainInfo { get; set; }
    }
}
