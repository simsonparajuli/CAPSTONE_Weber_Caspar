using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class Program
    {
        [Key]
        public int ProgramID { get; set; }
        [Required]
        public string ProgramName { get; set; }

    }
}
