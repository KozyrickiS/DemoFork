﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BulbaCourses.Youtube.Logic.Models
{
    public class SearchStory
    {
        public int? Id { get; set; }
        public DateTime? SearchDate { get; set; }
        public string UserId { get; set; } 
        public SearchRequest SearchRequest { get; set; }//reference
        public int? SearchRequestId { get; set; }
    }
}