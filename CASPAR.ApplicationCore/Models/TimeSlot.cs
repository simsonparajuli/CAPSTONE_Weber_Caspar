using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class TimeSlot
    {
        [Key]
        public int TimeSlotID { get; set; }
        [Required]
        public string? TimeSlotType { get; set;}
        [Required]
        public string? TimeSlotDescription { get; set;}
    }
}
