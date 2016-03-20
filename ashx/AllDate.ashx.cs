using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

namespace DtCms.Web.ashx
{
    /// <summary>
    /// AllDate 的摘要说明
    /// </summary>
    public class AllDate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            DataSet ds = new BLL.Article().GetModelDa("select  distinct (CONVERT(varchar(4), addtime,121)) as chen from dt_Article where ClassId=63  order by chen desc");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {

                    context.Response.Write("<li "+(i==0?"class=\"active\"":"")+"><span class=\"riq_01\"><a href=\"javascript:;\" rel=\"" + ds.Tables[0].Rows[i]["chen"] + "\">" + ds.Tables[0].Rows[i]["chen"] + "</a></span></li>");
                 
                }
            }
           

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