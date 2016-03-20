using System;
using System.Collections.Generic;
using System.Web;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// PageCount 的摘要说明
    /// </summary>
    public class PageCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            if (string.IsNullOrEmpty(context.Request.QueryString["showdate"]))
            {

                return;

            }

            if (string.IsNullOrEmpty(context.Request.QueryString["typeid"]))
            {

                return;

            }
            int typeid = DtCms.Common.Utils.returnIntByString(context.Request.QueryString["typeid"]);

            if (typeid == 0)
            {

                return;

            }


            context.Response.Write(new BLL.Article().GetCount(" classid in(" + new DtCms.BLL.Channel().returnAllStringTop(typeid,"cn") + ")  and AddTime='" + context.Request.QueryString["showdate"] + "'"));



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