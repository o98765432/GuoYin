using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Admin.Handler
{
    /// <summary>
    /// changeProductKeyBannerPaiXuAndIsLock 的摘要说明
    /// </summary>
    public class changeProductKeyBannerPaiXuAndIsLock : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
             
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}