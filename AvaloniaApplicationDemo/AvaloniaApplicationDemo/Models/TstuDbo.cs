using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplicationDemo.Models
{
    public class TstuDbo
    {

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string mingzi { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string pwd { get; set; }

    }
}
