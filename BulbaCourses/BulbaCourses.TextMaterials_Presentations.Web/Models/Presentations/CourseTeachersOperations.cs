﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BulbaCourses.TextMaterials_Presentations.Web.Models.StaffAndUsers;

namespace BulbaCourses.TextMaterials_Presentations.Web.Models.Presentations
{
    public class CourseTeachersOperations
    {   /// <summary>
        /// Get all Teachers from the Course Teachers list, returns IEnumerable
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public static IEnumerable<Teacher> GetAll(Course course)
        {
            return course.CourseTeachers.AsReadOnly();
        }

        /// <summary>
        /// Get Teacher from the Course Teachers list by Id, returns Teacher
        /// </summary>
        /// <param name="course"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Teacher GetById(Course course, string id)
        {
            return course.CourseTeachers.SingleOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Add Teacher to the Course Teachers list, returns added Teacher
        /// </summary>
        /// <param name="course"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public static Teacher Add(Course course, Teacher teacher)
        {
            teacher.Id = Guid.NewGuid().ToString();
            course.CourseTeachers.Add(teacher);
            return teacher;
        }

        /// <summary>
        /// Delete by Id Teacher from the Course Teachers list, returns true if was deleted
        /// </summary>
        /// <param name="course"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteById(Course course, string id)
        {
            Teacher deletedTeacher = course.CourseTeachers.SingleOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (deletedTeacher != null)
            {
                course.CourseTeachers.Remove(deletedTeacher);
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}