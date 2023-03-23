﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreCMS3.Areas.Prod.Models
{
    public class ProductionPhoto
    {
        [Key]
        public int ProPhotoId { get; set; }

        // Commented out for now, as requested
        // public byte[] PhotoFile { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}