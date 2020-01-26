﻿using BulbaCourses.Podcasts.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.Podcasts.Logic.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Result DeleteAsync(T thing, UserLogic user);

        Task<Result> UpdateAsync(T thing, UserLogic user);
    }
}
