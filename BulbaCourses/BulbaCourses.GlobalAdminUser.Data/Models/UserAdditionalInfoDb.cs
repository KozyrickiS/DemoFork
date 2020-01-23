﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulbaCourses.GlobalAdminUser.Data.Models
{
    public class UserAdditionalInfoDb
    {
        public string FirstName { get; set; }

        public string UserId { get; set; } //from UsersDb.AspNetUsers
        public string UserProfileId { get; set; } = Guid.NewGuid().ToString();
        public int Age { get; set; }
        public string Sex { get; set; }
        public string City { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
