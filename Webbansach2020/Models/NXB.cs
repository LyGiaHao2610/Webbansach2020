﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webbansach2020.Models
{
    public class NXB
    {
        [Key]
        public int ID { get; set; }
        public string TenNXB { get; set; }

    }
}