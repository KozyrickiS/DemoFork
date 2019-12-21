﻿using AutoMapper;
using BulbaCourses.DiscountAggregator.Logic.Models;
using BulbaCourses.DiscountAggregator.Logic.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BulbaCourses.DiscountAggregator.Web.Controllers
{
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly ICourseServices _courseService;
        
        public CourseController( ICourseServices courseService)
        {
            this._courseService = courseService;
        }

        [HttpGet, Route("")]
        [Description("Get all courses")]// для описания ,но в данном примере не работает...
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]// описать возможные ответы от сервиса, может быть Ок, badrequest, internalServer error...
        [SwaggerResponse(HttpStatusCode.NotFound, "Courses doesn't exists")]
        [SwaggerResponse(HttpStatusCode.OK, "Courses found", typeof(IEnumerable<Course>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult GetAll()
        {
            var result = _courseService.GetAll();
            return result == null ? NotFound() : (IHttpActionResult)Ok(result);
        }

        [HttpGet, Route("{id}")]//можно указать какой тип id
        [Description("Get course by Id")]// для описания ,но в данном примере не работает...
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]// описать возможные ответы от сервиса, может быть Ок, badrequest, internalServer error...
        [SwaggerResponse(HttpStatusCode.NotFound, "Course doesn't exists")]
        [SwaggerResponse(HttpStatusCode.OK, "Course found", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult GetById(string id)
        {
            //valideate id
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var _))
            {
                return BadRequest();
            }

            try
            {
                var result = _courseService.GetById(id); //Courseware.GetById(id);
                return result == null ? NotFound() : (IHttpActionResult)Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpDelete, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Ivalid paramater format")]
        [SwaggerResponse(HttpStatusCode.OK, "Course deleted", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult DeleteById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var _))
            {
                return BadRequest();
            }
            try
            {
                _courseService.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut, Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "All courses was updated", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult UpdateAll([FromBody]Course course)
        {
            try
            {
                _courseService.Update(course);
                return Ok();
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut, Route("id")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Ivalid paramater format")]
        [SwaggerResponse(HttpStatusCode.OK, "Course updated", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult UpdateById(string id, [FromBody]Course course)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var _))
            {
                return BadRequest();
            }

            try
            {
                _courseService.Update(course);
                return Ok();
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost, Route("")]
        public IHttpActionResult Create([FromBody]Course course)
        {
            //if (course == null || !Enum.IsDefined(typeof(CourseCategory), course.Category))
            //{
            //    return BadRequest();
            //}
            var newCourse = new Course
            {
                Id = course.Id,// "111",
                Domain = course.Domain,
                URL = "111",
                Category = "111",
                Title = "111",
                Description = "111",
                Price = 20.0,
                OldPrice = 20.0,
                DateOldPrice = DateTime.Now,
                Discount = 20,
                DateStartCourse = DateTime.Now,
                DateChange = DateTime.Now,

            };
            try
            {
                _courseService.Add(newCourse);
                return Ok(newCourse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }




    }
}
