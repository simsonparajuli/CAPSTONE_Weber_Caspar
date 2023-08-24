using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class WhoPays
    {
        [Key]
        public int WhoPaysID { get; set; }
        [Required]
        public string? WhoPaysType { get; set; }
    }
}
