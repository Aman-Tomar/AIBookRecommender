﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Entities
{
    public class BookUserRating
    {
        public int Rating { get; set; }
        public int UserID { get; set; }
        public string ISBN { get; set; }  

    }
}
