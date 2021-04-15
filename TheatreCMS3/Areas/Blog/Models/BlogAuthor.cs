﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Areas.Blog.Models
{
    public class BlogAuthor
    {
        public int BlogAuthorid { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime Joined { get; set; }
        public DateTime Left { get; set; }
    }

    public class BlogAuthorDBContext : DbContext
    {
        public DbSet<BlogAuthor> BlogAuthors { get; set; } 
    }
}