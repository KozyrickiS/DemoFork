﻿using BulbaCourses.DiscountAggregator.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.DiscountAggregator.Logic.Services
{
    public interface ICourseITAcademyServices
    {
        IEnumerable<CoursesITAcademy> GetAll();

        Task<IEnumerable<CoursesITAcademy>> GetAllAsync();
    }
}
