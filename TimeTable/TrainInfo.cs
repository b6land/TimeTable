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
    
    public partial class TrainInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainInfo()
        {
            this.StopTimes = new HashSet<StopTimes>();
        }
    
        public int Id { get; set; }
        public short TrainNo { get; set; }
        public string TrainTypeName { get; set; }
        public short StartingStationId { get; set; }
        public short EndingStationId { get; set; }
        public string StartingStationName { get; set; }
        public string EndingStationName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StopTimes> StopTimes { get; set; }
    }
}
