using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeTest.Models
{
    public class NumberToTextModel
    {
        [Required]
        public string InputNumber { get; set; }

        public string NumberAsText { get; set; }

    }
}