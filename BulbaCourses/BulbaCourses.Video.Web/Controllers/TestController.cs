﻿//using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    [RoutePrefix("api/upload")]
    public class FileUploadController : ApiController
    {
        // db_videoEntities1 wmsEN = new db_videoEntities1();
        [HttpPost, Route("")]
        public HttpResponseMessage UploadFiles()
        {
            var httpRequest = HttpContext.Current.Request;
            //Upload Image    
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string ff = "";
            try
            {
                for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                {
                    System.Web.HttpPostedFile hpf = hfc[iCnt];
                    if (hpf.ContentLength > 0)
                    {
                        var filename = (Path.GetFileName(hpf.FileName));
                        var filePath = HttpContext.Current.Server.MapPath("~/Static/" + filename);
                        hpf.SaveAs(filePath);
                        //VideoMaster obj = new VideoMaster();
                        ff = filePath + filename;
                    //obj.Videos = "http://localhost:50401/Vedios/" + filename;
                        //wmsEN.VideoMasters.Add(obj);
                        //wmsEN.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            { }
            var rq = Request.CreateResponse<string>(HttpStatusCode.Created, ff);
            return rq;
        }

        //[HttpPost]
        //public object Vedios()
        //{
        //    return null;//wmsEN.VideoMasters;
        //}
    }
}