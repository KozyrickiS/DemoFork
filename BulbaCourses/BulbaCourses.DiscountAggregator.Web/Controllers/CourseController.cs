﻿using AutoMapper;
using BulbaCourses.DiscountAggregator.Logic.Models;
using BulbaCourses.DiscountAggregator.Logic.Services;
using BulbaCourses.DiscountAggregator.Web.Filters;
using FluentValidation.WebApi;
using Swashbuckle.Swagger.Annotations;
using Swashbuckle.Examples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Claims;
using BulbaCourses.DiscountAggregator.Web.SwaggerExamples;

namespace BulbaCourses.DiscountAggregator.Web.Controllers
{
    [RoutePrefix("api/courses")]
    //[Authorize]
    public class CourseController : ApiController
    {
        private readonly ICourseServices _courseService;
        
        public CourseController( ICourseServices courseService)
        {
            this._courseService = courseService;
        }

        /// <summary>
        /// Get all course from DB
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [Description("Get all courses")]// для описания ,но в данном примере не работает...
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]// описать возможные ответы от сервиса, может быть Ок, badrequest, internalServer error...
        [SwaggerResponse(HttpStatusCode.NotFound, "Courses doesn't exist")]
        [SwaggerResponse(HttpStatusCode.OK, "Courses found", typeof(IEnumerable<Course>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult GetAll()
        {
            var result = _courseService.GetAll();
            return result == null ? NotFound() : (IHttpActionResult)Ok(result);
        }
        
        /// <summary>
        /// Get all courses from DB (async)
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Async")]
        [Description("Get all courses")]// для описания ,но в данном примере не работает...
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]// описать возможные ответы от сервиса, может быть Ок, badrequest, internalServer error...
        [SwaggerResponse(HttpStatusCode.NotFound, "Courses doesn't exist")]
        [SwaggerResponse(HttpStatusCode.OK, "Courses found", typeof(IEnumerable<Course>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        //[Authorize]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    var sub = (User as ClaimsPrincipal).FindFirst("sub");
            //}
            var result = await _courseService.GetAllAsync();
            return result == null ? NotFound() : (IHttpActionResult)Ok(result);
        }

        /// <summary>
        /// Get course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]//можно указать какой тип id
        [Description("Get course by Id")]// для описания ,но в данном примере не работает...
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]// описать возможные ответы от сервиса, может быть Ок, badrequest, internalServer error...
        [SwaggerResponse(HttpStatusCode.NotFound, "Course doesn't exist")]
        [SwaggerResponse(HttpStatusCode.OK, "Course found", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public IHttpActionResult GetById(string id)
        {
            //validate id
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var _))
            {
                return BadRequest();
            }

            try
            {
                var result = _courseService.GetById(id);
                return result == null ? NotFound() : (IHttpActionResult)Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get course by Id (async)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Async/{id}")]
        [Description("Get course by Id")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Course doesn't exist")]
        [SwaggerResponse(HttpStatusCode.OK, "Course found", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public async Task<IHttpActionResult> GetByIdAsync(string id)
        {
            //validate id
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var _))
            {
                return BadRequest();
            }

            try
            {
                var result = await _courseService.GetByIdAsync(id);
                return result == null ? NotFound() : (IHttpActionResult)Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get courses for user profile
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("Search")]
        [Description("Get courses for UserProfile")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Course doesn't exist")]
        [SwaggerResponse(HttpStatusCode.OK, "Course found", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public async Task<IHttpActionResult> GetByUserCriteriaAsync()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var sub = (User as ClaimsPrincipal).FindFirst("sub");
                    var result = await _courseService.GetByIdUserAsync(sub.Value);
                    return result == null ? NotFound() : (IHttpActionResult)Ok(result);
                }
                return BadRequest();
            }
            catch (InvalidOperationException ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get courses by user ID
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet, Route("Search/{idUser}")]
        [Description("Get courses by idUser")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Course doesn't exist")]
        [SwaggerResponse(HttpStatusCode.OK, "Course found", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public async Task<IHttpActionResult> GetByCriteriaAsync(string idUser)
        {
            if (idUser == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _courseService.GetByIdUserAsync(idUser);
                return result == null ? NotFound() : (IHttpActionResult)Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete course by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Ivalid paramater format")]
        [SwaggerResponse(HttpStatusCode.OK, "Course deleted", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public async Task<IHttpActionResult> DeleteById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out var _))
            {
                return BadRequest();
            }

            var result = await _courseService.DeleteByIdAsync(id);
            return result.IsError ? BadRequest(result.Message) : (IHttpActionResult)Ok(result.Data);

            
        }

        /// <summary>
        /// Update course by ID
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut, Route("id")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Ivalid paramater format")]
        [SwaggerResponse(HttpStatusCode.OK, "Course updated", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        public async Task<IHttpActionResult> Update([FromBody, CustomizeValidator(RuleSet = "default")]Course course)
        {
            if (course == null)
            {
                return BadRequest();
            }
            var result = await _courseService.UpdateAsync(course);
            return result.IsError ? BadRequest(result.Message) : (IHttpActionResult)Ok(result.Data);
        }

        /// <summary>
        /// Add new course
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost, Route("")]
        [SwaggerRequestExample(typeof(Course),typeof(SwaggerCourse))]
        [Description("Add new course")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid paramater format")]
        [SwaggerResponse(HttpStatusCode.OK, "Course added", typeof(Course))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Something wrong")]
        //[BadRequestFilter]
        //[Authorize]
        public async Task<IHttpActionResult> Create([FromBody, CustomizeValidator(RuleSet = "default")]Course course)
        {
            var user = this.User as ClaimsPrincipal;
            //user.FindFirst("preferred_username").Value;

            if (course == null)
            {
                return BadRequest();
            }

            var result = await _courseService.AddAsync(course);
            return  result.IsError ? BadRequest(result.Message) : (IHttpActionResult)Ok(result.Data);
        }
    }
}
