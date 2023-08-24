using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class PayModel
    {
        [Key]
        public int PayModelID { get; set; }
        [Required]
        public string? PayModelType { get; set; }

    }
}
