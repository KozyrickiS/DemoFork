﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using BulbaCourses.GlobalSearch.Data.Models;

namespace BulbaCourses.GlobalSearch.Data.EntitiesConfiguration
{
    class AuthorConfiguration : EntityTypeConfiguration<AuthorDB>
    {
        public AuthorConfiguration()
        {

        }
    }
}
