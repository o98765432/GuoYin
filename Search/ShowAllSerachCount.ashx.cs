using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.Search
{
    /// <summary>
    /// ShowAllSerachCount 的摘要说明
    /// </summary>
    public class ShowAllSerachCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            if (string.IsNullOrEmpty(context.Request.QueryString["keyword"]))
            {

                return;

            }


            int casepagecount = new BLL.Product().GetSerachCount(context.Request.QueryString["keyword"] == null ? "" : context.Request.QueryString["keyword"].ToString());
                
               

            
            context.Response.Write(casepagecount);

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