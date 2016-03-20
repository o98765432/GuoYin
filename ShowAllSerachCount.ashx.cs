using System;
using System.Collections.Generic;
using System.Web;
using DtCms.DBUtility;
namespace DtCms.Web
{
    /// <summary>
    /// ShowAllSerachCount 的摘要说明
    /// </summary>
    public class ShowAllSerachCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            if (string.IsNullOrEmpty(context.Request.QueryString["keyword"]) || context.Request.QueryString["keyword"].Equals("null"))
            {
                return;
            }
            
            string keyword = context.Request.QueryString["keyword"];

            int casepagecount = new BLL.Product().GetSerachCount(keyword);

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