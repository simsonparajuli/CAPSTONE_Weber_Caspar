using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class InstructorProgram
    {
        [Key]
        public int InstructorProgramID { get; set; }

        [Required]
        public String WNumber { get; set; }
        [ForeignKey("WNumber")]
        [ValidateNever]
        public Instructor Instructor { get; set; }

        public int ProgramID { get; set; }
        [ForeignKey("ProgramID")]
        [ValidateNever]
        public Program Program { get; set; }

    }
}
