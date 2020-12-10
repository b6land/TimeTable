using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeTable.Models
{
    public class InputModel
    {
        [Required]
        public string Value { get; set; }
    }
}