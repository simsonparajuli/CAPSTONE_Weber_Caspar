using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class InstructorPreferences : PreferenceDetails
    {
        [Required]
        public int PreferencesRank { get; set; }
    }
}
